using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ImpresionEtiquetas
{
    /// <summary>
    /// Verifica si existe una nueva versión en GitHub Releases y ofrece
    /// descargar e instalar la actualización de forma automática.
    /// </summary>
    public static class UpdateChecker
    {
        // ?? Ajusta esta URL con tu usuario/repo real de GitHub ??????????????
        private const string GitHubApiUrl =
            "https://api.github.com/repos/javoro/ImpresionEtiquetas/releases/latest";

        /// <summary>
        /// Consulta la última release de GitHub y, si la versión es mayor a la
        /// actual, ofrece al usuario descargar e instalar la actualización.
        /// Este método está pensado para ejecutarse en un hilo secundario; las
        /// interacciones con la UI se realizan mediante <see cref="Control.Invoke"/>.
        /// </summary>
        public static void CheckForUpdates()
        {
            try
            {
                // TLS 1.2 es necesario para conectar con la API de GitHub
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                var request = (HttpWebRequest)WebRequest.Create(GitHubApiUrl);
                request.UserAgent = "ImpresionEtiquetas-UpdateChecker";
                request.Timeout = 10000; // 10 segundos

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();

                    var serializer = new JavaScriptSerializer();
                    var release = serializer.Deserialize<Dictionary<string, object>>(json);

                    if (release == null || !release.ContainsKey("tag_name"))
                    {
                        return;
                    }

                    string tagName = release["tag_name"]?.ToString(); // Ej: "v2.1.0.0"
                    if (string.IsNullOrEmpty(tagName))
                    {
                        return;
                    }

                    string remoteVersionText = tagName.TrimStart('v', 'V');
                    if (!Version.TryParse(remoteVersionText, out Version latestVersion))
                    {
                        return;
                    }

                    Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

                    if (latestVersion <= currentVersion)
                    {
                        return; // Ya está actualizado
                    }

                    // Mostrar el diálogo en el hilo de la UI
                    DialogResult result = DialogResult.No;
                    if (Application.OpenForms.Count > 0)
                    {
                        Form mainForm = Application.OpenForms[0];
                        mainForm.Invoke((MethodInvoker)delegate
                        {
                            result = MessageBox.Show(
                                mainForm,
                                $"Hay una nueva versión disponible: {latestVersion}\n" +
                                $"Versión actual: {currentVersion}\n\n" +
                                "¿Desea descargar e instalar la actualización ahora?",
                                "Actualización disponible",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information);
                        });
                    }
                    else
                    {
                        result = MessageBox.Show(
                            $"Hay una nueva versión disponible: {latestVersion}\n" +
                            $"Versión actual: {currentVersion}\n\n" +
                            "¿Desea descargar e instalar la actualización ahora?",
                            "Actualización disponible",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);
                    }

                    if (result == DialogResult.Yes)
                    {
                        DownloadAndInstall(release);
                    }
                }
            }
            catch (Exception ex)
            {
                // No interrumpir la aplicación si falla la verificación
                Debug.WriteLine($"[UpdateChecker] Error al verificar actualizaciones: {ex.Message}");
            }
        }

        /// <summary>
        /// Descarga el instalador (.exe) adjunto a la release y lo ejecuta
        /// en modo silencioso. Al finalizar, cierra la aplicación actual.
        /// </summary>
        private static void DownloadAndInstall(Dictionary<string, object> release)
        {
            try
            {
                string downloadUrl = FindInstallerUrl(release);

                if (string.IsNullOrEmpty(downloadUrl))
                {
                    ShowMessageOnUI(
                        "No se encontró el instalador en la última release de GitHub.\n" +
                        "Visite https://github.com/javoro/ImpresionEtiquetas/releases para descargarlo manualmente.",
                        "Actualización",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                string tempPath = Path.Combine(Path.GetTempPath(), "SetupEtiquetas_update.exe");

                // TLS 1.2 es necesario para descargar desde GitHub
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

                using (var client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "ImpresionEtiquetas-UpdateChecker");
                    client.DownloadFile(downloadUrl, tempPath);
                }

                // Ejecutar el instalador en modo silencioso y cerrar la app
                Process.Start(new ProcessStartInfo
                {
                    FileName = tempPath,
                    Arguments = "/SILENT",
                    UseShellExecute = true
                });

                // Cerrar la aplicación para que el instalador pueda reemplazar los archivos
                if (Application.OpenForms.Count > 0)
                {
                    Application.OpenForms[0].Invoke((MethodInvoker)delegate
                    {
                        Application.Exit();
                    });
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                ShowMessageOnUI(
                    $"Error al descargar la actualización:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Busca en los assets de la release la URL de descarga del primer
        /// archivo que termine en ".exe".
        /// </summary>
        private static string FindInstallerUrl(Dictionary<string, object> release)
        {
            if (!release.ContainsKey("assets"))
            {
                return null;
            }

            if (!(release["assets"] is System.Collections.ArrayList assets))
            {
                return null;
            }

            foreach (var item in assets)
            {
                if (item is Dictionary<string, object> asset)
                {
                    string name = asset.ContainsKey("name") ? asset["name"]?.ToString() : null;
                    if (!string.IsNullOrEmpty(name) &&
                        name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                    {
                        return asset.ContainsKey("browser_download_url")
                            ? asset["browser_download_url"]?.ToString()
                            : null;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Muestra un MessageBox en el hilo de la UI de forma segura.
        /// </summary>
        private static void ShowMessageOnUI(string text, string caption,
            MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (Application.OpenForms.Count > 0)
            {
                Form mainForm = Application.OpenForms[0];
                mainForm.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(mainForm, text, caption, buttons, icon);
                });
            }
            else
            {
                MessageBox.Show(text, caption, buttons, icon);
            }
        }
    }
}
