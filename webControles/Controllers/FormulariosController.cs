using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClbModControles;
using ClbNegControles;
using webControles.Models;
using webControles.Models.Views;
using webControles.Helpers;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using webControles.Controllers.Apis;

namespace webControles.Controllers
{
    public class FormulariosController : Controller
    {
        #region Variables
       

        FormularioModel objFormularioModel;
        /// <summary>
        /// Variable privada de seguridad interna en capa de negocios
        /// </summary>
        private readonly clsNegCatFormularios objNegCatFormulario;
        /// <summary>
        /// Objeto perteneciente a modelo paginación
        /// </summary>
        clsModPaginacion objModPaginacion;
        /// <summary>
        /// Variable que contiene el total de registros que se mostraran por pagina
        /// </summary>
        private const int RegistroPagina = 50;
        /// <summary>
        /// Objeto que contiene variables dentro de FormularioViewModel
        /// </summary>
        FormularioViewModel objFormularioViewModel;
        /// <summary>
        /// Objeto que nos permite treaer error en dado caso que exista. Sino devuelve el resultado de cualquier petición
        /// </summary>
        clsModErrorBase objModErrorBase;
        /// <summary>
        /// Objeto que nos permite crear mensajes
        /// </summary>
        MensajeModel objMensajeModel;
        #endregion

        /// <summary>
        ///Constructor de formularios 
        /// </summary>
        public FormulariosController()
        {
           
            objFormularioModel = new FormularioModel();
            objNegCatFormulario = new clsNegCatFormularios();
            objFormularioViewModel = new FormularioViewModel();
        }

        //public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
        //{
        //    public override void OnActionExecuting(ActionExecutingContext filterContext)
        //    {
        //        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
        //        base.OnActionExecuting(filterContext);
        //    }
        //}

        #region Metodos para obtención de datos dentro de ViewModels o correspondientes
        /// <summary>
        /// Obtiene un listado de formularios que estan en la base de datos
        /// </summary>
        /// <returns></returns>
        //private FormularioViewModel GetFormularioViewModel()
        //{
        //    clsModErrorBase objModErrorBase = new clsModErrorBase();
        //    FormularioViewModel objFomularioViewModel = new FormularioViewModel();
        //    ICollection<clsModCatFormularios> lstModCatFormularios =
        //        objNegCatFormulario.CargarFomularios(out objModErrorBase);

        //    ICollection<FormularioModel> lstClienteFormulario =
        //        ObjectMapper.Instance.Convert<ICollection<clsModCatFormularios>, ICollection<FormularioModel>>(lstModCatFormularios);
        //    objFomularioViewModel.lstFormularioModel = lstClienteFormulario;
        //    return objFomularioViewModel;
        //}

        /// <summary>
        /// Metodo para obtener datos con paginación
        /// </summary>
        /// <param name="strTextoBusqueda">String de busqueda, en dado caso que se requiera</param>
        /// <param name="intPagina">Pagina con la que iniciara</param>
        /// <returns></returns>
        private FormularioViewModel GetPagFormularioViewModel(string strTextoBusqueda, int intPagina = 1)
       {
           if (strTextoBusqueda == null)
               strTextoBusqueda = "";

           objFormularioViewModel = new FormularioViewModel();
            clsModErrorBase objModErrorBase = new clsModErrorBase();


            ICollection<clsModCatFormularios> lstModCatFormularios =
                objNegCatFormulario.CargarFomularios(out objModErrorBase, strTextoBusqueda, out objModPaginacion, intPagina, RegistroPagina);



            objFormularioViewModel.objPaginacionModel = new PaginacionModel("Formularios", "Lista", objModPaginacion.Paginas, intPagina, objModPaginacion.TotalRegistros);




            ICollection<FormularioModel> lstClienteModel =
                ObjectMapper.Instance.Convert<ICollection<clsModCatFormularios>, ICollection<FormularioModel>>(lstModCatFormularios);
            objFormularioViewModel.lstFormularioModel = lstClienteModel;
            return objFormularioViewModel;
        }
        #endregion

