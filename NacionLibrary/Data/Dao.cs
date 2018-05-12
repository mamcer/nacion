using System;
using System.Data;

namespace Nacion.Data
{
    public abstract class Dao
    {
        public abstract DataTable GetFees();

        protected DataTable GetFeesDataTable()
        {
            DataTable dt = new DataTable(Constants.DATATABLE_FEES_NAME);
            dt.Columns.Add(new DataColumn(Constants.FEE_NUMBER, typeof(string)));
            dt.Columns.Add(new DataColumn(Constants.EXPIRATION, typeof(DateTime)));
            dt.Columns.Add(new DataColumn(Constants.MAIN, typeof(decimal)));
            dt.Columns.Add(new DataColumn(Constants.INTEREST, typeof(decimal)));
            dt.Columns.Add(new DataColumn(Constants.CHARGES, typeof(decimal)));
            dt.Columns.Add(new DataColumn(Constants.TAXES, typeof(decimal)));
            dt.Columns.Add(new DataColumn(Constants.TOTAL, typeof(decimal)));
            dt.Columns.Add(new DataColumn(Constants.STATUS, typeof(Constants.FeeStatus)));
            return dt;
        }

        public abstract string GetFirstExpiration();
        public abstract string GetLastExpiration();
        public abstract string GetActualLastExpiration();
        public abstract decimal GetRest();
        public abstract string GetNextExpiration();
        public abstract decimal GetNextFee();
        public abstract int GetCountOfNewFees();
        public abstract string[] Simulate(decimal money);
    }
}
