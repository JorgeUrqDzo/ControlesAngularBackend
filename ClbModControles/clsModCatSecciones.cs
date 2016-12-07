using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModCatSecciones
    {
        /// <summary>
        /// Identificador de la sección 
        /// </summary>
        public int IdSeccion { get; set; }
        /// <summary>
        /// Nombre que tomara la sección 
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Icono que llevará la sección 
        /// </summary>
        public string Icono { get; set; }
        /// <summary>
        /// Identificador del formulario donde se agregara la sección
        /// </summary>
        public int IdFormulario { get; set; }
        /// <summary>
        /// Identificador de tipo de seccion que tomara 
        /// </summary>
        public int IdTipoSeccion { get; set; }
        /// <summary>
        /// Identificador del grupo
        /// </summary>
        public int IdGrupo { get; set; }
        /// <summary>
        /// Orden que tomara la sección 
        /// </summary>
        /// 
        public string Grupo { get; set; }
        public int Orden { get; set; }

        public int Columnas { get; set; }
        /// <summary>
        /// Estatus de la sección 
        /// </summary>
        public bool Activo { get; set; }
        /// <summary>
        /// Identificador del usuario que llevo acabo la creación de una sección
        /// </summary>
        public int IdUsuarioCreacion { get; set; }
        /// <summary>
        /// Usuario que llevo acabo una modificación de una sección
        /// </summary>
        public int IdUsuarioModificacion { get; set; }
        /// <summary>
        /// Fecha de creación de sección
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de Modificación
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        public int IdSeccionIcono { get; set; }

        public string Tabla { get; set; }

        public string primaryKeyName { get; set; }

        public int IdTipoFormulario { get; set; }

        public ICollection<clsModCatSeccionControl> LstSeccionControl { get; set; }

    }
}
