using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModCatTipoProceso
    {
        public int IdTipoProceso { get; set; }

        public int IdAreaProceso { get; set; }

        public string TipoProceso { get; set; }

        public bool Activo { get; set; }

        public string TipoProcesoSiguiente { get; set; }
    }
}
