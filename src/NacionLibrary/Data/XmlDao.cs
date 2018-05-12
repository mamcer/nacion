using System;
using System.Data;

namespace Nacion.Data
{
    /// <summary>
    /// Summary description for XmlDao
    /// </summary>
    public sealed class XmlDao : Dao
    {
        private DataTable _dt = null;
        private const string XML_FILE_PATH = "./Data/fees.xml";

        public DataTable Fees
        {
            get 
            {
                if (_dt == null)
                {
                    _dt = GetFeesDataTable();
                    _dt.ReadXml(@"C:\Users\Necromancer\Documents\Visual Studio 2008\Projects\Nacion\NacionLibrary\Data\fees.xml");
                }
                return _dt;
            }
            set
            {
                _dt = value;
            }
        }

        public override DataTable GetFees()
        {
            return Fees;
        }

        public override string GetFirstExpiration()
        {
            if (Fees.Rows.Count > 0)
            {
                return ((DateTime)Fees.Rows[0][Constants.EXPIRATION]).ToShortDateString();
            }
            return string.Empty;
        }

        public override string GetLastExpiration()
        {
            if (Fees.Rows.Count > 0)
            {
                return ((DateTime)Fees.Rows[Fees.Rows.Count - 1][Constants.EXPIRATION]).ToShortDateString();
            }
            return string.Empty;
        }

        public override string GetActualLastExpiration()
        {
            if (Fees.Rows.Count > 0)
            {
                int forwarded = 0;
                for (int i = 0; i < Fees.Rows.Count; i++)
                {
                    if (((Constants.FeeStatus)Fees.Rows[i][Constants.STATUS]) == Constants.FeeStatus.New)
                    {
                        break;
                    }
                    else
                    {
                        if (((Constants.FeeStatus)Fees.Rows[i][Constants.STATUS]) == Constants.FeeStatus.Forwarded)
                        {
                            forwarded += 1;
                        }
                    }
                }
                return ((DateTime)Fees.Rows[Fees.Rows.Count - forwarded - 1][Constants.EXPIRATION]).ToShortDateString();
            }
            return DateTime.MinValue.ToShortDateString();
        }

        public override decimal GetRest()
        {
            if (Fees.Rows.Count > 0)
            {
                decimal rest = 0;
                for (int i = 0; i < Fees.Rows.Count; i++)
                {
                    if (((Constants.FeeStatus)Fees.Rows[i][Constants.STATUS]) == Constants.FeeStatus.New)
                    {
                        rest += Convert.ToDecimal(Fees.Rows[i][Constants.TOTAL]);
                    }
                }
                return rest;
            }
            return decimal.MaxValue;
        }

        public override string GetNextExpiration()
        {
            if (Fees.Rows.Count > 0)
            {
                for (int i = 0; i < Fees.Rows.Count; i++)
                {
                    if (((Constants.FeeStatus)Fees.Rows[i][Constants.STATUS]) == Constants.FeeStatus.New)
                    {
                        return ((DateTime)Fees.Rows[i][Constants.EXPIRATION]).ToShortDateString();
                    }
                }
            }
            return string.Empty;
        }

        public override decimal GetNextFee()
        {
            if (Fees.Rows.Count > 0)
            {
                for (int i = 0; i < Fees.Rows.Count; i++)
                {
                    if (((Constants.FeeStatus)Fees.Rows[i][Constants.STATUS]) == Constants.FeeStatus.New)
                    {
                        return Convert.ToDecimal(Fees.Rows[i][Constants.TOTAL]);
                    }
                }
            }
            return decimal.MaxValue;
        }

        public override int GetCountOfNewFees()
        {
            if (Fees.Rows.Count > 0)
            {
                int restOfNewFees = Fees.Rows.Count;
                int payedFees = 0;
                for (int i = 0; i < Fees.Rows.Count; i++)
                {
                    if (((Constants.FeeStatus)Fees.Rows[i][Constants.STATUS]) != Constants.FeeStatus.New)
                    {
                        payedFees += 1;
                    }
                }
                return restOfNewFees - payedFees;
            }
            return int.MaxValue;
        }

        public override string[] Simulate(decimal money)
        {
            string[] result = null;
            if (Fees.Rows.Count > 0)
            {
                result = new string[6];
                int payedFees = 0;
                decimal interest = 0;
                decimal main = 0;
                decimal rest = money;
                int nextFee = 0;
                int forwarded = 0;
                for (int i = 0; i < Fees.Rows.Count; i++)
                {
                    if (((Constants.FeeStatus)Fees.Rows[i][Constants.STATUS]) != Constants.FeeStatus.New)
                    {
                        if (((Constants.FeeStatus)Fees.Rows[i][Constants.STATUS]) == Constants.FeeStatus.Forwarded)
                        {
                            forwarded += 1;
                        }
                        continue;
                    }
                    if (rest != money)
                    {
                        if ((rest - Convert.ToDecimal(Fees.Rows[i][Constants.MAIN])) > 0)
                        {
                            rest -= Convert.ToDecimal(Fees.Rows[i][Constants.MAIN]);
                            interest += Convert.ToDecimal(Fees.Rows[i][Constants.INTEREST]);
                            main += Convert.ToDecimal(Fees.Rows[i][Constants.MAIN]);
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
                        rest -= Convert.ToDecimal(Fees.Rows[i][Constants.TOTAL]);
                        if (rest < 0)
                        {
                            break;
                        }
                    }
                }

                result[0] = Convert.ToString(payedFees);
                result[1] = ((DateTime)Fees.Rows[Fees.Rows.Count - forwarded - payedFees - 1][Constants.EXPIRATION]).ToShortDateString();
                result[2] = Convert.ToString(interest);
                result[3] = Convert.ToString(main);
                result[4] = Convert.ToString(nextFee);
                result[5] = Convert.ToString(rest);
            }
            return result;
        }
    }
}