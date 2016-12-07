using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class GenerarControlesModel
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public bool Null { get; set; }
        public int Longitud { get; set; }
        public string TablaRelacion { get; set; }
        public string SchemaTablaRelacion { get; set; }
    }
}