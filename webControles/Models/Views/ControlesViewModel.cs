using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webControles.Models.Views
{
    public class ControlesViewModel
    {

        public FormularioModel objFormularioModel { get; set; }

        /// <summary>
        /// Lista usada para la busqueda
        /// </summary>
        public ICollection<SeccionesModel> lstSeccionesModel { get; set; }
    }
}