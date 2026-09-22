using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ejercicio4_Monedas.Clases;
using Ejercicio4_Monedas.Helpers;

namespace Ejercicio4_Monedas
{
    public partial class Form1 : Form
    {
        private List<ConfiguracionMoneda> _monedasDisponibles = new List<ConfiguracionMoneda>();

        public Form1()
        {
            InitializeComponent();
            CargarConfiguracionMonedas();
            picMonedas.Image = ImageHelper.CrearImagenMonedasFintech(330, 130);
        }

        private void CargarConfiguracionMonedas()
        {
            _monedasDisponibles = ConfiguracionMoneda.ObtenerMonedasDisponibles();
            cmbMoneda.DataSource = _monedasDisponibles;
            cmbMoneda.DisplayMember = "Nombre";
            cmbMoneda.SelectedIndex = 0; // MXN por defecto
        }

        private ConfiguracionMoneda MonedaSeleccionada => cmbMoneda.SelectedItem as ConfiguracionMoneda ?? _monedasDisponibles[0];

        private void cmbMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEtiquetasDivisa();
            if (!string.IsNullOrWhiteSpace(txtCostoTotal.Text) && !string.IsNullOrWhiteSpace(txtMontoEntregado.Text))
            {
                btnCalcularMonedas_Click(this, EventArgs.Empty);
            }
        }

        private void ActualizarEtiquetasDivisa()
        {
            var m = MonedaSeleccionada;
            lblCostoTotal.Text = $"Cantidad a Pagar / Costo ({m.Simbolo}):";
            lblMontoEntregado.Text = $"Monto Entregado / Pago ({m.Simbolo}):";

            if (m.Codigo == "MXN")
            {
                pnlEquivalenciaBanner.Visible = false;
            }
            else
            {
                pnlEquivalenciaBanner.Visible = true;
                lblEquivalenciaMXN.Text = $"📊 1 {m.Codigo} = ${m.TasaCambioMXN:N2} MXN (Tipo de cambio)";
            }

            if (lstResultadoMonedas.Items.Count == 0)
            {
                lblCambioValor.Text = $"{m.Simbolo} 0.00";
            }
        }

        private void ValidarEntradaDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;

            char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            if ((e.KeyChar == '.' || e.KeyChar == ',') && !textBox.Text.Contains(".") && !textBox.Text.Contains(","))
            {
                e.KeyChar = separator;
                return;
            }

            e.Handled = true;
        }

        private void btnCalcularMonedas_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCostoTotal.Text) || string.IsNullOrWhiteSpace(txtMontoEntregado.Text))
                {
                    MessageBox.Show("Hay datos faltantes.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtCostoTotal.Text.Trim(), out decimal costoTotal) ||
                    !decimal.TryParse(txtMontoEntregado.Text.Trim(), out decimal montoEntregado))
                {
                    MessageBox.Show("Introducir sólo números.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var monedaActual = MonedaSeleccionada;
                CalculadorMonedas oCalculador = new CalculadorMonedas(costoTotal, montoEntregado, monedaActual);
                List<ResultadoMoneda> desglose = oCalculador.CalcularDesglose();

                lblCambioValor.Text = $"{monedaActual.Simbolo} {oCalculador.CambioTotal:N2}";

                // Si la divisa no es Peso Mexicano, calcular y mostrar la equivalencia exacta en MXN
                if (monedaActual.Codigo != "MXN")
                {
                    decimal cambioMXN = oCalculador.CambioTotal * monedaActual.TasaCambioMXN;
                    pnlEquivalenciaBanner.Visible = true;
                    lblEquivalenciaMXN.Text = $"📊 1 {monedaActual.Codigo} = ${monedaActual.TasaCambioMXN:N2} MXN | Cambio: ${cambioMXN:N2} MXN";
                }
                else
                {
                    pnlEquivalenciaBanner.Visible = false;
                }

                lstResultadoMonedas.Items.Clear();
                int totalPiezas = 0;
                foreach (ResultadoMoneda item in desglose)
                {
                    lstResultadoMonedas.Items.Add(item.ToString());
                    totalPiezas += item.Cantidad;
                }

                lblResumenPiezas.Text = $"Piezas a entregar: {totalPiezas} ({monedaActual.Codigo})";
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error de Operación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LimpiarResultados();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LimpiarResultados();
            }
            catch (Exception)
            {
                MessageBox.Show("Introducir sólo números.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LimpiarResultados();
            }
        }

        private void btnLimpiarMonedas_Click(object sender, EventArgs e)
        {
            txtCostoTotal.Clear();
            txtMontoEntregado.Clear();
            LimpiarResultados();
            txtCostoTotal.Focus();
        }

        private void LimpiarResultados()
        {
            var m = MonedaSeleccionada;
            lblCambioValor.Text = $"{m?.Simbolo ?? "$MXN"} 0.00";
            lstResultadoMonedas.Items.Clear();
            lblResumenPiezas.Text = "Piezas a entregar: 0 monedas/billetes";

            if (m != null && m.Codigo != "MXN")
            {
                pnlEquivalenciaBanner.Visible = true;
                lblEquivalenciaMXN.Text = $"📊 1 {m.Codigo} = ${m.TasaCambioMXN:N2} MXN (Tipo de cambio)";
            }
            else
            {
                pnlEquivalenciaBanner.Visible = false;
            }
        }

        private void btnProbarEjemplo_Click(object sender, EventArgs e)
        {
            cmbMoneda.SelectedIndex = 0; // MXN
            txtCostoTotal.Text = "73.26";
            txtMontoEntregado.Text = "100.00";
            btnCalcularMonedas_Click(this, EventArgs.Empty);
        }
    }
}
