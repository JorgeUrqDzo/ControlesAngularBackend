namespace ClbDatControles.Common
{

    using System.Configuration;

    /// <summary>
    /// Clase para las operaciones de obtención de cadena de conexión a base de datos.
    /// </summary>
    public sealed class DataBaseConnection
    {

        /// <summary>
        /// Instancia de la clase.
        /// </summary>
        public static readonly DataBaseConnection Instance = new DataBaseConnection();

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        private DataBaseConnection()
        {
        }

        /// <summary>
        /// Obtiene la cadena de conexión a la base de datos Seguros.
        /// </summary>
        /// <returns>Cadena de conexión a la base de datos</returns>
        public string GetConnectionString()
        {
            var varConec = ConfigurationManager.ConnectionStrings["Conexion"];

            if (varConec != null)
            {
                return varConec.ToString();
            }

            return string.Empty;
        }

    }
}