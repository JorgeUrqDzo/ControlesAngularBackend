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
    public class ComentariosController : ApiController
    {
        clsNegApiComentario _objComentario = null;
        clsModApiComentario objModApiComentario = new clsModApiComentario();
        public ComentariosController(){
            _objComentario = new clsNegApiComentario();
        }
        public List<clsModApiComentario> Get(int IdTablaDocumento, int IdDocumento, int IdUsuario)
        {
            clsModErrorBase objModErrorBase = null;
 
            return _objComentario.CargarComentarios(IdTablaDocumento, IdDocumento, IdUsuario, out objModErrorBase);
        }

        public Object Post(clsModApiComentario objModApiComentario)
        {    
            clsModErrorBase objModErrorBase = null;
            objModErrorBase = _objComentario.AgregarCatComentario(objModApiComentario);
            if(objModErrorBase.MsgError == "")
            {
                objModErrorBase.MsgError = "Comentario Agregado";
            }
            return Json(objModErrorBase);
        }

	}
}