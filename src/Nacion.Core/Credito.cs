using System;
using System.Data;
using Nacion.DataLayer;

namespace Nacion.Core
{
    /// <summary>
    /// La clase base del modelo, permite tener acceso a toda la información del prestamo.
    /// </summary>
    public sealed class Credito
    {
        private static Credito _instancia;
        private readonly IDataLayerBase _dataLayer;

        private Credito() 
        {
            _dataLayer = DataLayerFactory.Instancia.GetDataLayer(DataLayerType.SqlServer);
        }

        /// <summary>
        /// Retorna la unica instancia del crédito.
        /// </summary>
        public static Credito Instancia => _instancia ?? (_instancia = new Credito());

        public IDataLayerBase DataLayer => _dataLayer;

        /// <summary>
        /// Retorna todas las cuotas del crédito en un DataTable.
        /// </summary>
        /// <returns>Un DataTable con todas las cuotas.</returns>
        public DataTable GetCuotas()
        {
            return _dataLayer.GetCuotas();
        }

        /// <summary>
        /// Retorna la siguiente cuota a pagar.
        /// </summary>
        /// <returns>Cuota.</returns>
        public Cuota GetSiguienteCuota()
        {
            DataRow dr = _dataLayer.GetSiguienteCuota();
            if (dr != null)
            {
                return CrearCuotaDesdeDataRow(dr);
            }

            return null;
        }

        private Cuota CrearCuotaDesdeDataRow(DataRow dr)
        {
            Cuota cuota = new Cuota
            {
                Nro = Convert.ToInt32(dr[DataLayerConstants.NRO]),
                Vencimiento = Convert.ToDateTime(dr[DataLayerConstants.VENCIMIENTO]),
                Capital = Convert.ToDecimal(dr[DataLayerConstants.CAPITAL]),
                Interes = Convert.ToDecimal(dr[DataLayerConstants.INTERES]),
                Cargos = Convert.ToDecimal(dr[DataLayerConstants.CARGOS]),
                Impuestos = Convert.ToDecimal(dr[DataLayerConstants.IMPUESTOS]),
                Status = (StatusCuota) Convert.ToInt32(dr[DataLayerConstants.STATUS])
            };

            return cuota;
        }

        /// <summary>
        /// Retorna el resto a pagar del crédito.
        /// </summary>
        /// <returns>Un decimal con el resto a pagar.</returns>
        public decimal GetRestoAPagar()
        {
            return _dataLayer.GetRestoAPagar();
        }

        /// <summary>
        /// Retorna el vencimiento actual del crédito según la cantidad de cuotas adelantadas.
        /// </summary>
        /// <returns>Un DateTime con la fecha de vencimiento actual</returns>
        public DateTime GetUltimoVencimientoActual()
        {
            return _dataLayer.GetUltimoVencimientoActual();
        }

        /// <summary>
        /// Retorna el total pagado del crédito a la fecha.
        /// </summary>
        /// <returns>Un decimal.</returns>
        public decimal GetTotalPagado()
        {
            return _dataLayer.GetTotalPagado();
        }

        /// <summary>
        /// Retorna la cantidad de cuotas pagas a la fecha.
        /// </summary>
        /// <returns>La cantidad de cuotas adelantadas.</returns>
        public int GetCantidadCuotasPagas()
        {
            return _dataLayer.GetCantidadCuotasPagas();
        }

        /// <summary>
        /// Retorna la cantidad de cuotas nuevas, es decir que no han sido ni pagadas ni adelantadas.
        /// </summary>
        /// <returns>La cantidad de cuotas nuevas.</returns>
        public int GetCantidadCuotasNuevas()
        {
            return _dataLayer.GetCantidadCuotasNuevas();
        }

        /// <summary>
        /// Retorna el primer vencimiento original del crédito.
        /// </summary>
        /// <returns>Un DateTime con la primer fecha.</returns>
        public DateTime GetPrimerVencimientoOriginal()
        {
            return _dataLayer.GetPrimerVencimientoOriginal();
        }

        /// <summary>
        /// Retorna el ultimo vencimiento original del crédito.
        /// </summary>
        /// <returns>Un DateTime con la ultima fecha.</returns>
        public DateTime GetUltimoVencimientoOriginal()
        {
            return _dataLayer.GetUltimoVencimientoOriginal();
        }

