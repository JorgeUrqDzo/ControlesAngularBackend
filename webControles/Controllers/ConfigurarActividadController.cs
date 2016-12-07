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
    public class ConfigurarActividadController : Controller
    {

        #region Variables locales
        #region Total registros por pagina en lista
        //Total de registros que estaran por página
        public const int RegistroPagina = 10;
        #endregion
        #endregion

        #region Index
        //
        // GET: /ConfigurarActividad/
        public ActionResult Index()
        {
            return View(GetPagBusquedaAreaProcesoViewModel(""));
        }

        #endregion

        #region Details
        //
        // GET: /ConfigurarActividad/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        #endregion

        #region Create()
        //
        // GET: /ConfigurarActividad/Create
        public ActionResult Create()
        {
            return View();
        }
        #endregion
       
        #region Create(FormCollection collection)
        //
        // POST: /ConfigurarActividad/Create
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
        // GET: /ConfigurarActividad/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        #endregion

        #region Edit(int id, FormCollection collection)
        //
        // POST: /ConfigurarActividad/Edit/5
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
        // GET: /ConfigurarActividad/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        #endregion

        #region Delete(int id, FormCollection collection)
        //
        // POST: /ConfigurarActividad/Delete/5
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


  // ------------------------------------------------- //


        #region GetPagBusquedaConfigurarActividadViewModel
        public ConfigurarActividadViewModel GetPagBusquedaAreaProcesoViewModel(string strTextoBuscar, int intPagina = 1)
        {
            if (strTextoBuscar == null)
                strTextoBuscar = "";

            EncProcesoModel objEncProcesoModel = new EncProcesoModel();
            ConfigurarActividadViewModel objConfigurarActividadViewModel = new ConfigurarActividadViewModel();

            clsModErrorBase objModErrorBase = new clsModErrorBase();

            clsNegEncProceso objNegEncProceso = new clsNegEncProceso();

            clsModPaginacion objModPaginacion = new clsModPaginacion();


            ICollection<clsModEncProceso> lstModEncProceso =
                objNegEncProceso.CargarEncProceso(out objModErrorBase, strTextoBuscar, out objModPaginacion, intPagina, RegistroPagina);

            ICollection<EncProcesoModel> lstModelo =
               ObjectMapper.Instance.Convert<ICollection<clsModEncProceso>, ICollection<EncProcesoModel>>(lstModEncProceso);

            objConfigurarActividadViewModel.objPaginacionModel = new PaginacionModel("ConfigurarActividad", "Lista", objModPaginacion.Paginas, intPagina, objModPaginacion.TotalRegistros);

            objConfigurarActividadViewModel.lstEncProcesoModel = lstModelo;
            objConfigurarActividadViewModel.objEncProcesoModel = new EncProcesoModel();

            return objConfigurarActividadViewModel;
        }
        #endregion

        #region Lista
        public ActionResult Lista(string TextoBuscar = "", int Pagina = 1)
        {
            return PartialView(GetPagBusquedaAreaProcesoViewModel(TextoBuscar, Pagina));
        }
        #endregion

        #region CargarXId
        [HttpPost]
        public JsonResult CargarXId(EncProcesoModel objEncProcesoModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsModEncProceso objModEncProceso = new clsModEncProceso();
            clsNegEncProceso objNegEncProceso = new clsNegEncProceso();
            EncProcesoModel objEncProcesoModel_2 = new EncProcesoModel();

            objModEncProceso = ObjectMapper.Instance.Convert<EncProcesoModel, clsModEncProceso>(objEncProcesoModel);

            objEncProcesoModel_2 = ObjectMapper.Instance.Convert<clsModEncProceso, EncProcesoModel>(objNegEncProceso.CargarXId(objModEncProceso, out objModErrorBase));

            return Json(objEncProcesoModel_2);
        }
        #endregion

        #region Actividades Por Tipo de Proceso
        [HttpPost]
        public JsonResult obtenerActividadesXTipo()
        {
            clsNegEncProceso objNegEncProceso = new clsNegEncProceso();
            List<ActividadModel> lstActividadModel = new List<ActividadModel>();
            clsModErrorBase objModErrorBase = new clsModErrorBase();
          
           lstActividadModel =
                ObjectMapper.Instance.Convert<List<clsModCatActividad>, List<ActividadModel>>(
                    objNegEncProceso.ActividadesXTipoProceso(objModErrorBase));

            return Json(lstActividadModel);
        }
        #endregion

        #region guardar cambios

        [HttpPost]
        public JsonResult Guardar(EncProcesoModel objEncProcesoModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            MensajeModel objMensajeModel = new MensajeModel();
            clsNegEncProceso objNegEncProceso = new clsNegEncProceso();
            clsModEncProceso objEncProceso = new clsModEncProceso();

            objEncProceso = ObjectMapper.Instance.Convert<EncProcesoModel, clsModEncProceso>(objEncProcesoModel);
            objNegEncProceso.Guardar(objEncProceso);


            if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                objMensajeModel = new MensajeModel(true, objModErrorBase.MsgError);
            }
            else
            {
                objMensajeModel = new MensajeModel(false, "Datos Guardados", objModErrorBase.Id);
            }

            return Json(objMensajeModel);
        }
        #endregion
    }
}

