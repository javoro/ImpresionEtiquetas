using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using ImpresionEtiquetas.Models;

namespace ImpresionEtiquetas.Services
{
    public class PdfService
    {
        public Task<List<PdfItem>> ParsePdfAsync(string filePath, CatalogService catalogService = null)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("El archivo PDF no existe.", filePath);

                var rawRows = new List<PdfRawRow>();

                using (var document = PdfDocument.Open(filePath))
                {
                    for (int p = 1; p <= document.NumberOfPages; p++)
                    {
                        var page = document.GetPage(p);
                        var pageRows = ExtraerFilasDePagina(page);
                        rawRows.AddRange(pageRows);
                    }
                }

                // Agrupar por SKU sumando cantidades (sugerencia)
                var agrupados = new Dictionary<string, PdfItem>(StringComparer.OrdinalIgnoreCase);

                foreach (var raw in rawRows)
                {
                    if (string.IsNullOrWhiteSpace(raw.Sku)) continue;

                    string skuKey = raw.Sku.Trim().ToUpperInvariant();
                    var fragmentos = DesfragmentarDescripcion(raw.Descripcion);

                    if (agrupados.TryGetValue(skuKey, out var existente))
                    {
                        existente.CantidadSugerida += raw.Cantidad;
                        existente.Cantidad = existente.CantidadSugerida;
                    }
                    else
                    {
                        var item = new PdfItem
                        {
                            Seleccionado = true,
                            Sku = skuKey,
                            Modelo = fragmentos.Modelo.Trim().ToUpperInvariant(),
                            Color = fragmentos.Color.Trim().ToUpperInvariant(),
                            Medida = fragmentos.Medida.Trim(),
                            DescripcionOriginal = raw.Descripcion.Trim(),
                            CantidadSugerida = raw.Cantidad,
                            Cantidad = raw.Cantidad,
                            Marca = string.Empty,
                            Precio = null,
                            CoincideCatalogo = false
                        };

                        // Cruce con catálogo si está disponible
                        if (catalogService != null && catalogService.EstaCargado)
                        {
                            if (catalogService.IntentarObtener(skuKey, out var catalogItem))
                            {
                                item.Marca = catalogItem.Marca;
                                item.Precio = catalogItem.Precio;
                                item.CoincideCatalogo = true;
                                if (string.IsNullOrEmpty(item.Modelo) && !string.IsNullOrEmpty(catalogItem.Modelo))
                                {
                                    item.Modelo = catalogItem.Modelo;
                                }
                            }
                        }

                        agrupados[skuKey] = item;
                    }
                }

