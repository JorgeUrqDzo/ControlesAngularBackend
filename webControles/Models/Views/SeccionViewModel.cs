using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webControles.Models.Views
{
    public class SeccionViewModel
    {
        public SeccionViewModel()
        {
            objSeccionModel = new SeccionesModel();
        }
        /// <summary>
        /// Lista/Colección de modelo sección
        /// </summary>
        public ICollection<SeccionesModel> lstSeccionModel { get; set; }

        /// <summary>
        /// Objeto Seccion Model que contiene todas las variables pertenecientes a ese modelo
        /// </summary>
        public SeccionesModel objSeccionModel { get; set; }

        /// <summary>
        /// Objeto modelo paginación
        /// </summary>
        public PaginacionModel objPaginacionModel { get; set; }

        /// <summary>
        /// Continene el Id de Secciones
        /// </summary>
        public int IdSeccion { get; set; }
    }
}