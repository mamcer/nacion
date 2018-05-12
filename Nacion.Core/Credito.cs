using System;
using System.Data;

using Nacion.DataLayer;

namespace Nacion.Core
{
    /// <summary>
    /// Contiene los datos del resultado de una simulación.
    /// </summary>
    public sealed class ResultadoSimulacion
    {
        #region public properties

        public string NroCuotasAdelantadas
        {
            get;
            set;
        }

        public string CapitalAdelantado
        {
            get;
            set;
        }

        public string InteresesAdelantados
        {
            get;
            set;
        }

        public string VencimientoActual
        {
            get;
            set;
        }

        public string NroSiguienteCuota
        {
            get;
            set;
        }

        public string DineroRestante
        {
            get;
            set;
        }
        
        #endregion

        #region public methods

        internal void MapData(string[] mp)
        {
            if (mp != null)
            {
                NroCuotasAdelantadas = mp[0];
                VencimientoActual = mp[1];
                InteresesAdelantados = mp[2];
                CapitalAdelantado = mp[3];
                NroSiguienteCuota = mp[4];
                DineroRestante = mp[5];
            }
        }

        #endregion
    }

    /// <summary>
    /// Contiene Información General del crédito como el Id del cliente, CBU, etc.
    /// </summary>
    public struct InfoGeneral
    {
        public string NroCliente;
        public string NroPrestamo;
        public decimal TasaTEM;
        public decimal TasaTNAV;
        public DateTime PrimerVencimiento;
        public DateTime UltimoVencimiento;
        public decimal Capital;
        public string CBU;
        public string NroCajaAhorro;

        public InfoGeneral(string ci, string n, decimal tem, decimal tnav, DateTime f, DateTime l, decimal m, string cbu, string an)
        {
            NroCliente = ci;
            NroPrestamo = n;
            TasaTEM = tem;
            TasaTNAV = tnav;
            PrimerVencimiento = f;
            UltimoVencimiento = l;
            Capital = m;
            CBU = cbu;
            NroCajaAhorro = an;
        }
    }

    /// <summary>
    /// La clase base del modelo, permite tener acceso a toda la información del prestamo.
    /// </summary>
    public sealed class Credito
    {
        #region private members
        private static Credito instancia;
        private IDataLayerBase dataLayer;
        #endregion

        #region constructor
        private Credito() 
        {
            dataLayer = DataLayerFactory.Instancia.GetDataLayer(DataLayerType.SqlServer);
        }
        #endregion

