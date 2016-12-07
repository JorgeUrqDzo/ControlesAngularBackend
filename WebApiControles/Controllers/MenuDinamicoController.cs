using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClbModControles;
using ClbNegControles.Api;
using ClbModControles.Api;

namespace WebApiControles.Controllers
{
    public class MenuDinamicoController : ApiController
    {
        clsNegApiMenuDinamico _objMenu = null;
        public MenuDinamicoController()
        {
            _objMenu = new clsNegApiMenuDinamico();
        }

        public List<clsModApiMenuDinamico> GetMenu(string tipoMenu)
        {
            clsModErrorBase objModErrorBase = null;
            return _objMenu.menuDinamico(out objModErrorBase, Int32.Parse(tipoMenu));
        }
    }
}
