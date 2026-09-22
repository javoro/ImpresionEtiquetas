using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ImpresionEtiquetas.Models;
using ImpresionEtiquetas.Services;

namespace ImpresionEtiquetas
{
    public partial class FrmImportarPdf : Form
    {
        private readonly CatalogService _catalogService = new CatalogService();
        private readonly PdfService _pdfService = new PdfService();
        private List<PdfItem> _items = new List<PdfItem>();
        private ResultadoImportacionPdf _resultado = new ResultadoImportacionPdf();

        public List<PdfItem> ItemsParaImprimir { get; private set; } = new List<PdfItem>();

        public FrmImportarPdf()
        {
            InitializeComponent();
        }

        private void FrmImportarPdf_Load(object sender, EventArgs e)
        {
            ActualizarResumen();
        }

        private async void btnCargarCatalogo_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                ofd.Title = "Seleccionar Catálogo / Lista de Precios";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    btnCargarCatalogo.Enabled = false;
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        int total = await _catalogService.CargarCatalogoAsync(ofd.FileName);
                        lblEstadoCatalogo.Text = $"✓ Catálogo cargado: {total} productos ({Path.GetFileName(ofd.FileName)})";
                        lblEstadoCatalogo.ForeColor = Color.FromArgb(21, 128, 61);

