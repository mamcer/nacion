using System;
using System.Web.UI;

namespace Nacion.WebUI
{
    public partial class Info : System.Web.UI.Page
    {
        private NacionService.Service service = new Nacion.WebUI.NacionService.Service();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                NacionService.InfoGeneral infoGeneral = service.GetInfoGeneral();
                lblNroCliente.Text = infoGeneral.NroCliente;
                lblNroCajaAhorro.Text = infoGeneral.NroCajaAhorro;
                lblCBU.Text = infoGeneral.CBU;
                lblNroPrestamo.Text = infoGeneral.NroPrestamo;
                lblTasaTEM.Text = infoGeneral.TasaTEM.ToString();
                lblTasaTNAV.Text = infoGeneral.TasaTNAV.ToString();
                lblFechaPrimerVencimiento.Text = infoGeneral.PrimerVencimiento.ToShortDateString();
                lblFechaUltimoVencimiento.Text = infoGeneral.UltimoVencimiento.ToShortDateString();
                lblCapital.Text = string.Format("{0:c}", infoGeneral.Capital);
            }
        }
    }
}
