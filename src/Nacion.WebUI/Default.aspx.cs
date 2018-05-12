using System;
using System.Web.UI;

namespace Nacion.WebUI
{
    public partial class Default1 : Page
    {
        private readonly NacionService.Service _service = new NacionService.Service();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Siguiente cuota
                var cuota = _service.GetSiguienteCuota();
                lblNroSiguienteCuota.Text = cuota.Nro.ToString();
                decimal total = cuota.Capital + cuota.Interes + cuota.Cargos + cuota.Impuestos;
                lblMontoSiguienteCuota.Text = $"{total:c}";
                lblVencimientoSiguienteCuota.Text = _service.GetSiguienteVencimiento();

                //Información general del crédito
                lblTotalPagado.Text = $"{Convert.ToDecimal(_service.GetTotalPagado()):c}";
                lblCuotasPagas.Text = _service.GetCantidadCuotasPagas();
                lblCuotasAdelantadas.Text = _service.GetCantidadCuotasAdelantadas();
                lblRestoPagar.Text = $"{Convert.ToDecimal(_service.GetRestoPagar()):c}";
                lblVencimientoOriginal.Text = _service.GetVencimientoOriginal();
                lblVencimientoActual.Text = _service.GetVencimientoActual();
            }
        }
    }
}