using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles;

namespace ClbNegControles
{
    public class clsNegCatBandejas
    {
       
        private readonly  clsDatCatBandejas objDatCatBandejas = null;

         public clsNegCatBandejas()
        {
            objDatCatBandejas = new clsDatCatBandejas();
        }
       
        //Metodo para obtener los datos de las bandejas de la BD
         public ICollection<clsModCatBandejas> CargarBandejas(out clsModErrorBase objErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
         {
             clsDatCatBandejas objDatCatBandejas = new clsDatCatBandejas();

             ICollection<clsModCatBandejas> lstBandejas = objDatCatBandejas.CargarBandejas(out objErrorBase, strTextoBuscar, out objModPaginacion, intPagina, intRegistrosPorPagina);

             return lstBandejas;
           
         }

       
    }
}
