using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using ImpresionEtiquetas.Models;

namespace ImpresionEtiquetas.Services
{
    public class CatalogService
    {
        private readonly Dictionary<string, CatalogItem> _catalogo = new Dictionary<string, CatalogItem>(StringComparer.OrdinalIgnoreCase);

        public bool EstaCargado => _catalogo.Count > 0;
        public int TotalItems => _catalogo.Count;

        public Task<int> CargarCatalogoAsync(string rutaArchivo)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(rutaArchivo))
                    throw new FileNotFoundException("El archivo de catálogo no existe.", rutaArchivo);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(rutaArchivo)))
                {
                    // Buscar la hoja "Lista de Precios" o la primera hoja
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault(ws =>
                        ws.Name.IndexOf("Lista de Precios", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        ws.Name.IndexOf("Lista Precios", StringComparison.OrdinalIgnoreCase) >= 0)
                        ?? package.Workbook.Worksheets.FirstOrDefault();

                    if (worksheet == null || worksheet.Dimension == null)
                        throw new InvalidOperationException("El archivo de catálogo no contiene hojas válidas o está vacío.");

                    var mapa = DetectarColumnas(worksheet);
                    if (!mapa.ContainsKey("Sku"))
                        throw new InvalidOperationException("No se encontró la columna de SKU o Código en el catálogo.");

                    _catalogo.Clear();

                    int startRow = 2; // Asumiendo fila 1 encabezados
                    int totalRows = worksheet.Dimension.End.Row;

                    for (int row = startRow; row <= totalRows; row++)
                    {
                        string sku = worksheet.Cells[row, mapa["Sku"]].Text?.Trim().ToUpperInvariant();
                        if (string.IsNullOrWhiteSpace(sku)) continue;

                        string marca = mapa.ContainsKey("Marca") ? worksheet.Cells[row, mapa["Marca"]].Text?.Trim().ToUpperInvariant() : string.Empty;
                        string modelo = mapa.ContainsKey("Modelo") ? worksheet.Cells[row, mapa["Modelo"]].Text?.Trim().ToUpperInvariant() : string.Empty;

                        decimal? precio = null;
                        if (mapa.ContainsKey("Precio"))
                        {
                            string precioTexto = worksheet.Cells[row, mapa["Precio"]].Text?.Trim();
                            if (!string.IsNullOrEmpty(precioTexto))
                            {
                                precioTexto = precioTexto.Replace("$", "").Replace(",", "").Trim();
                                if (decimal.TryParse(precioTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal p))
                                {
                                    precio = p;
                                }
                            }
                        }

                        _catalogo[sku] = new CatalogItem
                        {
                            Sku = sku,
                            Marca = marca ?? string.Empty,
                            Modelo = modelo ?? string.Empty,
                            Precio = precio
                        };
                    }

                    return _catalogo.Count;
                }
            });
        }

        public bool IntentarObtener(string sku, out CatalogItem item)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                item = null;
                return false;
            }
            return _catalogo.TryGetValue(sku.Trim().ToUpperInvariant(), out item);
        }

        private Dictionary<string, int> DetectarColumnas(ExcelWorksheet ws)
        {
            var mapa = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            int cols = ws.Dimension.End.Column;

            for (int col = 1; col <= cols; col++)
            {
                string header = ws.Cells[1, col].Text?.Trim().ToLowerInvariant()
                    .Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
                    .Replace(" ", "").Replace("_", "").Replace("-", "");

                if (string.IsNullOrEmpty(header)) continue;

                if (!mapa.ContainsKey("Sku") && (header == "sku" || header == "codigo" || header == "codigosku" || header == "codsku"))
                    mapa["Sku"] = col;
                else if (!mapa.ContainsKey("Marca") && (header == "marca" || header == "linea" || header == "brand"))
                    mapa["Marca"] = col;
                else if (!mapa.ContainsKey("Modelo") && header == "modelo")
                    mapa["Modelo"] = col;
                else if (!mapa.ContainsKey("Precio") && (header.Contains("precio") || header.Contains("publico") || header.Contains("precioventa") || header.Contains("preciopublico")))
                    mapa["Precio"] = col;
            }

            return mapa;
        }
    }
}
