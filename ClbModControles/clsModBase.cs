using System;

namespace ClbModControles
{
    /// <summary>
    /// Modelo base para propiedades generales.
    /// Todos los objetos heredan directamente esta clase
    /// </summary>
    public class clsModBase
    {
       

        /// <summary>
        /// Identificador del usuario de creación.
        /// </summary>
        private int _idUsuarioCreacion;

        /// <summary>
        /// Identificador del usuario de modificación.
        /// </summary>
        private int _idUsuarioModificacion;

        /// <summary>
        /// Nombre del usuario de creación.
        /// </summary>
        private string _strUsuarioCreacion;

        /// <summary>
        /// Nombre del usuario de modificación.
        /// </summary>
        private string _strUsuarioModificacion;

        /// <summary>
        /// Fecha de creación.
        /// </summary>
        private DateTime _fechaCreacion;

        /// <summary>
        /// Fecha de modificación.
        /// </summary>
        private DateTime _fechaModificacion;

       
        /// <summary>
        /// Alamcena un mensaje de error.
        /// </summary>
        private string _msgError;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public clsModBase()
        {
            this.MsgError = string.Empty;
        }

       
        /// <summary>
        /// Fecha de creación.
        /// </summary>
        public DateTime FechaCreacion
        {
            get { return this._fechaCreacion; }
            set { this._fechaCreacion = value; }
        }

        /// <summary>
        /// Fecha de modificación.
        /// </summary>
        public DateTime FechaModificacion
        {
            get { return this._fechaModificacion; }
            set { this._fechaModificacion = value; }
        }

        /// <summary>
        /// Identificador del usuario de creación.
        /// </summary>
        public int IdUsuarioCreacion
        {
            get { return this._idUsuarioCreacion; }
            set { this._idUsuarioCreacion = value; }
        }

        /// <summary>
        /// Identificador del usuario de modificación.
        /// </summary>
        public int IdUsuarioModificacion
        {
            get { return this._idUsuarioModificacion; }
            set { this._idUsuarioModificacion = value; }
        }

        /// <summary>
        /// Nombre del usuario de creación.
        /// </summary>
        public string StrUsuarioCreacion
        {
            get { return this._strUsuarioCreacion; }
            set { this._strUsuarioCreacion = value; }
        }

        /// <summary>
        /// Nombre del usuario de modificación.
        /// </summary>
        public string StrUsuarioModificacion
        {
            get { return this._strUsuarioModificacion; }
            set { this._strUsuarioModificacion = value; }
        }

        /// <summary>
        /// Almacena un mensaje de error.
        /// </summary>
        public string MsgError
        {
            get { return this._msgError; }
            set { this._msgError = value; }
        }

        /// <summary>
        /// Fecha de creación en formato string "dd/MM/yyyy hh:mm".
        /// </summary>
        public string StrFechaCreacion
        {
            get { return this._fechaCreacion.ToString("dd/MM/yyyy hh:mm"); }
        }

        /// <summary>
        /// Fecha de modificación en formato string "dd/MM/yyyy hh:mm".
        /// </summary>
        public string StrFechaModificacion
        {
            get { return this._fechaModificacion.ToString("dd/MM/yyyy hh:mm"); }
        }

    }
}
