using System.Data;
using System.Globalization;
using System.Web.Services;

using Nacion.Core;

namespace Nacion.Services
{
    /// <summary>
    /// Este es el servicio que dá acceso al Credito.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : WebService
    {
        /// <summary>
        /// Retorna toda la información correspondiente a la siguiente cuota a pagar.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public Cuota GetSiguienteCuota()
        {
            return Credito.Instancia.GetSiguienteCuota();
        }

        /// <summary>
        /// Retorna la fecha del próximo vencimiento.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetSiguienteVencimiento()
        {
            return Credito.Instancia.GetVencimientoSiguienteCuota().ToShortDateString();
        }

        /// <summary>
        /// Retorna el total pagado del crédito al momento.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetTotalPagado()
        {
            return Credito.Instancia.GetTotalPagado().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Retorna el número de cuotas pagas.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetCantidadCuotasPagas()
        {
            return Credito.Instancia.GetCantidadCuotasPagas().ToString();
        }

        /// <summary>
        /// Retorna la cantidad de cuotas adelantadas.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetCantidadCuotasAdelantadas()
        {
            return Credito.Instancia.GetCantidadCuotasAdelantadas().ToString();
        }

        /// <summary>
        /// Retorna el monto total del crédito restante a pagar.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetRestoPagar()
        {
            return Credito.Instancia.GetRestoAPagar().ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Retorna el último vencimiento original del crédito.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetVencimientoOriginal()
        {
            return Credito.Instancia.GetUltimoVencimientoOriginal().ToShortDateString();
        }

        /// <summary>
        /// Retorna el último vencimiento actual del crédito, considerando las cuotas adelantadas.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetVencimientoActual()
        {
            return Credito.Instancia.GetUltimoVencimientoActual().ToShortDateString();
        }

        /// <summary>
        /// Retorna todas las cuotas del crédito.
        /// </summary>
        /// <returns>Un DataTable con todas las cuotas.</returns>
        [WebMethod]
        public DataTable GetCuotas()
        {
            return Credito.Instancia.GetCuotas();
        }

        /// <summary>
        /// Retorna el resultado de una simulación.
        /// </summary>
        /// <param name="dinero"></param>
        /// <returns>Un objeto ResultadoSimulacion con toda la info de la simulación.</returns>
        [WebMethod]
        public ResultadoSimulacion Simular(decimal dinero)
        {
            return Credito.Instancia.Simular(dinero);
        }

        /// <summary>
        /// Retorna la Información General del crédito como CBU, Nro de caja de ahorro, etc.
        /// </summary>
        /// <returns>Un objeto InfoGeneral.</returns>
        [WebMethod]
        public InfoGeneral GetInfoGeneral()
        {
            return Credito.Instancia.GetInfoGeneral();
        }

        /// <summary>
        /// Permite cambiar el status a pagado del nro de cuota pasado por parámetro.
        /// </summary>
        /// <param name="nro"></param>
        [WebMethod]
        public void PagarCuota(int nro)
        {
            Credito.Instancia.PagarCuotaNro(nro);
        }

        /// <summary>
        /// Permite cambiar el status a adelantada de la cuota pasada por parámetro.
        /// </summary>
        /// <param name="nro"></param>
        [WebMethod]
        public void AdelantarCuota(int nro)
        {
            Credito.Instancia.AdelantarCuotaNro(nro);
        }

        /// <summary>
        /// Permite cambiar el status a nueva de la cuota pasada por parámetro.
        /// </summary>
        /// <param name="nro"></param>
        [WebMethod]
        public void ResetearCuota(int nro)
        {
            Credito.Instancia.ResetearCuotaNro(nro);
        }
    }
}