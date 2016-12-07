using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles.Api;
using ClbModControles;


namespace ClbNegControles.Api
{
    public class clsNegApiMenuDinamico
    {
        clsDatApiMenuDinamico menu = null;
        public clsNegApiMenuDinamico()
        {
            menu = new clsDatApiMenuDinamico();
        }

        public List<clsModApiMenuDinamico> menuDinamico(out clsModErrorBase objErrorBase, int tipoMenu)
        {
            return menu.CargarMenuDinamico(out objErrorBase, tipoMenu);
        }
    }
}
