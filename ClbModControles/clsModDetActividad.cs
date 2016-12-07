using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModDetActividad
    {
        public int IdDetActividad { get; set; }
        public int IdEncActividad { get; set; }
        public bool Aprobacion { get; set; }
        public int IdEncActividadSiguiente { get; set; }
        public int IdEncProceso { get; set; }


        public clsModCatActividad objCatActividad { get; set; }

        public clsModCatTipoProceso objCatTipoProceso { get; set; }
    }
}
