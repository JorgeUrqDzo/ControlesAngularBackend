using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webControles.Controllers.Apis
{
    public class BandejaApiController : Controller
    {
        //
        // GET: /BandejaApi/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Parametros()
        {
            return View();
        }
        //
        // GET: /BandejaApi/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BandejaApi/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BandejaApi/Create
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
        // GET: /BandejaApi/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BandejaApi/Edit/5
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
        // GET: /BandejaApi/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BandejaApi/Delete/5
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
    }
}