        #region Metodos Acciones de vistas pertenecientes a FormularioController
        // GET: /Formulario/
        [AllowCrossSiteJson]
        public JsonResult getFormularios()
        {
            return Json(GetPagFormularioViewModel(""),JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View(GetPagFormularioViewModel(""));
        }
        // POST: /Formulario/Guardar
        public ActionResult Guardar(int Id = 0)
        {
            if (Id != 0)
            {
                //TODO: Joel Hernandez: Agregar metodo de capa de negocios cargar programa por ID
                objModErrorBase = null;
                clsModCatFormularios objModCatFormulario = objNegCatFormulario.CargarFormularioPorId(out objModErrorBase, Id);
                objFormularioModel = ObjectMapper.Instance.Convert<clsModCatFormularios, FormularioModel>(objModCatFormulario);
                return PartialView(objFormularioModel);
            }
            else
            {
                Catalogos objCatalogos = new Catalogos();
                ViewBag.tipoFormulario = objCatalogos.GetCatalogoTipoFormulario();
                return PartialView(new FormularioModel());
            }
        }
        // GET: /Formulario/ListaFormularios
        public ActionResult ListaFormularios(string strTextoBusqueda, int intPagina = 1)
        {
            clsNegCatFormularios objNegCatFormularios = new clsNegCatFormularios();
            clsModErrorBase objModErrorBase = null;
            ICollection<clsModCatFormularios> lstFormularios = objNegCatFormulario.CargarFomularios(out objModErrorBase, strTextoBusqueda, out objModPaginacion, intPagina, RegistroPagina);
            ICollection<FormularioModel> lst = ObjectMapper.Instance.Convert<ICollection<clsModCatFormularios>, ICollection<FormularioModel>>(lstFormularios);
            return View(lst);
        }

        [HttpGet]
        public ContentResult ObtenerUUID(int varId=0)
        {
            string UUID;

            
                clsNegCatFormularios objNetCatFormularios = new clsNegCatFormularios();
                clsModErrorBase objModErrorBase = null;
                UUID = objNegCatFormulario.CargarUUIDXId(out objModErrorBase, varId);
            
                return Content(UUID);
            
        }
        // GET: /Formulario/FormulariosCreados
        [HttpGet]
        public ActionResult FormulariosCreados(string TextoBusqueda = "", int Pagina = 1)
        {
           // return PartialView("Guardar",new FormularioModel());
            return PartialView(GetPagFormularioViewModel(TextoBusqueda, Pagina));
        }
        // EDIT: /Formulario/Actualizar
        public ActionResult Actualizar()
        {
            return View();
        }
        /// <summary>
        /// Acción que devuelve vista parcial de formularios en lista
        /// </summary>
        /// <param name="TextoBusqueda">Búsqueda</param>
        /// <param name="Pagina">Página seleccionada en paginador</param>
        /// <returns>Vista parcial</returns>
        [HttpGet]
        public ActionResult Lista(string TextoBusqueda = "", int Pagina = 1)
        {
            if (string.IsNullOrEmpty(TextoBusqueda)) { TextoBusqueda = ""; }
            return PartialView("Lista",GetPagFormularioViewModel(TextoBusqueda, Pagina));
        }
        #endregion

        #region Metodos ALTAS, BAJAS, CONSULTAS Y MODIFICACIONES de objetos Json
        /// <summary>
        /// Mapeo de datos a la clase de modelos y guarda formularios en base de datos
        /// </summary>
        /// <param name="objFormularioModel">Array Json enviado desde Ajax</param>
        /// <returns>Json con modelo</returns>


        [HttpPost]
        [AllowCrossSiteJson]
        public JsonResult Guardar(FormularioModel objFormularioModel)
        {
            Formulario objFormulario = new Formulario(); ;

            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsModCatFormularios objModCatFormularios = ObjectMapper.Instance.Convert<FormularioModel, clsModCatFormularios>(objFormularioModel);
            objModErrorBase = objNegCatFormulario.GuardarFormulario(objModCatFormularios);
            if (!string.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                objFormulario.ObjMensajeModel = new MensajeModel(true, objModErrorBase.MsgError);
            }
            else
            {
                objFormulario.ObjMensajeModel = new MensajeModel(false, "Formulario agregado con exito", objModErrorBase.Id);
                objFormulario.UUID = objModCatFormularios.UUID;
            }

            return Json(objFormulario, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Actualizar(FormularioModel objFormularioModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            if (objFormularioModel != null && ModelState.IsValid)
            {
                clsModCatFormularios objModCatFormulario = ObjectMapper.Instance.Convert<FormularioModel, clsModCatFormularios>(objFormularioModel);
                objModErrorBase = objNegCatFormulario.GuardarFormulario(objModCatFormulario);
                if (!string.IsNullOrEmpty(objModErrorBase.MsgError))
                {
                    ModelState.AddModelError(string.Empty, objModErrorBase.MsgError);
                }
                else
                {
                    
                }
            }
            else
            {
                string msgError = "Error General"; //$"{Generales.ErrorGeneral}";
                ModelState.AddModelError(string.Empty, msgError);
            }
            return Json(objModErrorBase);
        }

        //public JsonResult CargarUUID(FormularioModel objFormularioModel)
        //{
        //    clsModErrorBase objModErrorBase = new clsModErrorBase();
        //}

        public struct Formulario
        {

            public MensajeModel ObjMensajeModel { get; set; }
            public string UUID { get; set; }
        }
        #endregion

        [HttpPost]
        public JsonResult GetIdTipoFormulario(FormularioModel objFormularioModel)
        {
            clsNegCatFormularios objNetCatFormularios = new clsNegCatFormularios();
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            FormularioModel formularioModel = new FormularioModel();
            clsModCatFormularios objModCatFormularios = objNetCatFormularios.getIdTipoFormulario(out objModErrorBase, objFormularioModel.IdFormulario);
            formularioModel = ObjectMapper.Instance.Convert<clsModCatFormularios, FormularioModel>(objModCatFormularios);

            return Json(formularioModel);
        }

        public JsonResult GetSeccionesDeFormulario(FormularioModel objForm)
        {
            clsNegCatFormularios objMoCatFormularios = new clsNegCatFormularios();
            clsModErrorBase objModErrorBase;

            return Json(objMoCatFormularios.CargarSeccionesPorFormulario(objForm.IdFormulario,out objModErrorBase));
        }
    }
}