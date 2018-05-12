namespace Nacion.Core
{
    /// <summary>
    /// Contiene los datos del resultado de una simulación.
    /// </summary>
    public sealed class ResultadoSimulacion
    {
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
    }

}
