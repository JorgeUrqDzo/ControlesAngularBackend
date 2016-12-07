using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClbModControles;
using ClbNegControles;
using ClbUtilerias.Resources.Strings.Generales;
using webControles.Controllers.Apis;
using webControles.Helpers;
using webControles.Models;
using webControles.Models.Views;

namespace webControles.Controllers
{
    public class ActividadController : Controller
    {

        public const int RegistroPagina = 10;
        public readonly clsNegCatActividad objNegCatActividad;
        private clsModErrorBase objModErrorBase;
        private ActividadModel objActividadModel;
        private clsModPaginacion objModPaginacion;
        private Catalogos objCatalogos;


        public ActividadController()
        {
            objNegCatActividad = new clsNegCatActividad();
            objActividadModel = new ActividadModel();
        }

        //
        // GET: /Actividad/
        public ActionResult Index()
        {
            ViewBag.lstTipoProceso = GetCatalogoTipoProceso();
            return View(GetPagActividadModel(""));
        }

         [HttpPost]
        public JsonResult Agregar(ActividadModel objActividadModel)
        {

                clsModCatActividad objModCatActividad = ObjectMapper.Instance.Convert<ActividadModel, clsModCatActividad>(objActividadModel);
                //clsNegCatActividad objNegCatActividad = new clsNegCatActividad();
                MensajeModel objMensajeModel = null;
                 objModErrorBase = objNegCatActividad.AgregarCatActividad(objModCatActividad);

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

        private ActividadModel GetPagActividadModel(string strTextoBuscar, int intPagina = 1)
        {


            if (strTextoBuscar == null)
                strTextoBuscar = "";

            objActividadModel = new ActividadModel();
     

            ICollection<clsModCatActividad> listModCatActividad =
                objNegCatActividad.CargarActividad(out objModErrorBase, strTextoBuscar, out objModPaginacion, intPagina, RegistroPagina);

            objActividadModel.objPaginacionModel = new PaginacionModel("Actividad", "Lista", objModPaginacion.Paginas, intPagina, objModPaginacion.TotalRegistros);
         

            ICollection<ActividadModel> listActividadModel =
                ObjectMapper.Instance.Convert<ICollection<clsModCatActividad>, ICollection<ActividadModel>>(listModCatActividad);
            objActividadModel.listActividadModel = listActividadModel;
            return objActividadModel;
        }

        public ActionResult Lista(string TextoBuscar = "", int Pagina = 1)
        {
       
            return PartialView(GetPagActividadModel(TextoBuscar, Pagina));
        }

        private IEnumerable<ComboCatalogosModel> GetCatalogoTipoProceso()
        {
            objCatalogos= new Catalogos();
            return objCatalogos.GetCatalogoTipoProceso();
        }

        public ActionResult Create(int Id = 0)
        {
            ViewBag.lstTipoProceso = GetCatalogoTipoProceso();

            //Valida el id es igual a 0 devuelve un objeto nuevo si es diferente de cero manda llamar a la funcion de cargar el modelo
            if (Id != 0)
            {
                objModErrorBase = null;
                clsModCatActividad objModCatActividad = objNegCatActividad.CargarXId(Id, out objModErrorBase);
                objActividadModel = ObjectMapper.Instance.Convert<clsModCatActividad, ActividadModel>(objModCatActividad);
                return PartialView(objActividadModel);
            }
            else
            {

                return PartialView(new ActividadModel());
            }
        }


        public JsonResult ActualizarEstado(ActividadModel objActividadModel)
        {
            clsNegCatActividad objNegCatActividad = new clsNegCatActividad();

            clsModCatActividad capaModelo = new clsModCatActividad();

            capaModelo = ObjectMapper.Instance.Convert<ActividadModel, clsModCatActividad>(objActividadModel);

            clsModErrorBase objModErrorBase = new clsModErrorBase();
            MensajeModel objMensajeModel = null;
            objModErrorBase = objNegCatActividad.ActualizarEstado(capaModelo);
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