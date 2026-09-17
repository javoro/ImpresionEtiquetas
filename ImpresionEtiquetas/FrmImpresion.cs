using System.Reflection;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ImpresionEtiquetas
{
    public partial class frmImprimir : Form
    {
        private readonly string pathRoot = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string pathSalida;
        private readonly string urlPlantilla;
        private readonly string fileEtiquetas = "Etiquetas.txt";
        private readonly string urlBAT;
        private readonly string urlPlantillaExcel = "Plantilla.xlsx";
        private int? filaEnEdicion = null;
        private Dictionary<string, object> valoresOriginalesFila = new Dictionary<string, object>();
        private const string ColAccionPrincipal = "AccionPrincipal";
        private const string ColAccionSecundaria = "AccionSecundaria";
        private readonly Color colorFilaEditando = Color.FromArgb(254, 249, 195);

        // Longitudes máximas activas (se actualizan al guardar la configuración)
        private int maxLongitudMarca;
        private int maxLongitudModelo;
        private int maxLongitudSku;

        public frmImprimir()
        {
            InitializeComponent();
            urlPlantilla = Path.Combine(pathRoot, "Plantilla.txt");
            urlBAT = Path.Combine(pathRoot, "PrintEtiqueta.bat");

            pathSalida = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "ImpresionEtiquetas");

            if (!Directory.Exists(pathSalida))
            {
                Directory.CreateDirectory(pathSalida);
            }
        }

        private void frmImprimir_Load(object sender, EventArgs e)
        {
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.AllowUserToOrderColumns = false;
            dgvDatos.ReadOnly = false;
            this.WindowState = FormWindowState.Maximized;
            txtEmpresa.CharacterCasing = CharacterCasing.Upper;
            string empresaGuardada = LeerStringConfig("NombreEmpresa", "VISUALIZA+");
            txtEmpresa.Text = TextoMayusculas(empresaGuardada);
            txtEmpresa.Leave += txtEmpresa_Leave;
            this.FormClosing += frmImprimir_FormClosing;
            ConfigurarGridAcciones();
            ConfigurarModo();
            txtPrecioManual.KeyPress += TxtPrecioManual_KeyPress;
            txtCantidadManual.KeyPress += TxtCantidadManual_KeyPress;
            dgvDatos.EditingControlShowing += dgvDatos_EditingControlShowing;

            // Cargar configuración de longitudes desde App.config y reflejarla en pantalla
            CargarConfigLongitudEnPantalla();

            // Verificar actualizaciones en segundo plano
            Task.Run(() => UpdateChecker.CheckForUpdates());
        }

        // ─── Configuración de longitudes ────────────────────────────────────────────

        /// <summary>
        /// Lee los valores de App.config, los muestra en los NumericUpDown
        /// y los aplica en las variables de trabajo.
        /// </summary>
        private void CargarConfigLongitudEnPantalla()
        {
            maxLongitudMarca  = LeerLongitudConfig("MaxLongitudMarca",  defaultValue: 16);
            maxLongitudModelo = LeerLongitudConfig("MaxLongitudModelo", defaultValue: 12);
            maxLongitudSku    = LeerLongitudConfig("MaxLongitudSku",    defaultValue: 20);

            nudConfigMarca.Value  = maxLongitudMarca;
            nudConfigModelo.Value = maxLongitudModelo;
            nudConfigSku.Value    = maxLongitudSku;

            // Asegurar estado inicial deshabilitado para todos los campos configurables
            txtEmpresa.Enabled       = false;
            nudConfigMarca.Enabled   = false;
            nudConfigModelo.Enabled  = false;
            nudConfigSku.Enabled     = false;
            btnGuardarConfig.Enabled = false;
            chkEditarConfig.Checked  = false;
            chkEditarConfig.Text = "✏️ Editar";
        }

        /// <summary>
        /// Habilita o deshabilita los campos de configuración según el estado del checkbox.
        /// </summary>
        private void chkEditarConfig_CheckedChanged(object sender, EventArgs e)
        {
            bool editando = chkEditarConfig.Checked;

            txtEmpresa.Enabled       = editando;
            nudConfigMarca.Enabled   = editando;
            nudConfigModelo.Enabled  = editando;
            nudConfigSku.Enabled     = editando;
            btnGuardarConfig.Enabled = editando;

            chkEditarConfig.Text = editando ? "✕ Cancelar" : "✏️ Editar";

            if (editando)
            {
                txtEmpresa.Focus();
                txtEmpresa.SelectAll();
            }
            else
            {
                // Al cancelar, restaurar los valores guardados en config
                string empresaGuardada = LeerStringConfig("NombreEmpresa", "VISUALIZA+");
                txtEmpresa.Text = TextoMayusculas(empresaGuardada);
                txtEmpresa.BackColor = Color.White;
                errorProvider1.SetError(txtEmpresa, "");

                nudConfigMarca.Value  = maxLongitudMarca;
                nudConfigModelo.Value = maxLongitudModelo;
                nudConfigSku.Value    = maxLongitudSku;
            }
        }

        /// <summary>
        /// Guarda los valores de los NumericUpDown en App.config
        /// y actualiza las variables de trabajo.
        /// </summary>
        private void btnGuardarConfig_Click(object sender, EventArgs e)
        {
            try
            {
                string empresaNueva = txtEmpresa.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(empresaNueva))
                {
                    errorProvider1.SetError(txtEmpresa, "Ingrese el nombre de la empresa.");
                    txtEmpresa.BackColor = Color.FromArgb(254, 226, 226);
                    MessageBox.Show("El nombre de la empresa no puede quedar vacío.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmpresa.Focus();
                    return;
                }

                GuardarLongitudConfig("MaxLongitudMarca",  (int)nudConfigMarca.Value);
                GuardarLongitudConfig("MaxLongitudModelo", (int)nudConfigModelo.Value);
                GuardarLongitudConfig("MaxLongitudSku",    (int)nudConfigSku.Value);
                GuardarStringConfig("NombreEmpresa", empresaNueva);

                // Actualizar variables activas
                maxLongitudMarca  = (int)nudConfigMarca.Value;
                maxLongitudModelo = (int)nudConfigModelo.Value;
                maxLongitudSku    = (int)nudConfigSku.Value;

                txtEmpresa.Text = TextoMayusculas(empresaNueva);
                txtEmpresa.BackColor = Color.White;
                errorProvider1.SetError(txtEmpresa, "");

                // Volver al modo solo lectura
                chkEditarConfig.Checked = false;

                MessageBox.Show(
                    "Configuración de etiqueta guardada correctamente.",
                    "Configuración guardada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo guardar la configuración: {ex.Message}",
                    "Error al guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ─── Eventos de persistencia de empresa ────────────────────────────────────────

        private void txtEmpresa_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmpresa.Text))
            {
                try
                {
                    GuardarStringConfig("NombreEmpresa", txtEmpresa.Text.Trim());
                }
                catch { }
            }
        }

        private void frmImprimir_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmpresa.Text))
            {
                try
                {
                    GuardarStringConfig("NombreEmpresa", txtEmpresa.Text.Trim());
                }
                catch { }
            }
        }

        // ─── Helpers de configuración ────────────────────────────────────────────

        /// <summary>
        /// Ruta del archivo de configuración de usuario en LocalApplicationData.
        /// Se usa en lugar del App.config para evitar problemas de permisos en Program Files.
        /// </summary>
        private static readonly string rutaConfigUsuario = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ImpresionEtiquetas",
            "config.txt");

        /// <summary>
        /// Lee una clave de tipo texto del archivo de configuración de usuario en LocalApplicationData.
        /// Devuelve <paramref name="defaultValue"/> si la clave no existe o está vacía.
        /// </summary>
        private static string LeerStringConfig(string clave, string defaultValue)
        {
            if (!File.Exists(rutaConfigUsuario))
            {
                return defaultValue;
            }

            foreach (string linea in File.ReadAllLines(rutaConfigUsuario))
            {
                string lineaTrimmed = linea.Trim();
                if (lineaTrimmed.StartsWith(clave + "=", StringComparison.OrdinalIgnoreCase))
                {
                    string valor = lineaTrimmed.Substring(clave.Length + 1).Trim();
                    if (!string.IsNullOrEmpty(valor))
                    {
                        return valor;
                    }
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Lee una clave entera del archivo de configuración de usuario;
        /// devuelve <paramref name="defaultValue"/> si la clave no existe o no es un entero positivo.
        /// </summary>
        private static int LeerLongitudConfig(string clave, int defaultValue)
        {
            string val = LeerStringConfig(clave, null);
            if (val != null && int.TryParse(val, out int resultado) && resultado > 0)
            {
                return resultado;
            }
            return defaultValue;
        }

        /// <summary>
        /// Guarda una clave de tipo texto en el archivo de configuración de usuario en LocalApplicationData.
        /// </summary>
        private static void GuardarStringConfig(string clave, string valor)
        {
            string directorio = Path.GetDirectoryName(rutaConfigUsuario);
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            Dictionary<string, string> config = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (File.Exists(rutaConfigUsuario))
            {
                foreach (string linea in File.ReadAllLines(rutaConfigUsuario))
                {
                    string lineaTrimmed = linea.Trim();
                    int separador = lineaTrimmed.IndexOf('=');
                    if (separador > 0)
                    {
                        string key = lineaTrimmed.Substring(0, separador).Trim();
                        string val = lineaTrimmed.Substring(separador + 1).Trim();
                        config[key] = val;
                    }
                }
            }

            config[clave] = valor ?? string.Empty;

            var lineas = new List<string>();
            foreach (var par in config)
            {
                lineas.Add(par.Key + "=" + par.Value);
            }
            File.WriteAllLines(rutaConfigUsuario, lineas);
        }

        /// <summary>
        /// Escribe (o actualiza) una clave entera en el archivo de configuración de usuario
        /// ubicado en LocalApplicationData.
        /// </summary>
        private static void GuardarLongitudConfig(string clave, int valor)
        {
            GuardarStringConfig(clave, valor.ToString());
        }

        private static string Truncar(string texto, int longitudMaxima)
        {
            if (string.IsNullOrEmpty(texto) || longitudMaxima <= 0 || texto.Length <= longitudMaxima)
            {
                return texto ?? string.Empty;
            }
            return texto.Substring(0, longitudMaxima);
        }

        // ─── Resto del formulario (sin cambios) ─────────────────────────────────────

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposGenerales())
            {
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    CancelarEdicionActiva();
                    btnImportar.Enabled = false;
                    Cursor previousCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;

                    try
                    {
                        DataTable dt = ImportarExcel(filePath);

                        dgvDatos.Rows.Clear();
                        int filasCargadas = 0;

                        foreach (DataRow row in dt.Rows)
                        {
                            if (string.IsNullOrWhiteSpace(row["Marca"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["Modelo"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["Precio"]?.ToString()) ||
                                string.IsNullOrWhiteSpace(row["Cantidad"]?.ToString()))
                            {
                                continue;
                            }

                            string marca = TextoMayusculas(row["Marca"]?.ToString());
                            string modelo = TextoMayusculas(row["Modelo"]?.ToString());
                            string sku = TextoMayusculas(row["Sku"]?.ToString());
                            int indiceFila = dgvDatos.Rows.Add(marca, modelo, sku, row["Precio"], row["Cantidad"]);
                            EstablecerModoNormalFila(indiceFila);
                            filasCargadas++;
                        }

                        if (filasCargadas > 0)
                        {
                            MessageBox.Show($"Importación completada correctamente. Registros cargados: {filasCargadas}.", "Importación finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("El archivo no contiene registros válidos.", "Importación sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"No fue posible importar el archivo: {ex.Message}", "Error de importación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor.Current = previousCursor;
                        btnImportar.Enabled = true;
                    }
                }
            }
        }

        private void btnImportarPdf_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposGenerales())
            {
                return;
            }

            CancelarEdicionActiva();

            using (var frm = new FrmImportarPdf())
            {
                if (frm.ShowDialog(this) == DialogResult.OK && frm.ItemsParaImprimir != null && frm.ItemsParaImprimir.Count > 0)
                {
                    int agregados = 0;
                    foreach (var item in frm.ItemsParaImprimir)
                    {
                        string marca = Truncar(TextoMayusculas(item.Marca), maxLongitudMarca);
                        string modelo = Truncar(TextoMayusculas(item.Modelo), maxLongitudModelo);
                        string sku = Truncar(TextoMayusculas(item.Sku), maxLongitudSku);
                        string precio = item.Precio.HasValue ? item.Precio.Value.ToString("0.00", CultureInfo.InvariantCulture) : "0.00";
                        int cantidad = item.Cantidad > 0 ? item.Cantidad : 1;

                        int indiceFila = dgvDatos.Rows.Add(marca, modelo, sku, precio, cantidad);
                        EstablecerModoNormalFila(indiceFila);
                        agregados++;
                    }

                    if (agregados > 0)
                    {
                        MessageBox.Show(this,
                            $"Se transfirieron {agregados} productos desde el PDF a la cola de impresión.",
                            "Transferencia completada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposGenerales())
            {
                return;
            }

            if (dgvDatos.Rows.Count == 0)
            {
                MessageBox.Show("No existe informacion cargada para generar etiquetas. Primero importe el archivo de excel.",
                       "Etiquetas no encontradas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar datos en el DataGridView
            if (!ValidarDatos())
            {
                MessageBox.Show("Corrige los datos antes de continuar.", "Validación fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!File.Exists(urlPlantilla))
                {
                    MessageBox.Show("La plantilla de impresión no se encontró.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string plantilla = File.ReadAllText(urlPlantilla);
                string empresa = LimpiarTextoZpl(txtEmpresa.Text);

                StringBuilder zplBuilder = new StringBuilder();
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (row.Cells["Marca"].Value != null)
                    {
                        // Aplicar longitudes configuradas (sin hardcoding)
                        string marca  = Truncar(row.Cells["Marca"].Value.ToString(), maxLongitudMarca);
                        string modelo = Truncar(row.Cells["Modelo"].Value?.ToString() ?? string.Empty, maxLongitudModelo);
                        string sku    = Truncar(LimpiarTextoZpl(row.Cells["Sku"].Value?.ToString()), maxLongitudSku);

                        // Formatear el precio como moneda
                        if (!TryParsePrecio(row.Cells["Precio"].Value?.ToString(), out decimal precioDecimal))
                        {
                            MessageBox.Show("Error al convertir el precio a un formato válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        string precio = precioDecimal.ToString("C2"); // Formato de moneda

                        int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);

                        for (int i = 0; i < cantidad; i++)
                        {
                            string etiquetaZPL = GenerarEtiquetaZpl(plantilla, marca, modelo, precio, empresa, sku);
                            zplBuilder.AppendLine(etiquetaZPL);
                        }
                    }
                }

                if (!Directory.Exists(pathSalida))
                {
                    Directory.CreateDirectory(pathSalida);
                }

                string rutaEtiquetas = Path.Combine(pathSalida, fileEtiquetas);
                if (File.Exists(rutaEtiquetas))
                {
                    File.Delete(rutaEtiquetas);
                }

                File.WriteAllText(rutaEtiquetas, zplBuilder.ToString());

                MessageBox.Show("Etiquetas generadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al generar las etiquetas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void EjecutarBAT()
        {
            try
            {
                string rutaEtiquetas = Path.Combine(pathSalida, fileEtiquetas);

                Process process = new Process();
                process.StartInfo.FileName = urlBAT;
                process.StartInfo.Arguments = "\"" + rutaEtiquetas + "\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                process.WaitForExit();

                MessageBox.Show("El comando de impresion se ha enviado a la Impresora");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar ejecutar el archivo .bat: {ex.Message}");
            }
        }

        private DataTable ImportarExcel(string rutaArchivo)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(rutaArchivo)))
            {
                var dt = new DataTable();

                dt.Columns.Add("Marca");
                dt.Columns.Add("Modelo");
                dt.Columns.Add("Sku");
                dt.Columns.Add("Precio");
                dt.Columns.Add("Cantidad");

                var ws = package.Workbook.Worksheets.FirstOrDefault();
                if (ws == null)
                {
                    return dt;
                }

                if (ws.Dimension == null)
                {
                    return dt;
                }

                Dictionary<string, int> mapaColumnas = ObtenerMapaColumnasExcel(ws, out bool usaEncabezados);
                int filaInicio = usaEncabezados ? 2 : 1;

                for (int rowNum = filaInicio; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    string marca = LeerCelda(ws, rowNum, mapaColumnas["Marca"]);
                    string modelo = LeerCelda(ws, rowNum, mapaColumnas["Modelo"]);
                    string sku = LeerCelda(ws, rowNum, mapaColumnas["Sku"]);
                    string precio = LeerCelda(ws, rowNum, mapaColumnas["Precio"]);
                    string cantidad = LeerCelda(ws, rowNum, mapaColumnas["Cantidad"]);

                    if (!string.IsNullOrWhiteSpace(precio) && TryParsePrecio(precio, out decimal precioDecimal))
                    {
                        precio = precioDecimal.ToString("0.00", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        precio = string.Empty;
                    }

                    if (string.IsNullOrWhiteSpace(marca) && string.IsNullOrWhiteSpace(modelo) &&
                        string.IsNullOrWhiteSpace(precio) && string.IsNullOrWhiteSpace(cantidad) &&
                        string.IsNullOrWhiteSpace(sku))
                    {
                        continue;
                    }

                    dt.Rows.Add(marca, modelo, sku, precio, cantidad);
                }
                return dt;
            }
        }

        private bool ValidarDatos()
        {
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                string marca = row.Cells["Marca"].Value?.ToString();
                string modelo = row.Cells["Modelo"].Value?.ToString();
                string precio = row.Cells["Precio"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo) || string.IsNullOrWhiteSpace(precio))
                {
                    row.Selected = true;
                    return false;
                }

                if (!TryParsePrecio(precio, out _))
                {
                    row.Selected = true;
                    return false;
                }

                if (row.Cells["Cantidad"].Value == null || !int.TryParse(row.Cells["Cantidad"].Value.ToString(), out int cantidad) || cantidad < 1)
                {
                    row.Selected = true;
                    return false;
                }
            }
            return true;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            CancelarEdicionActiva();
            dgvDatos.Rows.Clear();
        }

        private void btnImprimirEtiquetas_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaEtiquetas = Path.Combine(pathSalida, fileEtiquetas);
                if (!File.Exists(rutaEtiquetas))
                {
                    MessageBox.Show("No se encontraron etiquetas generadas. Por favor, genere las etiquetas antes de intentar imprimir.",
                        "Etiquetas no encontradas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(urlBAT))
                {
                    MessageBox.Show("No se encontró el puente para utilizar la impresora. Verifique que el archivo PrintEtiqueta.bat exista en la ruta especificada.",
                        "Archivo BAT no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                EjecutarBAT();

                File.Delete(rutaEtiquetas);

                CancelarEdicionActiva();
                dgvDatos.Rows.Clear();

                MessageBox.Show("Impresión completada y archivo de etiquetas eliminado correctamente.",
                    "Proceso finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar imprimir las etiquetas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rdoModo_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurarModo();
        }

        private void ConfigurarModo()
        {
            CancelarEdicionActiva();

            bool esModoExcel = rdoExcel.Checked;
            btnImportar.Enabled = esModoExcel;
            btnImportarPdf.Enabled = esModoExcel;
            grpManual.Enabled = !esModoExcel;

            if (!esModoExcel)
            {
                txtMarcaManual.Focus();
            }
        }

        private void btnAgregarFila_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposManual(out string marca, out string modelo, out string sku, out string precioTexto, out int cantidad))
            {
                return;
            }

            dgvDatos.Rows.Add(
                Truncar(TextoMayusculas(marca),  maxLongitudMarca),
                Truncar(TextoMayusculas(modelo), maxLongitudModelo),
                Truncar(TextoMayusculas(sku),    maxLongitudSku),
                precioTexto,
                cantidad);

            EstablecerModoNormalFila(dgvDatos.Rows.Count - 1);
            LimpiarCamposManual();
            txtMarcaManual.Focus();
        }

        private void btnEditarFila_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para editar.", "Edición manual", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCamposManual(out string marca, out string modelo, out string sku, out string precioTexto, out int cantidad))
            {
                return;
            }

            DataGridViewRow row = dgvDatos.SelectedRows[0];
            row.Cells["Marca"].Value    = Truncar(TextoMayusculas(marca),  maxLongitudMarca);
            row.Cells["Modelo"].Value   = Truncar(TextoMayusculas(modelo), maxLongitudModelo);
            row.Cells["Sku"].Value      = Truncar(TextoMayusculas(sku),    maxLongitudSku);
            row.Cells["Precio"].Value   = precioTexto;
            row.Cells["Cantidad"].Value = cantidad;
            LimpiarCamposManual();
        }

        private void btnEliminarFila_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para eliminar.", "Eliminación manual", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvDatos.Rows.Remove(dgvDatos.SelectedRows[0]);

            if (filaEnEdicion.HasValue && dgvDatos.Rows.Count <= filaEnEdicion.Value)
            {
                filaEnEdicion = null;
                valoresOriginalesFila.Clear();
            }

            LimpiarCamposManual();
        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            if (!grpManual.Enabled || dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }

            DataGridViewRow row = dgvDatos.SelectedRows[0];
            txtMarcaManual.Text = row.Cells["Marca"].Value?.ToString() ?? string.Empty;
            txtModeloManual.Text = row.Cells["Modelo"].Value?.ToString() ?? string.Empty;
            txtSkuManual.Text = row.Cells["Sku"].Value?.ToString() ?? string.Empty;
            txtPrecioManual.Text = row.Cells["Precio"].Value?.ToString() ?? string.Empty;
            txtCantidadManual.Text = row.Cells["Cantidad"].Value?.ToString() ?? string.Empty;
        }

        private bool ValidarCamposGenerales()
        {
            bool valido = true;
            errorProvider1.Clear();

            txtEmpresa.Text = TextoMayusculas(txtEmpresa.Text);
            if (string.IsNullOrWhiteSpace(txtEmpresa.Text))
            {
                errorProvider1.SetError(txtEmpresa, "Ingrese el nombre de la empresa.");
                txtEmpresa.BackColor = Color.FromArgb(254, 226, 226);
                valido = false;
            }
            else
            {
                txtEmpresa.BackColor = Color.White;
                try
                {
                    GuardarStringConfig("NombreEmpresa", txtEmpresa.Text.Trim());
                }
                catch { }
            }

            if (!valido)
            {
                MessageBox.Show("Complete los datos obligatorios de empresa.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return valido;
        }

        private bool ValidarCamposManual(out string marca, out string modelo, out string sku, out string precioTexto, out int cantidad)
        {
            marca = txtMarcaManual.Text.Trim();
            modelo = txtModeloManual.Text.Trim();
            sku = txtSkuManual.Text.Trim();
            precioTexto = txtPrecioManual.Text.Trim();
            cantidad = 0;

            errorProvider1.SetError(txtMarcaManual, string.Empty);
            errorProvider1.SetError(txtModeloManual, string.Empty);
            errorProvider1.SetError(txtSkuManual, string.Empty);
            errorProvider1.SetError(txtPrecioManual, string.Empty);
            errorProvider1.SetError(txtCantidadManual, string.Empty);

            txtMarcaManual.BackColor = Color.White;
            txtModeloManual.BackColor = Color.White;
            txtSkuManual.BackColor = Color.White;
            txtPrecioManual.BackColor = Color.White;
            txtCantidadManual.BackColor = Color.White;

            bool valido = true;

            if (string.IsNullOrWhiteSpace(marca))
            {
                errorProvider1.SetError(txtMarcaManual, "Ingrese la marca.");
                txtMarcaManual.BackColor = Color.FromArgb(254, 226, 226);
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(modelo))
            {
                errorProvider1.SetError(txtModeloManual, "Ingrese el modelo.");
                txtModeloManual.BackColor = Color.FromArgb(254, 226, 226);
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(sku))
            {
                errorProvider1.SetError(txtSkuManual, "Ingrese el SKU.");
                txtSkuManual.BackColor = Color.FromArgb(254, 226, 226);
                valido = false;
            }

            if (!TryParsePrecio(precioTexto, out decimal precio) || precio < 0)
            {
                errorProvider1.SetError(txtPrecioManual, "Ingrese un precio válido.");
                txtPrecioManual.BackColor = Color.FromArgb(254, 226, 226);
                valido = false;
            }
            else
            {
                precioTexto = precio.ToString("0.00", CultureInfo.InvariantCulture);
            }

            if (!int.TryParse(txtCantidadManual.Text.Trim(), out cantidad) || cantidad < 1)
            {
                errorProvider1.SetError(txtCantidadManual, "Ingrese una cantidad mayor que cero.");
                txtCantidadManual.BackColor = Color.FromArgb(254, 226, 226);
                valido = false;
            }

            if (!valido)
            {
                MessageBox.Show("Complete correctamente los datos manuales antes de continuar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return valido;
        }

        private void LimpiarCamposManual()
        {
            txtMarcaManual.Text = string.Empty;
            txtModeloManual.Text = string.Empty;
            txtSkuManual.Text = string.Empty;
            txtPrecioManual.Text = string.Empty;
            txtCantidadManual.Text = string.Empty;

            txtMarcaManual.BackColor = Color.White;
            txtModeloManual.BackColor = Color.White;
            txtSkuManual.BackColor = Color.White;
            txtPrecioManual.BackColor = Color.White;
            txtCantidadManual.BackColor = Color.White;
        }

        private string GenerarEtiquetaZpl(string plantillaBase, string marca, string modelo, string precio, string empresa, string sku)
        {
            string plantilla = plantillaBase;
            bool plantillaIncluyeSku = plantillaBase.Contains("{SKU}") || plantillaBase.Contains("{4}") || plantillaBase.Contains("SKU:");

            plantilla = plantilla.Replace("VISUALIZA+", empresa);
            plantilla = plantilla.Replace("{EMPRESA}", empresa);
            plantilla = plantilla.Replace("{SKU}", sku);

            if (!string.IsNullOrWhiteSpace(sku) && !plantillaIncluyeSku)
            {
                plantilla = InsertarLineaSku(plantilla, sku);
            }

            int indiceMayor = ObtenerIndiceMayorPlaceholder(plantilla);
            object[] argumentos =
            {
                marca,
                modelo,
                precio,
                empresa,
                sku
            };

            if (indiceMayor >= argumentos.Length)
            {
                throw new InvalidOperationException("La plantilla contiene más campos dinámicos de los soportados por la aplicación.");
            }

            if (indiceMayor >= 0)
            {
                object[] argsReales = argumentos.Take(indiceMayor + 1).ToArray();
                return string.Format(plantilla, argsReales);
            }

            return plantilla;
        }

        private void ConfigurarGridAcciones()
        {
            Marca.ReadOnly = true;
            Modelo.ReadOnly = true;
            Sku.ReadOnly = true;
            Precio.ReadOnly = true;
            Cantidad.ReadOnly = true;

            AccionPrincipal.ReadOnly = true;
            AccionSecundaria.ReadOnly = true;
            AccionPrincipal.SortMode = DataGridViewColumnSortMode.NotSortable;
            AccionSecundaria.SortMode = DataGridViewColumnSortMode.NotSortable;

            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (!row.IsNewRow)
                {
                    EstablecerModoNormalFila(row.Index);
                }
            }
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string nombreColumna = dgvDatos.Columns[e.ColumnIndex].Name;
            if (nombreColumna != ColAccionPrincipal && nombreColumna != ColAccionSecundaria)
            {
                return;
            }

            if (nombreColumna == ColAccionPrincipal)
            {
                if (filaEnEdicion.HasValue && filaEnEdicion.Value == e.RowIndex)
                {
                    ConfirmarEdicionFila(e.RowIndex);
                }
                else
                {
                    IniciarEdicionFila(e.RowIndex);
                }
                return;
            }

            if (filaEnEdicion.HasValue && filaEnEdicion.Value == e.RowIndex)
            {
                CancelarEdicionFila(e.RowIndex);
                return;
            }

            EliminarFilaConConfirmacion(e.RowIndex);
        }

        private void dgvDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string nombreColumna = dgvDatos.Columns[e.ColumnIndex].Name;
            bool filaActiva = filaEnEdicion.HasValue && filaEnEdicion.Value == e.RowIndex;

            if (filaActiva)
            {
                if (nombreColumna == ColAccionPrincipal)
                {
                    DataGridViewCell cell = dgvDatos.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Style.ForeColor = Color.White;
                    cell.Style.SelectionForeColor = Color.White;
                    cell.Style.BackColor = Color.FromArgb(22, 163, 74);
                    cell.Style.SelectionBackColor = Color.FromArgb(22, 163, 74);
                }
                else if (nombreColumna == ColAccionSecundaria)
                {
                    DataGridViewCell cell = dgvDatos.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Style.ForeColor = Color.White;
                    cell.Style.SelectionForeColor = Color.White;
                    cell.Style.BackColor = Color.FromArgb(100, 116, 139);
                    cell.Style.SelectionBackColor = Color.FromArgb(100, 116, 139);
                }
                else
                {
                    // High-contrast dark text on warm light amber background
                    e.CellStyle.BackColor = Color.FromArgb(254, 243, 199);
                    e.CellStyle.ForeColor = Color.FromArgb(15, 23, 42);
                    e.CellStyle.SelectionBackColor = Color.FromArgb(253, 230, 138);
                    e.CellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
                }
            }
            else
            {
                if (nombreColumna == ColAccionPrincipal)
                {
                    DataGridViewCell cell = dgvDatos.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Style.ForeColor = Color.White;
                    cell.Style.SelectionForeColor = Color.White;
                    cell.Style.BackColor = Color.FromArgb(2, 132, 199);
                    cell.Style.SelectionBackColor = Color.FromArgb(2, 132, 199);
                }
                else if (nombreColumna == ColAccionSecundaria)
                {
                    DataGridViewCell cell = dgvDatos.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Style.ForeColor = Color.White;
                    cell.Style.SelectionForeColor = Color.White;
                    cell.Style.BackColor = Color.FromArgb(220, 38, 38);
                    cell.Style.SelectionBackColor = Color.FromArgb(220, 38, 38);
                }
            }
        }

        private void IniciarEdicionFila(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvDatos.Rows.Count)
            {
                return;
            }

            if (filaEnEdicion.HasValue && filaEnEdicion.Value != rowIndex)
            {
                CancelarEdicionFila(filaEnEdicion.Value);
            }

            if (filaEnEdicion.HasValue && filaEnEdicion.Value == rowIndex)
            {
                return;
            }

            GuardarValoresOriginales(rowIndex);
            filaEnEdicion = rowIndex;

            DataGridViewRow row = dgvDatos.Rows[rowIndex];
            row.Cells["Marca"].ReadOnly = false;
            row.Cells["Modelo"].ReadOnly = false;
            row.Cells["Sku"].ReadOnly = false;
            row.Cells["Precio"].ReadOnly = false;
            row.Cells["Cantidad"].ReadOnly = false;

            row.DefaultCellStyle.BackColor = Color.FromArgb(254, 243, 199);
            row.DefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42);
            row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(253, 230, 138);
            row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);

            row.Cells[ColAccionPrincipal].Value = "✓ Confirmar";
            row.Cells[ColAccionSecundaria].Value = "✕ Cancelar";

            dgvDatos.CurrentCell = row.Cells["Marca"];
            dgvDatos.BeginEdit(true);
            dgvDatos.InvalidateRow(rowIndex);
        }

        private void ConfirmarEdicionFila(int rowIndex)
        {
            if (!filaEnEdicion.HasValue || filaEnEdicion.Value != rowIndex)
            {
                return;
            }

            dgvDatos.EndEdit();

            if (!ValidarFilaEditada(rowIndex, out string mensaje))
            {
                MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SalirModoEdicionFila(rowIndex, restaurarValores: false);
        }

        private void CancelarEdicionFila(int rowIndex)
        {
            if (!filaEnEdicion.HasValue || filaEnEdicion.Value != rowIndex)
            {
                return;
            }

            dgvDatos.CancelEdit();
            SalirModoEdicionFila(rowIndex, restaurarValores: true);
        }

        private void CancelarEdicionActiva()
        {
            if (filaEnEdicion.HasValue)
            {
                CancelarEdicionFila(filaEnEdicion.Value);
            }
        }

        private void SalirModoEdicionFila(int rowIndex, bool restaurarValores)
        {
            if (rowIndex < 0 || rowIndex >= dgvDatos.Rows.Count)
            {
                filaEnEdicion = null;
                valoresOriginalesFila.Clear();
                return;
            }

            int primeraFilaVisible = ObtenerPrimeraFilaVisible();

            DataGridViewRow row = dgvDatos.Rows[rowIndex];
            if (restaurarValores)
            {
                RestaurarValoresOriginales(row);
            }

            row.Cells["Marca"].ReadOnly = true;
            row.Cells["Modelo"].ReadOnly = true;
            row.Cells["Sku"].ReadOnly = true;
            row.Cells["Precio"].ReadOnly = true;
            row.Cells["Cantidad"].ReadOnly = true;

            row.DefaultCellStyle.BackColor = Color.Empty;
            row.DefaultCellStyle.ForeColor = Color.Empty;
            row.DefaultCellStyle.SelectionBackColor = Color.Empty;
            row.DefaultCellStyle.SelectionForeColor = Color.Empty;

            row.Cells[ColAccionPrincipal].Value = "✏️ Editar";
            row.Cells[ColAccionSecundaria].Value = "🗑️ Eliminar";

            filaEnEdicion = null;
            valoresOriginalesFila.Clear();

            RestaurarPrimeraFilaVisible(primeraFilaVisible);
            dgvDatos.InvalidateRow(rowIndex);
        }

        private void EliminarFilaConConfirmacion(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvDatos.Rows.Count)
            {
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea eliminar este registro? Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            int primeraFilaVisible = ObtenerPrimeraFilaVisible();
            if (filaEnEdicion.HasValue && filaEnEdicion.Value == rowIndex)
            {
                filaEnEdicion = null;
                valoresOriginalesFila.Clear();
            }

            dgvDatos.Rows.RemoveAt(rowIndex);

            if (filaEnEdicion.HasValue && filaEnEdicion.Value > rowIndex)
            {
                filaEnEdicion--;
            }

            RestaurarPrimeraFilaVisible(primeraFilaVisible);
        }

        private void GuardarValoresOriginales(int rowIndex)
        {
            DataGridViewRow row = dgvDatos.Rows[rowIndex];
            valoresOriginalesFila = new Dictionary<string, object>
            {
                ["Marca"] = row.Cells["Marca"].Value,
                ["Modelo"] = row.Cells["Modelo"].Value,
                ["Sku"] = row.Cells["Sku"].Value,
                ["Precio"] = row.Cells["Precio"].Value,
                ["Cantidad"] = row.Cells["Cantidad"].Value
            };
        }

        private void RestaurarValoresOriginales(DataGridViewRow row)
        {
            if (valoresOriginalesFila.Count == 0)
            {
                return;
            }

            row.Cells["Marca"].Value = valoresOriginalesFila["Marca"];
            row.Cells["Modelo"].Value = valoresOriginalesFila["Modelo"];
            row.Cells["Sku"].Value = valoresOriginalesFila["Sku"];
            row.Cells["Precio"].Value = valoresOriginalesFila["Precio"];
            row.Cells["Cantidad"].Value = valoresOriginalesFila["Cantidad"];
        }

        private bool ValidarFilaEditada(int rowIndex, out string mensaje)
        {
            mensaje = string.Empty;
            DataGridViewRow row = dgvDatos.Rows[rowIndex];

            string marca    = row.Cells["Marca"].Value?.ToString();
            string modelo   = row.Cells["Modelo"].Value?.ToString();
            string sku      = row.Cells["Sku"].Value?.ToString();
            string precio   = row.Cells["Precio"].Value?.ToString();
            string cantidad = row.Cells["Cantidad"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(modelo) ||
                string.IsNullOrWhiteSpace(precio) || string.IsNullOrWhiteSpace(cantidad) ||
                string.IsNullOrWhiteSpace(sku))
            {
                mensaje = "Todos los campos de la fila son obligatorios para guardar los cambios.";
                return false;
            }

            if (!TryParsePrecio(precio, out decimal precioDecimal) || precioDecimal < 0)
            {
                mensaje = "El precio de la fila debe ser un número válido mayor o igual a cero.";
                return false;
            }

            if (!int.TryParse(cantidad, out int cantidadInt) || cantidadInt < 1)
            {
                mensaje = "La cantidad de la fila debe ser un entero mayor que cero.";
                return false;
            }

            row.Cells["Marca"].Value    = Truncar(TextoMayusculas(marca),  maxLongitudMarca);
            row.Cells["Modelo"].Value   = Truncar(TextoMayusculas(modelo), maxLongitudModelo);
            row.Cells["Sku"].Value      = Truncar(TextoMayusculas(sku),    maxLongitudSku);
            row.Cells["Precio"].Value   = precioDecimal.ToString("0.00", CultureInfo.InvariantCulture);
            row.Cells["Cantidad"].Value = cantidadInt;

            return true;
        }

        private int ObtenerPrimeraFilaVisible()
        {
            return dgvDatos.FirstDisplayedScrollingRowIndex >= 0 ? dgvDatos.FirstDisplayedScrollingRowIndex : 0;
        }

        private void RestaurarPrimeraFilaVisible(int indice)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                return;
            }

            int indiceAjustado = Math.Max(0, Math.Min(indice, dgvDatos.Rows.Count - 1));
            dgvDatos.FirstDisplayedScrollingRowIndex = indiceAjustado;
        }

        private void EstablecerModoNormalFila(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvDatos.Rows.Count)
            {
                return;
            }

            DataGridViewRow row = dgvDatos.Rows[rowIndex];
            row.Cells["Marca"].ReadOnly = true;
            row.Cells["Modelo"].ReadOnly = true;
            row.Cells["Sku"].ReadOnly = true;
            row.Cells["Precio"].ReadOnly = true;
            row.Cells["Cantidad"].ReadOnly = true;
            row.Cells[ColAccionPrincipal].Value = "✏️ Editar";
            row.Cells[ColAccionSecundaria].Value = "🗑️ Eliminar";
            row.DefaultCellStyle.BackColor = Color.Empty;
            row.DefaultCellStyle.ForeColor = Color.Empty;
            row.DefaultCellStyle.SelectionBackColor = Color.Empty;
            row.DefaultCellStyle.SelectionForeColor = Color.Empty;
        }

        private static int ObtenerIndiceMayorPlaceholder(string plantilla)
        {
            var matches = Regex.Matches(plantilla, @"\{(\d+)\}");
            int mayor = -1;

            foreach (Match match in matches)
            {
                if (int.TryParse(match.Groups[1].Value, out int indice) && indice > mayor)
                {
                    mayor = indice;
                }
            }

            return mayor;
        }

        private static string InsertarLineaSku(string plantilla, string sku)
        {
            string lineaSku = $"^FO170,235^A0N,18,18^FDSKU: {sku}^FS";

            int indicePq = plantilla.IndexOf("^PQ1", StringComparison.OrdinalIgnoreCase);
            if (indicePq >= 0)
            {
                return plantilla.Insert(indicePq, lineaSku + Environment.NewLine);
            }

            int indiceXz = plantilla.IndexOf("^XZ", StringComparison.OrdinalIgnoreCase);
            if (indiceXz >= 0)
            {
                return plantilla.Insert(indiceXz, lineaSku + Environment.NewLine);
            }

            return plantilla + Environment.NewLine + lineaSku;
        }

        private static string LimpiarTextoZpl(string valor)
        {
            return (valor ?? string.Empty).Replace("\r", " ").Replace("\n", " ").Trim();
        }

        private static string TextoMayusculas(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? string.Empty : valor.Trim().ToUpperInvariant();
        }

        private static bool TryParsePrecio(string valor, out decimal precio)
        {
            precio = 0m;
            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            string normalizado = valor.Trim().Replace(',', '.');
            return decimal.TryParse(normalizado, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out precio);
        }

        private void TxtPrecioManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.')
            {
                if (textBox != null && textBox.Text.Contains("."))
                {
                    e.Handled = true;
                }
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtCantidadManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvDatos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!(e.Control is TextBox textBox))
            {
                return;
            }

            textBox.KeyPress -= TxtPrecioManual_KeyPress;
            textBox.KeyPress -= TxtCantidadManual_KeyPress;

            string nombreColumna = dgvDatos.CurrentCell?.OwningColumn?.Name;
            if (string.Equals(nombreColumna, "Precio", StringComparison.OrdinalIgnoreCase))
            {
                textBox.KeyPress += TxtPrecioManual_KeyPress;
            }
            else if (string.Equals(nombreColumna, "Cantidad", StringComparison.OrdinalIgnoreCase))
            {
                textBox.KeyPress += TxtCantidadManual_KeyPress;
            }
        }

        private Dictionary<string, int> ObtenerMapaColumnasExcel(ExcelWorksheet ws, out bool usaEncabezados)
        {
            Dictionary<string, int> mapa = new Dictionary<string, int>();
            int maxCol = ws.Dimension.End.Column;
            int coincidencias = 0;

            for (int col = 1; col <= maxCol; col++)
            {
                string encabezado = NormalizarEncabezado(ws.Cells[1, col].Text);
                if (string.IsNullOrWhiteSpace(encabezado))
                {
                    continue;
                }

                if (!mapa.ContainsKey("Marca") && (encabezado == "marca" || encabezado == "producto"))
                {
                    mapa["Marca"] = col;
                    coincidencias++;
                }
                else if (!mapa.ContainsKey("Modelo") && (encabezado == "modelo" || encabezado == "descripcion" || encabezado == "descripción"))
                {
                    mapa["Modelo"] = col;
                    coincidencias++;
                }
                else if (!mapa.ContainsKey("Sku") && (encabezado == "sku" || encabezado == "codigo" || encabezado == "código" || encabezado == "codigosku" || encabezado == "codsku"))
                {
                    mapa["Sku"] = col;
                    coincidencias++;
                }
                else if (!mapa.ContainsKey("Precio") && (encabezado == "precio" || encabezado == "precioventa" || encabezado == "preciopublico" || encabezado == "precio público"))
                {
                    mapa["Precio"] = col;
                    coincidencias++;
                }
                else if (!mapa.ContainsKey("Cantidad") && (encabezado == "cantidad" || encabezado == "cant" || encabezado == "unidades"))
                {
                    mapa["Cantidad"] = col;
                    coincidencias++;
                }
            }

            usaEncabezados = coincidencias >= 2;

            if (!mapa.ContainsKey("Marca")) mapa["Marca"] = 1;
            if (!mapa.ContainsKey("Modelo")) mapa["Modelo"] = 2;
            if (!mapa.ContainsKey("Sku")) mapa["Sku"] = 3;
            if (!mapa.ContainsKey("Precio")) mapa["Precio"] = 4;
            if (!mapa.ContainsKey("Cantidad")) mapa["Cantidad"] = 5;

            return mapa;
        }

        private static string LeerCelda(ExcelWorksheet ws, int fila, int columna)
        {
            if (columna <= 0 || columna > ws.Dimension.End.Column)
            {
                return string.Empty;
            }

            var celda = ws.Cells[fila, columna];
            if (celda.Value == null)
            {
                return string.Empty;
            }

            if (celda.Value is DateTime fecha)
            {
                return fecha.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
            }

            return celda.Text?.Trim() ?? string.Empty;
        }

        private static string NormalizarEncabezado(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return string.Empty;
            }

            string normalizado = texto.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder(normalizado.Length);
            foreach (char c in normalizado)
            {
                UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(c);
                if (categoria != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            normalizado = sb.ToString().Normalize(NormalizationForm.FormC);
            normalizado = normalizado.Replace("_", string.Empty)
                                     .Replace("-", string.Empty)
                                     .Replace(" ", string.Empty);
            return normalizado;
        }

        #region Menú de Ayuda

        private void menuGuiaImpresora_Click(object sender, EventArgs e)
        {
            using (var dlg = new FrmAyudaImpresora(urlBAT))
            {
                dlg.ShowDialog(this);
            }
        }

        private void menuAbrirImpresoras_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("control", "printers");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"No se pudo abrir el panel de impresoras: {ex.Message}\n\nPuede abrirlo desde Panel de Control > Dispositivos e Impresoras.",
                    "Dispositivos e Impresoras",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void menuProbarConexion_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                bool ok = FrmAyudaImpresora.ProbarConexionZDesigner(out string mensaje);
                if (ok)
                {
                    MessageBox.Show(this, mensaje, "Diagnóstico de Impresora", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, mensaje, "Diagnóstico de Impresora", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error al verificar la conexión: {ex.Message}", "Diagnóstico de Impresora", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void menuAcercaDe_Click(object sender, EventArgs e)
        {
            string version = "v" + (Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "2.1.0.0");
            MessageBox.Show(this,
                $"Sistema de Impresión de Etiquetas {version}\n" +
                "Desarrollado por: Javier Orona\n\n" +
                "• Soporta importación masiva desde Excel (.xlsx)\n" +
                "• Soporta lectura automática de Catálogos / Facturas PDF\n" +
                "• Salida directa a impresoras Zebra (ZPL)\n" +
                "• Canal de impresión por lotes: \\\\localhost\\ZDesigner",
                "Acerca del Sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

    }
}

