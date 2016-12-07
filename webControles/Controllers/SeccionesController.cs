using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClbModControles;
using ClbNegControles;
using webControles.Models;
using webControles.Helpers;
using webControles.Models.Views;
using webControles.Controllers.Apis;
using ClbUtilerias.Resources.Strings.Generales;
using AutoMapper;

namespace webControles.Controllers
{
    public class SeccionesController : Controller
    {
        #region Variables
        /// <summary>
        /// Objeto modelo de secciones
        /// </summary>
        SeccionesModel objSeccionesModel;
        /// <summary>
        /// Variable privada de seguridad interna en capa de negocios
        /// </summary>
        private readonly clsNegCatSecciones objNegCatSecciones;
        /// <summary>
        /// Objeto modelo de paginación
        /// </summary>
        clsModPaginacion objModPaginacion;
        /// <summary>
        /// Variable que contiene el total de registros que se mostraran por pagina
        /// </summary>
        private const int RegistroPagina = 10;
        /// <summary>
        /// Objeto que contiene variables dentro de SeccionViewModel
        /// </summary>
        SeccionViewModel objSeccionViewModel;
        /// <summary>
        /// Objeto que nos permite traer error en dado caso que exista. Sino devuelve el resultado de cualquier petición
        /// </summary>
        clsModErrorBase objModErrorBase;
        /// <summary>
        /// Objeto que nos permite crear mensajes
        /// </summary>
        MensajeModel objMensajeModel;

        Catalogos objCatalogos = null;
        #endregion


        #region Metodos para obtención de datos dentro de ViewModels o correspondientes 
        private SeccionViewModel GetPagSeccionViewModel(string strTextoBusqueda, int intPagina = 1)
        {
            objModErrorBase = new clsModErrorBase();
            ICollection<clsModCatSecciones> lstModCatSeccion =
                objNegCatSecciones.CargarPagSecciones(out objModErrorBase, strTextoBusqueda, out objModPaginacion, intPagina, RegistroPagina);
            objSeccionViewModel.objPaginacionModel = new PaginacionModel("Secciones", "Lista", objModPaginacion.Paginas, intPagina, objModPaginacion.TotalRegistros);
            ICollection<SeccionesModel> lstClienteModel =
                ObjectMapper.Instance.Convert<ICollection<clsModCatSecciones>, ICollection<SeccionesModel>>(lstModCatSeccion);
            objSeccionViewModel.lstSeccionModel = lstClienteModel;
            return objSeccionViewModel;
        }

        #endregion
        /// <summary>
        /// Constructor de secciones
        /// </summary>
        public SeccionesController()
        {
            objSeccionesModel = new SeccionesModel();
            objNegCatSecciones = new clsNegCatSecciones();
            objSeccionViewModel = new SeccionViewModel();
            objCatalogos = new Catalogos();
        }
        //
        // GET: /Secciones/
        public ActionResult Index()
        {
            return View(GetPagSeccionViewModel(""));
        }
        //POST: /Secciones/Guardar
        [AllowCrossSiteJson]
        public ActionResult Guardar(int Id = 0)
        {
            int intIdFormuario = 0;
            clsNegCatSecciones NegCatsecciones = new clsNegCatSecciones();
            objModErrorBase = new clsModErrorBase();
            
            //Obtener id formulario por IdSeccion
            clsNegCatFormularios objNegCatFormulario = new clsNegCatFormularios();
            intIdFormuario = objNegCatFormulario.CargarIdPorIdSeccion(out objModErrorBase, Id);
            
            ViewBag.lstTipoSeccion = objCatalogos.GetCatalogoTipoSeccion();
            ViewBag.lstTipoControl = objCatalogos.GetCatalogoTipoControl();
            ViewBag.lstIdGrupo = objCatalogos.GetCatalogoGrupo(intIdFormuario);

            SeccionControlViewModel objSeccionControlViewModel = new SeccionControlViewModel();
            if (Id != 0)
            {
                
                objModErrorBase = null;
                clsModCatSecciones objModCatSeccion = objNegCatSecciones.CargarSeccionPorId(out objModErrorBase, Id);
                objSeccionControlViewModel.ObjSeccionesModel = ObjectMapper.Instance.Convert<clsModCatSecciones, SeccionesModel>(objModCatSeccion);
    
            }

            
            ViewBag.Iconos = objCatalogos.GetCatalogoIconoSeccion();
            return PartialView(objSeccionControlViewModel);
        }

