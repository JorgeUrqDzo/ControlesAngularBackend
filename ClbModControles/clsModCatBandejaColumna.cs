using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModCatBandejaColumna
    {
        public int IdDetConfBandejaColumna { get; set; }
        public int IdEncConfBandeja { get; set; }
        public string TituloColumna { get; set; }
        public string ColumnaBD { get; set; }
        public int IdTipoColumna { get; set; }
        public string TipoColumna { get; set; }
        public string Clase { get; set; }
        public string Formato { get; set; }
        public bool TipoLink { get; set; }
        public string PaginaLink { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int OrdenColumna { get; set; }


    }
}
