using System;

namespace Nacion
{
    /// <summary>
    /// Summary description for SimulatorResult
    /// </summary>
    public sealed class SimulatorResult
    {
        private string _forwardFees;
        private string _main;
        private string _interest;
        private string _actualExpiration;
        private string _nextFee;
        private string _rest;

        #region properties
        public string ForwardFees
        {
            get
            {
                return _forwardFees;
            }
            set
            {
                _forwardFees = value;
            }
        }

        public string Main
        {
            get
            {
                return _main;
            }
            set
            {
                _main = value;
            }
        }

        public string Interest
        {
            get
            {
                return _interest;
            }
            set
            {
                _interest = value;
            }
        }

        public string ActualExpiration
        {
            get
            {
                return _actualExpiration;
            }
            set
            {
                _actualExpiration = value;
            }
        }

        public string NextFee
        {
            get
            {
                return _nextFee;
            }
            set
            {
                _nextFee = value;
            }
        }

        public string Rest
        {
            get
            {
                return _rest;
            }
            set
            {
                _rest = value;
            }
        }
        #endregion

        public void MapData(string[] mp)
        {
            if (mp != null)
            {
                ForwardFees = mp[0];
                ActualExpiration = mp[1];
                Interest = mp[2];
                Main = mp[3];
                NextFee = mp[4];
                Rest = mp[5];
            }
        }
    }
}
