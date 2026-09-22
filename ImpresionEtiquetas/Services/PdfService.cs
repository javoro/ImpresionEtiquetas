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
        public Task<ResultadoImportacionPdf> ParsePdfAsync(string filePath, CatalogService catalogService = null)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("El archivo PDF no existe.", filePath);

                var rawRows = new List<PdfRawRow>();

                // El esquema se detecta con la fila de encabezados y se conserva entre páginas:
                // el inventario sólo la imprime en la primera hoja.
                EsquemaTabla esquema = null;

                using (var document = PdfDocument.Open(filePath))
                {
                    for (int p = 1; p <= document.NumberOfPages; p++)
                    {
                        var page = document.GetPage(p);
                        rawRows.AddRange(ExtraerFilasDePagina(page, ref esquema));
                    }
                }

                var resultado = new ResultadoImportacionPdf
                {
                    Formato = esquema?.Formato ?? FormatoPdf.Desconocido,
                    FilasLeidas = rawRows.Count,
                    PiezasLeidas = rawRows.Sum(r => r.Cantidad)
                };

                // Agrupar por SKU sumando cantidades (sugerencia)
                var agrupados = new Dictionary<string, PdfItem>(StringComparer.OrdinalIgnoreCase);
                var orden = new List<string>();

                foreach (var raw in rawRows)
                {
                    if (string.IsNullOrWhiteSpace(raw.Sku)) continue;

                    string skuKey = raw.Sku.Trim().ToUpperInvariant();

                    if (agrupados.TryGetValue(skuKey, out var existente))
                    {
                        existente.CantidadSugerida += raw.Cantidad;
                        existente.Cantidad = existente.CantidadSugerida;
                        continue;
                    }

                    var item = new PdfItem
                    {
                        Seleccionado = true,
                        Sku = skuKey,
                        Upc = raw.Upc.Trim(),
                        DescripcionOriginal = raw.Descripcion.Trim(),
                        CantidadSugerida = raw.Cantidad,
                        Cantidad = raw.Cantidad,
                        Marca = string.Empty,
                        Precio = ParsearPrecio(raw.PrecioTexto),
                        CoincideCatalogo = false,
                        Formato = raw.Formato,
                        SkuNoEstandar = !RegexSkuCompleto.IsMatch(skuKey)
                    };

                    // El traspaso describe "Modelo / Color / Medida", así que se desglosa.
                    // El inventario mezcla "Marca / Material / Género" con "Modelo / Color / Medida"
                    // en la misma columna, y no hay forma fiable de distinguirlos: se deja
                    // la descripción tal cual para no inventar un desglose equivocado.
                    if (raw.Formato == FormatoPdf.Inventario)
                    {
                        item.Modelo = raw.Descripcion.Trim().ToUpperInvariant();
                    }
                    else
                    {
                        var fragmentos = DesfragmentarDescripcion(raw.Descripcion);
                        item.Modelo = fragmentos.Modelo.Trim().ToUpperInvariant();
                        item.Color = fragmentos.Color.Trim().ToUpperInvariant();
                        item.Medida = fragmentos.Medida.Trim();
                    }

                    AplicarCatalogo(item, catalogService);

                    agrupados[skuKey] = item;
                    orden.Add(skuKey);
                }

                resultado.Items = orden.Select(k => agrupados[k]).ToList();
                return resultado;
            });
        }

        /// <summary>
        /// Completa marca y precio desde el catálogo. El precio que trae el PDF manda;
        /// el catálogo sólo rellena cuando el PDF no lo trae o viene en cero.
        /// </summary>
        public static void AplicarCatalogo(PdfItem item, CatalogService catalogService)
        {
            if (catalogService == null || !catalogService.EstaCargado) return;
            if (!catalogService.IntentarObtener(item.Sku, out var catalogItem)) return;

            item.Marca = catalogItem.Marca;
            item.CoincideCatalogo = true;

            if (!item.Precio.HasValue || item.Precio.Value <= 0)
            {
                item.Precio = catalogItem.Precio;
            }

            if (string.IsNullOrEmpty(item.Modelo) && !string.IsNullOrEmpty(catalogItem.Modelo))
            {
                item.Modelo = catalogItem.Modelo;
            }
        }

        private static readonly Regex RegexSkuCompleto =
            new Regex(@"^\d{3,4}[A-Z]{2,3}\d{2,4}", RegexOptions.Compiled);

        // ─── Detección del esquema de la tabla ──────────────────────────────────────

        private const string RolUpc = "UPC";
        private const string RolCodigo = "CODIGO";
        private const string RolDescripcion = "DESCRIPCION";
        private const string RolCantidad = "CANTIDAD";
        private const string RolPrecio = "PRECIO";
        private const string RolGenero = "GENERO";
        private const string RolCategoria = "CATEGORIA";

        /// <summary>
        /// Columnas de la tabla y las fronteras horizontales que las separan, deducidas
        /// de la fila de encabezados.
        /// </summary>
        private class EsquemaTabla
        {
            public FormatoPdf Formato;
            public List<string> Roles = new List<string>();
            public List<double> Fronteras = new List<double>();

            public string RolEn(double left)
            {
                for (int i = 0; i < Fronteras.Count; i++)
                {
                    if (left < Fronteras[i]) return Roles[i];
                }
                return Roles[Roles.Count - 1];
            }

            public bool Tiene(string rol)
            {
                return Roles.Contains(rol);
            }
        }

        /// <summary>
        /// Traduce el texto de un encabezado al rol de columna que representa.
        /// EXISTENCIA (inventario) y CANTIDAD (traspaso) cumplen la misma función.
        /// </summary>
        private static string RolDeEncabezado(string textoNormalizado)
        {
            switch (textoNormalizado)
            {
                case "UPC": return RolUpc;
                case "CODIGO": return RolCodigo;
                case "DESCRIPCION": return RolDescripcion;
                case "CANTIDAD": return RolCantidad;
                case "EXISTENCIA": return RolCantidad;
                case "PRECIO": return RolPrecio;
                case "GENERO": return RolGenero;
                case "CATEGORIA": return RolCategoria;
                default: return null;   // "COSTO" y cualquier otra palabra suelta
            }
        }

        private static EsquemaTabla ConstruirEsquema(List<Word> lineaEncabezado)
        {
            var columnas = new List<(string Rol, double Left, double Right)>();

            foreach (var w in lineaEncabezado.OrderBy(w => w.BoundingBox.Left))
            {
                string rol = RolDeEncabezado(NormalizarTexto(w.Text));
                if (rol == null) continue;
                if (columnas.Any(c => c.Rol == rol)) continue;

                columnas.Add((rol, w.BoundingBox.Left, w.BoundingBox.Right));
            }

            if (columnas.Count < 2) return null;

            var esquema = new EsquemaTabla
            {
                Roles = columnas.Select(c => c.Rol).ToList(),
                // EXISTENCIA y CATEGORIA sólo existen en el inventario; UPC sólo en el traspaso.
                Formato = columnas.Any(c => c.Rol == RolCategoria)
                    ? FormatoPdf.Inventario
                    : FormatoPdf.Traspaso
            };

            // Frontera entre dos columnas: pegada al encabezado de la derecha, porque el
            // contenido de una celda puede ser mucho más ancho que su título (la columna
            // DESCRIPCIÓN del traspaso) y puede empezar a la izquierda de él cuando la
            // celda está alineada a la derecha (PRECIO y GÉNERO del inventario).
            for (int i = 0; i < columnas.Count - 1; i++)
            {
                double frontera = Math.Max(columnas[i].Right + 2.0, columnas[i + 1].Left - 20.0);
                esquema.Fronteras.Add(frontera);
            }

            return esquema;
        }

        // ─── Lectura de la tabla ────────────────────────────────────────────────────

        private List<PdfRawRow> ExtraerFilasDePagina(Page page, ref EsquemaTabla esquema)
        {
            var result = new List<PdfRawRow>();

            var lineas = AgruparEnLineas(page.GetWords());
            if (lineas.Count == 0) return result;

            // Sin esquema todavía no se sabe dónde empieza la tabla: hay que esperar al
            // encabezado. Una vez detectado (en esta página o en una anterior) se procesa
            // desde el principio de la hoja.
            bool dentroDeTabla = esquema != null;
            PdfRawRow filaActual = null;
            var esquemaLocal = esquema;

            Action cerrarFila = () =>
            {
                if (filaActual == null) return;

                string sku = filaActual.Sku.Trim();
                filaActual.Descripcion = filaActual.Descripcion.Trim();

                if (!string.IsNullOrWhiteSpace(sku) &&
                    (RegexSkuCompleto.IsMatch(sku) || filaActual.TieneCantidadPropia))
                {
                    filaActual.Sku = sku.ToUpperInvariant();
                    result.Add(filaActual);
                }

                filaActual = null;
            };

            foreach (var linea in lineas)
            {
                if (EsLineaEncabezado(linea))
                {
                    cerrarFila();
                    var nuevo = ConstruirEsquema(linea);
                    if (nuevo != null) esquemaLocal = nuevo;
                    dentroDeTabla = true;
                    continue;
                }

                if (!dentroDeTabla || esquemaLocal == null) continue;

                if (EsFinDeTabla(linea))
                {
                    cerrarFila();
                    break;
                }

                var celdas = new Dictionary<string, List<string>>();
                foreach (var w in linea)
                {
                    string rol = esquemaLocal.RolEn(w.BoundingBox.Left);
                    if (!celdas.TryGetValue(rol, out var lista))
                    {
                        lista = new List<string>();
                        celdas[rol] = lista;
                    }
                    lista.Add(w.Text);
                }

                // UPC, CANTIDAD y PRECIO se unen sin separador porque el PDF parte el dato
                // en varias palabras ("$" + "1" + "049.00" = $1049.00). El CÓDIGO sí lleva
                // espacio: dentro de un mismo renglón puede ser texto de varias palabras
                // ("POLAROID OFT"). Lo que se pega sin separador es la continuación de la
                // línea siguiente, que es donde el PDF parte los SKU largos.
                string upc = Unir(celdas, RolUpc, false);
                string codigo = Unir(celdas, RolCodigo, true);
                string desc = Unir(celdas, RolDescripcion, true);
                string cantStr = Unir(celdas, RolCantidad, false);
                string precioStr = Unir(celdas, RolPrecio, false);

                if (upc.Length == 0 && codigo.Length == 0 && desc.Length == 0 && cantStr.Length == 0)
                    continue;

                bool tieneCantidad = int.TryParse(cantStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out int cantidad)
                                     && cantidad > 0;

                // Una fila nueva se reconoce porque trae valor en CANTIDAD/EXISTENCIA, que
                // nunca se repite en las líneas de continuación. El patrón de SKU sirve de
                // respaldo; no basta por sí solo porque el inventario incluye códigos de
                // texto libre como "ROSA" o "POLAROID OFT".
                bool esFilaNueva = tieneCantidad || (codigo.Length > 0 && RegexSkuCompleto.IsMatch(codigo));

                if (esFilaNueva)
                {
                    cerrarFila();

                    filaActual = new PdfRawRow
                    {
                        Upc = upc,
                        Sku = codigo,
                        Descripcion = desc,
                        Cantidad = tieneCantidad ? cantidad : 1,
                        PrecioTexto = precioStr,
                        Formato = esquemaLocal.Formato,
                        TieneCantidadPropia = tieneCantidad
                    };
                }
                else if (filaActual != null)
                {
                    // Línea de continuación: el PDF cortó la celda por ancho, no es un
                    // producto distinto. UPC y CÓDIGO se pegan sin separador; la
                    // DESCRIPCIÓN se une con un espacio.
                    filaActual.Upc += upc;
                    filaActual.Sku += codigo;

                    if (desc.Length > 0)
                    {
                        filaActual.Descripcion = (filaActual.Descripcion + " " + desc).Trim();
                    }

                    if (filaActual.PrecioTexto.Length == 0) filaActual.PrecioTexto = precioStr;
                }
            }

            cerrarFila();

            esquema = esquemaLocal;
            return result;
        }

        private static string Unir(Dictionary<string, List<string>> celdas, string rol, bool conEspacios)
        {
            if (!celdas.TryGetValue(rol, out var partes)) return string.Empty;
            return conEspacios ? string.Join(" ", partes).Trim() : string.Concat(partes).Trim();
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

        /// <summary>
        /// Convierte el texto de la columna PRECIO ("$449.00", "$1049.00") a decimal.
        /// Devuelve null cuando no hay precio o viene en cero, para que el renglón quede
        /// marcado como "Sin precio" y se complete con el catálogo o a mano.
        /// </summary>
        private static decimal? ParsearPrecio(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return null;

            string limpio = texto.Replace("$", string.Empty)
                                 .Replace(",", string.Empty)
                                 .Replace(" ", string.Empty)
                                 .Trim();

            if (!decimal.TryParse(limpio, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
                return null;

            return valor > 0 ? valor : (decimal?)null;
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
                    string[] headers = { "SKU", "Modelo", "Color", "Medida", "Marca", "Precio", "Cant. Sugerida", "Cant. Etiquetas", "Catálogo OK", "Revisar SKU" };
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
                        // Misma composición que se envía a la cola de impresión
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
                        ws.Cells[row, 10].Value = item.SkuNoEstandar ? "SÍ" : string.Empty;

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
            public string PrecioTexto { get; set; } = string.Empty;
            public int Cantidad { get; set; }
            public bool TieneCantidadPropia { get; set; }
            public FormatoPdf Formato { get; set; } = FormatoPdf.Desconocido;
        }
    }
}
