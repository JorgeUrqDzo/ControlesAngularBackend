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
    public class MenuController : Controller
    {

        public const int RegistroPagina = 10;
        public readonly clsNegCatMenu objNegCatMenu;
        private clsModErrorBase objModErrorBase;
        private MenuModel objMenuModel;

        private clsModPaginacion objModPaginacion;
        private Catalogos objCatalogos;
       


        //Manda llamar la clase de negocios y el modelo
        public MenuController()
        {
            objNegCatMenu = new clsNegCatMenu();
            objMenuModel = new MenuModel();
        }

        private MenuModel GetPagMenuModel(string strTextoBuscar, int intPagina = 1)
        {

            if (strTextoBuscar == null)
                strTextoBuscar = "";

            objMenuModel = new MenuModel();

            ICollection<clsModCatMenu> listModCatMenu =
                objNegCatMenu.CargarMenu(out objModErrorBase, strTextoBuscar, out objModPaginacion, intPagina, RegistroPagina);

            objMenuModel.objPaginacionModel = new PaginacionModel("Menu", "Lista", objModPaginacion.Paginas, intPagina, objModPaginacion.TotalRegistros);


            ICollection<MenuModel> listMenuModel =
                ObjectMapper.Instance.Convert<ICollection<clsModCatMenu>, ICollection<MenuModel>>(listModCatMenu);
            objMenuModel.listMenuModel = listMenuModel;
            return objMenuModel;
        }

        //
        // GET: /Menu/
        public ActionResult Index()
        {
            ViewBag.lstTipoMenu = GetCatalogoTipoMenu();
            ViewBag.lstMenu = GetCatMenu();
            return View(GetPagMenuModel(""));
           
        }

        [HttpPost]
        public JsonResult Agregar(MenuModel objMenuModel)
        {
           
            clsModCatMenu objModCatMenu = ObjectMapper.Instance.Convert<MenuModel, clsModCatMenu>(objMenuModel);
            MensajeModel objMensajeModel = null;
            objModErrorBase = objNegCatMenu.AgregarCatMenu(objModCatMenu);

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

        public ActionResult Lista(string TextoBuscar = "", int Pagina = 1)
        {

            return PartialView(GetPagMenuModel(TextoBuscar, Pagina));
        }

        private IEnumerable<ComboCatalogosModel> GetCatalogoTipoMenu()
        {
            objCatalogos = new Catalogos();
            return objCatalogos.GetCatalogoTipoMenu();
        }
        private IEnumerable<ComboCatalogosModel> GetCatMenu()
        {
            objCatalogos = new Catalogos();
            return objCatalogos.GetCatMenu();
        }

        public ActionResult Create(int Id = 0)
        {
            ViewBag.lstTipoMenu = GetCatalogoTipoMenu();
            ViewBag.lstMenu = GetCatMenu();

            //Valida el id es igual a 0 devuelve un objeto nuevo si es diferente de cero manda llamar a la funcion de cargar el modelo
            if (Id != 0)
            {
                objModErrorBase = null;
                clsModCatMenu objModCatMenu= objNegCatMenu.CargarXId(Id, out objModErrorBase);
                objMenuModel = ObjectMapper.Instance.Convert<clsModCatMenu, MenuModel>(objModCatMenu);
                return PartialView(objMenuModel);
            }
            else
            {

                return PartialView(new MenuModel());
            }
        }

        public JsonResult ActualizarEstado(MenuModel objMenuModel)
        {
            clsNegCatMenu objNegCatMenu = new clsNegCatMenu();

            clsModCatMenu capaModelo = new clsModCatMenu();

            capaModelo = ObjectMapper.Instance.Convert<MenuModel, clsModCatMenu>(objMenuModel);

            clsModErrorBase objModErrorBase = new clsModErrorBase();
            MensajeModel objMensajeModel = null;
            objModErrorBase = objNegCatMenu.ActualizarEstado(capaModelo);
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

        public JsonResult getIconos()
        {
            clsNegCatMenu objNegCatMenu = new clsNegCatMenu();
            List<MenuIconoModel> lstMenuIconoModel = new List<MenuIconoModel>();
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            lstMenuIconoModel =
                 ObjectMapper.Instance.Convert<List<clsModCatMenuIcono>, List<MenuIconoModel>>(
                     objNegCatMenu.CargarIconos(objModErrorBase));

            return Json(lstMenuIconoModel);
        }



    
	}
}