                        // Si ya hay ítems de PDF en la tabla, re-cruzar
                        if (_items.Count > 0)
                        {
                            foreach (var item in _items)
                            {
                                PdfService.AplicarCatalogo(item, _catalogService);
                            }
                            RefrescarGrid();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"Error al cargar catálogo: {ex.Message}", "Error de Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                        btnCargarCatalogo.Enabled = true;
                    }
                }
            }
        }

        private async void btnSeleccionarPdf_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos PDF (*.pdf)|*.pdf";
                ofd.Title = "Seleccionar Archivo de Pedido / Traspaso en PDF";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    btnSeleccionarPdf.Enabled = false;
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        lblArchivoPdf.Text = Path.GetFileName(ofd.FileName);

                        _resultado = await _pdfService.ParsePdfAsync(ofd.FileName, _catalogService);
                        _items = _resultado.Items;

                        RefrescarGrid();
                        ActualizarResumen();

                        if (_items.Count == 0)
                        {
                            MessageBox.Show(this, "No se detectaron productos ni filas de tabla en el archivo PDF.", "PDF sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (_resultado.Formato == FormatoPdf.Desconocido)
                        {
                            MessageBox.Show(this,
                                "No se reconoció el formato de la tabla del PDF. Se leyeron los renglones de todas formas, " +
                                "pero conviene revisarlos antes de transferirlos.",
                                "Formato no reconocido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            int sinSku = _items.Count(i => i.SkuNoEstandar);
                            if (sinSku > 0)
                            {
                                MessageBox.Show(this,
                                    $"Se detectaron {sinSku} productos cuyo código no tiene la forma habitual de SKU " +
                                    "(por ejemplo \"ROSA\" o \"POLAROID OFT\").\n\n" +
                                    "Están marcados en la tabla porque ese texto es lo que saldría impreso en la etiqueta.",
                                    "Códigos para revisar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"Error al procesar el archivo PDF: {ex.Message}", "Error de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                        btnSeleccionarPdf.Enabled = true;
                    }
                }
            }
        }

        private void RefrescarGrid()
        {
            dgvPdf.Rows.Clear();

            foreach (var item in _items)
            {
                string precioStr = item.Precio.HasValue ? item.Precio.Value.ToString("0.00", CultureInfo.InvariantCulture) : string.Empty;
                string estadoCat = item.CoincideCatalogo ? "✓ Coincide" : "Sin precio";

                int index = dgvPdf.Rows.Add(
                    item.Seleccionado,
                    item.Sku,
                    item.Modelo,
                    item.Color,
                    item.Medida,
                    item.Marca,
                    precioStr,
                    item.CantidadSugerida,
                    item.Cantidad,
                    estadoCat
                );

                // Colorear indicador de catálogo
                var row = dgvPdf.Rows[index];
                if (item.CoincideCatalogo)
                {
                    row.Cells["ColEstadoCatalogo"].Style.ForeColor = Color.FromArgb(21, 128, 61);
                    row.Cells["ColEstadoCatalogo"].Style.Font = new Font(dgvPdf.Font, FontStyle.Bold);
                }
                else
                {
                    row.Cells["ColEstadoCatalogo"].Style.ForeColor = Color.FromArgb(217, 119, 6);
                }

                // El codigo no tiene forma de SKU: se importa igual, pero se resalta porque
                // ese texto es el que acabaria impreso en la etiqueta.
                if (item.SkuNoEstandar)
                {
                    var celdaSku = row.Cells["ColSku"];
                    celdaSku.Style.BackColor = Color.FromArgb(254, 243, 199);
                    celdaSku.Style.ForeColor = Color.FromArgb(180, 83, 9);
                    celdaSku.Style.Font = new Font(dgvPdf.Font, FontStyle.Bold);
                    celdaSku.ToolTipText = "El codigo no tiene la forma habitual de SKU. Revise el texto antes de imprimir.";
                }
            }

            ActualizarResumen();
        }

        private void dgvPdf_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPdf.IsCurrentCellDirty)
            {
                dgvPdf.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvPdf_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _items.Count) return;

            var item = _items[e.RowIndex];
            var row = dgvPdf.Rows[e.RowIndex];

            string colName = dgvPdf.Columns[e.ColumnIndex].Name;

            if (colName == "ColSeleccionar")
            {
                item.Seleccionado = Convert.ToBoolean(row.Cells["ColSeleccionar"].Value);
            }
            else if (colName == "ColMarca")
            {
                item.Marca = row.Cells["ColMarca"].Value?.ToString()?.Trim()?.ToUpperInvariant() ?? string.Empty;
            }
            else if (colName == "ColPrecio")
            {
                string txt = row.Cells["ColPrecio"].Value?.ToString()?.Trim()?.Replace("$", "")?.Replace(",", ".");
                if (!string.IsNullOrEmpty(txt) && decimal.TryParse(txt, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal p))
                {
                    item.Precio = p;
                }
                else
                {
                    item.Precio = null;
                }
            }
            else if (colName == "ColCantidad")
            {
                string cantTxt = row.Cells["ColCantidad"].Value?.ToString()?.Trim();
                if (int.TryParse(cantTxt, out int c) && c > 0)
                {
                    item.Cantidad = c;
                }
                else
                {
                    row.Cells["ColCantidad"].Value = item.Cantidad; // revertir
                }
            }

            ActualizarResumen();
        }

        private void chkSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            bool sel = chkSeleccionarTodos.Checked;
            foreach (DataGridViewRow row in dgvPdf.Rows)
            {
                row.Cells["ColSeleccionar"].Value = sel;
            }
            foreach (var item in _items)
            {
                item.Seleccionado = sel;
            }
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            int totalProds = _items.Count;
            int totalSel = _items.Count(i => i.Seleccionado);
            int totalPiezas = _items.Where(i => i.Seleccionado).Sum(i => i.Cantidad);

            string prefijo = _resultado.Formato != FormatoPdf.Desconocido
                ? $"Formato {_resultado.NombreFormato} | "
                : string.Empty;

            lblResumen.Text = $"{prefijo}{totalProds} productos en PDF | {totalSel} seleccionados | {totalPiezas} piezas en total";
            btnTransferir.Enabled = totalSel > 0;
            btnExportarExcel.Enabled = totalProds > 0;
        }

        private async void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (_items.Count == 0) return;

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                sfd.FileName = $"Desglose_Pedido_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                sfd.Title = "Guardar Desglose de Pedido en Excel";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    try
                    {
                        await _pdfService.ExportarAExcelAsync(_items, sfd.FileName);
                        MessageBox.Show(this, "El archivo de Excel se generó correctamente.", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"Error al exportar a Excel: {ex.Message}", "Error al Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void btnTransferir_Click(object sender, EventArgs e)
        {
            var seleccionados = _items.Where(i => i.Seleccionado).ToList();
            if (seleccionados.Count == 0)
            {
                MessageBox.Show(this, "No hay productos seleccionados para transferir.", "Sin selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar si hay productos sin Marca o sin Precio
            var incompletos = seleccionados.Where(i => string.IsNullOrWhiteSpace(i.Marca) || !i.Precio.HasValue || i.Precio.Value <= 0).ToList();
            if (incompletos.Count > 0)
            {
                var resp = MessageBox.Show(this,
                    $"Hay {incompletos.Count} productos seleccionados que no tienen Marca o Precio asignado.\n\n" +
                    "¿Desea transferirlos de todos modos y completarlos en la tabla principal?",
                    "Productos con datos faltantes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resp != DialogResult.Yes) return;
            }

            ItemsParaImprimir = seleccionados;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
