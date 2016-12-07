using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModEncDataSource
    {
        public int IdEncDataSource { get; set; }
        public int IdSeccionControl { get; set; }
        public bool BaseDatos { get; set; }
        public string Consulta { get; set; }
        public string ValueField { get; set; }
        public string TextField { get; set; }
        public string Valores { get; set; }
    }
}
