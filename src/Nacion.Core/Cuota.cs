using System;
using Nacion.DataLayer;

namespace Nacion.Core
{
    /// <summary>
    /// Esta estructura contiene toda la información referente a una cuota del crédito.
    /// </summary>
    public sealed class Cuota
    {
        public int Nro
        {
            get;
            set;
        }

        public DateTime Vencimiento
        {
            get;
            set;
        }

        public decimal Capital
        {
            get;
            set;
        }

        public decimal Interes
        {
            get;
            set;
        }

        public decimal Cargos
        {
            get;
            set;
        }

        public decimal Impuestos
        {
            get;
            set;
        }

        public decimal Total => Capital + Interes + Cargos + Impuestos;

        public StatusCuota Status
        {
            get;
            set;
        }
    }
}