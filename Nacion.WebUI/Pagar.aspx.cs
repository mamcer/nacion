using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nacion.WebUI
{
    public partial class Pagar : System.Web.UI.Page
    {
        private NacionService.Service service = new Nacion.WebUI.NacionService.Service();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ActualizarSiguienteCuota();
                txtPagarAdelantar.Focus();
            }
        }

        private void ActualizarSiguienteCuota()
        {
            //Siguiente cuota
            Nacion.WebUI.NacionService.Cuota cuota = this.service.GetSiguienteCuota();
            lblNroSiguienteCuota.Text = cuota.Nro.ToString();
            decimal total = cuota.Capital + cuota.Interes + cuota.Cargos + cuota.Impuestos;
            lblMontoSiguienteCuota.Text = string.Format("{0:c}", total);
            lblVencimientoSiguienteCuota.Text = this.service.GetSiguienteVencimiento();
        }

        protected void lnkPagar_Click(object sender, EventArgs e)
        {
            service.PagarCuota(Convert.ToInt32(lblNroSiguienteCuota.Text));
            ActualizarSiguienteCuota();
        }

        protected void btnPagarAdelantar_Click(object sender, EventArgs e)
        {
            if (txtPagarAdelantar.Text != string.Empty)
            {
                int cantidad;
                try
                {
                    cantidad = Convert.ToInt32(txtPagarAdelantar.Text);
                }
                catch
                {
                    txtPagarAdelantar.Focus();
                    return;
                }
                int nroSiguienteCuota = Convert.ToInt32(lblNroSiguienteCuota.Text);
                service.PagarCuota(nroSiguienteCuota);
                for (int i = nroSiguienteCuota + 1; i < nroSiguienteCuota + cantidad + 1; i++)
                {
                    service.AdelantarCuota(i);
                }
                ActualizarSiguienteCuota();
            }
        }

        protected void btnAdelantar_Click(object sender, EventArgs e)
        {
            if (txtAdelantar.Text != string.Empty)
            {
                int cantidad;
                try
                {
                    cantidad = Convert.ToInt32(txtAdelantar.Text);
                }
                catch
                {
                    txtAdelantar.Focus();
                    return;
                }
                int nroSiguienteCuota = Convert.ToInt32(lblNroSiguienteCuota.Text);
                for (int i = nroSiguienteCuota; i < nroSiguienteCuota + cantidad; i++)
                {
                    service.AdelantarCuota(i);
                }
                ActualizarSiguienteCuota();
            }
        }

        protected void btnResetear_Click(object sender, EventArgs e)
        {
            if (txtResetear.Text != string.Empty)
            {
                int cantidad;
                try
                {
                    cantidad = Convert.ToInt32(txtResetear.Text);
                }
                catch
                {
                    txtResetear.Focus();
                    return;
                }
                int nroSiguienteCuota = Convert.ToInt32(lblNroSiguienteCuota.Text);
                for (int i = nroSiguienteCuota - 1; i > nroSiguienteCuota - cantidad - 1; i--)
                {
                    service.ResetearCuota(i);
                }
                ActualizarSiguienteCuota();
            }
        }
    }
}
