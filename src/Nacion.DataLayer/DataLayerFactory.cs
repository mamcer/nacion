using System;

namespace Nacion.DataLayer
{
    public enum DataLayerType
    {
        SqlServer
    }

    /// <summary>
    /// Esta clase permite crear cada una de las clases DataLayer.
    /// </summary>
    public sealed class DataLayerFactory
    {
        private static DataLayerFactory _instancia;

        private DataLayerFactory() { }

        public static DataLayerFactory Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new DataLayerFactory();
                }

                return _instancia;
            }
        }

        public IDataLayerBase GetDataLayer(DataLayerType tipo)
        {
            switch (tipo)
            {
                case DataLayerType.SqlServer:
                    {
                        return new SqlServerDataLayer();
                    }
                default:
                    {
                        throw new Exception($"tipo de DataLayer no reconocido:{tipo}");
                    }
            }
        }
    }
}