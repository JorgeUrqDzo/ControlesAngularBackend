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
    public class GridController : ApiController
    {
        clsNegApiBandeja _objBandeja = null;
        
        public GridController()
        {
            _objBandeja = new clsNegApiBandeja();
        }
        [ActionName("Get")]
        [HttpPost]
        public clsModApiBandeja Get(JObject jsonData)
        {
            clsModPaginacion objModPaginacion;
            List<clsModApiFiltros> listaFiltros = new List<clsModApiFiltros>();
            clsModApiFiltros filtro = null;
            if (jsonData["filtros"] != null)
            {
                foreach (var item in jsonData["filtros"])
                {
                    filtro = new clsModApiFiltros()
                    {
                        nombre = item["nombre"].ToString(),
                        valor = item["valor"].ToString()
                    };
                    listaFiltros.Add(filtro);
                }
            }
            clsModErrorBase objModErrorBase = null;
            clsModApiBandeja objModApiBandeja = _objBandeja.CargarCampoConsulta(jsonData["UUID"].ToString(), listaFiltros, Int32.Parse(jsonData["pagina"].ToString()), out objModErrorBase, out objModPaginacion);

            return objModApiBandeja;
        }

     
    }
}
