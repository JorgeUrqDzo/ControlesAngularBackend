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
    public class VisualizadorController : ApiController
    {
        private JavaScriptSerializer _JSerializer;
        private readonly clsNegUploadFiles _negUploadFiles;
        //private readonly clsNegSolicitud objNegSolicitud;

        public VisualizadorController()
        {
            _negUploadFiles = new clsNegUploadFiles();
            _JSerializer = new JavaScriptSerializer();
            _JSerializer.MaxJsonLength = 2147483644;
            //objNegSolicitud = clsNegFactory.Instance.GetNewNegSolicitud();
        }

        [HttpGet]
        public string GetDocuments()
        {
            string TipoConsulta = "", strPanel = "";
            int IdTablaRelacion = 0, IdRelacion = 0, IdDocumento = 0;
            var allQuerysStrings = ControllerContext.Request.GetQueryNameValuePairs();
            List<clsModUploadFiles> Documentos = new List<clsModUploadFiles>();

            if (allQuerysStrings != null)
            {
                TipoConsulta = (allQuerysStrings.Single(x => x.Key == "TipoConsulta").Value != null) ?
                    allQuerysStrings.Single(x => x.Key == "TipoConsulta").Value.ToString() : "";
                IdTablaRelacion = (allQuerysStrings.SingleOrDefault(x => x.Key == "IdTablaRelacion").Value != null) ?
                    Convert.ToInt32(allQuerysStrings.SingleOrDefault(x => x.Key == "IdTablaRelacion").Value) : 0;
                IdRelacion = (allQuerysStrings.SingleOrDefault(x => x.Key == "IdRelacion").Value != null) ?
                    Convert.ToInt32(allQuerysStrings.SingleOrDefault(x => x.Key == "IdRelacion").Value) : 0;
                IdDocumento = (allQuerysStrings.SingleOrDefault(x => x.Key == "IdDocumento").Value != null) ?
                    Convert.ToInt32(allQuerysStrings.SingleOrDefault(x => x.Key == "IdDocumento").Value) : 0;
            }
            switch (TipoConsulta.Trim())
            {
                case "Id":
                    if (IdTablaRelacion != 0 && IdRelacion != 0)
                    {
                        Documentos = _negUploadFiles.CargarDocumentosByIdNormal(IdTablaRelacion,IdRelacion, 0, 0);
                    }

                    break;
            }

            if (Documentos.Count > 0)
            {

                foreach (clsModUploadFiles Documento in Documentos)
                {
                    string fileBase64 = "", extension = "";

                    extension = Documento.NombreArchivo.Split(new string[] { "." }, StringSplitOptions.None)[Documento.NombreArchivo.Split(new string[] { "." }, StringSplitOptions.None).Length - 1];

                    string base64String = "";
                    switch (extension.ToLower().Trim())
                    {
                        case "pdf":
                            fileBase64 = "";
                            break;
                        case "xml":
                        case "txt":
                            fileBase64 = "";
                            break;
                        default:
                            //base64String = System.Convert.ToBase64String(Documento.Archivo, 0, Documento.Archivo.Length);
                            base64String = ConvertDocToBase64(Documento);
                            fileBase64 = base64String;
                            break;
                    }

                    strPanel += getTableWDoc(Documento, extension);
                }
            }

            return strPanel;
        }

        public HttpResponseMessage GetPdf()
        {
            int IdDocumento = 0;
            var allQuerysStrings = ControllerContext.Request.GetQueryNameValuePairs();
            List<clsModUploadFiles> Documentos = new List<clsModUploadFiles>();


            if (allQuerysStrings != null)
            {
                IdDocumento = (allQuerysStrings.SingleOrDefault(x => x.Key == "iddoc").Value != null) ?
                    Convert.ToInt32(allQuerysStrings.SingleOrDefault(x => x.Key == "iddoc").Value) : 0;
            }

            if (IdDocumento != 0)
            {

                Documentos = _negUploadFiles.CargarDocumentos(0, 0, IdDocumento, 0);
                var streamMemory = new MemoryStream(Documentos[0].BArchivo);
                var response = new HttpResponseMessage();
                response.Content = new StreamContent(streamMemory);
                response.StatusCode = HttpStatusCode.OK;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
                response.Content.Headers.ContentDisposition.FileName = Documentos[0].NombreArchivo;

                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        private string getTableWDoc(clsModUploadFiles Documento, string extension)
        {
            string sb = "", fileBase64 = "";
            string nomIdSpan = "span_" + Documento.IdDocumento;

            fileBase64 = ConvertDocToBase64(Documento);

            sb += "<div style=\"width: 100%; padding: 5px;\">";
            sb += "<table>";
            sb += "<tr>";
            sb += "<td>";
            sb += "<a  onclick=\"Visualizador.downLoadDoc('" + Documento.IdDocumento + "');\" target='_blank'>";
            sb += "<i class=\"glyphicon glyphicon-cloud-download\"></i>";
            sb += "</a>";
            sb += "</td>";
            sb += "<td>";
            sb += "&nbsp;<span id=\"" + nomIdSpan + "\" class=\"btnSelectDocumento\" onclick=\"Visualizador.showContentDoc('" + extension + "', '" + fileBase64 + "', " + Documento.IdDocumento + ");\"  rel=\"tooltip\" title=\"" + Documento.NombreArchivo + "\">";
            if (Documento.DocumentContent != null)
            {
                sb += "" + Documento.NombreArchivo.Replace(Documento.DocumentContent.Trim(), "") + "";
            }

            sb += "</span>";
            sb += "</td>";
            sb += "</tr>";
            sb += "</table>";
            sb += "</div>";


            return sb;
        }

        private string ConvertDocToBase64(clsModUploadFiles Documento)
        {
            string fileBase64 = "";

            if (Documento != null && Documento.Archivo != null)
            {
                fileBase64 = System.Convert.ToBase64String(Documento.Archivo, 0, Documento.Archivo.Length);
                return fileBase64;
            }

            return fileBase64;
        }
    }
}