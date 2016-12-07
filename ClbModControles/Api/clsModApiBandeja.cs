using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;

namespace ClbModControles.Api
{
    public class clsModApiBandeja
    {
        public clsModCatBandejas bandeja { get; set; }

        public Object consulta { get; set; }

        public Object LinkParametros { get; set; }

        public List<clsModCatBandejaColumna> bandejaColumnba {get;set;}
        public clsModPaginacion paginacion { get; set; }

    }
}
