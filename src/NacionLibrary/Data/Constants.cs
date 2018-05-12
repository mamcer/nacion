namespace Nacion.Data
{
    /// <summary>
    /// Summary description for Constants
    /// </summary>
    public sealed class Constants
    {
        public const string DATATABLE_FEES_NAME = "Cuotas";
        public const string FEE_NUMBER = "Numero";        
        public const string EXPIRATION = "Vencimiento";
        public const string MAIN = "Capital";
        public const string INTEREST = "Interes";
        public const string CHARGES = "Cargos";
        public const string TAXES = "Impuestos";
        public const string TOTAL = "Total";
        public const string STATUS = "Status";

        public enum FeeStatus
        {
            New = 0,
            Payed = 1,
            Forwarded = 2
        }
    }
}
