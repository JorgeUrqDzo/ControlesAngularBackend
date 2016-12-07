using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClbNegControles;
using webControles.Models;
using webControles.Helpers;
using ClbModControles;
using webControles.Controllers.Apis;
using webControles.Models.Views;

namespace webControles.Controllers
{
    public class BandejaColumnaController : Controller
    {
        private readonly clsNegCatBandejaColumna _objNegCatBandejaColumna = null;
        clsModPaginacion objModPaginacion;
        private const int RegistroPagina = 10;
        Catalogos objCatalogos = null;

        public BandejaColumnaController ()
	    {
            _objNegCatBandejaColumna = new clsNegCatBandejaColumna();
            objCatalogos = new Catalogos();
	    }
        //
        // GET: /BandejaColumna/ListaBandejaColumna/
        public ActionResult ListaBandejaColumna(int Id=0)
        {
            BusquedaBandejaViewModel objBusquedaBandejaViewModel = new BusquedaBandejaViewModel();
            ViewBag.lstTipoColumna = objCatalogos.GetCatalogoTipoColumna();
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            objBusquedaBandejaViewModel.lstBandejaColumnaModel = ObjectMapper.Instance.Convert<ICollection<clsModCatBandejaColumna>, ICollection<BandejaColumnaModel>>(
                _objNegCatBandejaColumna.CargarBandejaColumna(out objModErrorBase, Id));

            return PartialView(objBusquedaBandejaViewModel);
        }
        //
        // POST: /BandejaColumna/Create
        [HttpPost]
        public JsonResult Create(BandejaColumnaModel bandejaColumna)
        {
            clsModErrorBase objModErrorBase = null;
            ObjectMapper mapero = ObjectMapper.Instance;
            objModErrorBase = _objNegCatBandejaColumna.AgregarBandejaColumna(mapero.Convert<BandejaColumnaModel, clsModCatBandejaColumna>(bandejaColumna));
            return Json(objModErrorBase);
        }

        //
        // GET: /BandejaColumna/Edit/5
        public JsonResult Edit(int id)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            ObjectMapper mapero = ObjectMapper.Instance;
            BandejaColumnaModel libroSeleccionado = mapero.Convert<clsModCatBandejaColumna, BandejaColumnaModel>(_objNegCatBandejaColumna.CargarBandejaColumnaPorId(out objModErrorBase, id));
            return Json(libroSeleccionado, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /BandejaColumna/Edit/5
        [HttpPost]
        public JsonResult Edit(BandejaColumnaModel objModBandejaColumna)
        {
            clsModErrorBase objModErrorBase = null;
            ObjectMapper mapero = ObjectMapper.Instance;
            objModErrorBase = _objNegCatBandejaColumna.ActualizarBandejaColumna(mapero.Convert<BandejaColumnaModel, clsModCatBandejaColumna>(objModBandejaColumna));
            return Json(objModErrorBase);

        }
        //
        // POST: /BandejaColumna/Delete/5
        [HttpPost]
        public JsonResult Delete(BandejaColumnaModel bandejaColumna)
        {
            clsModErrorBase objModErrorBase = null;
            ObjectMapper mapero = ObjectMapper.Instance;
            objModErrorBase = _objNegCatBandejaColumna.EliminarBandejaColumna(mapero.Convert<BandejaColumnaModel, clsModCatBandejaColumna>(bandejaColumna));
            return Json(objModErrorBase);
        }

        [HttpPost]
        public JsonResult CambiarOrden(BandejaColumnaModel objBandejaColumnaModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsNegCatBandejaColumna objNegCatBandejaColumna = new clsNegCatBandejaColumna();
            clsModCatBandejaColumna objBandejaColumna =
                ObjectMapper.Instance.Convert<BandejaColumnaModel, clsModCatBandejaColumna>(objBandejaColumnaModel);
            objModErrorBase = objNegCatBandejaColumna.CambiarOrden(objBandejaColumna);
            return Json(objModErrorBase);
        }
    }
}
