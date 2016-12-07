using ClbDatControles;
using ClbModControles;
using ClbModControles.Documentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbNegControles
{
   public class clsNegDocumentosDescarga
    {
        /// <summary>
        /// Instancia a datos de descarga de documentos.
        /// </summary>
        private readonly clsDatDocumentosDescarga objDatDocumentosDescarga;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
      

        /// <summary>
        /// Obtiene la lista de documentos a descargar a partir de parámetros.
        /// </summary>
        /// <param name="intIdPadre">Id padre.</param>
        /// <param name="strIdsDocumento">Ids de documentos concatenados por coma.</param>
        /// <param name="objModErrorBase">Información de error.</param>
        /// <param name="intIdTablaRelacionPadre">Id tabla relación padre.</param>
        /// <returns>Lista de documentos.</returns>
        public IEnumerable<clsModUploadFiles> GetDocumentosByParams( string strIdsDocumento, out clsModErrorBase objModErrorBase)
        {
            return this.objDatDocumentosDescarga.GetDocumentosByParams(strIdsDocumento, out objModErrorBase);
        }

        /// <summary>
        /// Obtiene los bytes de un documento.
        /// </summary>
        /// <param name="intIdDocumento">Id del documento.</param>
        /// <param name="objModErrorBase">Información de error.</param>
        /// <returns>Bytes del documento..</returns>
        //public byte[] GetBytesDocumento(int intIdDocumento, out clsModErrorBase objModErrorBase)
        //{
        //    return this.objDatDocumentosDescarga.GetBytesDocumento(intIdDocumento, out objModErrorBase);
        //}
    }
}
