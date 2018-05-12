using System;
using System.Web.UI;

namespace Nacion.WebUI
{
    public partial class Pagar : Page
    {
        private readonly NacionService.Service _service = new NacionService.Service();

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
            var cuota = _service.GetSiguienteCuota();
            lblNroSiguienteCuota.Text = cuota.Nro.ToString();
            decimal total = cuota.Capital + cuota.Interes + cuota.Cargos + cuota.Impuestos;
            lblMontoSiguienteCuota.Text = $"{total:c}";
            lblVencimientoSiguienteCuota.Text = _service.GetSiguienteVencimiento();
        }

        protected void lnkPagar_Click(object sender, EventArgs e)
        {
            _service.PagarCuota(Convert.ToInt32(lblNroSiguienteCuota.Text));
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
                _service.PagarCuota(nroSiguienteCuota);
                for (int i = nroSiguienteCuota + 1; i < nroSiguienteCuota + cantidad + 1; i++)
                {
                    _service.AdelantarCuota(i);
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
                    _service.AdelantarCuota(i);
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
                    _service.ResetearCuota(i);
                }
                ActualizarSiguienteCuota();
            }
        }
    }
}