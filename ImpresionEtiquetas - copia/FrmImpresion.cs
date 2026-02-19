using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImpresionEtiquetas
{
    public partial class frmImprimir : Form
    {
        private string urlPlantilla = @"C:\Etiquetadora\Plantilla.txt";
        private string fileEtiquetas = "Etiquetas.txt";
        private string urlBAT = @"C:\Etiquetadora\PrintEtiqueta.bat";
        private string pathRoot = @"C:\Etiquetadora\";
        public frmImprimir()
        {
            InitializeComponent();
        }
        private void frmImprimir_Load(object sender, EventArgs e)
        {
            dgvDatos.AllowUserToAddRows = false;
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos Excel (*.xlsx)|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    DataTable dt = ImportarExcel(filePath);

                    // Llenar el DataGridView con columnas específicas
                    DataGridView dgvDatos = (DataGridView)Controls["dgvDatos"];
                    dgvDatos.Rows.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        // Validar que ninguna columna requerida esté vacía o nula
                        if (string.IsNullOrWhiteSpace(row["Marca"]?.ToString()) ||
                            string.IsNullOrWhiteSpace(row["Modelo"]?.ToString()) ||
                            string.IsNullOrWhiteSpace(row["Precio"]?.ToString()) ||
                            string.IsNullOrWhiteSpace(row["Cantidad"]?.ToString()))
                        {
                            // Saltar al siguiente renglón si hay valores vacíos
                            continue;
                        }

                        dgvDatos.Rows.Add(row["Marca"], row["Modelo"], row["Precio"], row["Cantidad"]);
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

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
                // Leer la plantilla ZPL
                if (!File.Exists(urlPlantilla))
                {
                    MessageBox.Show("La plantilla de impresión no se encontró.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string plantilla = File.ReadAllText(urlPlantilla);

                // Crear el contenido ZPL para las etiquetas
                StringBuilder zplBuilder = new StringBuilder();
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    if (row.Cells["Marca"].Value != null)
                    {
                        string marca = row.Cells["Marca"].Value.ToString();
                        if (marca.Length > 16)
                        {
                            marca = marca.Substring(0, 16);
                        }

                        string modelo = row.Cells["Modelo"].Value?.ToString() ?? string.Empty;
                        if (modelo.Length > 12)
                        {
                            modelo = modelo.Substring(0, 12);
                        }
                        // Formatear el precio como moneda
                        if (!decimal.TryParse(row.Cells["Precio"].Value?.ToString(), out decimal precioDecimal))
                        {
                            MessageBox.Show("Error al convertir el precio a un formato válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        string precio = precioDecimal.ToString("C2"); // Formato de moneda

                        int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);

                        for (int i = 0; i < cantidad; i++)
                        {
                            string etiquetaZPL = string.Format(plantilla, marca, modelo, precio);
                            zplBuilder.AppendLine(etiquetaZPL);
                        }
                    }
                }

                // Verificar si el directorio raíz existe
                if (!Directory.Exists(pathRoot))
                {
                    Directory.CreateDirectory(pathRoot);
                }

                // Verificar si el archivo de etiquetas existe y eliminarlo si es necesario
                string rutaEtiquetas = Path.Combine(pathRoot, fileEtiquetas);
                if (File.Exists(rutaEtiquetas))
                {
                    File.Delete(rutaEtiquetas);
                }

                // Escribir el archivo final con las etiquetas
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
                // Crear un nuevo proceso para ejecutar el archivo .bat
                Process process = new Process();
                process.StartInfo.FileName = urlBAT;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                // Iniciar el proceso
                process.Start();

                // Opcional: esperar a que el proceso termine
                process.WaitForExit();

                MessageBox.Show("El comando de impresion se ha enviado a la Impresora");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar ejecutar el archivo .bat: {ex.Message}");
            }
        }

        // Método ajustado para importar Excel
        private DataTable ImportarExcel(string rutaArchivo)
        {
            // Configurar la licencia
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(rutaArchivo)))
            {
                var ws = package.Workbook.Worksheets[0];
                var dt = new DataTable();

                // Leer encabezados
                dt.Columns.Add("Marca");
                dt.Columns.Add("Modelo");
                dt.Columns.Add("Precio");
                dt.Columns.Add("Cantidad");

                // Leer filas
                for (int rowNum = 2; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var marca = ws.Cells[rowNum, 1].Text;
                    var modelo = ws.Cells[rowNum, 2].Text;
                    var precio = ws.Cells[rowNum, 3].Text;
                    var cantidad = ws.Cells[rowNum, 4].Text;

                    dt.Rows.Add(marca, modelo, precio, cantidad);
                }
                return dt;
            }
        }

        // Método para validar datos
        private bool ValidarDatos()
        {
            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                if (row.Cells["Cantidad"].Value == null || !int.TryParse(row.Cells["Cantidad"].Value.ToString(), out int cantidad) || cantidad < 1)
                {
                    // Seleccionar la fila incorrecta y retornar false
                    row.Selected = true;
                    return false;
                }
            }
            return true;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();
        }

        private void btnImprimirEtiquetas_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar si existe el archivo de etiquetas
                string rutaEtiquetas = Path.Combine(pathRoot, fileEtiquetas);
                if (!File.Exists(rutaEtiquetas))
                {
                    MessageBox.Show("No se encontraron etiquetas generadas. Por favor, genere las etiquetas antes de intentar imprimir.",
                        "Etiquetas no encontradas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar si existe el archivo BAT
                if (!File.Exists(urlBAT))
                {
                    MessageBox.Show("No se encontró el puente para utilizar la impresora. Verifique que el archivo PrintEtiqueta.bat exista en la ruta especificada.",
                        "Archivo BAT no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ejecutar el BAT
                EjecutarBAT();

                // Eliminar el archivo de etiquetas después de imprimir
                File.Delete(rutaEtiquetas);

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

    }
}