        /// <summary>
        /// Retorna información general del crédito como el Nro de cuenta, CBU, etc.
        /// </summary>
        /// <returns>Un InfoGeneral con los datos.</returns>
        public InfoGeneral GetInfoGeneral()
        {
            DataRow dr = _dataLayer.GetInfoGeneral();
            if (dr != null)
            {
                string clientId = dr[DataLayerConstants.CLIENTE].ToString();
                string number = dr[DataLayerConstants.NRO_PRESTAMO].ToString();
                decimal temRate = Convert.ToDecimal(dr[DataLayerConstants.TASA_TEM]);
                decimal tnavRate = Convert.ToDecimal(dr[DataLayerConstants.TASA_TNAV]);
                DateTime firstExpiration = Convert.ToDateTime(dr[DataLayerConstants.FECHA_PRIMER_VENCIMIENTO]);
                DateTime lastExpiration = Convert.ToDateTime(dr[DataLayerConstants.FECHA_ULTIMO_VENCIMIENTO]);
                decimal main = Convert.ToDecimal(dr[DataLayerConstants.CAPITAL]);
                string cbu = dr[DataLayerConstants.CBU].ToString();
                string accountNumber = dr[DataLayerConstants.NRO_CAJA_AHORRO].ToString();
                return new InfoGeneral(clientId, number, temRate, tnavRate, firstExpiration, lastExpiration, main, cbu, accountNumber);
            }
            else
            {
                return new InfoGeneral();
            }
        }

        /// <summary>
        /// Devuelve el número de cuota especificado como parámetro.
        /// </summary>
        /// <param name="nro"></param>
        /// <returns>La cuota número 'nro'</returns>
        public Cuota GetCuotaNro(int nro)
        {
            DataRow dr = _dataLayer.GetCuotaNro(nro);
            if (dr != null)
            {
                return CrearCuotaDesdeDataRow(dr);
            }

            return null;
        }

        /// <summary>
        /// Cambia el status de la cuota número 'nro' a Pagada.
        /// </summary>
        /// <param name="nro">el número de cuota a pagar.</param>
        public void PagarCuotaNro(int nro)
        {
            if (GetSiguienteCuota().Nro == nro)
            {
                if (GetCuotaNro(nro).Status == StatusCuota.Nueva)
                {
                    _dataLayer.CambiarStatusCuota(nro, (int)StatusCuota.Pagada);
                }
            }
        }

        /// <summary>
        /// Cambia el status de la cuota número 'nro' a Nueva.
        /// </summary>
        /// <param name="nro">el número de cuota.</param>
        public void ResetearCuotaNro(int nro)
        {
            _dataLayer.CambiarStatusCuota(nro, (int)StatusCuota.Nueva);
        }

        /// <summary>
        /// Cambia el status de la cuota número 'nro' a Adelantada.
        /// </summary>
        /// <param name="nro">el número de cuota.</param>
        public void AdelantarCuotaNro(int nro)
        {
            if (GetSiguienteCuota().Nro == nro)
            {
                if (GetCuotaNro(nro).Status == StatusCuota.Nueva)
                {
                    _dataLayer.CambiarStatusCuota(nro, (int)StatusCuota.Adelantada);
                }
            }
        }

        /// <summary>
        /// Permite simular el pago de cuotas para ver cuantas se adelantarían.
        /// </summary>
        /// <param name="dinero">El dinero a pagar.</param>
        /// <returns>Un objeto ResultadoSimulacion con todos los datos de la simulación.</returns>
        public ResultadoSimulacion Simular(decimal dinero)
        {
            string[] resultadoBd = _dataLayer.Simular(dinero);
            if (resultadoBd != null)
            {
                ResultadoSimulacion resultado = new ResultadoSimulacion();
                resultado.MapData(resultadoBd);
                return resultado;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna el vencimiento de la siguiente cuota a pagar, independientemente de las que se hayan adelantado.
        /// </summary>
        /// <returns>Un DateTime con la fecha.</returns>
        public DateTime GetVencimientoSiguienteCuota()
        {
            return _dataLayer.GetVencimientoSiguienteCuota();
        }

        /// <summary>
        /// Retorna la cantidad de cuotas adelantadas a la fecha.
        /// </summary>
        /// <returns>Un entero representando la cantidad de cuotas adelantadas.</returns>
        public int GetCantidadCuotasAdelantadas()
        {
            return _dataLayer.GetCantidadCuotasAdelantadas();
        }
    }
}
