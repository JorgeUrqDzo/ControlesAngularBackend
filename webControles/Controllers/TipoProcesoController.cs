using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClbNegControles;
using ClbModControles;
using webControles.Models;
using webControles.Helpers;
using webControles.Controllers.Apis;

namespace webControles.Controllers
{
    public class TipoProcesoController : Controller
    {

        public const int RegistroPagina = 10;
        public readonly clsNegCatTipoProceso objNegCatTipoProceso;
        private clsModErrorBase objModErrorBase;
        private TipoProcesoModel objTipoProcesoModel;
        private clsModPaginacion objModPaginacion;
        private Catalogos objCatalogos;

         //Recibe el modelo de la vista y lo manda a la base de datos para agregarse.

         public TipoProcesoController()
        {
            objNegCatTipoProceso = new clsNegCatTipoProceso();
            objTipoProcesoModel = new TipoProcesoModel();
        }
         #region getPagTipoProcesoModel
         private TipoProcesoModel GetPagTipoProcesoModel(string strTextoBuscar, int intPagina = 1)
         {


             if (strTextoBuscar == null)
                 strTextoBuscar = "";

             objTipoProcesoModel = new TipoProcesoModel();


             ICollection<clsModCatTipoProceso> listModCatTipoProceso =
                 objNegCatTipoProceso.CargarTodos(out objModErrorBase, strTextoBuscar, out objModPaginacion, intPagina, RegistroPagina);

             objTipoProcesoModel.objPaginacionModel = new PaginacionModel("TipoProceso", "Lista", objModPaginacion.Paginas, intPagina, objModPaginacion.TotalRegistros);


             ICollection<TipoProcesoModel> listTipoProcesoModel =
                 ObjectMapper.Instance.Convert<ICollection<clsModCatTipoProceso>, ICollection<TipoProcesoModel>>(listModCatTipoProceso);
             objTipoProcesoModel.listTipoProcesoModel = listTipoProcesoModel;
             return objTipoProcesoModel;
         }
         #endregion

         #region Index
         //
        // GET: /TipoProceso/
        public ActionResult Index()
        {
            ViewBag.lstAreaProceso = GetCatalogoAreaProceso();
            return View(GetPagTipoProcesoModel(""));
        }

        #endregion

        private IEnumerable<ComboCatalogosModel> GetCatalogoAreaProceso()
        {
            objCatalogos = new Catalogos();
            return objCatalogos.GetCatalogoAreaProceso();
        }

        #region Lista
        public ActionResult Lista(string TextoBuscar = "", int Pagina = 1)
        {

            return PartialView(GetPagTipoProcesoModel(TextoBuscar, Pagina));
        }

        #endregion 

        #region Create
        public ActionResult Create(int Id = 0)
        {
            ViewBag.lstAreaProceso = GetCatalogoAreaProceso();

            //Valida el id es igual a 0 devuelve un objeto nuevo si es diferente de cero manda llamar a la funcion de cargar el modelo
            if (Id != 0)
            {
                objModErrorBase = null;
                clsModCatTipoProceso objModCatTipoProceso = objNegCatTipoProceso.CargarPorId(Id, out objModErrorBase);
                objTipoProcesoModel = ObjectMapper.Instance.Convert<clsModCatTipoProceso, TipoProcesoModel>(objModCatTipoProceso);
                return PartialView(objTipoProcesoModel);
            }
            else
            {

                return PartialView(new TipoProcesoModel());
            }
        }
        #endregion


        #region Agregar
        [HttpPost]
        public JsonResult Agregar(TipoProcesoModel objTipoProcesoModel)
        {
            clsModCatTipoProceso objModCatTipoProceso = ObjectMapper.Instance.Convert<TipoProcesoModel, clsModCatTipoProceso>(objTipoProcesoModel);
            MensajeModel objMensajeModel = null;
            objModErrorBase = objNegCatTipoProceso.Agregar(objModCatTipoProceso);

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

        public JsonResult ActualizarEstado(TipoProcesoModel objTipoProcesoModel)
        {
            clsNegCatTipoProceso objNegCatTipoProceso = new clsNegCatTipoProceso();

            clsModCatTipoProceso capaModelo = new clsModCatTipoProceso();

            capaModelo = ObjectMapper.Instance.Convert<TipoProcesoModel, clsModCatTipoProceso>(objTipoProcesoModel);

            clsModErrorBase objModErrorBase = new clsModErrorBase();
            MensajeModel objMensajeModel = null;
            objModErrorBase = objNegCatTipoProceso.ActualizarEstado(capaModelo);
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
    }

}