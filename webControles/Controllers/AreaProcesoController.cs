using ClbModControles;
using ClbNegControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webControles.Helpers;
using webControles.Models;
using webControles.Models.Views;

namespace webControles.Controllers
{
    public class AreaProcesoController : Controller
    {

        //Total de registros que estaran por página
        public const int RegistroPagina = 10;

        public AreaProcesoViewModel GetPagBusquedaAreaProcesoViewModel(string strTextoBuscar, int intPagina = 1)
        {
            if (strTextoBuscar == null)
                strTextoBuscar = "";

            AreaProcesoModel objAreaProcesoModel = new AreaProcesoModel();
            AreaProcesoViewModel objAreaProcesoViewModel = new AreaProcesoViewModel();
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsNegCatAreaProceso objNegCatAreaProceso = new clsNegCatAreaProceso();
            clsModPaginacion objModPaginacion = new clsModPaginacion();


            ICollection<clsModCatAreaProceso> lstModCatAreaProceso =
                objNegCatAreaProceso.CargarCatAreaProceso(out objModErrorBase, strTextoBuscar, out objModPaginacion, intPagina, RegistroPagina);

            ICollection<AreaProcesoModel> lstModelo =
               ObjectMapper.Instance.Convert<ICollection<clsModCatAreaProceso>, ICollection<AreaProcesoModel>>(lstModCatAreaProceso);

            objAreaProcesoViewModel.objPaginacionModel = new PaginacionModel("AreaProceso", "Lista", objModPaginacion.Paginas, intPagina, objModPaginacion.TotalRegistros);

            objAreaProcesoViewModel.lstAreaProcesoModel = lstModelo;
            objAreaProcesoViewModel.objAreaProcesoModel = new AreaProcesoModel();

            return objAreaProcesoViewModel;
        }

        public ActionResult Lista(string TextoBuscar = "", int Pagina = 1)
        {
            return PartialView(GetPagBusquedaAreaProcesoViewModel(TextoBuscar, Pagina));
        }

        #region Index

        //
        // GET: /AreaProceso/
        public ActionResult Index()
        {
            return View(GetPagBusquedaAreaProcesoViewModel(""));
        }

        #endregion

        #region Details

        //
        // GET: /AreaProceso/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        #endregion

        #region Create

        //
        // GET: /AreaProceso/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AreaProceso/Create
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

        #endregion

        #region Edit
        //
        // GET: /AreaProceso/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /AreaProceso/Edit/5
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
        #endregion

        #region Delete

        //
        // GET: /AreaProceso/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        //
        // POST: /AreaProceso/Delete/5
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

        #region Metodo Crear
        [HttpPost]
        public JsonResult Crear(AreaProcesoModel objAreaProcesoModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsNegCatAreaProceso objNegCatAreaProceso = new clsNegCatAreaProceso();
            clsModCatAreaProceso capaModelo = new clsModCatAreaProceso();


            capaModelo = ObjectMapper.Instance.Convert<AreaProcesoModel, clsModCatAreaProceso>(objAreaProcesoModel);
            MensajeModel objMensajeModel = null;
            objModErrorBase = objNegCatAreaProceso.Crear(capaModelo);
            if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                objMensajeModel = new MensajeModel(true, objModErrorBase.MsgError);
            }
            else
            {
                objMensajeModel = new MensajeModel(false, objModErrorBase.MsgError);
            }

            return Json(objMensajeModel);
        }
        #endregion 

        #region Metodo CargarXId   
        [HttpPost]
        public JsonResult CargarXId(AreaProcesoModel objAreaProcesoModel)
        {

            clsModErrorBase objModErrorBase = new clsModErrorBase();

            clsNegCatAreaProceso objNegCatAreaProceso = new clsNegCatAreaProceso();

            clsModCatAreaProceso capaModelo = new clsModCatAreaProceso();

            capaModelo = ObjectMapper.Instance.Convert<AreaProcesoModel, clsModCatAreaProceso>(objAreaProcesoModel);

           return Json(ObjectMapper.Instance.Convert<clsModCatAreaProceso,AreaProcesoModel>( objNegCatAreaProceso.CargarXId(capaModelo, out objModErrorBase)));

        }
        #endregion

        #region Actualizar
        [HttpPost]
        public JsonResult Actualizar(AreaProcesoModel objAreaProcesoModel)
        {
            clsNegCatAreaProceso objNegCatAreaProceso = new clsNegCatAreaProceso();

            clsModCatAreaProceso capaModelo = new clsModCatAreaProceso();

            capaModelo = ObjectMapper.Instance.Convert<AreaProcesoModel, clsModCatAreaProceso>(objAreaProcesoModel);

            clsModErrorBase objModErrorBase = new clsModErrorBase();
            MensajeModel objMensajeModel = null;
            objModErrorBase = objNegCatAreaProceso.Actualizar(capaModelo);
            if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                objMensajeModel = new MensajeModel(true, objModErrorBase.MsgError);
            }
            else
            {
                objMensajeModel = new MensajeModel(false, objModErrorBase.MsgError);
            }

            return Json(objMensajeModel);
        }
        #endregion

        #region Actualizar Estado
        [HttpPost]
        public JsonResult ActualizarEstado(AreaProcesoModel objAreaProcesoModel)
        {
            clsNegCatAreaProceso objNegCatAreaProceso = new clsNegCatAreaProceso();

            clsModCatAreaProceso capaModelo = new clsModCatAreaProceso();

            capaModelo = ObjectMapper.Instance.Convert<AreaProcesoModel, clsModCatAreaProceso>(objAreaProcesoModel);

            clsModErrorBase objModErrorBase = new clsModErrorBase();
            MensajeModel objMensajeModel = null;
            objModErrorBase = objNegCatAreaProceso.ActualizarEstado(capaModelo);
            if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                objMensajeModel = new MensajeModel(true, objModErrorBase.MsgError);
            }
            else
            {
                objMensajeModel = new MensajeModel(false, objModErrorBase.MsgError);
            }

            return Json(objMensajeModel);
        }
        #endregion


    }
}
