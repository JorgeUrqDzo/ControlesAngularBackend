using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModDetConfBandejaLinkParametro
    {
        public int IdDetConfBandejaLinkParametro { get; set; }
        public int IdEncConfBandeja { get; set; }
        public string Parametro { get; set; }
        public string Valor { get; set; }
        public int IdTipoParametro { get; set; }
        public int IdTipoEnvioParametro { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get;set; }
        public string TipoParametro { get; set; }
    }
}
