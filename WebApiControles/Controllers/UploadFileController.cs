using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClbModControles;
using ClbModControles.Documentos;
using ClbNegControles;
using System.Net.Http.Headers;
using System.Reflection;
using System.IO;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using System.Configuration;
using WebApiControles.Models;

namespace WebApiControles.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UploadFileController : ApiController
    {
        private readonly clsNegUploadFiles _negUploadFiles;
        private JavaScriptSerializer _JSerializer;

        public UploadFileController()
        {
            _negUploadFiles = new clsNegUploadFiles();
            _JSerializer = new JavaScriptSerializer();
            _JSerializer.MaxJsonLength = 2147483644;
        }

        private string GetRutaFIle()
        {
            try
            {
                return ConfigurationManager.AppSettings["Upload.Documents"];
            }
            catch
            {
                return string.Empty;
            }
        }

        [HttpPost]
        [ActionName("InitControl2")]
        public HttpResponseMessage InitUpload()
        {

            WebApiControles.Utils UT = null;
            UploadFiles Documento = new UploadFiles();
            Response objResp = new Response();
            var entro = false;

            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                var IdFolio = System.Web.HttpContext.Current.Request.Form["Id"];
                Documento.NombreArchivo = System.Web.HttpContext.Current.Request.Form["FileName"];
                Documento.FullName = System.Web.HttpContext.Current.Request.Form["FullName"];
                Documento.ExtensionArchivo = System.Web.HttpContext.Current.Request.Form["FileExtension"];
                Documento.DocumentContent = System.Web.HttpContext.Current.Request.Form["ContentType"];
                Documento.TokenFile = System.Web.HttpContext.Current.Request.Form["tokenFile"];
                Documento.IsFisico = bool.Parse(System.Web.HttpContext.Current.Request.Form["IsFisico"]);
                Documento.TablaRelacion = int.Parse(System.Web.HttpContext.Current.Request.Form["TablaRelacion"]);
                Documento.IdRelacion = int.Parse(System.Web.HttpContext.Current.Request.Form["IdRelacion"]);
                Documento.IdUsuarioCreacion = int.Parse(System.Web.HttpContext.Current.Request.Form["IdUsuarioCreacion"]);
            }

            foreach (string file in System.Web.HttpContext.Current.Request.Files)
            {
                var FileDataContent = System.Web.HttpContext.Current.Request.Files[file];


                if (FileDataContent != null && FileDataContent.ContentLength > 0)
                {
                    var stream = FileDataContent.InputStream;
                    var fileName = Path.GetFileName(FileDataContent.FileName);
                    var UploadPath = GetRutaFIle();

                    Directory.CreateDirectory(UploadPath);
                    string path = Path.Combine(UploadPath, fileName);

                    try
                    {
                        if (File.Exists(path))
                            File.Delete(path);

                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                        UT = new WebApiControles.Utils();
                        objResp = UT.MergeFile(path, FillObjDoc(Documento));

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }

            }
            HttpResponseMessage httpResponseMessage = Request.CreateResponse<Response>(HttpStatusCode.OK, objResp);
            return httpResponseMessage;

        }
  
        private clsModUploadFiles FillObjDoc(UploadFiles documento)
        {

            clsModUploadFiles objModCgDocumentos;

            objModCgDocumentos = new clsModUploadFiles();

            objModCgDocumentos.TablaRelacion = documento.TablaRelacion;
            objModCgDocumentos.IdRelacion = documento.IdRelacion;
            objModCgDocumentos.NombreArchivo = documento.NombreArchivo;
            objModCgDocumentos.FullName = documento.FullName;
            objModCgDocumentos.DocumentContent = documento.DocumentContent;
            objModCgDocumentos.IdUsuarioCreacion = documento.IdUsuarioCreacion;
            objModCgDocumentos.Estatus = 1;
            objModCgDocumentos.IsFisico = documento.IsFisico;
            objModCgDocumentos.TokenFile = documento.TokenFile;
            objModCgDocumentos.IdRequisito = documento.IdRequisito;

            return objModCgDocumentos;
        }
      
        public struct UploadFiles
        {
            private string _Image64;

            public int IdDocumento { get; set; }
            public int TablaRelacion { get; set; }
            public string[] requisitos { get; set; }
            public string NombreArchivo { get; set; }
            public string FullName { get; set; }
            public string Archivo { get; set; }
            public string DocumentContent { get; set; }
            public string ExtensionArchivo { get; set; }
            public string Estatus { get; set; }
            public List<List<string[]>> Capturas { get; set; }
            public int IdRelacion { get; set; }
            public int IdUsuarioCreacion { get; set; }
            public int IdUsuarioModificacion { get; set; }
            public int IdRequisito { get; set; }
            public string Texto { get; set; }
            public string TokenFile { get; set; }
            public bool IsFisico { get; set; }

            public byte[] BArchivo { get; set; }
            public string ImageTo64
            {
                get { return _Image64 = (BArchivo != null) ? Convert.ToBase64String(BArchivo) : null; }
            }
        }

    }
}
