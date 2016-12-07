using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class ParametrosModel
    {
        public int IdDetConfBandejaLinkParametro { get; set; }
        public int IdEncConfBandeja { get; set; }
        
        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "  *Campo Requerido")]
        public string Parametro { get; set; }

        [Display(Name = "Valor:")]
        [Required(ErrorMessage = "  *Campo Requerido")]
        public string Valor { get; set; }

        [Display(Name = "Tipo Parametro:")]
        [Required(ErrorMessage = "  *Campo Requerido")]
        public int IdTipoParametro { get; set; }

        [Display(Name = "Tipo Envio:")]
        [Required(ErrorMessage = "  *Campo Requerido")]
        public int IdTipoEnvioParametro { get; set; }

        public int IdUsuarioModificacion { get; set; }

        public int IdUsuarioCreacion { get; set; }

        public string TipoParametro { get; set; }

    }
}