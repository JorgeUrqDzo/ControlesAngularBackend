using ClbModControles;
using ClbModControles.Api;
using ClbNegControles.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiControles.Controllers
{
    public class DataSourceController : ApiController
    {
        public clsModApiDataSource Get(int Id, int Key)
        {

            clsModErrorBase objModErrorBase = null;
            clsNegApiFormulario objNegApiFormulario = new clsNegApiFormulario();

            return objNegApiFormulario.GetDataSource(Id, Key, out objModErrorBase);
        }
    }
}
