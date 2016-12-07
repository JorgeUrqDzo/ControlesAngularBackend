using ClbDatControles.Common;
using ClbModControles;
using ClbModControles.Documentos;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using ClbUtilerias;


namespace ClbDatControles
{
    public class clsDatDocumentosDescarga
    {
        private string _StrConnectionString;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public clsDatDocumentosDescarga()
        {
            this._StrConnectionString = DataBaseConnection.Instance.GetConnectionString();
        }

        /// <summary>
        /// Obtiene la lista de documentos a descargar a partir de parámetros.
        /// </summary>
        /// <param name="intIdPadre">Id padre.</param>
        /// <param name="strIdsDocumento">Ids de documentos concatenados por coma.</param>
        /// <param name="objModErrorBase">Información de error.</param>
        /// <param name="intIdTablaRelacionPadre">Id tabla relación padre.</param>
        /// <returns>Lista de documentos.</returns>
        public IEnumerable<clsModUploadFiles> GetDocumentosByParams(string strIdsDocumento, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            IList<clsModUploadFiles> lstInvolucrados = new List<clsModUploadFiles>();
            SqlDataReader sqlDataReader = null;
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@Ids", SqlDbType.VarChar) { Value = strIdsDocumento });

                sqlDataReader = SqlHelper.ExecuteReader(this._StrConnectionString, CommandType.StoredProcedure, "NzControles.SpdGetDocumentosByParams", lstSqlParameters.ToArray());
                if (sqlDataReader != null)
                {
                    while (sqlDataReader.Read())
                    {
                        clsModUploadFiles objClsModDocumentosDescarga = new clsModUploadFiles() {
                            IdDocumento = (int) (sqlDataReader["IdDocumento"] != DBNull.Value ? sqlDataReader["IdDocumento"] : 0),
                            NombreArchivo = sqlDataReader["NombreArchivo"] != DBNull.Value ? sqlDataReader["NombreArchivo"].ToString() : string.Empty,
                            DocumentContent = sqlDataReader["DocumentContent"] != DBNull.Value ? sqlDataReader["DocumentContent"].ToString() : string.Empty};

                        lstInvolucrados.Add(objClsModDocumentosDescarga);
                    }
                }
            }
            catch (Exception exception)
            {
                objModErrorBase.MsgError = exception.Message;
            }
            //finally
            //{
            //    sqlDataReader?.Close();
            //}

            return lstInvolucrados;
        }

        /// <summary>
        /// Obtiene los bytes de un documento.
        /// </summary>
        /// <param name="intIdDocumento">Id del documento.</param>
        /// <param name="objModErrorBase">Información de error.</param>
        /// <returns>Bytes del documento..</returns>
        //public byte[] GetBytesDocumento(int intIdDocumento, out ClsModErrorBase objModErrorBase)
        //{
        //    objModErrorBase = new ClsModErrorBase();
        //    SqlDataReader sqlDataReader = null;
        //    try
        //    {
        //        ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
        //        lstSqlParameters.Add(new SqlParameter("@IdDocumento ", SqlDbType.Int) { Value = intIdDocumento });
        //        lstSqlParameters.Add(new SqlParameter("@DBDocumentosName", SqlDbType.VarChar) {Value = ReadSettings.Instance.GetDocumentsDataBaseNameValue()});

        //        sqlDataReader = SqlHelper.ExecuteReader(this._StrConnectionString, CommandType.StoredProcedure, "Documentos.SpdGetBytesDocumento", lstSqlParameters.ToArray());
        //        if (sqlDataReader != null)
        //        {
        //            if (sqlDataReader.HasRows)
        //            {
        //                sqlDataReader.Read();
        //                byte[] bytes = (byte[]) (sqlDataReader["Archivo"] != DBNull.Value ? sqlDataReader["Archivo"] : null);
        //                return bytes;
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        objModErrorBase.MsgError = exception.Message;
        //    }
        //    finally
        //    {
        //        sqlDataReader?.Close();
        //    }

        //    return null;
        //}
    }
}

