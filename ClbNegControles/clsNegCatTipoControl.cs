using ClbModControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;

namespace ClbNegControles
{
    public class clsNegCatTipoControl
    {
        public ICollection<clsModCatTipoControl> Cargar(out clsModErrorBase objModErrorBase)
        {
            clsDatCatTipoControl objDatCatTipoControl = new clsDatCatTipoControl();

            return objDatCatTipoControl.Cargar(out objModErrorBase);
        }
    }
}
