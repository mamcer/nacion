using System;
using System.Web.UI;

namespace Nacion.WebUI
{
    public partial class Simulador : Page
    {
        private readonly NacionService.Service _service = new NacionService.Service();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var cuota = _service.GetSiguienteCuota();
                decimal total = cuota.Capital + cuota.Interes + cuota.Cargos + cuota.Impuestos;
                lblMontoSiguienteCuota.Text = $"{total:c}";
                lblVencimientoSiguienteCuota.Text = _service.GetSiguienteVencimiento();
                txtDinero.Focus();
            }
            else
            {
                if (txtDinero.Text != string.Empty)
                {
                    Simular();
                }
            }
        }

        protected void btnSimular_Click(object sender, EventArgs e)
        {
            Simular();
        }

        private void Simular()
        {
            if (txtDinero.Text != string.Empty)
            {
                decimal dinero;
                try
                {
                    dinero = Convert.ToDecimal(txtDinero.Text);
                }
                catch
                {
                    pnlResultado.Visible = false;
                    txtDinero.Focus(); 
                    return;
                }

                var resultado = _service.Simular(dinero);
                lblCapitalAdelantado.Text = $"{Convert.ToDecimal(resultado.CapitalAdelantado):c}";
                lblCuotasAdelantadas.Text = resultado.NroCuotasAdelantadas;
                lblDineroRestante.Text = $"{Convert.ToDecimal(resultado.DineroRestante):c}";
                lblInteresesAhorrados.Text = $"{Convert.ToDecimal(resultado.InteresesAdelantados):c}";
                lblNuevoVencimiento.Text = resultado.VencimientoActual;
                lblSiguienteNroCuota.Text = resultado.NroSiguienteCuota;
                txtDinero.Focus();
                pnlResultado.Visible = true;
            }
        }
    }
}