using System;
using System.Linq;

namespace ImpresionEtiquetas.Models
{
    public class PdfItem
    {
        public bool Seleccionado { get; set; } = true;
        public string Sku { get; set; } = string.Empty;
        public string Upc { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Medida { get; set; } = string.Empty;
        public string DescripcionOriginal { get; set; } = string.Empty;
        public int CantidadSugerida { get; set; } = 1;
        public int Cantidad { get; set; } = 1;
        public string Marca { get; set; } = string.Empty;
        public decimal? Precio { get; set; }
        public bool CoincideCatalogo { get; set; }

        /// <summary>
        /// Descripción compuesta "Modelo / Color / Medida" que se envía a la cola de impresión
        /// y a la exportación a Excel. Omite las partes vacías para no dejar separadores sueltos.
        /// </summary>
        public string ModeloCompleto
        {
            get
            {
                var partes = new[] { Modelo, Color, Medida }
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .Select(p => p.Trim());

                return string.Join(" / ", partes);
            }
        }
    }

    public class CatalogItem
    {
        public string Sku { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public decimal? Precio { get; set; }
    }
}
