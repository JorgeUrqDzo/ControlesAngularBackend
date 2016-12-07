using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModCatTipoControl : clsModBase
    {
        public int IdTipoControl { get; set; }

        public string CodTipoControl { get; set; }
        public string TipoControl { get; set; }
        public bool DataSource { get; set; }
    }
}
