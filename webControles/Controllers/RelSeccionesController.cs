using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClbModControles;
using ClbNegControles;
using webControles.Helpers;
using webControles.Models;

namespace webControles.Controllers
{
    public class RelSeccionesController : Controller
    {
        //
        // GET: /RelSecciones/
        [HttpPost]
        public JsonResult CargarRelaciones(int idFormulario)
        {
            clsNegRelSecciones objNegRelSecciones = new clsNegRelSecciones();
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            List<RelSeccionesModel> lstRelSeccionesModel =
                ObjectMapper.Instance.Convert<List<clsModRelSecciones>, List<RelSeccionesModel>>(
                    objNegRelSecciones.CargarRelaciones(idFormulario, out objModErrorBase));
            return Json(lstRelSeccionesModel);
        }

        [HttpPost]
        public JsonResult GuardarRelacion(RelSeccionesModel objRelSeccionesModel)
        {
            clsModRelSecciones objModRelSecciones =
                ObjectMapper.Instance.Convert<RelSeccionesModel, clsModRelSecciones>(objRelSeccionesModel);
            clsNegRelSecciones objNegRelSecciones = new clsNegRelSecciones();

            return Json(objNegRelSecciones.GuardarRelacion(objModRelSecciones));
        }

        [HttpPost]
        public JsonResult EliminarRelacion(RelSeccionesModel objRelSeccionesModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsModRelSecciones objModRelSecciones =
                ObjectMapper.Instance.Convert<RelSeccionesModel, clsModRelSecciones>(objRelSeccionesModel);
            clsNegRelSecciones objNegRelSecciones = new clsNegRelSecciones();

            List<RelSeccionesModel> lstRelSeccionesModel =
                ObjectMapper.Instance.Convert<List<clsModRelSecciones>, List<RelSeccionesModel>>(
                    objNegRelSecciones.EliminarRelacion(objModRelSecciones, out objModErrorBase));

            return Json(lstRelSeccionesModel);
        }

        [HttpPost]
        public JsonResult CambiarOrden(RelSeccionesModel objRelSeccionesModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsNegRelSecciones objNegRelSecciones = new clsNegRelSecciones();
            clsModRelSecciones objRelSecciones =
                ObjectMapper.Instance.Convert<RelSeccionesModel, clsModRelSecciones>(objRelSeccionesModel);
            objModErrorBase = objNegRelSecciones.CambiarOrden(objRelSecciones);
            return Json(objModErrorBase);
        }
    }
}