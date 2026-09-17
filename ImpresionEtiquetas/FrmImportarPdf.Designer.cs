namespace ImpresionEtiquetas
{
    partial class FrmImportarPdf
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSuperior = new System.Windows.Forms.Panel();
            this.grpPasos = new System.Windows.Forms.GroupBox();
            this.lblArchivoPdf = new System.Windows.Forms.Label();
            this.btnSeleccionarPdf = new System.Windows.Forms.Button();
            this.lblEstadoCatalogo = new System.Windows.Forms.Label();
            this.btnCargarCatalogo = new System.Windows.Forms.Button();
            this.pnlResumen = new System.Windows.Forms.Panel();
            this.chkSeleccionarTodos = new System.Windows.Forms.CheckBox();
            this.lblResumen = new System.Windows.Forms.Label();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.btnTransferir = new System.Windows.Forms.Button();
            this.dgvPdf = new System.Windows.Forms.DataGridView();
            this.ColSeleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColSku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColModelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMarca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCantSugerida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEstadoCatalogo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSuperior.SuspendLayout();
            this.grpPasos.SuspendLayout();
            this.pnlResumen.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPdf)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSuperior
            // 
            this.pnlSuperior.Controls.Add(this.grpPasos);
            this.pnlSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSuperior.Location = new System.Drawing.Point(0, 0);
            this.pnlSuperior.Name = "pnlSuperior";
            this.pnlSuperior.Padding = new System.Windows.Forms.Padding(12);
            this.pnlSuperior.Size = new System.Drawing.Size(1084, 115);
            this.pnlSuperior.TabIndex = 0;
            // 
            // grpPasos
            // 
            this.grpPasos.Controls.Add(this.lblArchivoPdf);
            this.grpPasos.Controls.Add(this.btnSeleccionarPdf);
            this.grpPasos.Controls.Add(this.lblEstadoCatalogo);
            this.grpPasos.Controls.Add(this.btnCargarCatalogo);
            this.grpPasos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPasos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPasos.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.grpPasos.Location = new System.Drawing.Point(12, 12);
            this.grpPasos.Name = "grpPasos";
            this.grpPasos.Size = new System.Drawing.Size(1060, 91);
            this.grpPasos.TabIndex = 0;
            this.grpPasos.TabStop = false;
            this.grpPasos.Text = "Flujo de Importación y Enriquecimiento";
            // 
            // lblArchivoPdf
            // 
            this.lblArchivoPdf.AutoSize = true;
            this.lblArchivoPdf.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArchivoPdf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblArchivoPdf.Location = new System.Drawing.Point(530, 60);
            this.lblArchivoPdf.Name = "lblArchivoPdf";
            this.lblArchivoPdf.Size = new System.Drawing.Size(161, 15);
            this.lblArchivoPdf.TabIndex = 3;
            this.lblArchivoPdf.Text = "Ningún archivo seleccionado";
            // 
            // btnSeleccionarPdf
            // 
            this.btnSeleccionarPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(98)))), ((int)(((byte)(59)))));
            this.btnSeleccionarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarPdf.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionarPdf.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarPdf.Location = new System.Drawing.Point(530, 23);
            this.btnSeleccionarPdf.Name = "btnSeleccionarPdf";
            this.btnSeleccionarPdf.Size = new System.Drawing.Size(250, 32);
            this.btnSeleccionarPdf.TabIndex = 2;
            this.btnSeleccionarPdf.Text = "📄 2. Seleccionar Pedido PDF";
            this.btnSeleccionarPdf.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnSeleccionarPdf.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarPdf.UseVisualStyleBackColor = false;
            this.btnSeleccionarPdf.Click += new System.EventHandler(this.btnSeleccionarPdf_Click);
            // 
            // lblEstadoCatalogo
            // 
            this.lblEstadoCatalogo.AutoSize = true;
            this.lblEstadoCatalogo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoCatalogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblEstadoCatalogo.Location = new System.Drawing.Point(20, 60);
            this.lblEstadoCatalogo.Name = "lblEstadoCatalogo";
            this.lblEstadoCatalogo.Size = new System.Drawing.Size(126, 15);
            this.lblEstadoCatalogo.TabIndex = 1;
            this.lblEstadoCatalogo.Text = "Catálogo: No cargado";
            // 
            // btnCargarCatalogo
            // 
            this.btnCargarCatalogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(125)))), ((int)(((byte)(154)))));
            this.btnCargarCatalogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCargarCatalogo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarCatalogo.ForeColor = System.Drawing.Color.White;
            this.btnCargarCatalogo.Location = new System.Drawing.Point(20, 23);
            this.btnCargarCatalogo.Name = "btnCargarCatalogo";
            this.btnCargarCatalogo.Size = new System.Drawing.Size(260, 32);
            this.btnCargarCatalogo.TabIndex = 0;
            this.btnCargarCatalogo.Text = "📊 1. Cargar Catálogo (Excel)";
            this.btnCargarCatalogo.BackColor = System.Drawing.Color.FromArgb(2, 132, 199);
            this.btnCargarCatalogo.FlatAppearance.BorderSize = 0;
            this.btnCargarCatalogo.UseVisualStyleBackColor = false;
            this.btnCargarCatalogo.Click += new System.EventHandler(this.btnCargarCatalogo_Click);
            // 
            // pnlResumen
            // 
            this.pnlResumen.BackColor = System.Drawing.Color.FromArgb(240, 253, 244);
            this.pnlResumen.Controls.Add(this.chkSeleccionarTodos);
            this.pnlResumen.Controls.Add(this.lblResumen);
            this.pnlResumen.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlResumen.Location = new System.Drawing.Point(0, 465);
            this.pnlResumen.Name = "pnlResumen";
            this.pnlResumen.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);
            this.pnlResumen.Size = new System.Drawing.Size(1084, 38);
            this.pnlResumen.TabIndex = 3;
            // 
            // chkSeleccionarTodos
            // 
            this.chkSeleccionarTodos.AutoSize = true;
            this.chkSeleccionarTodos.Checked = true;
            this.chkSeleccionarTodos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSeleccionarTodos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSeleccionarTodos.Location = new System.Drawing.Point(16, 9);
            this.chkSeleccionarTodos.Name = "chkSeleccionarTodos";
            this.chkSeleccionarTodos.Size = new System.Drawing.Size(137, 20);
            this.chkSeleccionarTodos.TabIndex = 0;
            this.chkSeleccionarTodos.Text = "Seleccionar todos";
            this.chkSeleccionarTodos.UseVisualStyleBackColor = true;
            this.chkSeleccionarTodos.CheckedChanged += new System.EventHandler(this.chkSeleccionarTodos_CheckedChanged);
            // 
            // lblResumen
            // 
            this.lblResumen.AutoSize = true;
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumen.ForeColor = System.Drawing.Color.FromArgb(22, 101, 52);
            this.lblResumen.Location = new System.Drawing.Point(180, 10);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(265, 16);
            this.lblResumen.TabIndex = 1;
            this.lblResumen.Text = "0 productos | 0 seleccionados | 0 piezas";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.Controls.Add(this.btnExportarExcel);
            this.pnlAcciones.Controls.Add(this.btnTransferir);
            this.pnlAcciones.Controls.Add(this.btnCancelar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(0, 503);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Padding = new System.Windows.Forms.Padding(12);
            this.pnlAcciones.Size = new System.Drawing.Size(1084, 58);
            this.pnlAcciones.TabIndex = 1;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(972, 12);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 34);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Text = "✕ Cancelar";
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportarExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(125)))), ((int)(((byte)(154)))));
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportarExcel.Location = new System.Drawing.Point(582, 12);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(170, 34);
            this.btnExportarExcel.TabIndex = 1;
            this.btnExportarExcel.Text = "📊 Exportar a Excel";
            this.btnExportarExcel.BackColor = System.Drawing.Color.FromArgb(15, 118, 110);
            this.btnExportarExcel.FlatAppearance.BorderSize = 0;
            this.btnExportarExcel.UseVisualStyleBackColor = false;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnTransferir
            // 
            this.btnTransferir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransferir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(98)))), ((int)(((byte)(59)))));
            this.btnTransferir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferir.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransferir.ForeColor = System.Drawing.Color.White;
            this.btnTransferir.Location = new System.Drawing.Point(762, 12);
            this.btnTransferir.Name = "btnTransferir";
            this.btnTransferir.Size = new System.Drawing.Size(200, 34);
            this.btnTransferir.TabIndex = 0;
            this.btnTransferir.Text = "📥 Transferir a Impresión";
            this.btnTransferir.BackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnTransferir.FlatAppearance.BorderSize = 0;
            this.btnTransferir.UseVisualStyleBackColor = false;
            this.btnTransferir.Click += new System.EventHandler(this.btnTransferir_Click);
            // 
            // dgvPdf
            // 
            this.dgvPdf.AllowUserToAddRows = false;
            this.dgvPdf.AllowUserToDeleteRows = false;
            this.dgvPdf.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPdf.BackgroundColor = System.Drawing.Color.White;
            this.dgvPdf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvPdf.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPdf.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.dgvPdf.EnableHeadersVisualStyles = false;
            this.dgvPdf.RowTemplate.Height = 28;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPdf.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPdf.ColumnHeadersHeight = 32;
            this.dgvPdf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPdf.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColSeleccionar,
            this.ColSku,
            this.ColModelo,
            this.ColColor,
            this.ColMedida,
            this.ColMarca,
            this.ColPrecio,
            this.ColCantSugerida,
            this.ColCantidad,
            this.ColEstadoCatalogo});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPdf.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPdf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPdf.Location = new System.Drawing.Point(0, 115);
            this.dgvPdf.Name = "dgvPdf";
            this.dgvPdf.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPdf.Size = new System.Drawing.Size(1084, 350);
            this.dgvPdf.TabIndex = 2;
            this.dgvPdf.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPdf_CellValueChanged);
            this.dgvPdf.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvPdf_CurrentCellDirtyStateChanged);
            // 
            // ColSeleccionar
            // 
            this.ColSeleccionar.FillWeight = 40F;
            this.ColSeleccionar.HeaderText = "Sel.";
            this.ColSeleccionar.Name = "ColSeleccionar";
            // 
            // ColSku
            // 
            this.ColSku.FillWeight = 90F;
            this.ColSku.HeaderText = "SKU";
            this.ColSku.Name = "ColSku";
            this.ColSku.ReadOnly = true;
            // 
            // ColModelo
            // 
            this.ColModelo.FillWeight = 85F;
            this.ColModelo.HeaderText = "Modelo";
            this.ColModelo.Name = "ColModelo";
            this.ColModelo.ReadOnly = true;
            // 
            // ColColor
            // 
            this.ColColor.FillWeight = 60F;
            this.ColColor.HeaderText = "Color";
            this.ColColor.Name = "ColColor";
            this.ColColor.ReadOnly = true;
            // 
            // ColMedida
            // 
            this.ColMedida.FillWeight = 55F;
            this.ColMedida.HeaderText = "Medida";
            this.ColMedida.Name = "ColMedida";
            this.ColMedida.ReadOnly = true;
            // 
            // ColMarca
            // 
            this.ColMarca.FillWeight = 90F;
            this.ColMarca.HeaderText = "Marca (Editable)";
            this.ColMarca.Name = "ColMarca";
            // 
            // ColPrecio
            // 
            this.ColPrecio.FillWeight = 70F;
            this.ColPrecio.HeaderText = "Precio (Editable)";
            this.ColPrecio.Name = "ColPrecio";
            // 
            // ColCantSugerida
            // 
            this.ColCantSugerida.FillWeight = 55F;
            this.ColCantSugerida.HeaderText = "Sugerida";
            this.ColCantSugerida.Name = "ColCantSugerida";
            this.ColCantSugerida.ReadOnly = true;
            // 
            // ColCantidad
            // 
            this.ColCantidad.FillWeight = 60F;
            this.ColCantidad.HeaderText = "Cantidad (Editable)";
            this.ColCantidad.Name = "ColCantidad";
            // 
            // ColEstadoCatalogo
            // 
            this.ColEstadoCatalogo.FillWeight = 65F;
            this.ColEstadoCatalogo.HeaderText = "Catálogo";
            this.ColEstadoCatalogo.Name = "ColEstadoCatalogo";
            this.ColEstadoCatalogo.ReadOnly = true;
            // 
            // FrmImportarPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.Controls.Add(this.dgvPdf);
            this.Controls.Add(this.pnlResumen);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.pnlSuperior);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "FrmImportarPdf";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Importar Pedido desde PDF y Cruce con Catálogo";
            this.Load += new System.EventHandler(this.FrmImportarPdf_Load);
            this.pnlSuperior.ResumeLayout(false);
            this.grpPasos.ResumeLayout(false);
            this.grpPasos.PerformLayout();
            this.pnlResumen.ResumeLayout(false);
            this.pnlResumen.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPdf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSuperior;
        private System.Windows.Forms.GroupBox grpPasos;
        private System.Windows.Forms.Button btnCargarCatalogo;
        private System.Windows.Forms.Label lblEstadoCatalogo;
        private System.Windows.Forms.Button btnSeleccionarPdf;
        private System.Windows.Forms.Label lblArchivoPdf;
        private System.Windows.Forms.Panel pnlResumen;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.DataGridView dgvPdf;
        private System.Windows.Forms.Button btnTransferir;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.CheckBox chkSeleccionarTodos;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSku;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColModelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMedida;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMarca;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCantSugerida;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEstadoCatalogo;
    }
}