        #region public properties
        /// <summary>
        /// Retorna la unica instancia del crédito.
        /// </summary>
        public static Credito Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Credito();
                }
                return instancia;
            }
        }

        public IDataLayerBase DataLayer
        {
            get
            {
                return this.dataLayer;
            }
        }
        #endregion

        #region public methods

        /// <summary>
        /// Retorna todas las cuotas del crédito en un DataTable.
        /// </summary>
        /// <returns>Un DataTable con todas las cuotas.</returns>
        public DataTable GetCuotas()
        {
            return this.dataLayer.GetCuotas();
        }

        /// <summary>
        /// Retorna la siguiente cuota a pagar.
        /// </summary>
        /// <returns>Cuota.</returns>
        public Cuota GetSiguienteCuota()
        {
            DataRow dr = this.dataLayer.GetSiguienteCuota();
            if (dr != null)
            {
                return CrearCuotaDesdeDataRow(dr);
            }
            else
            {
                return null;
            }
        }

        private Cuota CrearCuotaDesdeDataRow(DataRow dr)
        {
            Cuota cuota = new Cuota(); 
            cuota.Nro = Convert.ToInt32(dr[DataLayerConstants.NRO]);
            cuota.Vencimiento = Convert.ToDateTime(dr[DataLayerConstants.VENCIMIENTO]);
            cuota.Capital = Convert.ToDecimal(dr[DataLayerConstants.CAPITAL]);
            cuota.Interes = Convert.ToDecimal(dr[DataLayerConstants.INTERES]);
            cuota.Cargos = Convert.ToDecimal(dr[DataLayerConstants.CARGOS]);
            cuota.Impuestos = Convert.ToDecimal(dr[DataLayerConstants.IMPUESTOS]);
            cuota.Status = (StatusCuota)Convert.ToInt32(dr[DataLayerConstants.STATUS]);
            return cuota;
        }

        /// <summary>
        /// Retorna el resto a pagar del crédito.
        /// </summary>
        /// <returns>Un decimal con el resto a pagar.</returns>
        public decimal GetRestoAPagar()
        {
            return this.dataLayer.GetRestoAPagar();
        }

        /// <summary>
        /// Retorna el vencimiento actual del crédito según la cantidad de cuotas adelantadas.
        /// </summary>
        /// <returns>Un DateTime con la fecha de vencimiento actual</returns>
        public DateTime GetUltimoVencimientoActual()
        {
            return this.dataLayer.GetUltimoVencimientoActual();
        }

        /// <summary>
        /// Retorna el total pagado del crédito a la fecha.
        /// </summary>
        /// <returns>Un decimal.</returns>
        public decimal GetTotalPagado()
        {
            return this.dataLayer.GetTotalPagado();
        }

        /// <summary>
        /// Retorna la cantidad de cuotas pagas a la fecha.
        /// </summary>
        /// <returns>La cantidad de cuotas adelantadas.</returns>
        public int GetCantidadCuotasPagas()
        {
            return this.dataLayer.GetCantidadCuotasPagas();
        }

        /// <summary>
        /// Retorna la cantidad de cuotas nuevas, es decir que no han sido ni pagadas ni adelantadas.
        /// </summary>
        /// <returns>La cantidad de cuotas nuevas.</returns>
        public int GetCantidadCuotasNuevas()
        {
            return this.dataLayer.GetCantidadCuotasNuevas();
        }

        /// <summary>
        /// Retorna el primer vencimiento original del crédito.
        /// </summary>
        /// <returns>Un DateTime con la primer fecha.</returns>
        public DateTime GetPrimerVencimientoOriginal()
        {
            return this.dataLayer.GetPrimerVencimientoOriginal();
        }

        /// <summary>
        /// Retorna el ultimo vencimiento original del crédito.
        /// </summary>
        /// <returns>Un DateTime con la ultima fecha.</returns>
        public DateTime GetUltimoVencimientoOriginal()
        {
            return this.dataLayer.GetUltimoVencimientoOriginal();
        }

        /// <summary>
        /// Retorna información general del crédito como el Nro de cuenta, CBU, etc.
        /// </summary>
        /// <returns>Un InfoGeneral con los datos.</returns>
        public InfoGeneral GetInfoGeneral()
        {
            DataRow dr = this.dataLayer.GetInfoGeneral();
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
            DataRow dr = this.dataLayer.GetCuotaNro(nro);
            if (dr != null)
            {
                return CrearCuotaDesdeDataRow(dr);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Cambia el status de la cuota número 'nro' a Pagada.
        /// </summary>
        /// <param name="nro">el número de cuota a pagar.</param>
        public void PagarCuotaNro(int nro)
        {
            if (this.GetSiguienteCuota().Nro == nro)
            {
                if (this.GetCuotaNro(nro).Status == StatusCuota.Nueva)
                {
                    this.dataLayer.CambiarStatusCuota(nro, (int)StatusCuota.Pagada);
                }
            }
        }

        /// <summary>
        /// Cambia el status de la cuota número 'nro' a Nueva.
        /// </summary>
        /// <param name="nro">el número de cuota.</param>
        public void ResetearCuotaNro(int nro)
        {
            this.dataLayer.CambiarStatusCuota(nro, (int)StatusCuota.Nueva);
        }

        /// <summary>
        /// Cambia el status de la cuota número 'nro' a Adelantada.
        /// </summary>
        /// <param name="nro">el número de cuota.</param>
        public void AdelantarCuotaNro(int nro)
        {
            if (this.GetSiguienteCuota().Nro == nro)
            {
                if (this.GetCuotaNro(nro).Status == StatusCuota.Nueva)
                {
                    this.dataLayer.CambiarStatusCuota(nro, (int)StatusCuota.Adelantada);
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
            string[] resultadoBD = this.dataLayer.Simular(dinero);
            if (resultadoBD != null)
            {
                ResultadoSimulacion resultado = new ResultadoSimulacion();
                resultado.MapData(resultadoBD);
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
            return this.dataLayer.GetVencimientoSiguienteCuota();
        }

        /// <summary>
        /// Retorna la cantidad de cuotas adelantadas a la fecha.
        /// </summary>
        /// <returns>Un entero representando la cantidad de cuotas adelantadas.</returns>
        public int GetCantidadCuotasAdelantadas()
        {
            return this.dataLayer.GetCantidadCuotasAdelantadas();
        }

        #endregion
    }
}
