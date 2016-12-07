using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles.Common;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;

namespace ClbDatControles
{
    public class clsDatDetActividad
    {
        #region Variables Locales
        private string _strConexion;
        #endregion

        #region Constructor
        public clsDatDetActividad()
        {
            this._strConexion = DataBaseConnection.Instance.GetConnectionString();
        }
        #endregion

        #region Cargar Configuracion Actividad
        public List<clsModDetActividad> Cargar(int id, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            List<clsModDetActividad> lstModDetActividad = new List<clsModDetActividad>();
            clsModDetActividad objModDetActividad = null;

            SqlParameter[] arrpar = {
                                        new SqlParameter("@IdEncProceso", SqlDbType.Int) { Value = id}
                                    };
            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdDetActividadCargarFlujo", arrpar);

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        objModDetActividad = new clsModDetActividad();
                        objModDetActividad.objCatActividad = new clsModCatActividad();
                        objModDetActividad.objCatTipoProceso = new clsModCatTipoProceso();

                        //Datos Pertenecientes a la tabla DetActividad
                        objModDetActividad.IdEncActividad = int.Parse(dr["IdEncActividad"].ToString());
                        objModDetActividad.Aprobacion = bool.Parse(dr["Aprobacion"].ToString());
                        objModDetActividad.IdEncActividadSiguiente = int.Parse(dr["IdEncActividadSiguiente"].ToString());
                        objModDetActividad.IdEncProceso = int.Parse(dr["IdEncProceso"].ToString());
                        
                        //Datos Pretenecientes a la tabla EncActividad
                        objModDetActividad.objCatActividad.Actividad = dr["Actividad"].ToString();
                        objModDetActividad.objCatActividad.ActividadSiguiente = dr["ActividadSiguiente"].ToString();

                        //Datos Pertenecientes a la tabla CatTipoProceso
                        objModDetActividad.objCatTipoProceso.TipoProceso = dr["TipoProceso"].ToString();
                        objModDetActividad.objCatTipoProceso.TipoProcesoSiguiente =
                            dr["TipoProcesoSiguiente"].ToString();

                        //Ids De las Tablas
                        objModDetActividad.IdDetActividad = int.Parse(dr["IdDetActividad"].ToString());
                        objModDetActividad.objCatTipoProceso.IdTipoProceso = int.Parse(dr["IdTipoProceso"].ToString());
                        objModDetActividad.objCatActividad.IdEncActividad = int.Parse(dr["IdEncActividad"].ToString());
                        

                        lstModDetActividad.Add(objModDetActividad);
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstModDetActividad;
        }
        #endregion

        #region GuardarCambios

        public clsModErrorBase Guardar(List<clsModDetActividad> lstModDetActividad, int idEncProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            SqlParameter[] id =
            {
                new SqlParameter("@IdEncProceso", SqlDbType.Int){Value = idEncProceso}, 
            };
            try
            {
                SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.StoredProcedure,
                    "NzControles.spdDetActividadElimnar", id);
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            try
            {
                foreach (var objModDetActividad in lstModDetActividad)
                {
                    SqlParameter[] arrpar =
                    {
                        new SqlParameter("@IdEncProceso",SqlDbType.Int){Value = idEncProceso},
                        new SqlParameter("@IdEncActividad", SqlDbType.Int){Value = int.Parse(objModDetActividad.IdEncActividad.ToString())}, 
                        new SqlParameter("@IdEncActividadSiguiente",SqlDbType.Int){Value = objModDetActividad.IdEncActividadSiguiente},
                        new SqlParameter("@Aprobacion", SqlDbType.Bit){Value = objModDetActividad.Aprobacion}, 
                    };

                    SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.StoredProcedure,
                        "NzControles.spdDetActividadGuardar", arrpar);
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            return objModErrorBase;
        }
        #endregion
    }
}
