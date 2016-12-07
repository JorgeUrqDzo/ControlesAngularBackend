using ClbDatControles.Common;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbDatControles
{


    public class clsDatEncProceso
    {

        #region Variables Locales
        private string _strConexion;
        #endregion

        #region Constructor
        public clsDatEncProceso()
        {
            this._strConexion = DataBaseConnection.Instance.GetConnectionString();
        }
        #endregion

        #region Metodo para Paginacion y Busqueda
        public ICollection<clsModEncProceso> CargarBandejas(out clsModErrorBase objErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();

            ICollection<clsModEncProceso> lstModEncProceso = new Collection<clsModEncProceso>();

            SqlParameter[] arrParameters = new SqlParameter[3];
            arrParameters[0] = new SqlParameter("@TextoBuscar", SqlDbType.VarChar)
            {
                Value = strTextoBuscar
            };
            arrParameters[1] = new SqlParameter("@Pagina", SqlDbType.Int)
            {
                Value = intPagina
            };
            arrParameters[2] = new SqlParameter("@RegistrosPorPagina", SqlDbType.Int)
            {
                Value = intRegistrosPorPagina
            };


            SqlDataReader sqlDataReader = null;
            clsModEncProceso objModEncProceso = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdEncProcesoCargar", arrParameters);
                if (sqlDataReader != null)
                {

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            if (objModPaginacion.TotalRegistros == 0)
                            {
                                objModPaginacion.Asignar(intRegistrosPorPagina, decimal.Parse(sqlDataReader["Registros"].ToString()));
                            }
                            objModEncProceso = this.LlenarObjetoPaginacion(sqlDataReader);
                            lstModEncProceso.Add(objModEncProceso);

                        }
                    }

                }
            }
            catch (Exception exception)
            {
                objErrorBase.MsgError = exception.Message;
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
            return lstModEncProceso;
        }
        #endregion

        #region Llenar Objeto paginacion

        //Metodo para llenar el objeto paginacion con los datos de el store
        public clsModEncProceso LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {
            clsModEncProceso objModCatAreaProceso = new clsModEncProceso()
            {
                IdEncProceso = (int)(sqlDataReader["IdEncProceso"] != DBNull.Value ? sqlDataReader["IdEncProceso"] : 0),
                Nombre = (String)(sqlDataReader["Nombre"] != DBNull.Value ? sqlDataReader["Nombre"] : ""),
                Descripcion = (String)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"] : ""),
                Activo = (bool)(sqlDataReader["Activo"] != DBNull.Value ? sqlDataReader["Activo"] : "")
            };
            return objModCatAreaProceso;
        }

        #endregion

        #region CargarXId
        public clsModEncProceso CargarXId(clsModEncProceso objModEncProceso, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsModEncProceso objModelo = new clsModEncProceso();

            SqlParameter[] arrpar = {
                                        new SqlParameter("@IdEncProceso", SqlDbType.Int){Value = int.Parse(objModEncProceso.IdEncProceso.ToString())}
                                    };
            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdEncProcesoCargarXId", arrpar);

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        objModelo.IdEncProceso = int.Parse(dr["IdEncProceso"].ToString());
                        objModelo.Nombre = dr["Nombre"].ToString();
                        objModelo.Descripcion = dr["Descripcion"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModelo;
        }
        #endregion

        #region Obtener Actividades Por Tipo Proceso

        public List<clsModCatActividad> ActividadesXTipoProceso(clsModErrorBase objModErrorBase)
        {
            List<clsModCatActividad> lstCatActividad = new List<clsModCatActividad>();
            clsModCatActividad objCatActividad = null;
            objModErrorBase = new clsModErrorBase();

            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdCatTipoProcesoCargarActividades");

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        objCatActividad = new clsModCatActividad();
                        objCatActividad.objTipoProceso = new clsModCatTipoProceso();

                        objCatActividad.Actividad = dr["Actividad"].ToString();
                        objCatActividad.IdEncActividad = int.Parse(dr["IdEncActividad"].ToString());
                        objCatActividad.objTipoProceso.TipoProceso = dr["TipoProceso"].ToString();
                        objCatActividad.objTipoProceso.IdTipoProceso = int.Parse(dr["IdTipoProceso"].ToString());

                        lstCatActividad.Add(objCatActividad);
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstCatActividad;
        }
        #endregion

        #region Guardar Cambios

        public clsModErrorBase Guardar(clsModEncProceso objModEncProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();


            SqlParameter[] arrpar =
            {
                new SqlParameter("@Nombre", SqlDbType.VarChar){Value = objModEncProceso.Nombre.ToString()},
                new SqlParameter("@Descripcion", SqlDbType.VarChar){Value = objModEncProceso.Descripcion.ToString()}, 
            };

            try
            {
                objModErrorBase.Id =
                    int.Parse(
                        SqlHelper.ExecuteScalar(this._strConexion, CommandType.StoredProcedure, "NzControles.spdEncProcesoGuardar", arrpar).ToString());
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }
        #endregion

        #region Actualizar Cambios

        public clsModErrorBase Actualizar(clsModEncProceso objModEncProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();


            SqlParameter[] arrpar =
            {
                new SqlParameter("@IdEncProceso", SqlDbType.Int){Value = int.Parse(objModEncProceso.IdEncProceso.ToString())},
                new SqlParameter("@Nombre", SqlDbType.VarChar){Value = objModEncProceso.Nombre.ToString()},
                new SqlParameter("@Descripcion", SqlDbType.VarChar){Value = objModEncProceso.Descripcion.ToString()}, 
            };

            try
            {
                SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.StoredProcedure, "NzControles.spdEncProcesoActualizar", arrpar);
                //object objId =
                //        SqlHelper.ExecuteScalar(this._strConexion, CommandType.StoredProcedure, "NzControles.spdEncProcesoActualizar", arrpar);
                //objModErrorBase.Id = int.Parse(objId.ToString());
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
