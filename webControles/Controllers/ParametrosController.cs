using ClbModControles;
using ClbNegControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webControles.Controllers.Apis;
using webControles.Helpers;
using webControles.Models;
using webControles.Models.Views;

namespace webControles.Controllers
{
    public class ParametrosController : Controller
    {
        //
        // GET: /Parametros/
        public ActionResult Index()
        {
            clsNegDetConfBandejaLinkParametro objCapaNegocio = new clsNegDetConfBandejaLinkParametro();

            ParametrosModel objParametrosModel = new ParametrosModel();

            List<ParametrosModel> lstParametrosModel = new List<ParametrosModel>();

            lstParametrosModel = ObjectMapper.Instance.Convert<List<clsModDetConfBandejaLinkParametro>, List<ParametrosModel>>(objCapaNegocio.CargarParametros());

            ParametrosViewModel objParametrosViewModel = new ParametrosViewModel();
            objParametrosViewModel.lstParametrosModel = lstParametrosModel;


            Catalogos objCatalogos = new Catalogos();

            ViewBag.lstTipoParametro = objCatalogos.GetCatalogoTipoParmetro();
            ViewBag.lstTipoEnvio = objCatalogos.GetCatalogoTipoEnvio();

            return PartialView(objParametrosViewModel);
        }

        #region MetodosDefault
        

        //
        // GET: /Parametros/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Parametros/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Parametros/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Parametros/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Parametros/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Parametros/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Parametros/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion


        public JsonResult CargarParametros(ParametrosModel objParametrosModel)
        {
            clsModErrorBase objModErroBase = new clsModErrorBase();
            clsNegDetConfBandejaLinkParametro objCapaNegocio = new clsNegDetConfBandejaLinkParametro();
            ParametrosViewModel objParametrosViewModel = new ParametrosViewModel();

            List<ParametrosModel> lstParametrosModel = new List<ParametrosModel>();

            clsModDetConfBandejaLinkParametro objDetConfBandejaLinkParametro =
                ObjectMapper.Instance.Convert<ParametrosModel, clsModDetConfBandejaLinkParametro>(objParametrosModel);

            lstParametrosModel = ObjectMapper.Instance.Convert<List<clsModDetConfBandejaLinkParametro>, List<ParametrosModel>>(objCapaNegocio.CargarParametros(out objModErroBase, objDetConfBandejaLinkParametro.IdEncConfBandeja));
            objParametrosViewModel.lstParametrosModel = lstParametrosModel;

            return Json(objParametrosViewModel);
        }


        #region Metodo para Agregar Parametros
        public JsonResult AgregarParametros(ParametrosModel objParametrosModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsModDetConfBandejaLinkParametro objCapaModelo = new clsModDetConfBandejaLinkParametro();
            objCapaModelo = ObjectMapper.Instance.Convert<ParametrosModel, clsModDetConfBandejaLinkParametro>(objParametrosModel);

            clsNegDetConfBandejaLinkParametro objCapaNegocio = new clsNegDetConfBandejaLinkParametro();
            objModErrorBase = objCapaNegocio.InsertarParametros(objCapaModelo);

            if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                return Json(objModErrorBase.MsgError);
            }

            return Json(objModErrorBase.Id);
        }
        #endregion

        #region Metodo para Eliminar Parametro
        public JsonResult EliminarParametro(ParametrosModel objParametrosModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsModDetConfBandejaLinkParametro objCapaModelo = new clsModDetConfBandejaLinkParametro();
            objCapaModelo = ObjectMapper.Instance.Convert<ParametrosModel, clsModDetConfBandejaLinkParametro>(objParametrosModel);

            clsNegDetConfBandejaLinkParametro objCapaNegocio = new clsNegDetConfBandejaLinkParametro();
            objModErrorBase = objCapaNegocio.EliminarParametro(objCapaModelo.IdDetConfBandejaLinkParametro);

            if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                return Json(objModErrorBase.MsgError);
            }

            return Json(objModErrorBase.Id);
        }
        
        #endregion

        #region Metodo para Cargar Parametros X Id
        public JsonResult CargarParametroXId(ParametrosModel objParametrosModel)
        {
            
            clsNegDetConfBandejaLinkParametro objCapaNegocio = new clsNegDetConfBandejaLinkParametro();

            clsModErrorBase objModErrorBase = new clsModErrorBase();

            clsModDetConfBandejaLinkParametro objCapaModelo = new clsModDetConfBandejaLinkParametro();

            objCapaModelo = ObjectMapper.Instance.Convert<ParametrosModel, clsModDetConfBandejaLinkParametro>(objParametrosModel);

            objCapaModelo = objCapaNegocio.CargarParametroXId(objCapaModelo.IdDetConfBandejaLinkParametro, out objModErrorBase);

            return Json(objCapaModelo);
        }
        #endregion

        #region Metodo para Actualizar Parametros
        public JsonResult ActualizarParametros(ParametrosModel objParametrosModel)
        {
            clsNegDetConfBandejaLinkParametro objCapaNegocio = new clsNegDetConfBandejaLinkParametro();
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            clsModDetConfBandejaLinkParametro objCapaModelo = new clsModDetConfBandejaLinkParametro();
            objCapaModelo = ObjectMapper.Instance.Convert<ParametrosModel, clsModDetConfBandejaLinkParametro>(objParametrosModel);

            objModErrorBase = objCapaNegocio.ActualizarParametros(objCapaModelo);
          
            if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                return Json(objModErrorBase.MsgError);
            }

            return Json(objModErrorBase.Id);
        }
        #endregion


    }
}
