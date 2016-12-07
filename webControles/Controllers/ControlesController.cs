using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webControles.Models;
using webControles.Models.Views;

namespace webControles.Controllers
{
    public class ControlesController : Controller
    {
        //
        // GET: /Controles/
        public ActionResult Index(int? id)
        {
            ControlesViewModel objControlesViewModel = new ControlesViewModel();
            
            
            return View(objControlesViewModel);
        }

        public ActionResult VistaPrevia()
        {
            


            return View();
        }

        public ActionResult EjemploGuardar()
        {
            return View();
        }
        public ActionResult EjemploEditar() {
            return View();
        }

        public ActionResult EjemploUpload()
        {
            return View();
        }

        public ActionResult EjemploVisor()
        {
            return View();
        }
    }
}
