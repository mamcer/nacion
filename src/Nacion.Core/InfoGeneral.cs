using System;

namespace Nacion.Core
{
    /// <summary>
    /// Contiene Información General del crédito como el Id del cliente, CBU, etc.
    /// </summary>
    public struct InfoGeneral
    {
        public string NroCliente;
        public string NroPrestamo;
        public decimal TasaTem;
        public decimal TasaTnav;
        public DateTime PrimerVencimiento;
        public DateTime UltimoVencimiento;
        public decimal Capital;
        public string Cbu;
        public string NroCajaAhorro;

        public InfoGeneral(string ci, string n, decimal tem, decimal tnav, DateTime f, DateTime l, decimal m, string cbu, string an)
        {
            NroCliente = ci;
            NroPrestamo = n;
            TasaTem = tem;
            TasaTnav = tnav;
            PrimerVencimiento = f;
            UltimoVencimiento = l;
            Capital = m;
            Cbu = cbu;
            NroCajaAhorro = an;
        }
    }
}
