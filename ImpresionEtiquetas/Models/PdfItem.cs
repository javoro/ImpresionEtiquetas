using System;
using System.Collections.Generic;
using System.Linq;

namespace ImpresionEtiquetas.Models
{
    /// <summary>
    /// Formatos de PDF que reconoce la importación. Se detecta automáticamente
    /// leyendo la fila de encabezados de la tabla.
    /// </summary>
    public enum FormatoPdf
    {
        Desconocido = 0,
        /// <summary>Traspaso entre almacenes: UPC, CÓDIGO, DESCRIPCIÓN, CANTIDAD, PRECIO COSTO.</summary>
        Traspaso = 1,
        /// <summary>Inventario de local: CÓDIGO, DESCRIPCIÓN, PRECIO, GÉNERO, CATEGORIA, EXISTENCIA.</summary>
        Inventario = 2
    }

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

        /// <summary>Formato del PDF del que se extrajo el renglón.</summary>
        public FormatoPdf Formato { get; set; } = FormatoPdf.Desconocido;

        /// <summary>
        /// El CÓDIGO del PDF no tiene la forma habitual de SKU (por ejemplo "ROSA" o
        /// "POLAROID OFT"). El renglón se importa igual, pero conviene revisarlo antes
        /// de imprimir porque ese texto es lo que va a salir en la etiqueta.
        /// </summary>
        public bool SkuNoEstandar { get; set; }

        /// <summary>
        /// Descripción compuesta que se envía a la cola de impresión y a la exportación.
        /// En el formato de traspaso reconstruye "Modelo / Color / Medida"; en el de
        /// inventario devuelve la descripción tal cual venía, sin interpretarla.
        /// Omite las partes vacías para no dejar separadores sueltos.
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

    /// <summary>
    /// Resultado de leer un PDF: los renglones detectados más la información de
    /// diagnóstico que se muestra en la pantalla de importación.
    /// </summary>
    public class ResultadoImportacionPdf
    {
        public List<PdfItem> Items { get; set; } = new List<PdfItem>();
        public FormatoPdf Formato { get; set; } = FormatoPdf.Desconocido;

        /// <summary>Renglones leídos del PDF antes de agrupar por SKU.</summary>
        public int FilasLeidas { get; set; }

        /// <summary>Suma de las cantidades leídas (CANTIDAD o EXISTENCIA según el formato).</summary>
        public int PiezasLeidas { get; set; }

        public string NombreFormato
        {
            get
            {
                switch (Formato)
                {
                    case FormatoPdf.Traspaso: return "Traspaso";
                    case FormatoPdf.Inventario: return "Inventario";
                    default: return "No reconocido";
                }
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
