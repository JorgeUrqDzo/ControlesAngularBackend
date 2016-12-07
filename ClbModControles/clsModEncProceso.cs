using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModEncProceso
    {
        public int IdEncProceso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public clsModDetActividad objModDetActividad { get; set; }

        public List<clsModDetActividad> lstModDetActividad { get; set; }
    }
}
