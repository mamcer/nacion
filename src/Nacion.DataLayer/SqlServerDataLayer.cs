using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace Nacion.DataLayer
{
    /// <summary>
    /// Esta clase implementa la interface IDataLayerBase para el motor SQL Server.
    /// </summary>
    internal class SqlServerDataLayer : IDataLayerBase
    {
        public SqlServerDataLayer()
        {
            if (ConfigurationManager.ConnectionStrings["BDNacion"] != null)
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["BDNacion"].ConnectionString;
            }
        }

        public string ConnectionString { get; set; }

        private object GetFirstResult(string spName)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = spName
                };

                DataTable dt = new DataTable("Result");
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0];
                }

                return null;
            }
        }

        public DataTable GetCuotas()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetAllFees"
                };

                DataTable dt = new DataTable("results");
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(dt);
                return dt;
            }
        }

        public DataRow GetSiguienteCuota()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetNextFee"
                };

                DataTable dt = new DataTable("results");
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0];
                }

                return null;
            }
        }

        public decimal GetRestoAPagar()
        {
            object obj = GetFirstResult("GetTotalLeft");
            if (obj != null)
            {
                return (decimal)obj;
            }

            return 0;
        }

        public DateTime GetUltimoVencimientoActual()
        {
            object obj = GetFirstResult("GetActualLastExpiration");
            if (obj != null)
            {
                return (DateTime)obj;
            }

            return DateTime.MinValue;
        }

        public decimal GetTotalPagado()
        {
            object obj = GetFirstResult("GetTotalPayed");
            if (obj != null)
            {
                return (decimal)obj;
            }

            return 0;
        }

        public int GetCantidadCuotasPagas()
        {
            return 240 - GetCantidadCuotasNuevas();
        }

        public int GetCantidadCuotasNuevas()
        {
            object obj = GetFirstResult("GetCountOfNewFees");
            if (obj != null)
            {
                return (int)obj;
            }

            return 0;
        }

        public DateTime GetPrimerVencimientoOriginal()
        {
            object obj = GetFirstResult("GetFirstExpiration");
            if (obj != null)
            {
                return (DateTime)obj;
            }

            return DateTime.MinValue;
        }

        public DateTime GetUltimoVencimientoOriginal()
        {
            object obj = GetFirstResult("GetLastExpiration");
            if (obj != null)
            {
                return (DateTime)obj;
            }

            return DateTime.MinValue;
        }

        public DataRow GetInfoGeneral()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetGeneralInfo"
                };

                DataTable dt = new DataTable("results");
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0];
                }

                return null;
            }
        }

        public DataRow GetCuotaNro(int nro)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetSpecificFee"
                };

                command.Parameters.Add(new SqlParameter("Nro", nro));
                DataTable dt = new DataTable("results");
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0];
                }

                return null;
            }
        }

        public void CambiarStatusCuota(int nro, int status)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "ChangeFeeStatus"
                };

                command.Parameters.Add(new SqlParameter("Nro", nro));
                command.Parameters.Add(new SqlParameter("Status", status));
                command.ExecuteNonQuery();
            }
        }

        public string[] Simular(decimal dinero)
        {
            DataTable dt = GetCuotas();
            string[] result = null;
            if (dt.Rows.Count > 0)
            {
                int payedFees = 0;
                decimal interest = 0;
                decimal main = 0;
                decimal rest = dinero;
                int nextFee = 0;
                int forwarded = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (((StatusCuota)dt.Rows[i][DataLayerConstants.STATUS]) != StatusCuota.Nueva)
                    {
                        if (((StatusCuota)dt.Rows[i][DataLayerConstants.STATUS]) == StatusCuota.Adelantada)
                        {
                            forwarded += 1;
                        }
                        continue;
                    }
                    if (rest != dinero)
                    {
                        if ((rest - Convert.ToDecimal(dt.Rows[i][DataLayerConstants.CAPITAL])) > 0)
                        {
                            rest -= Convert.ToDecimal(dt.Rows[i][DataLayerConstants.CAPITAL]);
                            interest += Convert.ToDecimal(dt.Rows[i][DataLayerConstants.INTERES]);
                            main += Convert.ToDecimal(dt.Rows[i][DataLayerConstants.CAPITAL]);
                            payedFees += 1;
                            nextFee = i + 2;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        rest -= Convert.ToDecimal(dt.Rows[i][DataLayerConstants.TOTAL]);
                        if (rest < 0)
                        {
                            break;
                        }
                    }
                }

                result = new string[6];
                result[0] = Convert.ToString(payedFees);
                result[1] = ((DateTime)dt.Rows[dt.Rows.Count - forwarded - payedFees - 1][DataLayerConstants.VENCIMIENTO]).ToShortDateString();
                result[2] = Convert.ToString(interest, CultureInfo.InvariantCulture);
                result[3] = Convert.ToString(main, CultureInfo.InvariantCulture);
                result[4] = Convert.ToString(nextFee);
                result[5] = Convert.ToString(rest, CultureInfo.InvariantCulture);
            }
            return result;
        }

        public DateTime GetVencimientoSiguienteCuota()
        {
            object obj = GetFirstResult("GetNextExpiration");
            if (obj != null)
            {
                return (DateTime)obj;
            }

            return DateTime.MinValue;
        }

        public int GetCantidadCuotasAdelantadas()
        {
            object obj = GetFirstResult("GetCountOfForwardedFees");
            if (obj != null)
            {
                return (int)obj;
            }

            return 0;
        }
    }
}