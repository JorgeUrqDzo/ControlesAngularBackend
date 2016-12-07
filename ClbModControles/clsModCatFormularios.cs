using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    /// <summary>
    /// Clase Modelo donde las variables tomararan los valores correspondientes de algun metodo en especifico
    /// </summary>
    public class clsModCatFormularios
    {
        /// <summary>
        /// Identificador del formulario
        /// </summary>
        public int IdFormulario { get; set; }
        /// <summary>
        /// Nombre del formulario
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Descripcion del formulario
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Veremos el status del formulario si esta activo o no
        /// </summary>
        public bool Activo { get; set; }
        /// <summary>
        /// Usuario que llevo acabo la creación del formulario
        /// </summary>
        public int IdusuarioCreacion { get; set; }
        /// <summary>
        /// Usuario que modificó algun formulario
        /// </summary>
        /// 

        public int IdusuarioModificacion { get; set; }
        /// <summary>
        /// Fecha de creación del formulario
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del formulario
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Identificador unico del formulario
        /// </summary>
        public string UUID { get; set; }

        public int IdTipoFormulario { get; set; }

        public string FormatoFecha { get; set; }
    }
}
