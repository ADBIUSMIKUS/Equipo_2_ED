namespace Ejercicio4_Monedas
{
    partial class Form1
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
            this.pnlHeaderAccent = new System.Windows.Forms.Panel();
            this.lblTituloHeader = new System.Windows.Forms.Label();
            this.lblHeaderIcon = new System.Windows.Forms.Label();

            this.pnlContenido = new System.Windows.Forms.Panel();

            // Tarjeta Izquierda (Player 1 / Operación)
            this.pnlCardIzquierda = new System.Windows.Forms.Panel();
            this.lblTituloCardIzquierda = new System.Windows.Forms.Label();
            this.picMonedas = new System.Windows.Forms.PictureBox();
            this.lblDivisa = new System.Windows.Forms.Label();
            this.cmbMoneda = new System.Windows.Forms.ComboBox();
            this.lblCostoTotal = new System.Windows.Forms.Label();
            this.txtCostoTotal = new System.Windows.Forms.TextBox();
            this.lblMontoEntregado = new System.Windows.Forms.Label();
            this.txtMontoEntregado = new System.Windows.Forms.TextBox();
            this.btnCalcularMonedas = new System.Windows.Forms.Button();
            this.btnProbarEjemplo = new System.Windows.Forms.Button();
            this.btnLimpiarMonedas = new System.Windows.Forms.Button();

            // Tarjeta Derecha (High Score / Desglose)
            this.pnlCardDerecha = new System.Windows.Forms.Panel();
            this.lblTituloCardDerecha = new System.Windows.Forms.Label();
            this.pnlCambioBanner = new System.Windows.Forms.Panel();
            this.lblCambioHeader = new System.Windows.Forms.Label();
            this.lblCambioValor = new System.Windows.Forms.Label();

            // Banner Equivalencia MXN (Retro Cyan)
            this.pnlEquivalenciaBanner = new System.Windows.Forms.Panel();
            this.lblEquivalenciaMXN = new System.Windows.Forms.Label();

            this.lblSubtituloTabla = new System.Windows.Forms.Label();
            this.lstResultadoMonedas = new System.Windows.Forms.ListBox();
            this.pnlFooterStatus = new System.Windows.Forms.Panel();
            this.lblResumenPiezas = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlCardIzquierda.SuspendLayout();
            this.pnlCardDerecha.SuspendLayout();
            this.pnlCambioBanner.SuspendLayout();
            this.pnlEquivalenciaBanner.SuspendLayout();
            this.pnlFooterStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMonedas)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(8)))), ((int)(((byte)(54))))); // Retro Cabinet Purple
            this.pnlHeader.Controls.Add(this.pnlHeaderAccent);
            this.pnlHeader.Controls.Add(this.lblHeaderIcon);
            this.pnlHeader.Controls.Add(this.lblTituloHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(830, 75);

            // pnlHeaderAccent (Línea Neón Magenta inferior)
            this.pnlHeaderAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127))))); // Neon Magenta
            this.pnlHeaderAccent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderAccent.Location = new System.Drawing.Point(0, 72);
            this.pnlHeaderAccent.Size = new System.Drawing.Size(830, 3);

            // lblHeaderIcon
            this.lblHeaderIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(255))))); // Neon Cyan
            this.lblHeaderIcon.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeaderIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.lblHeaderIcon.Location = new System.Drawing.Point(20, 13);
            this.lblHeaderIcon.Size = new System.Drawing.Size(46, 46);
            this.lblHeaderIcon.Text = "🕹️";
            this.lblHeaderIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblTituloHeader
            this.lblTituloHeader.AutoSize = true;
            this.lblTituloHeader.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            this.lblTituloHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(0))))); // Neon Yellow
            this.lblTituloHeader.Location = new System.Drawing.Point(78, 22);
            this.lblTituloHeader.Text = "🕹️ RETRO COIN MACHINE - DESGLOSE DE CAMBIO";

            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33))))); // Deep Synthwave Black/Purple
            this.pnlContenido.Controls.Add(this.pnlCardIzquierda);
            this.pnlContenido.Controls.Add(this.pnlCardDerecha);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 75);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(830, 535);

            // 
            // TARJETA IZQUIERDA (PLAYER 1 / CRÉDITOS Y PAGO)
            // 
            this.pnlCardIzquierda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(9)))), ((int)(((byte)(51))))); // Cabinet Dark Card
            this.pnlCardIzquierda.Controls.Add(this.lblTituloCardIzquierda);
            this.pnlCardIzquierda.Controls.Add(this.picMonedas);
            this.pnlCardIzquierda.Controls.Add(this.lblDivisa);
            this.pnlCardIzquierda.Controls.Add(this.cmbMoneda);
            this.pnlCardIzquierda.Controls.Add(this.lblCostoTotal);
            this.pnlCardIzquierda.Controls.Add(this.txtCostoTotal);
            this.pnlCardIzquierda.Controls.Add(this.lblMontoEntregado);
            this.pnlCardIzquierda.Controls.Add(this.txtMontoEntregado);
            this.pnlCardIzquierda.Controls.Add(this.btnCalcularMonedas);
            this.pnlCardIzquierda.Controls.Add(this.btnProbarEjemplo);
            this.pnlCardIzquierda.Controls.Add(this.btnLimpiarMonedas);
            this.pnlCardIzquierda.Location = new System.Drawing.Point(22, 20);
            this.pnlCardIzquierda.Name = "pnlCardIzquierda";
            this.pnlCardIzquierda.Size = new System.Drawing.Size(375, 485);

            // lblTituloCardIzquierda
            this.lblTituloCardIzquierda.AutoSize = true;
            this.lblTituloCardIzquierda.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloCardIzquierda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(255))))); // Neon Cyan
            this.lblTituloCardIzquierda.Location = new System.Drawing.Point(20, 14);
            this.lblTituloCardIzquierda.Text = "🕹️ PLAYER 1: CRÉDITOS Y PAGO";

            // picMonedas
            this.picMonedas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.picMonedas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMonedas.Location = new System.Drawing.Point(20, 40);
            this.picMonedas.Name = "picMonedas";
            this.picMonedas.Size = new System.Drawing.Size(335, 130);

            // lblDivisa
            this.lblDivisa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDivisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127))))); // Neon Magenta
            this.lblDivisa.Location = new System.Drawing.Point(20, 180);
            this.lblDivisa.Text = "SELECT CURRENCY / DIVISA DEL MUNDO:";
            this.lblDivisa.Size = new System.Drawing.Size(335, 18);

            // cmbMoneda
            this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMoneda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.cmbMoneda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.cmbMoneda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(0))))); // Neon Yellow
            this.cmbMoneda.Location = new System.Drawing.Point(20, 200);
            this.cmbMoneda.Size = new System.Drawing.Size(335, 25);
            this.cmbMoneda.SelectedIndexChanged += new System.EventHandler(this.cmbMoneda_SelectedIndexChanged);

            // lblCostoTotal
            this.lblCostoTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCostoTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(255))))); // Neon Cyan
            this.lblCostoTotal.Location = new System.Drawing.Point(20, 238);
            this.lblCostoTotal.Text = "TOTAL AMOUNT / COSTO A PAGAR ($MXN):";
            this.lblCostoTotal.Size = new System.Drawing.Size(335, 18);

            // txtCostoTotal
            this.txtCostoTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.txtCostoTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.txtCostoTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20))))); // Phosphor Green
            this.txtCostoTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCostoTotal.Location = new System.Drawing.Point(20, 258);
            this.txtCostoTotal.Size = new System.Drawing.Size(335, 26);
            this.txtCostoTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidarEntradaDecimal_KeyPress);

            // lblMontoEntregado
            this.lblMontoEntregado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMontoEntregado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(255))))); // Neon Cyan
            this.lblMontoEntregado.Location = new System.Drawing.Point(20, 292);
            this.lblMontoEntregado.Text = "PAYMENT / MONTO ENTREGADO ($MXN):";
            this.lblMontoEntregado.Size = new System.Drawing.Size(335, 18);

            // txtMontoEntregado
            this.txtMontoEntregado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.txtMontoEntregado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.txtMontoEntregado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20))))); // Phosphor Green
            this.txtMontoEntregado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMontoEntregado.Location = new System.Drawing.Point(20, 312);
            this.txtMontoEntregado.Size = new System.Drawing.Size(335, 26);
            this.txtMontoEntregado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidarEntradaDecimal_KeyPress);

            // btnCalcularMonedas
            this.btnCalcularMonedas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(0))))); // Arcade Yellow
            this.btnCalcularMonedas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33))))); // Black text
            this.btnCalcularMonedas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalcularMonedas.FlatAppearance.BorderSize = 0;
            this.btnCalcularMonedas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCalcularMonedas.Location = new System.Drawing.Point(20, 355);
            this.btnCalcularMonedas.Size = new System.Drawing.Size(335, 42);
            this.btnCalcularMonedas.Text = "🕹️ PRESS START (CALCULAR)";
            this.btnCalcularMonedas.Click += new System.EventHandler(this.btnCalcularMonedas_Click);

            // btnProbarEjemplo
            this.btnProbarEjemplo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127))))); // Neon Magenta
            this.btnProbarEjemplo.ForeColor = System.Drawing.Color.White;
            this.btnProbarEjemplo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProbarEjemplo.FlatAppearance.BorderSize = 0;
            this.btnProbarEjemplo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnProbarEjemplo.Location = new System.Drawing.Point(20, 408);
            this.btnProbarEjemplo.Size = new System.Drawing.Size(162, 36);
            this.btnProbarEjemplo.Text = "👾 DEMO ($73.26)";
            this.btnProbarEjemplo.Click += new System.EventHandler(this.btnProbarEjemplo_Click);

            // btnLimpiarMonedas
            this.btnLimpiarMonedas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(19)))), ((int)(((byte)(84))))); // Dark Purple
            this.btnLimpiarMonedas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(255))))); // Cyan
            this.btnLimpiarMonedas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarMonedas.FlatAppearance.BorderSize = 0;
            this.btnLimpiarMonedas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimpiarMonedas.Location = new System.Drawing.Point(193, 408);
            this.btnLimpiarMonedas.Size = new System.Drawing.Size(162, 36);
            this.btnLimpiarMonedas.Text = "🔄 RESET GAME";
            this.btnLimpiarMonedas.Click += new System.EventHandler(this.btnLimpiarMonedas_Click);

            // 
            // TARJETA DERECHA (HIGH SCORE / DESGLOSE)
            // 
            this.pnlCardDerecha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(9)))), ((int)(((byte)(51))))); // Cabinet Dark Card
            this.pnlCardDerecha.Controls.Add(this.lblTituloCardDerecha);
            this.pnlCardDerecha.Controls.Add(this.pnlCambioBanner);
            this.pnlCardDerecha.Controls.Add(this.pnlEquivalenciaBanner);
            this.pnlCardDerecha.Controls.Add(this.lblSubtituloTabla);
            this.pnlCardDerecha.Controls.Add(this.lstResultadoMonedas);
            this.pnlCardDerecha.Controls.Add(this.pnlFooterStatus);
            this.pnlCardDerecha.Location = new System.Drawing.Point(415, 20);
            this.pnlCardDerecha.Name = "pnlCardDerecha";
            this.pnlCardDerecha.Size = new System.Drawing.Size(390, 485);

            // lblTituloCardDerecha
            this.lblTituloCardDerecha.AutoSize = true;
            this.lblTituloCardDerecha.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloCardDerecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(127))))); // Neon Magenta
            this.lblTituloCardDerecha.Location = new System.Drawing.Point(20, 14);
            this.lblTituloCardDerecha.Text = "👾 HIGH SCORE: DESGLOSE MÍNIMO";

            // pnlCambioBanner
            this.pnlCambioBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.pnlCambioBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCambioBanner.Controls.Add(this.lblCambioHeader);
            this.pnlCambioBanner.Controls.Add(this.lblCambioValor);
            this.pnlCambioBanner.Location = new System.Drawing.Point(20, 40);
            this.pnlCambioBanner.Name = "pnlCambioBanner";
            this.pnlCambioBanner.Size = new System.Drawing.Size(350, 60);

            // lblCambioHeader
            this.lblCambioHeader.AutoSize = true;
            this.lblCambioHeader.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCambioHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(255))))); // Cyan
            this.lblCambioHeader.Location = new System.Drawing.Point(12, 8);
            this.lblCambioHeader.Text = "SCORE / CAMBIO TOTAL A DEVOLVER:";

            // lblCambioValor
            this.lblCambioValor.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblCambioValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20))))); // Phosphor Green
            this.lblCambioValor.Location = new System.Drawing.Point(10, 24);
            this.lblCambioValor.Size = new System.Drawing.Size(330, 30);
            this.lblCambioValor.Text = "$MXN 0.00";

            // pnlEquivalenciaBanner
            this.pnlEquivalenciaBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.pnlEquivalenciaBanner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEquivalenciaBanner.Controls.Add(this.lblEquivalenciaMXN);
            this.pnlEquivalenciaBanner.Location = new System.Drawing.Point(20, 106);
            this.pnlEquivalenciaBanner.Name = "pnlEquivalenciaBanner";
            this.pnlEquivalenciaBanner.Size = new System.Drawing.Size(350, 42);
            this.pnlEquivalenciaBanner.Visible = false;

            // lblEquivalenciaMXN
            this.lblEquivalenciaMXN.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEquivalenciaMXN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(0))))); // Neon Yellow
            this.lblEquivalenciaMXN.Location = new System.Drawing.Point(8, 8);
            this.lblEquivalenciaMXN.Size = new System.Drawing.Size(332, 26);
            this.lblEquivalenciaMXN.Text = "📊 Equivalencia en Peso Mexicano: -";
            this.lblEquivalenciaMXN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // lblSubtituloTabla
            this.lblSubtituloTabla.AutoSize = true;
            this.lblSubtituloTabla.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.lblSubtituloTabla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(255)))), ((int)(((byte)(20))))); // Phosphor Green
            this.lblSubtituloTabla.Location = new System.Drawing.Point(20, 155);
            this.lblSubtituloTabla.Text = "DETALLE 8-BIT DE DENOMINACIONES:";

            // lstResultadoMonedas
            this.lstResultadoMonedas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.lstResultadoMonedas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstResultadoMonedas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lstResultadoMonedas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(0))))); // Neon Yellow
            this.lstResultadoMonedas.FormattingEnabled = true;
            this.lstResultadoMonedas.ItemHeight = 20;
            this.lstResultadoMonedas.Location = new System.Drawing.Point(20, 175);
            this.lstResultadoMonedas.Name = "lstResultadoMonedas";
            this.lstResultadoMonedas.Size = new System.Drawing.Size(350, 220);

            // pnlFooterStatus
            this.pnlFooterStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.pnlFooterStatus.Controls.Add(this.lblResumenPiezas);
            this.pnlFooterStatus.Location = new System.Drawing.Point(20, 402);
            this.pnlFooterStatus.Name = "pnlFooterStatus";
            this.pnlFooterStatus.Size = new System.Drawing.Size(350, 42);

            // lblResumenPiezas
            this.lblResumenPiezas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblResumenPiezas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(240)))), ((int)(((byte)(255))))); // Cyan
            this.lblResumenPiezas.Location = new System.Drawing.Point(10, 10);
            this.lblResumenPiezas.Size = new System.Drawing.Size(330, 22);
            this.lblResumenPiezas.Text = "PIEZAS TOTALES EN CAJA: 0";
            this.lblResumenPiezas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Form1 Main Setup
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(2)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(830, 610);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🕹️ RETRO ARCADE - CASINO & COIN CHANGE SYSTEM 👾";

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            this.pnlCardIzquierda.ResumeLayout(false);
            this.pnlCardIzquierda.PerformLayout();
            this.pnlCardDerecha.ResumeLayout(false);
            this.pnlCardDerecha.PerformLayout();
            this.pnlCambioBanner.ResumeLayout(false);
            this.pnlCambioBanner.PerformLayout();
            this.pnlEquivalenciaBanner.ResumeLayout(false);
            this.pnlFooterStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMonedas)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlHeaderAccent;
        private System.Windows.Forms.Label lblHeaderIcon;
        private System.Windows.Forms.Label lblTituloHeader;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlCardIzquierda;
        private System.Windows.Forms.Label lblTituloCardIzquierda;
        private System.Windows.Forms.PictureBox picMonedas;
        private System.Windows.Forms.Label lblDivisa;
        private System.Windows.Forms.ComboBox cmbMoneda;
        private System.Windows.Forms.Label lblCostoTotal;
        private System.Windows.Forms.TextBox txtCostoTotal;
        private System.Windows.Forms.Label lblMontoEntregado;
        private System.Windows.Forms.TextBox txtMontoEntregado;
        private System.Windows.Forms.Button btnCalcularMonedas;
        private System.Windows.Forms.Button btnProbarEjemplo;
        private System.Windows.Forms.Button btnLimpiarMonedas;
        private System.Windows.Forms.Panel pnlCardDerecha;
        private System.Windows.Forms.Label lblTituloCardDerecha;
        private System.Windows.Forms.Panel pnlCambioBanner;
        private System.Windows.Forms.Label lblCambioHeader;
        private System.Windows.Forms.Label lblCambioValor;
        private System.Windows.Forms.Panel pnlEquivalenciaBanner;
        private System.Windows.Forms.Label lblEquivalenciaMXN;
        private System.Windows.Forms.Label lblSubtituloTabla;
        private System.Windows.Forms.ListBox lstResultadoMonedas;
        private System.Windows.Forms.Panel pnlFooterStatus;
        private System.Windows.Forms.Label lblResumenPiezas;
    }
}