        [AllowCrossSiteJson]
        public JsonResult GetIconos()
        {
            return Json(objCatalogos.GetCatalogoIconoSeccion(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowCrossSiteJson]
        public JsonResult GuardarGrupo(clsModCatSecciones objModSecciones)
        {
            clsModErrorBase objModErrorBase = null;
            if (ModelState.IsValid)
            {
                clsNegCatSecciones objNegSecciones = new clsNegCatSecciones();
                objModErrorBase = objNegSecciones.AgregarGrupo(objModSecciones);
            }
            else
            {
                objModErrorBase = new clsModErrorBase();
                objModErrorBase.MsgError = "Modelo Invalido";
            }

            return Json(objModErrorBase);
        }

        [HttpGet]
        public JsonResult ObtenerGrupos(int IdFormulario)
        {

            IEnumerable<ComboCatalogosModel> lstComboCatalogo = objCatalogos.GetCatalogoGrupo(IdFormulario);

            return Json(lstComboCatalogo, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Lista(string TextoBusqueda = "", int Pagina = 1)
        {
            //return PartialView("Guardar", new SeccionesModel());
            return PartialView("Guardar");
        }
        /// <summary>
        /// Mapeo de datos a la clase modelos y guarda secciones en base de datos
        /// </summary>
        /// <param name="objSeccionesModel">Array Json enviado desde Ajax</param>
        /// <returns>Json con modelo</returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public JsonResult GuardarSeccion(SeccionControlViewModel objSeccionControlViewModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            //transforma las secciones
            clsModCatSecciones objModCatSecciones = ObjectMapper.Instance.Convert<SeccionesModel, clsModCatSecciones>(objSeccionControlViewModel.ObjSeccionesModel);
            //transforma los controles de las secciones
            objModCatSecciones.LstSeccionControl = ObjectMapper.Instance.Convert<ICollection<SeccionControlModel>, ICollection<clsModCatSeccionControl>>(objSeccionControlViewModel.LstSeccionControl);

            objModErrorBase = objNegCatSecciones.Guardar(objModCatSecciones);
            if (!string.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                objMensajeModel = new MensajeModel(true, objModErrorBase.MsgError);
            }
            else
            {
                objMensajeModel = new MensajeModel(false, Generales.SuccessSeccion, objModErrorBase.Id);
            }
            return Json(objMensajeModel);
        }


        /// <summary>
        /// Actualiza el orden de la seccion
        /// </summary>
        [HttpPost]
        public JsonResult ActualizarOrden(clsModCatSecciones objModSecciones)
        {
            clsModErrorBase objModErrorBase = null;
            if (objModSecciones != null)
            {
                clsNegCatSecciones objNegSecciones = new clsNegCatSecciones();
                objModErrorBase = objNegSecciones.ActualizarOrden(objModSecciones.IdSeccion, objModSecciones.Orden);
            }
            else
            {
                objModErrorBase = new clsModErrorBase();
                objModErrorBase.MsgError = "Modelo Invalido";
            }

            return Json(objModErrorBase);
        }

  
        [AllowCrossSiteJson]
        public JsonResult getDBTablesName()
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsNegGetDBTablesName objClsNegGetDbTablesName = new clsNegGetDBTablesName();
            List<getDBTablesNameModel> lstDbTablesNameModel = new List<getDBTablesNameModel>();

            List<clsModGetDBTablesName> lstModGetDbTablesNames = new List<clsModGetDBTablesName>();
            lstModGetDbTablesNames = objClsNegGetDbTablesName.GetDbTablesNames(out objModErrorBase);

            lstDbTablesNameModel =
                ObjectMapper.Instance.Convert<List<clsModGetDBTablesName>, List<getDBTablesNameModel>>(
                    lstModGetDbTablesNames);
            return Json(lstDbTablesNameModel,JsonRequestBehavior.AllowGet);
        }

        [AllowCrossSiteJson]
        public JsonResult getTableColumnName(getTableColumnsNamesModel objGetTableColumnsNamesModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsNegGetTableColumnsNames objNegGetTableColumnsNames = new clsNegGetTableColumnsNames();
            List<getTableColumnsNamesModel> lstGetColumnsNamesModel = new List<getTableColumnsNamesModel>();

            clsModGetTableColumnsNames objModGetTableColumnsNames =
                ObjectMapper.Instance.Convert<getTableColumnsNamesModel, clsModGetTableColumnsNames>(
                    objGetTableColumnsNamesModel);

            List<clsModGetTableColumnsNames> lstModGetTableColumnsNames = new List<clsModGetTableColumnsNames>();
            lstModGetTableColumnsNames = objNegGetTableColumnsNames.GetDbTablesNames(out objModErrorBase, objModGetTableColumnsNames);

            lstGetColumnsNamesModel =
                ObjectMapper.Instance.Convert<List<clsModGetTableColumnsNames>, List<getTableColumnsNamesModel>>(
                    lstModGetTableColumnsNames);
            return Json(lstGetColumnsNamesModel, JsonRequestBehavior.AllowGet);
        }
	}
}