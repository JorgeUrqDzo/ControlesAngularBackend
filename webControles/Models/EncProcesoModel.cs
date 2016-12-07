using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class EncProcesoModel
    {
        public int IdEncProceso { get; set; }
        [Required(ErrorMessage = "  *Campo Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "  *Campo Requerido")] 
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public DetActividadModel objDetActividad { get; set; }
        public List<DetActividadModel> lstModDetActividad { get; set; }

    }
}