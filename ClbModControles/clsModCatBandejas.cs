using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModCatBandejas
    {
        //Declarar los campos de la tabla de Bandejas
        public int IdEncConfBandeja { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int NumeroPaginas { get; set; }

        public string Consulta { get; set; }

        public int IdUsuarioCreacion { get; set; }

        public int IdUsuarioModificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
        public string ClaseBandeja { get; set; }
        public string UUID { get; set; }
}
}

