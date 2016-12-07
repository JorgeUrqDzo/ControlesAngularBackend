using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
   public class clsModCatMenu
    {

        //Declarar los campos de la tabla CatMenu
        public int IdMenu { get; set; }
        public int IdTipoMenu { get; set; }
        public string Descripcion { get; set; }
        public string Menu { get; set; }
        public string Url { get; set; }
        public int IdMenuIcono { get; set; }
        public int IdMenuPadre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public string Icono { get; set; }

        //public clsModCatMenuIcono objModCatMenuIcono{ get; set; }

        public List<clsModCatMenuIcono> lstModCatMenuIcono { get; set; }

    }
}
