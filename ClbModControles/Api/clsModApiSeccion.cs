using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles.Api
{
    public class clsModApiSeccion
    {
        public clsModApiSeccion()
        {
            Columnas = 4;
            objModApiControl = new clsModApiControl();
        }

        public int IdSeccion { get; set; }
        public string Nombre { get; set; }
        public string NombreModel { get; set; }
        public int IdGrupo { get; set; }
        public int Orden { get; set; }
        public string Icono { get; set; }
        public int IdTipoSeccion { get; set; }

        public int Columnas { get; set; }
        public ICollection<clsModApiControl> LstModApiControl { get; set; }

        public clsModApiControl objModApiControl { get; set; }

    }
}
