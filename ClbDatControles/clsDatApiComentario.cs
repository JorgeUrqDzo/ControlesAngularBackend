using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;
using ClbModControles.Api;

namespace ClbDatControles
{
   public class clsDatApiComentario
    {
        private string strConexion = "";

          public clsDatApiComentario()
        {
            this.strConexion = Common.DataBaseConnection.Instance.GetConnectionString();
        }

         public clsModErrorBase AgregarCatComentario(clsModApiComentario objModApiComentario)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            int identity = 0;
            
            try
            {
                SqlParameter[] arrPar = new SqlParameter[5];
             
              
                arrPar[0] = new SqlParameter("@Comentario", SqlDbType.VarChar)
                {
                    Value = objModApiComentario.Comentario
                };
                arrPar[1] = new SqlParameter("@IdTablaDocumento", SqlDbType.Int)
                {
                    Value = 1
                };
                arrPar[2] = new SqlParameter("@IdDocumento", SqlDbType.Int)
                {
                    Value = 1
                };
                arrPar[3] = new SqlParameter("@FechaCreacion", SqlDbType.DateTime)
                {
                    Value = DateTime.Now
                };
                arrPar[4] = new SqlParameter("@IdUsuario", SqlDbType.Int)
                {
                    Value = 1104
                };
             
             
                object IdComentario = SqlHelper.ExecuteScalar(strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdCatComentarioAgregar", arrPar);
                if (IdComentario != null)
                {
                    int.TryParse(IdComentario.ToString(), out identity);
                    objModErrorBase.MsgError = "Comentario Agregado";
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            //objModErrorBase.Id = identity;
            return objModErrorBase;
        }

          private clsModApiComentario LlenarObjeto(SqlDataReader sqlDataReader)
        {
            clsModApiComentario objModApiComentario = new clsModApiComentario()
            {
              
                IdComentario = (int)(sqlDataReader["IdComentario"] != DBNull.Value ? sqlDataReader["IdComentario"] : 0),
                Comentario = (string)(sqlDataReader["Comentario"] != DBNull.Value ? sqlDataReader["Comentario"] : ""),
                IdTablaDocumento = (int)(sqlDataReader["IdTablaDocumento"] != DBNull.Value ? sqlDataReader["IdTablaDocumento"] : 0),
                IdDocumento = (int)(sqlDataReader["IdDocumento"] != DBNull.Value ? sqlDataReader["IdDocumento"] : 0),
                FechaCreacion = (DateTime)(sqlDataReader["FechaCreacion"] != DBNull.Value ? sqlDataReader["FechaCreacion"] : ""),
                IdUsuario = (int)(sqlDataReader["IdUsuario"] != DBNull.Value ? sqlDataReader["IdUsuario"] : 0),
                Usuario = (string)(sqlDataReader["Usuario"] != DBNull.Value ? sqlDataReader["Usuario"] :"")
            };
            return objModApiComentario;
        }
        public List<clsModApiComentario> CargarComentarios(int IdTablaDocumento, int IdDocumento, int IdUsuario, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            List<clsModApiComentario> objModApiComentario = new List<clsModApiComentario>();
            SqlParameter[] arParms = new SqlParameter[3];
            arParms[0] = new SqlParameter("@IdTablaDocumento", SqlDbType.Int);
            arParms[0].Value = IdTablaDocumento;
            arParms[1] = new SqlParameter("@IdDocumento", SqlDbType.Int);
            arParms[1].Value = IdDocumento;     
            arParms[2] = new SqlParameter("@IdUsuario", SqlDbType.Int);
            arParms[2].Value = IdUsuario;

            SqlDataReader leer = null;
            try
            {

                leer = SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure, "NzControles.SpdCatComentarioCargar", arParms);
                if (leer != null)
                {
                    while (leer.Read())
                    {

                        objModApiComentario.Add(this.LlenarObjeto(leer));

                    }
                }

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            finally
            {
                if (leer != null)
                {
                    leer.Close();
                }
            }
            return objModApiComentario;
        }

    }
    }

