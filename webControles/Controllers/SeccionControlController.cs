using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webControles.Models;
using webControles.Controllers.Apis;
using webControles.Models.Views;
using ClbModControles;
using ClbUtilerias.Resources.Strings.Generales;
using ClbNegControles;
using webControles.Helpers;

namespace webControles.Controllers
{
    public class SeccionControlController : Controller
    {
        Catalogos objCatalogos;
        public SeccionControlController()
        {
            objCatalogos = new Catalogos();
        }


        //
        // GET: /SeccionControl/
        public ActionResult Index(int? IdSeccion)
        {
            SeccionControlViewModel objSeccionControlViewModel = null;
            if (IdSeccion > 0)
            {

            }
            else
            {
                objSeccionControlViewModel = new SeccionControlViewModel();
            }
            ViewBag.lstTipoControl = GetCatalogoTipoControl();


            return View(objSeccionControlViewModel);
        }

        //
        // GET: /SeccionControl/
        [AllowCrossSiteJson]
        public JsonResult Editar(int id)
        {
            clsModErrorBase objModErrorBase = null;
            clsNegCatSeccionControl objNegCatSeccionControl = new clsNegCatSeccionControl();

            ICollection<clsModCatSeccionControl> lstModSeccionControl = objNegCatSeccionControl.Cargar(id, out objModErrorBase);

            ICollection<SeccionControlModel> LstSeccionControl = ObjectMapper.Instance.Convert<ICollection<clsModCatSeccionControl>, ICollection<SeccionControlModel>>(lstModSeccionControl);
            return Json(LstSeccionControl, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /SeccionControl/Details/5
        public ActionResult Guardar(int id)
        {
            return View();
        }

        //
        // [HttpPost]
        //public JsonResult Guardar(SeccionControlViewModel objSeccionControlViewModel)
        //{
        //    MensajeModel objMensajeModel = null;

        //    clsModErrorBase objModErrorBase = null;
        //    clsNegCatSeccionControl objNegCatSeccionControl = new clsNegCatSeccionControl();
        //    clsModCatSecciones objModCatSecciones = ObjectMapper.Instance.Convert<SeccionControlViewModel, clsModCatSecciones>(objSeccionControlViewModel);
        //    objModErrorBase = objNegCatSeccionControl.Guardar(objModCatSecciones);
        //    if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
        //    {
        //        objMensajeModel = new MensajeModel(true, objModErrorBase.MsgError);
        //    }
        //    else
        //    {
        //        objMensajeModel = new MensajeModel(false, Generales.SuccessSeccionControles);
        //    }
        //    return Json(objMensajeModel);
        //}

        private IEnumerable<ComboCatalogosModel> GetCatalogoTipoControl()
        {


            return objCatalogos.GetCatalogoTipoControl();

        }

        //

        [HttpPost]
        public JsonResult GenerarControles(string[] objTabla)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsNegCatSeccionControl objNegCatSeccionControl = new clsNegCatSeccionControl();

            string tablaSeleccionada = objTabla[0];
            string[] Tabla = tablaSeleccionada.Split('.');
            

           return Json(ObjectMapper.Instance.Convert<List<clsModControlesGenerados>, List<GenerarControlesModel>>(objNegCatSeccionControl.GenerarControles(Tabla[0], Tabla[1], out objModErrorBase)));
        }
    }
}
