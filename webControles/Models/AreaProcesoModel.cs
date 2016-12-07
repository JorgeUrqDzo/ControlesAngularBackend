using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class AreaProcesoModel
    {
        public int IdAreaProceso { get; set; }
        [Display(Name = "Area Proceso")]
        [Required(ErrorMessage = "*Campo Requerido")]
        public string AreaProceso { get; set; }
        public bool Activo { get; set; }
    }
}