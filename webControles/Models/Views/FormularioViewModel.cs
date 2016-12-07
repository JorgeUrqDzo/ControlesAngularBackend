using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webControles.Models.Views
{
    public class FormularioViewModel
    {
        public FormularioViewModel()
        {
            objFormularioModel = new FormularioModel();
        }
        /// <summary>
        /// Lista/Colección de modelo Formularios
        /// </summary>
        public ICollection<FormularioModel> lstFormularioModel { get; set; }

        /// <summary>
        /// Objeto Formulario Model que contiene todas las variables pertenecientes a ese modelo, Permite obtener los valores de dicho modelo.
        /// </summary>
        public FormularioModel objFormularioModel { get; set; }

        /// <summary>
        /// Objeto modelo Paginación
        /// </summary>
        public PaginacionModel objPaginacionModel { get; set; }

        /// <summary>
        /// Contiene el Id de formulario
        /// </summary>
        public int IdFormulario { get; set; }
    }
}