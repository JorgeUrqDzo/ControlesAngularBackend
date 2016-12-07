using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using webControles.Models.Views;

namespace webControles.Models
{
    public class SeccionesModel
    {
        /// <summary>
        /// Nombre de la sección
        /// </summary>
        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "*Campo requerido")]
        public string Nombre { get; set; }

        /// <summary>
        /// Icono de seccion
        /// </summary>
        //[Display(Name = "Icono")]
        ////[Required(ErrorMessage = "Campo Vacío")]
        //public string Icono { get; set; }
        /// <summary>
        /// Tipo de sección PANEL o TAB
        /// </summary>
        [Display(Name = "Tipo Sección:")]
        [Required(ErrorMessage = "*Se tiene que seleccionar una sección")]
        public int IdTipoSeccion { get; set; }

        /// <summary>
        /// Grupo perteneciente a una sección
        /// </summary>
        //[Display(Name = "Grupo")]
        ////[Required(ErrorMessage = "Se tiene que seleccionar un grupo")]
        //public int Grupo { get; set; }
        public int IdSeccion { get; set; }

        public int IdFormulario { get; set; }

        public int IdGrupo { get; set; }

        [Display(Name = "Columnas:")]
        [Required(ErrorMessage = "*Campo requerido")]
        public string Columnas { get; set; }

        [Display(Name = "Grupo:")]
        public string Grupo { get; set; }

        [Display(Name = "Icono:")]
        public int IdSeccionIcono { get; set; }

        public string Tabla { get; set; }

        [Display(Name = "Nombre Primary Key:")]
        public string primaryKeyName { get; set; }

        public int IdTipoFormulario { get; set; }


    }
}