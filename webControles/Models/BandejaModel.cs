using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class BandejaModel
    {
        /// <summary>
        /// Titulo/ nombre del Formulario
        /// </summary>

        [Display(Name = "Id")]
        public int IdEncConfBandeja { get; set; }

        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "  *Campo Requerido")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion del Formulario
        /// </summary>

        [Display(Name = "Descripcion:")]
        [Required(ErrorMessage = "  *Campo Requerido")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Numero de paginas que se cargaran
        /// </summary>

        [Display(Name = "Numero de Paginas:")]
        [Required(ErrorMessage = "  *Campo Requerido")]
        public int NumeroPaginas { get; set; }

        /// <summary>
        /// Consulta SQL que ocupara el Formulario para su funcionamiento
        /// </summary>

        [Display(Name = "Editar Consulta (SQL) ")]
        [Required(ErrorMessage = "  *Campo Requerido")]
        public string Consulta { get; set; }

        //public string Controller{ get; set; }

        //public string Action { get; set; }

        //public BandejaModel(string strController, string strAction)
        //{
        //    this.Controller = strController;
        //    this.Action = strAction;
        //}

        /// <summary>
        /// Id del usuario que crea el formulario
        /// </summary>

        public int IdColumna { get; set; }

        public string NombreColumna { get; set; }
        [Display(Name = "Formularios:")]
        public int IdFormulario { get; set; }

        [Display(Name = "Tipo Consulta:")]
        public int IdTipoConsulta { get; set; }

        [Display(Name = "Clase:")]
        public string ClaseBandeja { get; set; }

        public string UUID { get; set; }
    }
}