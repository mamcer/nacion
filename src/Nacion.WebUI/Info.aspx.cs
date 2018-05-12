using System;
using System.Globalization;
using System.Web.UI;

namespace Nacion.WebUI
{
    public partial class Info : Page
    {
        private readonly NacionService.Service _service = new NacionService.Service();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                NacionService.InfoGeneral infoGeneral = _service.GetInfoGeneral();
                lblNroCliente.Text = infoGeneral.NroCliente;
                lblNroCajaAhorro.Text = infoGeneral.NroCajaAhorro;
                lblCBU.Text = infoGeneral.CBU;
                lblNroPrestamo.Text = infoGeneral.NroPrestamo;
                lblTasaTEM.Text = infoGeneral.TasaTEM.ToString(CultureInfo.InvariantCulture);
                lblTasaTNAV.Text = infoGeneral.TasaTNAV.ToString(CultureInfo.InvariantCulture);
                lblFechaPrimerVencimiento.Text = infoGeneral.PrimerVencimiento.ToShortDateString();
                lblFechaUltimoVencimiento.Text = infoGeneral.UltimoVencimiento.ToShortDateString();
                lblCapital.Text = $"{infoGeneral.Capital:c}";
            }
        }
    }
}