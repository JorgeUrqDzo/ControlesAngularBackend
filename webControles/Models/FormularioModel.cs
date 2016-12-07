using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class FormularioModel
    {
        /// <summary>
        /// Identidicador del Formulario
        /// </summary>
        [Display(Name = "Id")]
        public int IdFormulario { get; set; }
        /// <summary>
        /// Nombre del formulario
        /// </summary>
        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "Campo Vacío")]
        public string Nombre{ get; set; }
        /// <summary>
        /// Descripción del formulario
        /// </summary>
        [Display(Name = "Descripción:")]
        [Required(ErrorMessage = "Campo Vacío")]
        public string Descripcion { get; set; }

        [Display(Name ="Tipo Formulario:")]
        [Required(ErrorMessage = "Campo Vacío")]
        public int IdTipoFormulario { get; set; }

        /// <summary>
        /// Verifica si formulario esta activo o no
        /// </summary>
        public bool Activo{ get; set; }

        [Display(Name = "Formato de Fecha:")]
        public string FormatoFecha { get; set; }

        public string UUID { get; set; }

        //TODO: Se agregaran los demas campos que conforman el modelo, se agregaron los necesarios para saber si la función trabaja
    }
}