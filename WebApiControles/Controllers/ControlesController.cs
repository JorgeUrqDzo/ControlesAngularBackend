using ClbModControles;
using ClbModControles.Api;
using ClbNegControles.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebApiControles.Models;

namespace WebApiControles.Controllers
{
    //[EnableCors(origins: "http://localhost:2397", headers: "*", methods: "*")]
    public class ControlesController : ApiController
    {


        [System.Web.Http.ActionName("Guardar")]
        public Object Guardar(ControlesModel[] objFormulario)
        {
            Hashtable hashtable = new Hashtable();
            string key = null;
            foreach (ControlesModel i in objFormulario)
            {
                if (i.IdControl == 0)
                {
                    if (i.Value != null)
                        key = i.Value.ToString();
                }
                else
                {
                    hashtable.Add(i.IdControl, i.Value);
                }
            }

            clsNegApiControles objNegApiControles = new clsNegApiControles();
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            objModErrorBase = objNegApiControles.GuardarDatosFormulario(hashtable, key);

            if (objModErrorBase.MsgError == "")
            {
                objModErrorBase.MsgError = "Datos Guardados";
            }

            return Json(objModErrorBase);
        }




        [System.Web.Http.ActionName("Get")]
        public clsModApiFormulario Get(string id, string key)
        {
            clsModErrorBase objModErrorBase = null;
            clsNegApiFormulario objNegApiFormulario = new clsNegApiFormulario();
            return objNegApiFormulario.Cargar(id, key, out objModErrorBase);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [System.Web.Http.ActionName("Test")]
        [System.Web.Http.HttpGet]
        public Object Test()
        {
            List<object> lst = new List<object>();
            lst.Add("Hola");
            lst.Add("Mundo");
            lst.Add("Desde");
            lst.Add("la Api");
            return Json(lst);
        }
    }
}
