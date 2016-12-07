using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;

namespace ClbDatControles
{
    public class clsDatRelSecciones
    {
        string _strConexion;

        public clsDatRelSecciones()
        {
            this._strConexion = Common.DataBaseConnection.Instance.GetConnectionString();
        }

        public List<clsModRelSecciones> CargarRelaciones(int idFormulario, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            List<clsModRelSecciones> lstModRelSecciones = new List<clsModRelSecciones>();
            SqlParameter[] arrpar =
            {
                new SqlParameter("@IdFormulario",SqlDbType.Int){Value = idFormulario}, 
            };
            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdRelSeccionesCargarRelaciones", arrpar);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clsModRelSecciones objModRelSecciones = new clsModRelSecciones();
                        objModRelSecciones.IdRelSecciones = int.Parse(dr["IdRelSecciones"].ToString());
                        objModRelSecciones.IdSeccionPadre = int.Parse(dr["IdSeccionPadre"].ToString());
                        objModRelSecciones.IdSeccionHijo = int.Parse(dr["IdSeccionHijo"].ToString());
                        objModRelSecciones.KeyHijo = dr["KeyHijo"].ToString();
                        objModRelSecciones.KeyPadre = dr["KeyPadre"].ToString();
                        objModRelSecciones.Orden = int.Parse(dr["Orden"].ToString());

                        lstModRelSecciones.Add(objModRelSecciones);
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstModRelSecciones.OrderBy(x=>x.Orden).ToList();
        }

        public clsModErrorBase GuardarRelacion(clsModRelSecciones objModRelSecciones)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            SqlParameter[] arrpar =
            {
                new SqlParameter("@IdSeccionPadre",SqlDbType.Int){Value = objModRelSecciones.IdSeccionPadre},
                new SqlParameter("@IdSeccionHijo",SqlDbType.Int){Value = objModRelSecciones.IdSeccionHijo},
                new SqlParameter("@KeyPadre",SqlDbType.VarChar){Value = objModRelSecciones.KeyPadre},
                new SqlParameter("@KeyHijo",SqlDbType.VarChar){Value = objModRelSecciones.KeyHijo}, 
                new SqlParameter("@IdFormulario",SqlDbType.Int){Value = objModRelSecciones.IdFormulario}, 
            };

            try
            {
                objModErrorBase.Id = int.Parse(SqlHelper.ExecuteScalar(this._strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdRelSeccionesGuardarRelacion", arrpar).ToString());
            }
            catch (Exception ex)
            {

                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }

        public List<clsModRelSecciones> EliminarRelacion(int idRelSeccion, out clsModErrorBase objModErrorBase)
        {
             objModErrorBase = new clsModErrorBase();
            List<clsModRelSecciones> lstModRelSecciones = new List<clsModRelSecciones>();

            SqlParameter[] arrpar =
            {
                new SqlParameter("@IdRelSecciones", SqlDbType.Int) {Value = idRelSeccion}, 
            };
            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdRelSeccionesEliminarRelacion", arrpar);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clsModRelSecciones objModRelSecciones = new clsModRelSecciones();
                        objModRelSecciones.IdRelSecciones = int.Parse(dr["IdRelSecciones"].ToString());
                        objModRelSecciones.IdSeccionPadre = int.Parse(dr["IdSeccionPadre"].ToString());
                        objModRelSecciones.IdSeccionHijo = int.Parse(dr["IdSeccionHijo"].ToString());
                        objModRelSecciones.KeyHijo = dr["KeyHijo"].ToString();
                        objModRelSecciones.KeyPadre = dr["KeyPadre"].ToString();
                        objModRelSecciones.Orden = int.Parse(dr["Orden"].ToString());

                        lstModRelSecciones.Add(objModRelSecciones);
                    }
                }

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstModRelSecciones;
        }

        public clsModErrorBase CambiarOrden(clsModRelSecciones objRelSecciones)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            SqlParameter[] arrpar =
            {
                new SqlParameter("@IdRelSecciones", SqlDbType.Int){Value = objRelSecciones.IdRelSecciones}, 
                new SqlParameter("@Orden", SqlDbType.Int){Value = objRelSecciones.Orden}, 
            };

            try
            {
                SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdRelSeccionesCambiarOrden", arrpar);
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }
    }
}
