using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class TipoProcesoModel
    {
        [Display(Name = "#")]
        public int IdTipoProceso { get; set; }

        [Required(ErrorMessage = "*Seleccione una opcion")]
        public int IdAreaProceso { get; set; }

        [Required(ErrorMessage = "*Campo Requerido")]
        public string TipoProceso { get; set; }

        [Display(Name = "Activo:")]
        public bool Activo { get; set; }

        public string TipoProcesoSiguiente { get; set; }

        public PaginacionModel objPaginacionModel { get; set; }


        public ICollection<TipoProcesoModel> listTipoProcesoModel { get; set; }


        public TipoProcesoModel objTipoProceso{ get; set; }
    }
}