using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class DetActividadModel
    {
        public int IdDetActividad { get; set; }
        public int IdEncActividad { get; set; }
        public bool Aprobacion { get; set; }
        public int IdEncActividadSiguiente { get; set; }
        public int IdEncProceso { get; set; }

        public ActividadModel objCatActividad { get; set; }

        public TipoProcesoModel objCatTipoProceso { get; set; }

    }
}