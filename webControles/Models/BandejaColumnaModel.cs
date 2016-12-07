using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class BandejaColumnaModel
    {
        [Display(Name = "Id")]
        public int IdDetConfBandejaColumna { get; set; }
        public int IdEncConfBandeja { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "*Campo Requerido")]
        public string TituloColumna { get; set; }

        [Display(Name = "Campo valor")]
        [Required(ErrorMessage = "*Campo Requerido")]
        public string ColumnaBD { get; set; }

        [Display(Name = "Id Tipo columna")]
        public int IdTipoColumna { get; set; }

        [Display(Name = "Tipo columna")]
        [Required(ErrorMessage = "*Campo Requerido")]
        public string TipoColumna { get; set; }

        [Display(Name = "Clase")]
        public string Clase { get; set; }

        [Display(Name = "Formato")]
        public string Formato { get; set; }

        [Display(Name = "Pagina link")]
        public string PaginaLink { get; set; }

        [Display(Name = "Utilizar Link Tomado de BD: ")]
        public bool TipoLink { get; set; }

        public int IdUsuarioCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        //public BandejaColumnaModel bandejaModelo { get; set; }
        //public PaginacionModel objPaginacionModel { get; set; }

        public int OrdenColumna { get; set; }


    }
}