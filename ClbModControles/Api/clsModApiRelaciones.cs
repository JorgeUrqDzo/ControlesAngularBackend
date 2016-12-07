using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles.Api
{
    public class clsModApiRelaciones
    {
        public int IdControl { get; set; }
        public string Valor { get; set; }
        public int Orden { get; set; }
        public int IdSeccion { get; set; }
        public bool IsPadre { get; set; }
        public bool IsHijo { get; set; }
        public string KeyPadre { get; set; }
        public string KeyHijo { get; set; } 

    }
}
