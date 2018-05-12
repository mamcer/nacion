using System;
using System.Data;
using Nacion.Data;

namespace Nacion
{
    /// <summary>
    /// Summary description for Credit
    /// </summary>
    public sealed class Credit
    {
        private SqlDao _dao = null;
        private static Credit _instance = null;

        public static Credit Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Credit();
                }
                return _instance;
            }
        }

        private SqlDao Dao
        {
            get 
            {
                if(_dao == null)
                {
                    _dao = new SqlDao();
                }
                return _dao;
            }
        }

        public DataTable GetAllFees()
        {
            return Dao.GetAllFees();
        }

        public string GetFirstExpiration()
        {
            return Dao.GetFirstExpiration();
        }

        public string GetLastExpiration()
        {
            return Dao.GetLastExpiration();
        }

        public string GetActualLastExpiration()
        {
            return Dao.GetActualLastExpiration();
        }

        public decimal GetTotalLeft()
        {
            return Dao.GetTotalLeft();
        }

        public string GetNextExpiration()
        {
            return Dao.GetNextExpiration();
        }

        public decimal GetNextFee()
        {
            return Dao.GetNextFee();
        }

        public decimal GetTotalPayed()
        {
            return Dao.GetTotalPayed();
        }

        public int GetCountOfNewFees()
        {
            return Dao.GetCountOfNewFees();
        }

        public int GetCountOfForwardedFees()
        {
            return Dao.GetCountOfForwardedFees();
        }

        public SimulatorResult Simulate(decimal money)
        { 
            SimulatorResult result = new SimulatorResult();
            result.MapData(Dao.Simulate(money));
            return result;
        }
        public GeneralInfo GetGeneralInfo()
        {
            return Dao.GetGeneralInfo();
        }
    }
}
