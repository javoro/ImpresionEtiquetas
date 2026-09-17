namespace ImpresionEtiquetas
{
    partial class FrmAyudaImpresora
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnAbrirImpresoras = new System.Windows.Forms.Button();
            this.btnProbarConexion = new System.Windows.Forms.Button();
            this.btnImprimirPrueba = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.grpPaso5 = new System.Windows.Forms.GroupBox();
            this.lblDescPaso5 = new System.Windows.Forms.Label();
            this.grpPaso4 = new System.Windows.Forms.GroupBox();
            this.lblDescPaso4 = new System.Windows.Forms.Label();
            this.grpPaso3 = new System.Windows.Forms.GroupBox();
            this.lblAlertaPaso3 = new System.Windows.Forms.Label();
            this.lblDescPaso3 = new System.Windows.Forms.Label();
            this.grpPaso2 = new System.Windows.Forms.GroupBox();
            this.lblDescPaso2 = new System.Windows.Forms.Label();
            this.grpPaso1 = new System.Windows.Forms.GroupBox();
            this.lblDescPaso1 = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.grpPaso5.SuspendLayout();
            this.grpPaso4.SuspendLayout();
            this.grpPaso3.SuspendLayout();
            this.grpPaso2.SuspendLayout();
            this.grpPaso1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.pnlHeader.Controls.Add(this.lblSubtitulo);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 14, 20, 14);
            this.pnlHeader.Size = new System.Drawing.Size(844, 76);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(251)))), ((int)(((byte)(241)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(22, 42);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(564, 15);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Instrucciones técnicas indispensables para que el sistema comunique las órdenes ZPL a la impresora Zebra.";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 14);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(462, 23);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "📖 Guía de Configuración e Instalación - Impresora Zebra";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlFooter.Controls.Add(this.btnAbrirImpresoras);
            this.pnlFooter.Controls.Add(this.btnProbarConexion);
            this.pnlFooter.Controls.Add(this.btnImprimirPrueba);
            this.pnlFooter.Controls.Add(this.btnCerrar);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 601);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlFooter.Size = new System.Drawing.Size(844, 60);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnAbrirImpresoras
            // 
            this.btnAbrirImpresoras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.btnAbrirImpresoras.FlatAppearance.BorderSize = 0;
            this.btnAbrirImpresoras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirImpresoras.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirImpresoras.ForeColor = System.Drawing.Color.White;
            this.btnAbrirImpresoras.Location = new System.Drawing.Point(16, 14);
            this.btnAbrirImpresoras.Name = "btnAbrirImpresoras";
            this.btnAbrirImpresoras.Size = new System.Drawing.Size(200, 32);
            this.btnAbrirImpresoras.TabIndex = 0;
            this.btnAbrirImpresoras.Text = "🖨️ Panel de Impresoras";
            this.btnAbrirImpresoras.UseVisualStyleBackColor = false;
            this.btnAbrirImpresoras.Click += new System.EventHandler(this.btnAbrirImpresoras_Click);
            // 
            // btnProbarConexion
            // 
            this.btnProbarConexion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnProbarConexion.FlatAppearance.BorderSize = 0;
            this.btnProbarConexion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProbarConexion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProbarConexion.ForeColor = System.Drawing.Color.White;
            this.btnProbarConexion.Location = new System.Drawing.Point(224, 14);
            this.btnProbarConexion.Name = "btnProbarConexion";
            this.btnProbarConexion.Size = new System.Drawing.Size(200, 32);
            this.btnProbarConexion.TabIndex = 1;
            this.btnProbarConexion.Text = "🔍 Diagnosticar Conexión";
            this.btnProbarConexion.UseVisualStyleBackColor = false;
            this.btnProbarConexion.Click += new System.EventHandler(this.btnProbarConexion_Click);
            // 
            // btnImprimirPrueba
            // 
            this.btnImprimirPrueba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnImprimirPrueba.FlatAppearance.BorderSize = 0;
            this.btnImprimirPrueba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirPrueba.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirPrueba.ForeColor = System.Drawing.Color.White;
            this.btnImprimirPrueba.Location = new System.Drawing.Point(432, 14);
            this.btnImprimirPrueba.Name = "btnImprimirPrueba";
            this.btnImprimirPrueba.Size = new System.Drawing.Size(190, 32);
            this.btnImprimirPrueba.TabIndex = 2;
            this.btnImprimirPrueba.Text = "🏷️ Imprimir Etiqueta de Prueba";
            this.btnImprimirPrueba.UseVisualStyleBackColor = false;
            this.btnImprimirPrueba.Click += new System.EventHandler(this.btnImprimirPrueba_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnCerrar.Location = new System.Drawing.Point(732, 14);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(96, 32);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // pnlContenido
            // 
            this.pnlContenido.AutoScroll = true;
            this.pnlContenido.Controls.Add(this.grpPaso5);
            this.pnlContenido.Controls.Add(this.grpPaso4);
            this.pnlContenido.Controls.Add(this.grpPaso3);
            this.pnlContenido.Controls.Add(this.grpPaso2);
            this.pnlContenido.Controls.Add(this.grpPaso1);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 76);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlContenido.Size = new System.Drawing.Size(844, 525);
            this.pnlContenido.TabIndex = 2;
            // 
            // grpPaso5
            // 
            this.grpPaso5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPaso5.BackColor = System.Drawing.Color.White;
            this.grpPaso5.Controls.Add(this.lblDescPaso5);
            this.grpPaso5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPaso5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpPaso5.Location = new System.Drawing.Point(20, 480);
            this.grpPaso5.Name = "grpPaso5";
            this.grpPaso5.Padding = new System.Windows.Forms.Padding(14);
            this.grpPaso5.Size = new System.Drawing.Size(785, 125);
            this.grpPaso5.TabIndex = 4;
            this.grpPaso5.TabStop = false;
            this.grpPaso5.Text = "Paso 5: Dimensiones de Etiqueta y Calibración de Sensor";
            // 
            // lblDescPaso5
            // 
            this.lblDescPaso5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescPaso5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescPaso5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDescPaso5.Location = new System.Drawing.Point(14, 32);
            this.lblDescPaso5.Name = "lblDescPaso5";
            this.lblDescPaso5.Size = new System.Drawing.Size(757, 59);
            this.lblDescPaso5.TabIndex = 0;
            this.lblDescPaso5.Text = "• Ingrese a 'Preferencias de impresión' de la Zebra y ajuste las dimensiones físicas del rollo (ancho y alto de etiquetas).\r\n• Tipo de sensor: Configure 'Marca negra' (Black Mark) o 'Espacio / Hueco' (Web / Gap) según su material.\r\n• Calibración: Si la impresora salta etiquetas en blanco, mantenga presionado el botón de alimentación (FEED) hasta que parpadee 2 veces para auto-calibrar el sensor.";
            // 
            // grpPaso4
            // 
            this.grpPaso4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPaso4.BackColor = System.Drawing.Color.White;
            this.grpPaso4.Controls.Add(this.lblDescPaso4);
            this.grpPaso4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPaso4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpPaso4.Location = new System.Drawing.Point(20, 370);
            this.grpPaso4.Name = "grpPaso4";
            this.grpPaso4.Padding = new System.Windows.Forms.Padding(14);
            this.grpPaso4.Size = new System.Drawing.Size(785, 95);
            this.grpPaso4.TabIndex = 3;
            this.grpPaso4.TabStop = false;
            this.grpPaso4.Text = "Paso 4: Red Local y Servicio de Cola de Impresión (Spooler)";
            // 
            // lblDescPaso4
            // 
            this.lblDescPaso4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescPaso4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescPaso4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDescPaso4.Location = new System.Drawing.Point(14, 32);
            this.lblDescPaso4.Name = "lblDescPaso4";
            this.lblDescPaso4.Size = new System.Drawing.Size(757, 49);
            this.lblDescPaso4.TabIndex = 0;
            this.lblDescPaso4.Text = "• En Windows, asegúrese de que el perfil de red privada tenga habilitado \'Activar el uso compartido de archivos e impresoras\'.\r\n• El servicio de Windows \'Cola de impresión\' (Print Spooler) debe encontrarse en estado \'En ejecución\'.\r\n• Puede comprobar si el recurso existe abriendo una consola CMD y ejecutando: net view \\\\localhost";
            // 
            // grpPaso3
            // 
            this.grpPaso3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPaso3.BackColor = System.Drawing.Color.White;
            this.grpPaso3.Controls.Add(this.lblAlertaPaso3);
            this.grpPaso3.Controls.Add(this.lblDescPaso3);
            this.grpPaso3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPaso3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpPaso3.Location = new System.Drawing.Point(20, 235);
            this.grpPaso3.Name = "grpPaso3";
            this.grpPaso3.Padding = new System.Windows.Forms.Padding(14);
            this.grpPaso3.Size = new System.Drawing.Size(785, 120);
            this.grpPaso3.TabIndex = 2;
            this.grpPaso3.TabStop = false;
            this.grpPaso3.Text = "Paso 3: Nombre Exacto del Recurso: ZDesigner (Obligatorio)";
            // 
            // lblAlertaPaso3
            // 
            this.lblAlertaPaso3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.lblAlertaPaso3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblAlertaPaso3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertaPaso3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.lblAlertaPaso3.Location = new System.Drawing.Point(14, 76);
            this.lblAlertaPaso3.Name = "lblAlertaPaso3";
            this.lblAlertaPaso3.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.lblAlertaPaso3.Size = new System.Drawing.Size(757, 30);
            this.lblAlertaPaso3.TabIndex = 1;
            this.lblAlertaPaso3.Text = "⚠️ ATENCIÓN: Si el nombre compartido tiene espacios, números adicionales o letras distintas, la impresión fallará.";
            this.lblAlertaPaso3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescPaso3
            // 
            this.lblDescPaso3.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescPaso3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescPaso3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDescPaso3.Location = new System.Drawing.Point(14, 32);
            this.lblDescPaso3.Name = "lblDescPaso3";
            this.lblDescPaso3.Size = new System.Drawing.Size(757, 40);
            this.lblDescPaso3.TabIndex = 0;
            this.lblDescPaso3.Text = "• En el campo \'Nombre del recurso compartido\' (Share Name), escriba textualmente: ZDesigner\r\n• El puente de impresión del sistema ejecuta: COPY /B Etiquetas.txt \\\\localhost\\ZDesigner";
            // 
            // grpPaso2
            // 
            this.grpPaso2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPaso2.BackColor = System.Drawing.Color.White;
            this.grpPaso2.Controls.Add(this.lblDescPaso2);
            this.grpPaso2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPaso2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpPaso2.Location = new System.Drawing.Point(20, 125);
            this.grpPaso2.Name = "grpPaso2";
            this.grpPaso2.Padding = new System.Windows.Forms.Padding(14);
            this.grpPaso2.Size = new System.Drawing.Size(785, 95);
            this.grpPaso2.TabIndex = 1;
            this.grpPaso2.TabStop = false;
            this.grpPaso2.Text = "Paso 2: Compartir la Impresora en Windows (Crítico)";
            // 
            // lblDescPaso2
            // 
            this.lblDescPaso2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescPaso2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescPaso2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDescPaso2.Location = new System.Drawing.Point(14, 32);
            this.lblDescPaso2.Name = "lblDescPaso2";
            this.lblDescPaso2.Size = new System.Drawing.Size(757, 49);
            this.lblDescPaso2.TabIndex = 0;
            this.lblDescPaso2.Text = "• Abra el Panel de Control > Dispositivos e Impresoras (o use el botón inferior \'Panel de Impresoras\').\r\n• Clic derecho sobre la impresora Zebra > \'Propiedades de la impresora\' (NO en \'Preferencias\').\r\n• Seleccione la pestaña \'Uso compartido\' y marque la casilla: [✓] Compartir esta impresora.";
            // 
            // grpPaso1
            // 
            this.grpPaso1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPaso1.BackColor = System.Drawing.Color.White;
            this.grpPaso1.Controls.Add(this.lblDescPaso1);
            this.grpPaso1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPaso1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grpPaso1.Location = new System.Drawing.Point(20, 16);
            this.grpPaso1.Name = "grpPaso1";
            this.grpPaso1.Padding = new System.Windows.Forms.Padding(14);
            this.grpPaso1.Size = new System.Drawing.Size(785, 95);
            this.grpPaso1.TabIndex = 0;
            this.grpPaso1.TabStop = false;
            this.grpPaso1.Text = "Paso 1: Instalación del Controlador y Conexión Física";
            // 
            // lblDescPaso1
            // 
            this.lblDescPaso1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescPaso1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescPaso1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDescPaso1.Location = new System.Drawing.Point(14, 32);
            this.lblDescPaso1.Name = "lblDescPaso1";
            this.lblDescPaso1.Size = new System.Drawing.Size(757, 49);
            this.lblDescPaso1.TabIndex = 0;
            this.lblDescPaso1.Text = "• Conecte la impresora Zebra por cable USB directo al equipo (o red Ethernet local) y enciéndala.\r\n• Instale el controlador oficial \'ZebraDesigner Driver\' (ZDesigner) correspondiente a su modelo (ej. TLP 2844, GK420t, GC420t, Serie ZD) o en su defecto el driver \'Genérico / Solo Texto\'.\r\n• Confirme que el indicador LED de la impresora se mantenga en color VERDE fijo (sin parpadeos de luz roja).";
            // 
            // FrmAyudaImpresora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(844, 661);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAyudaImpresora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Guía de Configuración - Impresora Zebra";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.grpPaso5.ResumeLayout(false);
            this.grpPaso4.ResumeLayout(false);
            this.grpPaso3.ResumeLayout(false);
            this.grpPaso2.ResumeLayout(false);
            this.grpPaso1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnAbrirImpresoras;
        private System.Windows.Forms.Button btnProbarConexion;
        private System.Windows.Forms.Button btnImprimirPrueba;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.GroupBox grpPaso1;
        private System.Windows.Forms.Label lblDescPaso1;
        private System.Windows.Forms.GroupBox grpPaso2;
        private System.Windows.Forms.Label lblDescPaso2;
        private System.Windows.Forms.GroupBox grpPaso3;
        private System.Windows.Forms.Label lblDescPaso3;
        private System.Windows.Forms.Label lblAlertaPaso3;
        private System.Windows.Forms.GroupBox grpPaso4;
        private System.Windows.Forms.Label lblDescPaso4;
        private System.Windows.Forms.GroupBox grpPaso5;
        private System.Windows.Forms.Label lblDescPaso5;
    }
}
