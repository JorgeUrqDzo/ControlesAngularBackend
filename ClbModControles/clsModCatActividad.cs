using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModCatActividad
    {
        //Declarar los campos de la tabla de EncActividad
        public int IdEncActividad { get; set; }

        public int IdTipoProceso { get; set; }

        public int Orden { get; set; }

        public bool Activo { get; set; }

        public string Actividad { get; set; }

        public string UrlActividad { get; set; }

        public string UrlDestinoAlTerminar { get; set; }

        public int TiempoPromedioObjetivo { get; set; }

        public clsModCatTipoProceso objTipoProceso { get; set; }

        public string ActividadSiguiente { get; set; }

    }
}
