using System;

namespace ImpresionEtiquetas.Models
{
    public class PdfItem
    {
        public bool Seleccionado { get; set; } = true;
        public string Sku { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Medida { get; set; } = string.Empty;
        public string DescripcionOriginal { get; set; } = string.Empty;
        public int CantidadSugerida { get; set; } = 1;
        public int Cantidad { get; set; } = 1;
        public string Marca { get; set; } = string.Empty;
        public decimal? Precio { get; set; }
        public bool CoincideCatalogo { get; set; }
    }

    public class CatalogItem
    {
        public string Sku { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public decimal? Precio { get; set; }
    }
}
