using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles.Api
{
    public class clsModApiControl
    {
        public int IdSeccionControl { get; set; }
        public string Nombre { get; set; }
        public int IdTipoControl { get; set; }
        public int IdSeccionControlPadre { get; set; }
        public int Longitud { get; set; }
        public string Formato { get; set; }
        public bool Requerido{ get; set; }
        public int Orden { get; set; }
        public string ValorDefault { get; set; }
        public string Caption { get; set; }
        public string NombreColumna { get; set; }
        public string CssClass { get; set; }
        public string HelpText { get; set; }


        public string ClaseArbol { get; set; }

        public string TextoSeleccionado { get; set; }
        public string TextoNoSeleccionado { get; set; }

        public bool DataSource { get; set; }
        public int TextAreaHeight { get; set; }
        public clsModApiDataSource ObjDataSource { get; set; }

        public clsModApiControl()
        {
            ObjDataSource = new clsModApiDataSource();
        }


    }
}