                return agrupados.Values.ToList();
            });
        }

        private List<PdfRawRow> ExtraerFilasDePagina(Page page)
        {
            var result = new List<PdfRawRow>();

            // Filtrar palabras de la tabla (descartar cabeceras superiores y pie de página)
            var words = page.GetWords()
                .Where(w => w.BoundingBox.Bottom < 700 && w.BoundingBox.Bottom > 45)
                .OrderByDescending(w => w.BoundingBox.Bottom)
                .ToList();

            if (words.Count == 0) return result;

            // Agrupar palabras por coordenada Y (tolerancia de 6 puntos)
            var lineas = new List<List<Word>>();
            List<Word> lineaActual = null;
            double currentY = -1;

            foreach (var word in words)
            {
                if (currentY < 0)
                {
                    currentY = word.BoundingBox.Bottom;
                    lineaActual = new List<Word> { word };
                }
                else if (Math.Abs(word.BoundingBox.Bottom - currentY) <= 6)
                {
                    lineaActual.Add(word);
                }
                else
                {
                    lineas.Add(lineaActual.OrderBy(w => w.BoundingBox.Left).ToList());
                    lineaActual = new List<Word> { word };
                    currentY = word.BoundingBox.Bottom;
                }
            }
            if (lineaActual != null && lineaActual.Count > 0)
            {
                lineas.Add(lineaActual.OrderBy(w => w.BoundingBox.Left).ToList());
            }

            // Procesar líneas agrupadas detectando continuaciones por celdas estrechas
            for (int i = 0; i < lineas.Count; i++)
            {
                var r = lineas[i];

                var skuWords = r.Where(w => w.BoundingBox.Left >= 65 && w.BoundingBox.Left < 130).ToList();
                var descWords = r.Where(w => w.BoundingBox.Left >= 125 && w.BoundingBox.Left < 435).ToList();
                var cantWords = r.Where(w => w.BoundingBox.Left >= 435 && w.BoundingBox.Left < 480).ToList();

                string sku = string.Concat(skuWords.Select(w => w.Text));
                string desc = string.Join(" ", descWords.Select(w => w.Text)).Trim();
                string cantStr = string.Concat(cantWords.Select(w => w.Text)).Trim();

                // Si la siguiente línea es un remanente/envoltura de SKU/UPC (alrededor de 10 puntos abajo)
                if (i + 1 < lineas.Count)
                {
                    var sig = lineas[i + 1];
                    double diffY = r[0].BoundingBox.Bottom - sig[0].BoundingBox.Bottom;

                    // Si la línea siguiente sólo contiene fragmentos de SKU o UPC y está a unos 8-14 puntos abajo
                    if (diffY >= 6 && diffY <= 15)
                    {
                        var sigSkuWords = sig.Where(w => w.BoundingBox.Left >= 65 && w.BoundingBox.Left < 130).ToList();
                        var sigDescWords = sig.Where(w => w.BoundingBox.Left >= 125 && w.BoundingBox.Left < 435).ToList();

                        if (sigDescWords.Count == 0 && sigSkuWords.Count > 0)
                        {
                            sku += string.Concat(sigSkuWords.Select(w => w.Text));
                            i++; // Consumir línea de continuación
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(sku) && (Regex.IsMatch(sku, @"\d{4}[A-Z]{2}\d{3}") || desc.Contains("/")))
                {
                    int cantidad = 1;
                    if (!string.IsNullOrEmpty(cantStr) && int.TryParse(cantStr, out int c) && c > 0)
                    {
                        cantidad = c;
                    }

                    result.Add(new PdfRawRow
                    {
                        Sku = sku.Trim().ToUpperInvariant(),
                        Descripcion = desc,
                        Cantidad = cantidad
                    });
                }
            }

            return result;
        }

        private (string Modelo, string Color, string Medida) DesfragmentarDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                return (string.Empty, string.Empty, string.Empty);

            var partes = descripcion.Split(new[] { '/' }, StringSplitOptions.None)
                .Select(p => p.Trim())
                .ToArray();

            string modelo = partes.Length > 0 ? partes[0] : descripcion;
            string color = partes.Length > 1 ? partes[1] : string.Empty;
            string medida = partes.Length > 2 ? partes[2] : string.Empty;

            return (modelo, color, medida);
        }

        public Task ExportarAExcelAsync(List<PdfItem> items, string rutaArchivo)
        {
            return Task.Run(() =>
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var ws = package.Workbook.Worksheets.Add("Desglose Pedido");

                    // Encabezados
                    string[] headers = { "SKU", "Modelo", "Color", "Medida", "Marca", "Precio", "Cant. Sugerida", "Cant. Etiquetas", "Catálogo OK" };
                    for (int c = 0; c < headers.Length; c++)
                    {
                        ws.Cells[1, c + 1].Value = headers[c];
                        ws.Cells[1, c + 1].Style.Font.Bold = true;
                        ws.Cells[1, c + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[1, c + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(45, 125, 154));
                        ws.Cells[1, c + 1].Style.Font.Color.SetColor(Color.White);
                        ws.Cells[1, c + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    int row = 2;
                    foreach (var item in items)
                    {
                        ws.Cells[row, 1].Value = item.Sku;
                        ws.Cells[row, 2].Value = item.Modelo;
                        ws.Cells[row, 3].Value = item.Color;
                        ws.Cells[row, 4].Value = item.Medida;
                        ws.Cells[row, 5].Value = item.Marca;

                        if (item.Precio.HasValue)
                        {
                            ws.Cells[row, 6].Value = (double)item.Precio.Value;
                            ws.Cells[row, 6].Style.Numberformat.Format = "$#,##0.00";
                        }
                        else
                        {
                            ws.Cells[row, 6].Value = string.Empty;
                        }

                        ws.Cells[row, 7].Value = item.CantidadSugerida;
                        ws.Cells[row, 8].Value = item.Cantidad;
                        ws.Cells[row, 9].Value = item.CoincideCatalogo ? "SÍ" : "NO";

                        row++;
                    }

                    ws.Cells[1, 1, row - 1, headers.Length].AutoFitColumns();

                    var file = new FileInfo(rutaArchivo);
                    if (file.Exists) file.Delete();
                    package.SaveAs(file);
                }
            });
        }

        private class PdfRawRow
        {
            public string Sku { get; set; }
            public string Descripcion { get; set; }
            public int Cantidad { get; set; }
        }
    }
}
