using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class EncDatasourceModel
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