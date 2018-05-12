using System;
using System.Data;

namespace Nacion.DataLayer
{
    public enum StatusCuota { Nueva = 1, Pagada = 2, Adelantada = 4 }
    

    /// <summary>
    /// Es la interface que todos las DataLayer deben implementar
    /// </summary>
    public interface IDataLayerBase
    {
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
    }
}
