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
    public class clsDatCatAreaProceso
    {
        #region Variables Privadas
        private string _strConexion;
        #endregion

        #region Constructor
        public clsDatCatAreaProceso()
        {
            this._strConexion = DataBaseConnection.Instance.GetConnectionString();
        }
        #endregion

        #region Metodo para Cargar Area Proceso
        public List<clsModCatAreaProceso> CargarAreaProceso(out clsModErrorBase objModErrorBase)
        {
            List<clsModCatAreaProceso> lstCatAreaProceso = new List<clsModCatAreaProceso>();

            clsModCatAreaProceso objModCatAreaProceso = null;

            objModErrorBase = new clsModErrorBase();

            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdCatAreaProcesoCargar");

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        objModCatAreaProceso = new clsModCatAreaProceso();

                        objModCatAreaProceso.IdAreaProceso = int.Parse(dr["IdAreaProceso"].ToString());
                        objModCatAreaProceso.AreaProceso = dr["AreaProceso"].ToString();
                        objModCatAreaProceso.Activo = bool.Parse(dr["Activo"].ToString());

                        lstCatAreaProceso.Add(objModCatAreaProceso);
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
            }
            return lstCatAreaProceso;
        }
        #endregion

        #region Metodo para Crear Area Proceso
        public clsModErrorBase Crear(clsModCatAreaProceso objModCatAreaProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            SqlParameter[] arrpar =  {
                                        new SqlParameter("@AreaProceso",SqlDbType.VarChar){ Value = objModCatAreaProceso.AreaProceso.ToString() }
                                    };
            try
            {
                object objId = null;
                objId = SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.StoredProcedure, "NzControles.spdCatAreaProcesoInsertar", arrpar);
                if (objId != null)
                {
                    objModErrorBase.Id = int.Parse(objId.ToString());
                    objModErrorBase.MsgError = "Proceso Creado";
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }
        #endregion

        #region Meotodo para CargarXId
        public clsModCatAreaProceso CargarXId(int id, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsModCatAreaProceso capaModelo = null;

            SqlParameter[] arrpar ={
                                       new SqlParameter("@IdAreaProceso", SqlDbType.Int) { Value = id}
                                  };
            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdCatAreaProcesoCargarXId", arrpar);

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        capaModelo = new clsModCatAreaProceso();

                        capaModelo.IdAreaProceso = int.Parse(dr["IdAreaProceso"].ToString());
                        capaModelo.AreaProceso = dr["AreaProceso"].ToString();
                        capaModelo.Activo = bool.Parse(dr["Activo"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                }
            }

            return capaModelo;
        }
        #endregion

        #region Metodo para Actualizar
        public clsModErrorBase Actualizar(clsModCatAreaProceso objModCatAreaProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            SqlParameter[] arrpar = {
                                        new SqlParameter("@IdAreaProceso",SqlDbType.Int) {Value= objModCatAreaProceso.IdAreaProceso},
                                        new SqlParameter("@AreaProceso",SqlDbType.VarChar){Value = objModCatAreaProceso.AreaProceso},
                                    };
            try
            {
                SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.StoredProcedure, "NzControles.spdCatAreaProcesoActualizar", arrpar);
                objModErrorBase.MsgError = "Proceso Actualizado";

            }catch(Exception ex){
                objModErrorBase.MsgError = ex.Message;
            }
            return objModErrorBase;
        }
        #endregion

        #region Metodo para Paginacion y Busqueda
        public ICollection<clsModCatAreaProceso> CargarBandejas(out clsModErrorBase objErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatAreaProceso> lstCatAreaProceso = new Collection<clsModCatAreaProceso>();

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
            clsModCatAreaProceso objModCatAreaProceso = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdCatAreaProcesoCargarX", arrParameters);
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
                            objModCatAreaProceso = this.LlenarObjetoPaginacion(sqlDataReader);
                            lstCatAreaProceso.Add(objModCatAreaProceso);

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
            return lstCatAreaProceso;
        }
        #endregion

        #region Llenar Objeto paginacion
        
        //Metodo para llenar el objeto paginacion con los datos de el store
        public clsModCatAreaProceso LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {
            clsModCatAreaProceso objModCatAreaProceso = new clsModCatAreaProceso()
            {
                IdAreaProceso = (int)(sqlDataReader["IdAreaProceso"] != DBNull.Value ? sqlDataReader["IdAreaProceso"] : 0),
                AreaProceso = (String)(sqlDataReader["AreaProceso"] != DBNull.Value ? sqlDataReader["AreaProceso"] : ""),
                Activo = (bool)(sqlDataReader["Activo"] != DBNull.Value ? sqlDataReader["Activo"] : "")
            };
            return objModCatAreaProceso;
        }

        #endregion

        #region Metodo Actualizar Estado
        public clsModErrorBase ActualizarEstado(clsModCatAreaProceso objModCatAreaProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            SqlParameter[] arrpar = {
                                        new SqlParameter("@IdAreaProceso",SqlDbType.Int) {Value= objModCatAreaProceso.IdAreaProceso},
                                        new SqlParameter("@Activo",SqlDbType.Bit){Value=objModCatAreaProceso.Activo}
                                    };
            try
            {
                SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.StoredProcedure, "NzControles.spdCatAreaProcesoActualizarEstado", arrpar);
                objModErrorBase.MsgError = "Estado Actualizado";

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
