using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{

    /// <summary>
    /// Modelo base para el manejo de identificadores y errores.
    /// Permite almacenar un identificador de identidad de base de datos y/o un mensaje de error.
    /// Usualmente se utiliza esta clase para regresar errores en metodos de insercion
    /// </summary>
    public class clsModErrorBase
    {
        /// <summary>
        /// Identificador general.
        /// </summary>
        private int _Id;

        /// <summary>
        /// Mensaje de error.
        /// </summary>
        private string _MsgError;
        
        /// <summary>
        /// Constructor de la clase que incializa la variable de error.
        /// </summary>
        public clsModErrorBase()
        {
            this._MsgError = string.Empty;
        }
        /// <summary>
        /// Identificador general.
        /// </summary>
        public int Id
        {
            get { return this._Id; }
            set { this._Id = value; }
        }

        /// <summary>
        /// Mensaje de error.
        /// </summary>
        public string MsgError
        {
            get { return this._MsgError; }
            set { this._MsgError = value; }
        }
    }
}
