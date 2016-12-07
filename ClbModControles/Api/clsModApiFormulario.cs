using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles.Api
{
    public class clsModApiFormulario
    {
        
        public string Nombre { get; set; }
        public string FormatoFecha { get; set; }
        public ICollection<clsModApiSeccion> LstModApiSeccion { get; set; }
        public clsModApiSeccion objApiSeccion { get; set; }


        public clsModApiFormulario()
        {
            objApiSeccion = new clsModApiSeccion();
        }
    }
}
