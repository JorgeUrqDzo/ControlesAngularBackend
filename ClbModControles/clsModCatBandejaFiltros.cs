using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModCatBandejaFiltros
    {
        public int IdDentConfBandejaFiltros { get; set; }
        public int IdEncConfBandeja { get; set; }
        public string Filtro { get; set; }
        public int IdTipoControl { get; set; }
        public string TipoControl { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
