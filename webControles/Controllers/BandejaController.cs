using ClbModControles;
using ClbNegControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webControles.Helpers;
using webControles.Models;

namespace webControles.Controllers
{
    public class BandejaController : Controller
    {
        //
        // GET: /Bandeja/
        public ActionResult Index()
        {
            return PartialView();
        }

        //
        // GET: /Bandeja/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Bandeja/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bandeja/Create
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

        //
        // GET: /Bandeja/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Bandeja/Edit/5
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

        //
        // GET: /Bandeja/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Bandeja/Delete/5
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

        [HttpPost]
        public JsonResult InsertarFormulario(BandejaModel objBandejaModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            MensajeModel objMensajeModel = new MensajeModel();

            clsModEncConfBandeja objModEncConfBandeja = ObjectMapper.Instance.Convert<BandejaModel, clsModEncConfBandeja>(objBandejaModel);
            clsNegEncConfBandeja objNegEncConfBandeja = new clsNegEncConfBandeja();
            objModErrorBase = objNegEncConfBandeja.AgregarFormulario(objModEncConfBandeja);

            if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                objMensajeModel = new MensajeModel(true, objModErrorBase.MsgError);
            }
            else
            {
                objMensajeModel = new MensajeModel(false, "Consulta Correcta", objModErrorBase.Id);
            }

            return Json(objMensajeModel);
        }

        [HttpPost]
        public JsonResult ValidarSQL(BandejaModel objBandejaModel)
        {
            MensajeModel objMensajeModel = new MensajeModel();

            clsModErrorBase objModErrorBase = new clsModErrorBase();

            clsModEncConfBandeja objModEncConfBandeja = ObjectMapper.Instance.Convert<BandejaModel, clsModEncConfBandeja>(objBandejaModel);
            clsNegEncConfBandeja objNegEncConfBandeja = new clsNegEncConfBandeja();


            objModErrorBase = objNegEncConfBandeja.ValidarSQL(objModEncConfBandeja);

            if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                objMensajeModel = new MensajeModel(true, objModErrorBase.MsgError);
            }
            else
            {
                objMensajeModel = new MensajeModel(false, "Consulta Correcta");
            }


            return Json(objMensajeModel);
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

        [HttpPost]
        [ActionName("getColumnas")]
        public JsonResult getColumnName(BandejaModel objBandejaModel)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsNegEncConfBandeja objNegEncConfBandeja = new clsNegEncConfBandeja();

            //BandejaModel objBandejaModel = new BandejaModel();
            //objBandejaModel.IdEncConfBandeja = id;

            clsModEncConfBandeja objModEncConfBandeja = ObjectMapper.Instance.Convert<BandejaModel, clsModEncConfBandeja>(objBandejaModel);

            List<clsModEncConfBandeja> lstModEncConfBandeja = new List<clsModEncConfBandeja>();
            lstModEncConfBandeja = objNegEncConfBandeja.NombreColumnas(objModEncConfBandeja, out objModErrorBase);

            List<BandejaModel> lstBandejaModel = new List<BandejaModel>();
            lstBandejaModel = ObjectMapper.Instance.Convert<List<clsModEncConfBandeja>, List<BandejaModel>>(lstModEncConfBandeja);

            //return Json(lstBandejaModel, JsonRequestBehavior.AllowGet);
            return Json(lstBandejaModel);


        }

    }
}
