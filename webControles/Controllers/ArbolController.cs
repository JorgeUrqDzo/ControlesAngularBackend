using ClbNegControles;
using ClbModControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webControles.Helpers;

namespace webControles.Controllers
{
    public class ArbolController : Controller
    {
        /// <summary>
        /// Recibe el id del formulario y devuelve toda su informacion
        /// </summary>
        /// <param name="id">Id Del formulario a consultar</param>
        /// <returns>regresa toda la informacion del formulario</returns>
        /// 
        [AllowCrossSiteJson]
        public JsonResult Inicializar(int id = 0)
        {
            clsNegCatFormularios objNegCatFormularios = new clsNegCatFormularios();
            clsModErrorBase objModErrorBase = null;
            string strArbol = objNegCatFormularios.CargarArbol(id, out objModErrorBase);

            return Json(strArbol, JsonRequestBehavior.AllowGet);
        }

      
    }
}
