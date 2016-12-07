using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class BandejaFiltrosModel
    {
        [Display(Name = "Id")]
        public int IdDentConfBandejaFiltros { get; set; }

        public int IdEncConfBandeja { get; set; }

        [Display(Name = "Nombre filtro")]
        [Required(ErrorMessage = "*Campo Requerido")]
        public string Filtro { get; set; }

        [Display(Name = "Tipo control")]
        [Required(ErrorMessage = "*Campo Requerido")]
        public int IdTipoControl { get; set; }
        public string TipoControl { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public int IdUsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public BandejaFiltrosModel bandejaFiltrosModelo { get; set; }
        public PaginacionModel objPaginacionModel { get; set; }
    }
}