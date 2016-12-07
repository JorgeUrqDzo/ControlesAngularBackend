using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles.Api
{
   public class clsModApiActividadProceso
    {
     
        public int IdTablaDocumento { get; set; }

        public int IdDocumento { get; set; }

        public int IdEncProceso { get; set; }

        public int IdEncActividad { get; set; }
       public bool Aprobado { get; set; }

    }
}
