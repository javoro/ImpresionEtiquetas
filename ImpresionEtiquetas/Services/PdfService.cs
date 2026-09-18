using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
                            Upc = raw.Upc.Trim(),
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

        private static readonly Regex RegexSkuCompleto =
            new Regex(@"^\d{3,4}[A-Z]{2,3}\d{2,4}", RegexOptions.Compiled);

        /// <summary>
        /// Posición horizontal (Left) donde inicia cada columna de la tabla del PDF.
        /// Se detecta leyendo la fila de encabezados; si el PDF no la trae se usan los valores por defecto.
        /// </summary>
        private class ColumnasTabla
        {
            public const double Tolerancia = 2.0;

            public double Upc = 16.0;
            public double Codigo = 73.0;
            public double Descripcion = 130.0;
            public double Cantidad = 440.0;
            public double Precio = 497.0;

            public int IndiceColumna(double left)
            {
                if (left >= Precio - Tolerancia) return 4;
                if (left >= Cantidad - Tolerancia) return 3;
                if (left >= Descripcion - Tolerancia) return 2;
                if (left >= Codigo - Tolerancia) return 1;
                return 0;
            }
        }

        private List<PdfRawRow> ExtraerFilasDePagina(Page page)
        {
            var result = new List<PdfRawRow>();

            var lineas = AgruparEnLineas(page.GetWords());
            if (lineas.Count == 0) return result;

            var columnas = new ColumnasTabla();
            bool hayEncabezado = lineas.Any(EsLineaEncabezado);

            // Hasta encontrar el encabezado se ignora el bloque superior del documento
            // (título, fecha, almacén origen/destino). Si el PDF no trae encabezado se procesa todo.
            bool dentroDeTabla = !hayEncabezado;
            PdfRawRow filaActual = null;

            Action cerrarFila = () =>
            {
                if (filaActual == null) return;

                string sku = filaActual.Sku.Trim();
                string desc = filaActual.Descripcion.Trim();

                if (!string.IsNullOrWhiteSpace(sku) && (RegexSkuCompleto.IsMatch(sku) || desc.Contains("/")))
                {
                    filaActual.Sku = sku.ToUpperInvariant();
                    filaActual.Descripcion = desc;
                    result.Add(filaActual);
                }

                filaActual = null;
            };

            foreach (var linea in lineas)
            {
                if (EsLineaEncabezado(linea))
                {
                    cerrarFila();
                    LeerColumnasDeEncabezado(linea, columnas);
                    dentroDeTabla = true;
                    continue;
                }

                if (!dentroDeTabla) continue;

                if (EsFinDeTabla(linea))
                {
                    cerrarFila();
                    break;
                }

                string upc = string.Concat(linea.Where(w => columnas.IndiceColumna(w.BoundingBox.Left) == 0).Select(w => w.Text));
                string codigo = string.Concat(linea.Where(w => columnas.IndiceColumna(w.BoundingBox.Left) == 1).Select(w => w.Text));
                string desc = string.Join(" ", linea.Where(w => columnas.IndiceColumna(w.BoundingBox.Left) == 2).Select(w => w.Text)).Trim();
                string cantStr = string.Concat(linea.Where(w => columnas.IndiceColumna(w.BoundingBox.Left) == 3).Select(w => w.Text)).Trim();

                if (upc.Length == 0 && codigo.Length == 0 && desc.Length == 0 && cantStr.Length == 0)
                    continue;

                bool tieneCantidad = int.TryParse(cantStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out int cantidad) && cantidad > 0;

                // Una fila nueva se reconoce porque su CÓDIGO tiene forma de SKU completo, o porque
                // trae valor en la columna CANTIDAD (que nunca se repite en las líneas de continuación).
                bool esFilaNueva = (codigo.Length > 0 && RegexSkuCompleto.IsMatch(codigo))
                                   || (tieneCantidad && (codigo.Length > 0 || desc.Length > 0));

                if (esFilaNueva)
                {
                    cerrarFila();

                    filaActual = new PdfRawRow
                    {
                        Upc = upc,
                        Sku = codigo,
                        Descripcion = desc,
                        Cantidad = tieneCantidad ? cantidad : 1
                    };
                }
                else if (filaActual != null)
                {
                    // Línea de continuación: el PDF cortó la celda por ancho, no es un producto distinto.
                    // UPC y CÓDIGO se pegan sin separador; la DESCRIPCIÓN se une con un espacio.
                    filaActual.Upc += upc;
                    filaActual.Sku += codigo;

                    if (desc.Length > 0)
                    {
                        filaActual.Descripcion = (filaActual.Descripcion + " " + desc).Trim();
                    }

                    if (tieneCantidad) filaActual.Cantidad = cantidad;
                }
            }

            cerrarFila();

            return result;
        }

        /// <summary>
        /// Agrupa las palabras de la página en líneas visuales usando su coordenada vertical.
        /// </summary>
        private static List<List<Word>> AgruparEnLineas(IEnumerable<Word> words)
        {
            var lineas = new List<List<Word>>();

            var ordenadas = words
                .Where(w => !string.IsNullOrWhiteSpace(w.Text))
                .OrderByDescending(w => w.BoundingBox.Bottom)
                .ToList();

            List<Word> lineaActual = null;
            double currentY = 0;

            foreach (var word in ordenadas)
            {
                if (lineaActual == null)
                {
                    lineaActual = new List<Word> { word };
                    currentY = word.BoundingBox.Bottom;
                }
                else if (Math.Abs(word.BoundingBox.Bottom - currentY) <= 3.0)
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

            return lineas;
        }

        private static bool EsLineaEncabezado(List<Word> linea)
        {
            bool codigo = false, descripcion = false;

            foreach (var w in linea)
            {
                string t = NormalizarTexto(w.Text);
                if (t == "CODIGO") codigo = true;
                else if (t == "DESCRIPCION") descripcion = true;
            }

            return codigo && descripcion;
        }

        private static void LeerColumnasDeEncabezado(List<Word> linea, ColumnasTabla columnas)
        {
            foreach (var w in linea)
            {
                switch (NormalizarTexto(w.Text))
                {
                    case "UPC": columnas.Upc = w.BoundingBox.Left; break;
                    case "CODIGO": columnas.Codigo = w.BoundingBox.Left; break;
                    case "DESCRIPCION": columnas.Descripcion = w.BoundingBox.Left; break;
                    case "CANTIDAD": columnas.Cantidad = w.BoundingBox.Left; break;
                    case "PRECIO": columnas.Precio = w.BoundingBox.Left; break;
                }
            }
        }

        private static bool EsFinDeTabla(List<Word> linea)
        {
            string texto = NormalizarTexto(string.Join(" ", linea.Select(w => w.Text)));
            return texto.StartsWith("TOTAL")
                   || texto.StartsWith("OBSERVACIONES")
                   || texto.Contains("TCPDF");
        }

        private static string NormalizarTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;

            string normalizado = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(normalizado.Length);

            foreach (char c in normalizado)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC).Trim().ToUpperInvariant();
        }

        private (string Modelo, string Color, string Medida) DesfragmentarDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                return (string.Empty, string.Empty, string.Empty);

            string limpia = descripcion.Trim();

            // El separador real de la descripción es " / ". Un "/" sin espacios forma parte del dato
            // (por ejemplo el color "601/71" o "001/87") y no debe partirse.
            while (limpia.EndsWith("/"))
            {
                limpia = limpia.Substring(0, limpia.Length - 1).TrimEnd();
            }

            var partes = limpia.Split(new[] { " / " }, StringSplitOptions.None)
                .Select(p => p.Trim())
                .ToArray();

            if (partes.Length == 1 && limpia.Contains("/"))
            {
                partes = limpia.Split(new[] { '/' }, StringSplitOptions.None)
                    .Select(p => p.Trim())
                    .ToArray();
            }

            string modelo = partes.Length > 0 ? partes[0] : limpia;
            string color = partes.Length > 1 ? partes[1] : string.Empty;
            string medida = partes.Length > 2 ? string.Join(" / ", partes.Skip(2)) : string.Empty;

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
                        // Misma composición que se envía a la cola de impresión: Modelo / Color / Medida
                        ws.Cells[row, 2].Value = item.ModeloCompleto;
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
            public string Upc { get; set; } = string.Empty;
            public string Sku { get; set; } = string.Empty;
            public string Descripcion { get; set; } = string.Empty;
            public int Cantidad { get; set; }
        }
    }
}
