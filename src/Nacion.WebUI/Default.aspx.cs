using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nacion.WebUI
{
    public partial class Default1 : System.Web.UI.Page
    {
        private NacionService.Service service = new Nacion.WebUI.NacionService.Service();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Siguiente cuota
                Nacion.WebUI.NacionService.Cuota cuota = this.service.GetSiguienteCuota();
                lblNroSiguienteCuota.Text = cuota.Nro.ToString();
                decimal total = cuota.Capital + cuota.Interes + cuota.Cargos + cuota.Impuestos;
                lblMontoSiguienteCuota.Text = string.Format("{0:c}", total);
                lblVencimientoSiguienteCuota.Text = this.service.GetSiguienteVencimiento();

                //Información general del crédito
                lblTotalPagado.Text = string.Format("{0:c}", Convert.ToDecimal(this.service.GetTotalPagado()));
                lblCuotasPagas.Text = this.service.GetCantidadCuotasPagas();
                lblCuotasAdelantadas.Text = this.service.GetCantidadCuotasAdelantadas();
                lblRestoPagar.Text = string.Format("{0:c}", Convert.ToDecimal(this.service.GetRestoPagar()));
                lblVencimientoOriginal.Text = this.service.GetVencimientoOriginal();
                lblVencimientoActual.Text = this.service.GetVencimientoActual();
            }
        }
    }
}
