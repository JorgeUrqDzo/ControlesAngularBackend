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
    public class ActividadProcesoController : ApiController
    {
        clsNegApiActividadProceso _objActividad = null;
        List<clsModApiActividadProceso> Actividades = null;

        public ActividadProcesoController()
        {
            _objActividad = new clsNegApiActividadProceso();
              Actividades = new List<clsModApiActividadProceso>();

        }

        [ActionName("Save")]
        [HttpPost]
        public clsModApiActividadProceso Save(clsModApiActividadProceso objModApiActividadProceso)
        {
            clsModErrorBase objModErrorBase = null;
            return _objActividad.CargarActividades(objModApiActividadProceso.IdTablaDocumento,
                objModApiActividadProceso.IdDocumento, objModApiActividadProceso.IdEncActividad, objModApiActividadProceso.IdEncProceso,
                objModApiActividadProceso.Aprobado, out objModErrorBase);
        }
    
       
	}
}