using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles.Documentos;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using ClbUtilerias;

namespace ClbDatControles
{
    public class clsDatUploadFiles
    {
        private string _MsgError;

        /// <summary>
        /// Cadena de la conexión a la base de datos.
        /// </summary>
        private string strConexion;
        /// <summary>
        /// Constructor de la clase para la conexión.
        /// </summary>
        public clsDatUploadFiles()
        {
            this.strConexion = Common.DataBaseConnection.Instance.GetConnectionString();
            this._MsgError = string.Empty;
        }

        public int Agregar(int strTablaRelacion, int intIdRelacion, clsModUploadFiles objBasEncDocumentos)
        {
            int IdDocumento = 0;
            if (objBasEncDocumentos.DocumentContent == ".xls" || objBasEncDocumentos.DocumentContent == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                objBasEncDocumentos.DocumentContent = "application/vnd.ms-excel";
            }
            try
            {
                SqlParameter[] arParms = new SqlParameter[7];
                arParms[0] = new SqlParameter("@TablaRelacion", SqlDbType.Int);
                arParms[0].Value = strTablaRelacion;
                arParms[1] = new SqlParameter("@IdRelacion", SqlDbType.Int);
                arParms[1].Value = intIdRelacion;
                arParms[2] = new SqlParameter("@NombreArchivo", SqlDbType.VarChar);
                arParms[2].Value = objBasEncDocumentos.NombreArchivo;
                arParms[3] = new SqlParameter("@Archivo", SqlDbType.Image);
                arParms[3].Value = objBasEncDocumentos.Archivo;
                arParms[4] = new SqlParameter("@DocumentContent", SqlDbType.VarChar);
                arParms[4].Value = objBasEncDocumentos.DocumentContent;
                arParms[5] = new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int);
                arParms[5].Value = objBasEncDocumentos.IdUsuarioCreacion;
                arParms[6] = new SqlParameter("@DBDocumentosName", SqlDbType.VarChar) { Value = ReadSettings.Instance.GetDocumentsDataBaseNameValue() };
                DataSet ds = SqlHelper.ExecuteDataset(strConexion, CommandType.StoredProcedure, "NzControles.spDocumentosAgregarGeneric", arParms);
                DataTable dt = ds.Tables["table"];
                if (dt.Rows.Count >= 1)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        IdDocumento = int.Parse(r["ID"].ToString());
                    }
                }
                return IdDocumento;
            }
            catch (Exception ex)
            {
                _MsgError = ex.Message;
                return 0;
            }
        }
        public int AgregarLargeFile(int strTablaRelacion, int intIdRelacion, clsModUploadFiles objBasEncDocumentos)
        {
            int IdDocumento = 0;
            if (objBasEncDocumentos.DocumentContent == ".xls" || objBasEncDocumentos.DocumentContent == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                objBasEncDocumentos.DocumentContent = "application/vnd.ms-excel";
            }
            try
            {
                SqlParameter[] arParms = new SqlParameter[9];
                arParms[0] = new SqlParameter("@TablaRelacion", SqlDbType.Int);
                arParms[0].Value = strTablaRelacion;
                arParms[1] = new SqlParameter("@IdRelacion", SqlDbType.Int);
                arParms[1].Value = intIdRelacion;
                arParms[2] = new SqlParameter("@NombreArchivo", SqlDbType.VarChar);
                arParms[2].Value = objBasEncDocumentos.FullName;
                arParms[3] = new SqlParameter("@Archivo", SqlDbType.Image);
                arParms[3].Value = objBasEncDocumentos.Archivo;
                arParms[4] = new SqlParameter("@DocumentContent", SqlDbType.VarChar);
                arParms[4].Value = objBasEncDocumentos.DocumentContent;
                arParms[5] = new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int);
                arParms[5].Value = objBasEncDocumentos.IdUsuarioCreacion;
                arParms[6] = new SqlParameter("@DBDocumentosName", SqlDbType.VarChar) { Value = ReadSettings.Instance.GetDocumentsDataBaseNameValue() };
                arParms[7] = new SqlParameter("@IsFisico", SqlDbType.Bit);
                arParms[7].Value = objBasEncDocumentos.IsFisico;
                arParms[8] = new SqlParameter("@TokenDoc", SqlDbType.VarChar);
                arParms[8].Value = objBasEncDocumentos.TokenFile;

                DataSet ds = SqlHelper.ExecuteDataset(strConexion, CommandType.StoredProcedure, "NzControles.spDocumentosAgregarLargeFile", arParms);
                DataTable dt = ds.Tables["table"];
                if (dt.Rows.Count >= 1)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        IdDocumento = int.Parse(r["ID"].ToString());
                    }
                }
                return IdDocumento;
            }
            catch (Exception ex)
            {
                _MsgError = ex.Message;
                return 0;
            }
        }

        public List<clsModUploadFiles> CargarDocumentosByIdNormal(int IdTablaRelacion, int intIdRelacion, int intId, int IdUsuario)
        {
            List<clsModUploadFiles> lst = new List<clsModUploadFiles>();
            try
            {
                SqlParameter[] arParms = new SqlParameter[5];
                arParms[0] = new SqlParameter("@TablaRelacion", SqlDbType.VarChar);
                arParms[0].Value = IdTablaRelacion;
                arParms[1] = new SqlParameter("@IdRelacion", SqlDbType.Int);
                arParms[1].Value = intIdRelacion;
                arParms[2] = new SqlParameter("@Id", SqlDbType.Int);
                arParms[2].Value = intId;
                arParms[3] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                arParms[3].Value = IdUsuario;
                arParms[4] = new SqlParameter("@DBDocumentosName", SqlDbType.VarChar) { Value = ReadSettings.Instance.GetDocumentsDataBaseNameValue() };
                DataSet ds = SqlHelper.ExecuteDataset(this.strConexion, CommandType.StoredProcedure, "NzControles.spDocumentosCargarGeneric", arParms);
                DataTable dt = ds.Tables["table"];
                if (dt.Rows.Count >= 1)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        lst.Add(RenglonArchivoNormal(r));
                    }
                }
            }
            catch (Exception ex)
            {
                _MsgError = ex.Message;
            }
            return lst;
        }

        public List<clsModUploadFiles> CargarDocumentos(int IdTablaRelacion, int intIdRelacion, int intId, int IdUsuario)
        {
            List<clsModUploadFiles> lst = new List<clsModUploadFiles>();
            try
            {
                SqlParameter[] arParms = new SqlParameter[5];
                arParms[0] = new SqlParameter("@TablaRelacion", SqlDbType.VarChar);
                arParms[0].Value = IdTablaRelacion;  
                arParms[1] = new SqlParameter("@IdRelacion", SqlDbType.Int);
                arParms[1].Value = intIdRelacion;
                arParms[2] = new SqlParameter("@Id", SqlDbType.Int);
                arParms[2].Value = intId;
                arParms[3] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                arParms[3].Value = IdUsuario;
                arParms[4] = new SqlParameter("@DBDocumentosName", SqlDbType.VarChar) { Value = ReadSettings.Instance.GetDocumentsDataBaseNameValue() };

                DataSet ds = SqlHelper.ExecuteDataset(this.strConexion, CommandType.StoredProcedure, "NzControles.spDocumentosCargarGeneric", arParms);
                DataTable dt = ds.Tables["table"];
                if (dt.Rows.Count >= 1)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        lst.Add(RenglonSinArchivoJudicial(r));
                    }
                }
            }
            catch (Exception ex)
            {
                _MsgError = ex.Message;
            }
            return lst;
        }
       
        private clsModUploadFiles RenglonSinArchivoJudicial(DataRow r)
        {
            clsModUploadFiles Obj = new clsModUploadFiles();
            try
            {
                Obj.IdDocumento = int.Parse(r["IdDocumento"].ToString());
                Obj.NombreArchivo = r["NombreArchivo"].ToString();
                Obj.BArchivo = (byte[])r["Archivo"];
                Obj.DocumentContent = r["DocumentContent"].ToString();
                Obj.TablaRelacion = Convert.ToInt32(r["TablaRelacion"].ToString());
                Obj.IdRelacion = int.Parse(r["IdRelacion"].ToString());
                Obj.IdUsuarioCreacion = int.Parse(r["IdUsuarioCreacion"].ToString());
                Obj.IdUsuarioModificacion = int.Parse(r["IdUsuarioModificacion"].ToString());


            }
            catch (Exception ex)
            {
                _MsgError = ex.Message;
            }

            return Obj;
        }
        private clsModUploadFiles RenglonArchivoNormal(DataRow r)
        {
            clsModUploadFiles Obj = new clsModUploadFiles();
            try
            {
                Obj.IdDocumento = int.Parse(r["IdDocumento"].ToString());
                Obj.NombreArchivo = r["NombreArchivo"].ToString();
                Obj.Archivo = (byte[])r["Archivo"];
                Obj.DocumentContent = r["DocumentContent"].ToString();
                Obj.TablaRelacion = Convert.ToInt32(r["IdTablaRelacion"].ToString());
                Obj.IdRelacion = int.Parse(r["IdRelacion"].ToString());
                Obj.IdUsuarioCreacion = int.Parse(r["IdUsuarioCreacion"].ToString());
                Obj.IdUsuarioModificacion = int.Parse(r["IdUsuarioModificacion"].ToString());


            }
            catch (Exception ex)
            {
                _MsgError = ex.Message;
            }

            return Obj;
        }
    }
}
