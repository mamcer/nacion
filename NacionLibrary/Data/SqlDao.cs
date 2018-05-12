using System;
using System.Data;
using System.Data.SqlClient;

namespace Nacion.Data
{
    public struct GeneralInfo
    {
        public string ClientId;
        public string Number;
        public decimal TEMRate;
        public decimal TNAVRate;
        public DateTime FirstExpiration;
        public DateTime LastExpiration;
        public decimal Main;
        public string CBU;
        public string AccountNumber;

        public GeneralInfo(string ci, string n, decimal tem, decimal tnav, DateTime f, DateTime l, decimal m, string cbu, string an)
        {
            ClientId = ci;
            Number = n;
            TEMRate = tem;
            TNAVRate = tnav;
            FirstExpiration = f;
            LastExpiration = l;
            Main = m;
            CBU = cbu;
            AccountNumber = an;
        }
    }

    public sealed class SqlDao
    {
        #region private fields
        private string _connString;
        #endregion

        #region public properties
        public string ConnectionString
        {
            get
            {
                if (_connString == null)
                {
                    _connString = "Data Source=Hades\\Hades;Initial Catalog=Nacion;UID=sa;pwd=paladin22;Timeout=10";
                }
                return _connString;
            }
        }
        #endregion

        #region public fields
        private object GetFirstResult(string spName)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = spName;
                DataTable dt = new DataTable("Result");
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0];
                }
                else
                {
                    return null;
                }
            }
        }

        public string GetFirstExpiration()
        {
            object obj = GetFirstResult("GetFirstExpiration");
            if (obj != null)
            {
                return ((DateTime)obj).ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetLastExpiration()
        {
            object obj = GetFirstResult("GetLastExpiration");
            if (obj != null)
            {
                return ((DateTime)obj).ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetActualLastExpiration()
        {
            object obj = GetFirstResult("GetActualLastExpiration");
            if (obj != null)
            {
                return ((DateTime)obj).ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }

        public decimal GetTotalLeft()
        {
            object obj = GetFirstResult("GetTotalLeft");
            if (obj != null)
            {
                return (decimal)obj;
            }
            else
            {
                return 0;
            }
        }

        public string GetNextExpiration()
        {
            object obj = GetFirstResult("GetNextExpiration");
            if (obj != null)
            {
                return ((DateTime)obj).ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }

        public decimal GetNextFee()
        {
            object obj = GetFirstResult("GetNextTotal");
            if (obj != null)
            {
                return (decimal)obj;
            }
            else
            {
                return 0;
            }
        }

        public decimal GetTotalPayed()
        {
            object obj = GetFirstResult("GetTotalPayed");
            if (obj != null)
            {
                return (decimal)obj;
            }
            else
            {
                return 0;
            }
        }

        public int GetCountOfNewFees()
        {
            object obj = GetFirstResult("GetCountOfNewFees");
            if (obj != null)
            {
                return (int)obj;
            }
            else
            {
                return 0;
            }
        }

        public int GetCountOfForwardedFees()
        {
            object obj = GetFirstResult("GetCountOfForwardedFees");
            if (obj != null)
            {
                return (int)obj;
            }
            else
            {
                return 0;
            }
        }

        public string[] Simulate(decimal money)
        {
            DataTable dt = Credit.Instance.GetAllFees();
            string[] result = null;
            if (dt.Rows.Count > 0)
            {
                int payedFees = 0;
                decimal interest = 0;
                decimal main = 0;
                decimal rest = money;
                int nextFee = 0;
                int forwarded = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (((Constants.FeeStatus)dt.Rows[i][Constants.STATUS]) != Constants.FeeStatus.New)
                    {
                        if (((Constants.FeeStatus)dt.Rows[i][Constants.STATUS]) == Constants.FeeStatus.Forwarded)
                        {
                            forwarded += 1;
                        }
                        continue;
                    }
                    if (rest != money)
                    {
                        if ((rest - Convert.ToDecimal(dt.Rows[i][Constants.MAIN])) > 0)
                        {
                            rest -= Convert.ToDecimal(dt.Rows[i][Constants.MAIN]);
                            interest += Convert.ToDecimal(dt.Rows[i][Constants.INTEREST]);
                            main += Convert.ToDecimal(dt.Rows[i][Constants.MAIN]);
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
                        rest -= Convert.ToDecimal(dt.Rows[i][Constants.TOTAL]);
                        if (rest < 0)
                        {
                            break;
                        }
                    }
                }

                result = new string[6];
                result[0] = Convert.ToString(payedFees);
                result[1] = ((DateTime)dt.Rows[dt.Rows.Count - forwarded - payedFees - 1][Constants.EXPIRATION]).ToShortDateString();
                result[2] = Convert.ToString(interest);
                result[3] = Convert.ToString(main);
                result[4] = Convert.ToString(nextFee);
                result[5] = Convert.ToString(rest);
            }
            return result;
        }

        public DataTable GetAllFees()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetAllFees";
                DataTable dt = new DataTable("results");
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(dt);
                return dt;
            }
        }

        public GeneralInfo GetGeneralInfo()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetGeneralInfo";
                DataTable dt = new DataTable("results");
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(command);
                sqlAdapter.Fill(dt);

                string clientId = dt.Rows[0]["Cliente"].ToString();
                string number = dt.Rows[0]["NroPrestamo"].ToString();
                decimal temRate = Convert.ToDecimal(dt.Rows[0]["TasaTEM"]);
                decimal tnavRate = Convert.ToDecimal(dt.Rows[0]["TasaTNAV"]);
                DateTime firstExpiration = Convert.ToDateTime(dt.Rows[0]["FechaPrimerVencimiento"]);
                DateTime lastExpiration = Convert.ToDateTime(dt.Rows[0]["FechaUltimoVencimiento"]);
                decimal main = Convert.ToDecimal(dt.Rows[0]["Capital"]);
                string cbu = dt.Rows[0]["CBU"].ToString();
                string accountNumber = dt.Rows[0]["CajaAhorroNro"].ToString();
                return new GeneralInfo(clientId, number, temRate, tnavRate, firstExpiration, lastExpiration, main, cbu, accountNumber);
            }
        }
        #endregion
    }
}
