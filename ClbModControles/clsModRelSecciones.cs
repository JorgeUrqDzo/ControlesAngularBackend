using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModRelSecciones
    {
        public int IdRelSecciones { get; set; }
        public int IdSeccionPadre { get; set; }
        public int IdSeccionHijo { get; set; }
        public string KeyPadre { get; set; }
        public string KeyHijo { get; set; }
        public int Orden { get; set; }
        public int IdFormulario { get; set; }

    }
}
