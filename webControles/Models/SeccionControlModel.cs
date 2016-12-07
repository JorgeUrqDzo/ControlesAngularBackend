using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class SeccionControlModel
    {
        public int IdSeccionControl { get; set; }
        
        [Display(Name = "Titulo:")]
        [Required(ErrorMessage = "*Campo requerido")]
        public string Nombre { get; set; }
        public string NombreColumna { get; set; }

        [Display(Name = "Tipo Control:")]
        [Required(ErrorMessage = "*Campo requerido")]
        public int IdTipoControl { get; set; }

        [Display(Name = "Longitud:")]
        public int Longitud { get; set; }

        [Display(Name = "Formato:")]
        public string Formato { get; set; }
        public bool Requerido { get; set; }
        public int Orden { get; set; }
        public string TipoControl { get; set; }

        [Display(Name = "Valor default:")]
        public string ValorDefault { get; set; }
        
        [Display(Name = "Caption:")]
        public string Caption { get; set; }
        
        [Display(Name = "Texto de ayuda:")]
        public string HelpText { get; set; }
        
        [Display(Name = "Titulo opción Verdadero:")]
        public string TextoSeleccionado { get; set; }

        [Display(Name = "Titulo opción Falso:")]
        public string TextoNoSeleccionado { get; set; }
        
        [Display(Name = "Altura del campo:")]
        public int TextAreaHeight { get; set; }
        public EncDatasourceModel objEncDataSource { get; set; }

        public int IdSeccionControlPadre { get; set; }
    }
}