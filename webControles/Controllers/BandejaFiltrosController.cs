using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClbNegControles;
using ClbModControles;
using webControles.Controllers.Apis;
using webControles.Helpers;
using webControles.Models;


namespace webControles.Controllers
{
    public class BandejaFiltrosController : Controller
    {
        private readonly clsNegCatBandejaFiltros _objNegCatBandejaFiltros = null;
        clsModPaginacion objModPaginacion;
        private const int RegistroPagina = 10;
        Catalogos objCatalogos = null;
        public BandejaFiltrosController()
        {
            _objNegCatBandejaFiltros = new clsNegCatBandejaFiltros();
            objCatalogos = new Catalogos();
        }
        //
        // GET: /BandejaFiltros/ListaBandejaFiltros/
        public ActionResult ListaBandejaFiltros(string strTextoBusqueda = "", int Pagina = 1)
        {
            ViewBag.lstTipoControl = objCatalogos.GetCatalogoTipoControl();
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            ICollection<BandejaFiltrosModel> lstClienteModel = ObjectMapper.Instance.Convert<ICollection<clsModCatBandejaFiltros>, ICollection<BandejaFiltrosModel>>(_objNegCatBandejaFiltros.CargarBandejaFiltros(out objModErrorBase, strTextoBusqueda, out objModPaginacion, Pagina, RegistroPagina));
            lstClienteModel.ElementAt(0).bandejaFiltrosModelo = new BandejaFiltrosModel();
            lstClienteModel.ElementAt(0).objPaginacionModel = new PaginacionModel("BandejaFiltros", "ListaBandejaFiltros", objModPaginacion.Paginas, Pagina, objModPaginacion.TotalRegistros);
            return PartialView(lstClienteModel);
        }

        //
        // POST: /BandejaFiltros/Create
        [HttpPost]
        public JsonResult Create(BandejaFiltrosModel bandejaFiltros)
        {
            bandejaFiltros.IdUsuarioCreacion = 2;
            bandejaFiltros.IdUsuarioModificacion = 20;
            bandejaFiltros.IdEncConfBandeja = 2;
            clsModErrorBase objModErrorBase = null;
            ObjectMapper mapero = ObjectMapper.Instance;
            objModErrorBase = _objNegCatBandejaFiltros.AgregarBandejaFiltro(mapero.Convert<BandejaFiltrosModel, clsModCatBandejaFiltros>(bandejaFiltros));
            return Json(objModErrorBase);
        }

        //
        // GET: /BandejaFiltros/Edit/5
        public JsonResult Edit(int id)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            ObjectMapper mapero = ObjectMapper.Instance;
            BandejaFiltrosModel bandejaFiltros = mapero.Convert<clsModCatBandejaFiltros, BandejaFiltrosModel>(_objNegCatBandejaFiltros.CargarBandejaFiltrosPorId(out objModErrorBase, id));
            return Json(bandejaFiltros, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /BandejaFiltros/Edit/5
        [HttpPost]
        public JsonResult Edit(BandejaFiltrosModel bandejaFiltros)
        {
            clsModErrorBase objModErrorBase = null;
            ObjectMapper mapero = ObjectMapper.Instance;
            objModErrorBase = _objNegCatBandejaFiltros.ActualizarBandejaFiltros(mapero.Convert<BandejaFiltrosModel, clsModCatBandejaFiltros>(bandejaFiltros));
            return Json(objModErrorBase);
        }
        //
        // POST: /BandejaFiltros/Delete/5
        [HttpPost]
        public JsonResult Delete(BandejaFiltrosModel bandejaFiltros)
        {
            clsModErrorBase objModErrorBase = null;
            ObjectMapper mapero = ObjectMapper.Instance;
            objModErrorBase = _objNegCatBandejaFiltros.EliminarBandejaFiltros(mapero.Convert<BandejaFiltrosModel, clsModCatBandejaFiltros>(bandejaFiltros));
            return Json(objModErrorBase);
        }
    }
}
