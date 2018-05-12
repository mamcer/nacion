using System;
using System.Data;

namespace Nacion.DataLayer
{
    public enum StatusCuota { Nueva = 1, Pagada = 2, Adelantada = 4 }
    
    /// <summary>
    /// Contiene las contstantes que utilizaran todas las DataLayer que implementen la interface
    /// </summary>
    public sealed class DataLayerConstants
    {
        #region constants

        public const string NRO = "Nro";
        public const string VENCIMIENTO = "Vencimiento";
        public const string CAPITAL = "Capital";
        public const string INTERES = "Interes";
        public const string CARGOS = "Cargos";
        public const string IMPUESTOS = "Impuestos";
        public const string TOTAL = "Total";
        public const string STATUS = "Status";

        public const string CLIENTE = "Cliente";
        public const string NRO_PRESTAMO = "NroPrestamo";
        public const string TASA_TEM = "TasaTEM";
        public const string TASA_TNAV = "TasaTNAV";
        public const string FECHA_PRIMER_VENCIMIENTO = "FechaPrimerVencimiento";
        public const string FECHA_ULTIMO_VENCIMIENTO = "FechaUltimoVencimiento";
        public const string CBU = "CBU";
        public const string NRO_CAJA_AHORRO = "CajaAhorroNro";
        #endregion
    }

    /// <summary>
    /// Es la interface que todos las DataLayer deben implementar
    /// </summary>
    public interface IDataLayerBase
    {
        #region methods
        
        DataTable GetCuotas();
        DataRow GetSiguienteCuota();
        decimal GetRestoAPagar();
        DateTime GetUltimoVencimientoActual();
        decimal GetTotalPagado();
        int GetCantidadCuotasPagas();
        int GetCantidadCuotasNuevas();
        DateTime GetPrimerVencimientoOriginal();
        DateTime GetUltimoVencimientoOriginal();
        DataRow GetInfoGeneral();
        DataRow GetCuotaNro(int nro);
        void CambiarStatusCuota(int nro, int status);
        string[] Simular(decimal dinero);
        DateTime GetVencimientoSiguienteCuota();
        int GetCantidadCuotasAdelantadas();

        #endregion
    }
}
