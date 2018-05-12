using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nacion.WebUI
{
    public partial class Simulador : System.Web.UI.Page
    {
        private NacionService.Service service = new Nacion.WebUI.NacionService.Service();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Nacion.WebUI.NacionService.Cuota cuota = this.service.GetSiguienteCuota();
                decimal total = cuota.Capital + cuota.Interes + cuota.Cargos + cuota.Impuestos;
                lblMontoSiguienteCuota.Text = string.Format("{0:c}", total);
                lblVencimientoSiguienteCuota.Text = this.service.GetSiguienteVencimiento();
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
                Nacion.WebUI.NacionService.ResultadoSimulacion resultado = this.service.Simular(dinero);
                lblCapitalAdelantado.Text = string.Format("{0:c}", Convert.ToDecimal(resultado.CapitalAdelantado));
                lblCuotasAdelantadas.Text = resultado.NroCuotasAdelantadas;
                lblDineroRestante.Text = string.Format("{0:c}", Convert.ToDecimal(resultado.DineroRestante));
                lblInteresesAhorrados.Text = string.Format("{0:c}", Convert.ToDecimal(resultado.InteresesAdelantados));
                lblNuevoVencimiento.Text = resultado.VencimientoActual;
                lblSiguienteNroCuota.Text = resultado.NroSiguienteCuota;
                txtDinero.Focus();
                pnlResultado.Visible = true;
            }
        }
    }
}
