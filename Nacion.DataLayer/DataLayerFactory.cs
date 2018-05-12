using System;

namespace Nacion.DataLayer
{
    public enum DataLayerType { SqlServer};
    /// <summary>
    /// Esta clase permite crear cada una de las clases DataLayer.
    /// </summary>
    public sealed class DataLayerFactory
    {
        #region private members
        private static DataLayerFactory instancia;
        #endregion

        #region constructor
        private DataLayerFactory() { }
        #endregion

        #region public properties
        public static DataLayerFactory Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new DataLayerFactory();
                }
                return instancia;
            }
        }
        #endregion

        #region public methods
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
                        throw new Exception(string.Format("tipo de DataLayer no reconocido:{0}", tipo));
                    }
            }
        }
        #endregion
    }
}
