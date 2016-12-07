using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClbModControles;
using ClbNegControles.Api;
using ClbModControles.Api;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebApiControles.Controllers
{
    public class FormularioController : ApiController
    {
        clsNegApiBandeja _objBandeja = null;

        public FormularioController()
        {
            _objBandeja = new clsNegApiBandeja();
        }
        [ActionName("Get")]
        [HttpPost]
        public string Get(JObject jsonData)
        {

            clsModErrorBase objModErrorBase = null;
            return _objBandeja.Cargar(jsonData["UUID"].ToString(), out objModErrorBase);

        }
    }
}
