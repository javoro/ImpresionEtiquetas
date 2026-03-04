namespace ImpresionEtiquetas
{
    partial class frmImprimir
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImprimir));
            this.grpEmpresa = new System.Windows.Forms.GroupBox();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.txtEmpresa = new System.Windows.Forms.TextBox();
            this.grpConfigLongitud = new System.Windows.Forms.GroupBox();
            this.chkEditarConfig = new System.Windows.Forms.CheckBox();
            this.btnGuardarConfig = new System.Windows.Forms.Button();
            this.lblConfigMaxCaracteres = new System.Windows.Forms.Label();
            this.lblConfigMarca = new System.Windows.Forms.Label();
            this.nudConfigMarca = new System.Windows.Forms.NumericUpDown();
            this.lblConfigModelo = new System.Windows.Forms.Label();
            this.nudConfigModelo = new System.Windows.Forms.NumericUpDown();
            this.lblConfigSku = new System.Windows.Forms.Label();
            this.nudConfigSku = new System.Windows.Forms.NumericUpDown();
            this.grpModo = new System.Windows.Forms.GroupBox();
            this.rdoManual = new System.Windows.Forms.RadioButton();
            this.rdoExcel = new System.Windows.Forms.RadioButton();
            this.btnImportar = new System.Windows.Forms.Button();
            this.grpManual = new System.Windows.Forms.GroupBox();
            this.btnEliminarFila = new System.Windows.Forms.Button();
            this.btnEditarFila = new System.Windows.Forms.Button();
            this.btnAgregarFila = new System.Windows.Forms.Button();
            this.lblCantidadManual = new System.Windows.Forms.Label();
            this.txtCantidadManual = new System.Windows.Forms.TextBox();
            this.lblPrecioManual = new System.Windows.Forms.Label();
            this.txtPrecioManual = new System.Windows.Forms.TextBox();
            this.lblSkuManual = new System.Windows.Forms.Label();
            this.txtSkuManual = new System.Windows.Forms.TextBox();
            this.lblModeloManual = new System.Windows.Forms.Label();
            this.txtModeloManual = new System.Windows.Forms.TextBox();
            this.lblMarcaManual = new System.Windows.Forms.Label();
            this.txtMarcaManual = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.Marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccionPrincipal = new System.Windows.Forms.DataGridViewButtonColumn();
            this.AccionSecundaria = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnImprimirEtiquetas = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpEmpresa.SuspendLayout();
            this.grpConfigLongitud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConfigMarca)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudConfigModelo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudConfigSku)).BeginInit();
            this.grpModo.SuspendLayout();
            this.grpManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpEmpresa
            // 
            this.grpEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEmpresa.Controls.Add(this.lblEmpresa);
            this.grpEmpresa.Controls.Add(this.txtEmpresa);
            this.grpEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEmpresa.Location = new System.Drawing.Point(16, 15);
            this.grpEmpresa.Margin = new System.Windows.Forms.Padding(4);
            this.grpEmpresa.Name = "grpEmpresa";
            this.grpEmpresa.Padding = new System.Windows.Forms.Padding(4);
            this.grpEmpresa.Size = new System.Drawing.Size(1238, 82);
            this.grpEmpresa.TabIndex = 0;
            this.grpEmpresa.TabStop = false;
            this.grpEmpresa.Text = "Información de etiqueta";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.Location = new System.Drawing.Point(18, 38);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(131, 16);
            this.lblEmpresa.TabIndex = 0;
            this.lblEmpresa.Text = "Nombre de empresa:";
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpresa.Location = new System.Drawing.Point(156, 35);
            this.txtEmpresa.MaxLength = 80;
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Size = new System.Drawing.Size(1056, 22);
            this.txtEmpresa.TabIndex = 1;
            // 
            // grpConfigLongitud
            // 
            this.grpConfigLongitud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConfigLongitud.Controls.Add(this.chkEditarConfig);
            this.grpConfigLongitud.Controls.Add(this.btnGuardarConfig);
            this.grpConfigLongitud.Controls.Add(this.lblConfigMaxCaracteres);
            this.grpConfigLongitud.Controls.Add(this.lblConfigMarca);
            this.grpConfigLongitud.Controls.Add(this.nudConfigMarca);
            this.grpConfigLongitud.Controls.Add(this.lblConfigModelo);
            this.grpConfigLongitud.Controls.Add(this.nudConfigModelo);
            this.grpConfigLongitud.Controls.Add(this.lblConfigSku);
            this.grpConfigLongitud.Controls.Add(this.nudConfigSku);
            this.grpConfigLongitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpConfigLongitud.Location = new System.Drawing.Point(16, 105);
            this.grpConfigLongitud.Margin = new System.Windows.Forms.Padding(4);
            this.grpConfigLongitud.Name = "grpConfigLongitud";
            this.grpConfigLongitud.Padding = new System.Windows.Forms.Padding(4);
            this.grpConfigLongitud.Size = new System.Drawing.Size(1238, 75);
            this.grpConfigLongitud.TabIndex = 10;
            this.grpConfigLongitud.TabStop = false;
            this.grpConfigLongitud.Text = "Configuración de longitudes de etiqueta";
            // 
            // chkEditarConfig
            // 
            this.chkEditarConfig.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkEditarConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEditarConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkEditarConfig.Location = new System.Drawing.Point(18, 25);
            this.chkEditarConfig.Name = "chkEditarConfig";
            this.chkEditarConfig.Size = new System.Drawing.Size(80, 28);
            this.chkEditarConfig.TabIndex = 0;
            this.chkEditarConfig.Text = "✎ Editar";
            this.chkEditarConfig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEditarConfig.UseVisualStyleBackColor = true;
            this.chkEditarConfig.CheckedChanged += new System.EventHandler(this.chkEditarConfig_CheckedChanged);
            // 
            // btnGuardarConfig
            // 
            this.btnGuardarConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(98)))), ((int)(((byte)(59)))));
            this.btnGuardarConfig.Enabled = false;
            this.btnGuardarConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarConfig.ForeColor = System.Drawing.Color.White;
            this.btnGuardarConfig.Location = new System.Drawing.Point(601, 25);
            this.btnGuardarConfig.Name = "btnGuardarConfig";
            this.btnGuardarConfig.Size = new System.Drawing.Size(130, 28);
            this.btnGuardarConfig.TabIndex = 8;
            this.btnGuardarConfig.Text = "💾 Guardar";
            this.btnGuardarConfig.UseVisualStyleBackColor = false;
            this.btnGuardarConfig.Click += new System.EventHandler(this.btnGuardarConfig_Click);
            // 
            // lblConfigMaxCaracteres
            // 
            this.lblConfigMaxCaracteres.AutoSize = true;
            this.lblConfigMaxCaracteres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfigMaxCaracteres.Location = new System.Drawing.Point(115, 32);
            this.lblConfigMaxCaracteres.Name = "lblConfigMaxCaracteres";
            this.lblConfigMaxCaracteres.Size = new System.Drawing.Size(110, 16);
            this.lblConfigMaxCaracteres.TabIndex = 1;
            this.lblConfigMaxCaracteres.Text = "Máx. caracteres:";
            // 
            // lblConfigMarca
            // 
            this.lblConfigMarca.AutoSize = true;
            this.lblConfigMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfigMarca.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblConfigMarca.Location = new System.Drawing.Point(238, 32);
            this.lblConfigMarca.Name = "lblConfigMarca";
            this.lblConfigMarca.Size = new System.Drawing.Size(44, 15);
            this.lblConfigMarca.TabIndex = 2;
            this.lblConfigMarca.Text = "Marca:";
            // 
            // nudConfigMarca
            // 
            this.nudConfigMarca.Enabled = false;
            this.nudConfigMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudConfigMarca.Location = new System.Drawing.Point(285, 29);
            this.nudConfigMarca.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            this.nudConfigMarca.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudConfigMarca.Name = "nudConfigMarca";
            this.nudConfigMarca.Size = new System.Drawing.Size(58, 22);
            this.nudConfigMarca.TabIndex = 3;
            this.nudConfigMarca.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // lblConfigModelo
            // 
            this.lblConfigModelo.AutoSize = true;
            this.lblConfigModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfigModelo.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblConfigModelo.Location = new System.Drawing.Point(360, 32);
            this.lblConfigModelo.Name = "lblConfigModelo";
            this.lblConfigModelo.Size = new System.Drawing.Size(50, 15);
            this.lblConfigModelo.TabIndex = 4;
            this.lblConfigModelo.Text = "Modelo:";
            // 
            // nudConfigModelo
            // 
            this.nudConfigModelo.Enabled = false;
            this.nudConfigModelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudConfigModelo.Location = new System.Drawing.Point(413, 29);
            this.nudConfigModelo.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            this.nudConfigModelo.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudConfigModelo.Name = "nudConfigModelo";
            this.nudConfigModelo.Size = new System.Drawing.Size(58, 22);
            this.nudConfigModelo.TabIndex = 5;
            this.nudConfigModelo.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // lblConfigSku
            // 
            this.lblConfigSku.AutoSize = true;
            this.lblConfigSku.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfigSku.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblConfigSku.Location = new System.Drawing.Point(488, 32);
            this.lblConfigSku.Name = "lblConfigSku";
            this.lblConfigSku.Size = new System.Drawing.Size(35, 15);
            this.lblConfigSku.TabIndex = 6;
            this.lblConfigSku.Text = "SKU:";
            // 
            // nudConfigSku
            // 
            this.nudConfigSku.Enabled = false;
            this.nudConfigSku.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudConfigSku.Location = new System.Drawing.Point(526, 29);
            this.nudConfigSku.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            this.nudConfigSku.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudConfigSku.Name = "nudConfigSku";
            this.nudConfigSku.Size = new System.Drawing.Size(58, 22);
            this.nudConfigSku.TabIndex = 7;
            this.nudConfigSku.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // grpModo
            // 
            this.grpModo.Controls.Add(this.rdoManual);
            this.grpModo.Controls.Add(this.rdoExcel);
            this.grpModo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpModo.Location = new System.Drawing.Point(16, 190);
            this.grpModo.Margin = new System.Windows.Forms.Padding(4);
            this.grpModo.Name = "grpModo";
            this.grpModo.Padding = new System.Windows.Forms.Padding(4);
            this.grpModo.Size = new System.Drawing.Size(343, 85);
            this.grpModo.TabIndex = 1;
            this.grpModo.TabStop = false;
            this.grpModo.Text = "Origen de datos";
            // 
            // rdoManual
            // 
            this.rdoManual.AutoSize = true;
            this.rdoManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoManual.Location = new System.Drawing.Point(21, 53);
            this.rdoManual.Name = "rdoManual";
            this.rdoManual.Size = new System.Drawing.Size(117, 20);
            this.rdoManual.TabIndex = 1;
            this.rdoManual.Text = "Entrada manual";
            this.rdoManual.UseVisualStyleBackColor = true;
            this.rdoManual.CheckedChanged += new System.EventHandler(this.rdoModo_CheckedChanged);
            // 
            // rdoExcel
            // 
            this.rdoExcel.AutoSize = true;
            this.rdoExcel.Checked = true;
            this.rdoExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoExcel.Location = new System.Drawing.Point(21, 27);
            this.rdoExcel.Name = "rdoExcel";
            this.rdoExcel.Size = new System.Drawing.Size(129, 20);
            this.rdoExcel.TabIndex = 0;
            this.rdoExcel.TabStop = true;
            this.rdoExcel.Text = "Importar de Excel";
            this.rdoExcel.UseVisualStyleBackColor = true;
            this.rdoExcel.CheckedChanged += new System.EventHandler(this.rdoModo_CheckedChanged);
            // 
            // btnImportar
            // 
            this.btnImportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(125)))), ((int)(((byte)(154)))));
            this.btnImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.ForeColor = System.Drawing.Color.White;
            this.btnImportar.Location = new System.Drawing.Point(370, 211);
            this.btnImportar.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(260, 52);
            this.btnImportar.TabIndex = 2;
            this.btnImportar.Text = "Importar Excel";
            this.btnImportar.UseVisualStyleBackColor = false;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // grpManual
            // 
            this.grpManual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpManual.Controls.Add(this.btnEliminarFila);
            this.grpManual.Controls.Add(this.btnEditarFila);
            this.grpManual.Controls.Add(this.btnAgregarFila);
            this.grpManual.Controls.Add(this.lblCantidadManual);
            this.grpManual.Controls.Add(this.txtCantidadManual);
            this.grpManual.Controls.Add(this.lblPrecioManual);
            this.grpManual.Controls.Add(this.txtPrecioManual);
            this.grpManual.Controls.Add(this.lblSkuManual);
            this.grpManual.Controls.Add(this.txtSkuManual);
            this.grpManual.Controls.Add(this.lblModeloManual);
            this.grpManual.Controls.Add(this.txtModeloManual);
            this.grpManual.Controls.Add(this.lblMarcaManual);
            this.grpManual.Controls.Add(this.txtMarcaManual);
            this.grpManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpManual.Location = new System.Drawing.Point(16, 283);
            this.grpManual.Margin = new System.Windows.Forms.Padding(4);
            this.grpManual.Name = "grpManual";
            this.grpManual.Padding = new System.Windows.Forms.Padding(4);
            this.grpManual.Size = new System.Drawing.Size(1238, 94);
            this.grpManual.TabIndex = 3;
            this.grpManual.TabStop = false;
            this.grpManual.Text = "Registro manual";
            // 
            // btnEliminarFila
            // 
            this.btnEliminarFila.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarFila.Location = new System.Drawing.Point(1122, 31);
            this.btnEliminarFila.Name = "btnEliminarFila";
            this.btnEliminarFila.Size = new System.Drawing.Size(100, 35);
            this.btnEliminarFila.TabIndex = 10;
            this.btnEliminarFila.Text = "Eliminar";
            this.btnEliminarFila.UseVisualStyleBackColor = true;
            this.btnEliminarFila.Visible = false;
            this.btnEliminarFila.Click += new System.EventHandler(this.btnEliminarFila_Click);
            // 
            // btnEditarFila
            // 
            this.btnEditarFila.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarFila.Location = new System.Drawing.Point(1016, 31);
            this.btnEditarFila.Name = "btnEditarFila";
            this.btnEditarFila.Size = new System.Drawing.Size(100, 35);
            this.btnEditarFila.TabIndex = 9;
            this.btnEditarFila.Text = "Editar";
            this.btnEditarFila.UseVisualStyleBackColor = true;
            this.btnEditarFila.Visible = false;
            this.btnEditarFila.Click += new System.EventHandler(this.btnEditarFila_Click);
            // 
            // btnAgregarFila
            // 
            this.btnAgregarFila.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarFila.Location = new System.Drawing.Point(1122, 31);
            this.btnAgregarFila.Name = "btnAgregarFila";
            this.btnAgregarFila.Size = new System.Drawing.Size(100, 35);
            this.btnAgregarFila.TabIndex = 8;
            this.btnAgregarFila.Text = "Agregar fila";
            this.btnAgregarFila.UseVisualStyleBackColor = true;
            this.btnAgregarFila.Click += new System.EventHandler(this.btnAgregarFila_Click);
            // 
            // lblCantidadManual
            // 
            this.lblCantidadManual.AutoSize = true;
            this.lblCantidadManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadManual.Location = new System.Drawing.Point(939, 25);
            this.lblCantidadManual.Name = "lblCantidadManual";
            this.lblCantidadManual.Size = new System.Drawing.Size(64, 16);
            this.lblCantidadManual.TabIndex = 6;
            this.lblCantidadManual.Text = "Cantidad";
            // 
            // txtCantidadManual
            // 
            this.txtCantidadManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadManual.Location = new System.Drawing.Point(942, 44);
            this.txtCantidadManual.MaxLength = 3;
            this.txtCantidadManual.Name = "txtCantidadManual";
            this.txtCantidadManual.Size = new System.Drawing.Size(170, 22);
            this.txtCantidadManual.TabIndex = 7;
            // 
            // lblPrecioManual
            // 
            this.lblPrecioManual.AutoSize = true;
            this.lblPrecioManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecioManual.Location = new System.Drawing.Point(717, 25);
            this.lblPrecioManual.Name = "lblPrecioManual";
            this.lblPrecioManual.Size = new System.Drawing.Size(50, 16);
            this.lblPrecioManual.TabIndex = 4;
            this.lblPrecioManual.Text = "Precio";
            // 
            // txtPrecioManual
            // 
            this.txtPrecioManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioManual.Location = new System.Drawing.Point(720, 44);
            this.txtPrecioManual.MaxLength = 20;
            this.txtPrecioManual.Name = "txtPrecioManual";
            this.txtPrecioManual.Size = new System.Drawing.Size(200, 22);
            this.txtPrecioManual.TabIndex = 6;
            // 
            // lblSkuManual
            // 
            this.lblSkuManual.AutoSize = true;
            this.lblSkuManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkuManual.Location = new System.Drawing.Point(499, 25);
            this.lblSkuManual.Name = "lblSkuManual";
            this.lblSkuManual.Size = new System.Drawing.Size(33, 16);
            this.lblSkuManual.TabIndex = 4;
            this.lblSkuManual.Text = "SKU";
            // 
            // txtSkuManual
            // 
            this.txtSkuManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSkuManual.Location = new System.Drawing.Point(502, 44);
            this.txtSkuManual.MaxLength = 60;
            this.txtSkuManual.Name = "txtSkuManual";
            this.txtSkuManual.Size = new System.Drawing.Size(200, 22);
            this.txtSkuManual.TabIndex = 5;
            // 
            // lblModeloManual
            // 
            this.lblModeloManual.AutoSize = true;
            this.lblModeloManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModeloManual.Location = new System.Drawing.Point(252, 25);
            this.lblModeloManual.Name = "lblModeloManual";
            this.lblModeloManual.Size = new System.Drawing.Size(53, 16);
            this.lblModeloManual.TabIndex = 2;
            this.lblModeloManual.Text = "Modelo";
            // 
            // txtModeloManual
            // 
            this.txtModeloManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModeloManual.Location = new System.Drawing.Point(255, 44);
            this.txtModeloManual.MaxLength = 60;
            this.txtModeloManual.Name = "txtModeloManual";
            this.txtModeloManual.Size = new System.Drawing.Size(232, 22);
            this.txtModeloManual.TabIndex = 3;
            // 
            // lblMarcaManual
            // 
            this.lblMarcaManual.AutoSize = true;
            this.lblMarcaManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarcaManual.Location = new System.Drawing.Point(19, 25);
            this.lblMarcaManual.Name = "lblMarcaManual";
            this.lblMarcaManual.Size = new System.Drawing.Size(47, 16);
            this.lblMarcaManual.TabIndex = 0;
            this.lblMarcaManual.Text = "Marca";
            // 
            // txtMarcaManual
            // 
            this.txtMarcaManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarcaManual.Location = new System.Drawing.Point(22, 44);
            this.txtMarcaManual.MaxLength = 80;
            this.txtMarcaManual.Name = "txtMarcaManual";
            this.txtMarcaManual.Size = new System.Drawing.Size(220, 22);
            this.txtMarcaManual.TabIndex = 1;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDatos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatos.BackgroundColor = System.Drawing.Color.White;
            this.dgvDatos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(236)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDatos.ColumnHeadersHeight = 32;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Marca,
            this.Modelo,
            this.Sku,
            this.Precio,
            this.Cantidad,
            this.AccionPrincipal,
            this.AccionSecundaria});
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.dgvDatos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDatos.Location = new System.Drawing.Point(16, 385);
            this.dgvDatos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(1238, 355);
            this.dgvDatos.TabIndex = 4;
            this.dgvDatos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellContentClick);
            this.dgvDatos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDatos_CellFormatting);
            this.dgvDatos.SelectionChanged += new System.EventHandler(this.dgvDatos_SelectionChanged);
            // 
            // Marca
            // 
            this.Marca.HeaderText = "Marca";
            this.Marca.Name = "Marca";
            this.Marca.ReadOnly = true;
            this.Marca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Modelo
            // 
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            this.Modelo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Sku
            // 
            this.Sku.HeaderText = "SKU";
            this.Sku.Name = "Sku";
            this.Sku.ReadOnly = true;
            this.Sku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MaxInputLength = 3;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // AccionPrincipal
            // 
            this.AccionPrincipal.HeaderText = "Acción";
            this.AccionPrincipal.Name = "AccionPrincipal";
            this.AccionPrincipal.ReadOnly = true;
            this.AccionPrincipal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AccionPrincipal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AccionPrincipal.Width = 85;
            // 
            // AccionSecundaria
            // 
            this.AccionSecundaria.HeaderText = "Acción";
            this.AccionSecundaria.Name = "AccionSecundaria";
            this.AccionSecundaria.ReadOnly = true;
            this.AccionSecundaria.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AccionSecundaria.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AccionSecundaria.Width = 85;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(98)))), ((int)(((byte)(59)))));
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(16, 755);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(265, 49);
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Generar Etiquetas";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.btnBorrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrar.ForeColor = System.Drawing.Color.White;
            this.btnBorrar.Location = new System.Drawing.Point(1030, 755);
            this.btnBorrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(224, 49);
            this.btnBorrar.TabIndex = 7;
            this.btnBorrar.Text = "Borrar Lista";
            this.btnBorrar.UseVisualStyleBackColor = false;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnImprimirEtiquetas
            // 
            this.btnImprimirEtiquetas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImprimirEtiquetas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(84)))), ((int)(((byte)(148)))));
            this.btnImprimirEtiquetas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirEtiquetas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirEtiquetas.ForeColor = System.Drawing.Color.White;
            this.btnImprimirEtiquetas.Location = new System.Drawing.Point(289, 755);
            this.btnImprimirEtiquetas.Margin = new System.Windows.Forms.Padding(4);
            this.btnImprimirEtiquetas.Name = "btnImprimirEtiquetas";
            this.btnImprimirEtiquetas.Size = new System.Drawing.Size(265, 49);
            this.btnImprimirEtiquetas.TabIndex = 6;
            this.btnImprimirEtiquetas.Text = "Imprimir Etiquetas";
            this.btnImprimirEtiquetas.UseVisualStyleBackColor = false;
            this.btnImprimirEtiquetas.Click += new System.EventHandler(this.btnImprimirEtiquetas_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmImprimir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 818);
            this.Controls.Add(this.grpConfigLongitud);
            this.Controls.Add(this.grpManual);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.grpModo);
            this.Controls.Add(this.grpEmpresa);
            this.Controls.Add(this.btnImprimirEtiquetas);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.dgvDatos);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmImprimir";
            this.Text = "Impresión de Etiquetas";
            this.Load += new System.EventHandler(this.frmImprimir_Load);
            this.grpEmpresa.ResumeLayout(false);
            this.grpEmpresa.PerformLayout();
            this.grpConfigLongitud.ResumeLayout(false);
            this.grpConfigLongitud.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudConfigMarca)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudConfigModelo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudConfigSku)).EndInit();
            this.grpModo.ResumeLayout(false);
            this.grpModo.PerformLayout();
            this.grpManual.ResumeLayout(false);
            this.grpManual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.TextBox txtEmpresa;
        private System.Windows.Forms.GroupBox grpConfigLongitud;
        private System.Windows.Forms.CheckBox chkEditarConfig;
        private System.Windows.Forms.Button btnGuardarConfig;
        private System.Windows.Forms.Label lblConfigMaxCaracteres;
        private System.Windows.Forms.Label lblConfigMarca;
        private System.Windows.Forms.NumericUpDown nudConfigMarca;
        private System.Windows.Forms.Label lblConfigModelo;
        private System.Windows.Forms.NumericUpDown nudConfigModelo;
        private System.Windows.Forms.Label lblConfigSku;
        private System.Windows.Forms.NumericUpDown nudConfigSku;
        private System.Windows.Forms.GroupBox grpModo;
        private System.Windows.Forms.RadioButton rdoManual;
        private System.Windows.Forms.RadioButton rdoExcel;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.GroupBox grpManual;
        private System.Windows.Forms.Button btnEliminarFila;
        private System.Windows.Forms.Button btnEditarFila;
        private System.Windows.Forms.Button btnAgregarFila;
        private System.Windows.Forms.Label lblCantidadManual;
        private System.Windows.Forms.TextBox txtCantidadManual;
        private System.Windows.Forms.Label lblPrecioManual;
        private System.Windows.Forms.TextBox txtPrecioManual;
        private System.Windows.Forms.Label lblSkuManual;
        private System.Windows.Forms.TextBox txtSkuManual;
        private System.Windows.Forms.Label lblModeloManual;
        private System.Windows.Forms.TextBox txtModeloManual;
        private System.Windows.Forms.Label lblMarcaManual;
        private System.Windows.Forms.TextBox txtMarcaManual;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marca;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sku;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewButtonColumn AccionPrincipal;
        private System.Windows.Forms.DataGridViewButtonColumn AccionSecundaria;
        private System.Windows.Forms.Button btnImprimirEtiquetas;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

