using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webControles.Helpers;
using webControles.Models;
using ClbModControles;
using ClbNegControles;
using webControles.Models.Views;
using webControles.Controllers.Apis;

namespace webControles.Controllers
{
    public class BusquedaController : Controller
    {

        //Variables 

        //Se obtiene el modelo de la vista
        BusquedaBandejaModel objBusquedaBandejaModel;

        //Variable privada de la clase de negocios
        clsNegCatBandejas objNegCatBandejas;

        //Variable privada de la clase de negocios
        //clsNegCatBandejaColumna objNegCatBandejasColumnas;

        //Obtiene el modelo de paginación
        clsModPaginacion objModPaginacion;

        //Total de registros que estaran por página
        public const int RegistroPagina = 10;

        //Obtiene las variables de BusquedaBandejaViewModel
        BusquedaBandejaViewModel objBusquedaBandejaViewModel;

        clsModErrorBase objModErrorBase;

        MensajeModel objMensajeModel;

        BandejaModel objBandejaModel;
       clsModEncConfBandeja objModEncConfBandeja;

       Catalogos objCatalogos = null;

         public BusquedaController()
        {
            objBusquedaBandejaModel = new BusquedaBandejaModel();
            objNegCatBandejas = new clsNegCatBandejas();
            objBusquedaBandejaViewModel = new BusquedaBandejaViewModel();
            objBandejaModel = new BandejaModel();
            objCatalogos = new Catalogos();
           // objNegCatBandejasColumnas = new clsNegCatBandejaColumna();
        }

        //Metodo para obtener los datos con la paginacion
         public BusquedaBandejaViewModel GetPagBusquedaBandejaViewModel(string strTextoBuscar, int intPagina = 1)
         {
             if (strTextoBuscar == null)
                 strTextoBuscar = "";

             objBusquedaBandejaViewModel = new BusquedaBandejaViewModel();
             clsModErrorBase objModErrorBase = new clsModErrorBase();


             ICollection<clsModCatBandejas> lstModCatBandejas =
                 objNegCatBandejas.CargarBandejas(out objModErrorBase, strTextoBuscar, out objModPaginacion, intPagina, RegistroPagina);

             //objBusquedaBandejaViewModel.lstBandejaColumnaModel = ObjectMapper.Instance.Convert<ICollection<clsModCatBandejaColumna>, ICollection<BandejaColumnaModel>>(objNegCatBandejasColumnas.CargarBandejaColumna(out objModErrorBase,1));


             objBusquedaBandejaViewModel.objPaginacionModel = new PaginacionModel("Busqueda", "Lista", objModPaginacion.Paginas, intPagina, objModPaginacion.TotalRegistros);
             //objBusquedaBandejaViewModel.objBandejaModel = new BandejaModel("Bandeja", "index");
             ViewBag.lstTipoColumna = objCatalogos.GetCatalogoTipoColumna();


             ICollection<BusquedaBandejaModel> lstModelo =
                 ObjectMapper.Instance.Convert<ICollection<clsModCatBandejas>, ICollection<BusquedaBandejaModel>>(lstModCatBandejas);
             objBusquedaBandejaViewModel.lstBusquedaBandejaModel = lstModelo;


             clsNegDetConfBandejaLinkParametro objCapaNegocio = new clsNegDetConfBandejaLinkParametro();

             ParametrosModel objParametrosModel = new ParametrosModel();

             List<ParametrosModel> lstParametrosModel = new List<ParametrosModel>();

             lstParametrosModel = ObjectMapper.Instance.Convert<List<clsModDetConfBandejaLinkParametro>, List<ParametrosModel>>(objCapaNegocio.CargarParametros());

             ParametrosViewModel objParametrosViewModel = new ParametrosViewModel();
             objParametrosViewModel.lstParametrosModel = lstParametrosModel;

             ViewBag.lstTipoParametro = objCatalogos.GetCatalogoTipoParmetro();
             ViewBag.lstTipoEnvio = objCatalogos.GetCatalogoTipoEnvio();

             ViewBag.tipoFormulario = objCatalogos.getTipoFormulario();
             ViewBag.tipoConsulta = objCatalogos.getTipoConsulta();
             return objBusquedaBandejaViewModel;
         }
        //
        // GET: /Busqueda/
        public ActionResult Index()
        {
            return View(GetPagBusquedaBandejaViewModel(""));
        }
        

        //Regresa la lista parcial de la seccion de lista
        public ActionResult Lista(string TextoBuscar = "", int Pagina = 1)
        {
            if (string.IsNullOrEmpty(TextoBuscar)) { TextoBuscar = ""; }
            return PartialView("Lista", GetPagBusquedaBandejaViewModel(TextoBuscar, Pagina));
        }

        [HttpPost]
        [ActionName("Editar")]
        public JsonResult CargarXId(BandejaModel objBandejaModel)
        {
            MensajeModel objMensajeModel = new MensajeModel();
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            clsModEncConfBandeja objModEncConfBandeja = ObjectMapper.Instance.Convert<BandejaModel, clsModEncConfBandeja>(objBandejaModel);

            clsNegEncConfBandeja objNegEncConfBandeja = new clsNegEncConfBandeja();
            objModEncConfBandeja = objNegEncConfBandeja.CargarXId(objModEncConfBandeja, out objModErrorBase);

            BandejaModel objBandeja = new BandejaModel();
            objBandeja = ObjectMapper.Instance.Convert<clsModEncConfBandeja, BandejaModel>(objModEncConfBandeja);

            return Json(objBandeja);
        }
	}
   
}