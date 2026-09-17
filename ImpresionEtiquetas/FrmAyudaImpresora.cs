using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ImpresionEtiquetas
{
    public partial class FrmAyudaImpresora : Form
    {
        private readonly string _urlBAT;

        public FrmAyudaImpresora(string urlBAT = null)
        {
            InitializeComponent();
            _urlBAT = urlBAT;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAbrirImpresoras_Click(object sender, EventArgs e)
        {
            try
            {
                // Abrir directamente la ventana de Dispositivos e Impresoras de Windows
                Process.Start("control", "printers");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"No se pudo abrir el panel de impresoras automáticamente: {ex.Message}\n\nPuede abrirlo manualmente ingresando a: Panel de Control > Dispositivos e Impresoras.",
                    "Apertura manual requerida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            btnProbarConexion.Enabled = false;

            try
            {
                bool resultado = ProbarConexionZDesigner(out string mensaje);

                if (resultado)
                {
                    MessageBox.Show(this, mensaje, "Diagnóstico Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, mensaje, "Advertencia de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error al ejecutar el diagnóstico: {ex.Message}", "Error de Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnProbarConexion.Enabled = true;
            }
        }

        private void btnImprimirPrueba_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            btnImprimirPrueba.Enabled = false;

            try
            {
                string batPath = _urlBAT;
                if (string.IsNullOrEmpty(batPath) || !File.Exists(batPath))
                {
                    string rutaLocal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PrintEtiqueta.bat");
                    if (File.Exists(rutaLocal))
                    {
                        batPath = rutaLocal;
                    }
                }

                if (string.IsNullOrEmpty(batPath) || !File.Exists(batPath))
                {
                    MessageBox.Show(this,
                        "No se encontró el archivo 'PrintEtiqueta.bat'. Verifique que el archivo exista en la carpeta del programa.",
                        "Archivo no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                bool enviado = EnviarEtiquetaPrueba(batPath, out string mensaje);

                if (enviado)
                {
                    MessageBox.Show(this, mensaje, "Prueba Enviada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, mensaje, "Error al Enviar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                btnImprimirPrueba.Enabled = true;
            }
        }

        public static bool ProbarConexionZDesigner(out string mensaje)
        {
            bool zebraInstalada = false;
            string nombreZebra = "";

            try
            {
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    if (printer.IndexOf("ZDesigner", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        printer.IndexOf("Zebra", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        zebraInstalada = true;
                        nombreZebra = printer;
                        break;
                    }
                }
            }
            catch { }

            try
            {
                // Consultar recursos compartidos en localhost vía cmd
                var psi = new ProcessStartInfo("cmd.exe", "/c net view \\\\localhost")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var proc = Process.Start(psi))
                {
                    string output = proc.StandardOutput.ReadToEnd();
                    proc.WaitForExit(3500);

                    if (output.IndexOf("ZDesigner", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        mensaje = "✓ ¡CONEXIÓN EXITOSA!\n\n" +
                                  "El recurso compartido '\\\\localhost\\ZDesigner' está activo y listo en este equipo.\n\n" +
                                  "Las etiquetas generadas se enviarán correctamente a la impresora Zebra.";
                        return true;
                    }
                    else
                    {
                        string detalleZebra = zebraInstalada
                            ? $"Se detectó una impresora instalada: '{nombreZebra}', pero NO está compartida con el nombre exacto 'ZDesigner'."
                            : "No se detectó el recurso compartido 'ZDesigner' en Windows.";

                        mensaje = "⚠️ RECURSO NO DETECTADO:\n\n" +
                                  detalleZebra + "\n\n" +
                                  "Solución:\n" +
                                  "1. Abra 'Panel de Impresoras' con el botón inferior.\n" +
                                  "2. Clic derecho en su impresora Zebra > 'Propiedades de la impresora'.\n" +
                                  "3. Pestaña 'Uso compartido' > Marcar 'Compartir esta impresora'.\n" +
                                  "4. Escribir exactamente: ZDesigner en el nombre del recurso compartido.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"No se pudo consultar el estado de red de Windows: {ex.Message}";
                return false;
            }
        }

        public static bool EnviarEtiquetaPrueba(string rutaBat, out string mensaje)
        {
            try
            {
                string tempFile = Path.Combine(Path.GetTempPath(), "ZebraTest_" + Guid.NewGuid().ToString("N").Substring(0, 6) + ".txt");

                // Etiqueta ZPL estándar de prueba con código de barras y timestamp
                string zplPrueba = "^XA\n" +
                                   "^FO40,30^A0N,26,22^FDPRUEBA DE CONEXION^FS\n" +
                                   "^FO40,65^A0N,20,18^FDIMPRESION ETIQUETAS OK^FS\n" +
                                   "^FO40,95^A0N,18,16^FD" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "^FS\n" +
                                   "^FO40,120^BY2,2,35^BCN,35,Y,N,N^FDTEST12345^FS\n" +
                                   "^XZ\n";

                File.WriteAllText(tempFile, zplPrueba, Encoding.ASCII);

                var proc = new Process();
                proc.StartInfo.FileName = rutaBat;
                proc.StartInfo.Arguments = "\"" + tempFile + "\"";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.WaitForExit(4000);

                try { File.Delete(tempFile); } catch { }

                mensaje = "Se despachó el comando de prueba a través de 'PrintEtiqueta.bat'.\n\n" +
                          "Verifique si la impresora física expulsó la etiqueta de prueba correctamente.";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al despachar la etiqueta de prueba: {ex.Message}";
                return false;
            }
        }
    }
}
