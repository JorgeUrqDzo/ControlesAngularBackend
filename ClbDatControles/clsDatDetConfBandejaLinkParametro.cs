using ClbDatControles.Common;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbDatControles
{
    public class clsDatDetConfBandejaLinkParametro
    {
        #region Variables privadas
        private string _strConexion;
        #endregion

        #region Constructor

        public clsDatDetConfBandejaLinkParametro()
        {
            this._strConexion = DataBaseConnection.Instance.GetConnectionString();
        }

        #endregion

        #region Cargar Prametros
        public List<clsModDetConfBandejaLinkParametro> CargarParametros(out clsModErrorBase objModErrorBase, int id)
        {
            List<clsModDetConfBandejaLinkParametro> lstModDetConfBandejaLinkParmetro = new List<clsModDetConfBandejaLinkParametro>();
            clsModDetConfBandejaLinkParametro objModDetConfBandejaLinkParametro = null;
            objModErrorBase = new clsModErrorBase();
            SqlParameter[] arrpar = { new SqlParameter("@IdEncConfBandeja", SqlDbType.Int) { Value = id } };

            SqlDataReader dataReader = null;
            try
            {
                dataReader = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdDetConfBandejaLinkParametroCargarParametros", arrpar);
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        objModDetConfBandejaLinkParametro = new clsModDetConfBandejaLinkParametro();
                        objModDetConfBandejaLinkParametro.IdDetConfBandejaLinkParametro = int.Parse(dataReader["IdDetConfBandejaLinkParametro"].ToString());
                        objModDetConfBandejaLinkParametro.IdEncConfBandeja = int.Parse(dataReader["IdEncConfBandeja"].ToString());
                        objModDetConfBandejaLinkParametro.Valor = dataReader["Valor"].ToString();
                        objModDetConfBandejaLinkParametro.Parametro = dataReader["Parametro"].ToString();
                        objModDetConfBandejaLinkParametro.TipoParametro = dataReader["TipoParametro"].ToString();
                        objModDetConfBandejaLinkParametro.IdTipoParametro = int.Parse(dataReader["IdTipoParametro"].ToString());
                        objModDetConfBandejaLinkParametro.IdTipoEnvioParametro = int.Parse(dataReader["IdTipoEnvioParametro"].ToString());
                        objModDetConfBandejaLinkParametro.IdUsuarioCreacion = int.Parse(dataReader["IdUsuarioCreacion"].ToString());
                        objModDetConfBandejaLinkParametro.IdUsuarioModificacion = int.Parse(dataReader["IdUsuarioModificacion"].ToString());
                        objModDetConfBandejaLinkParametro.FechaCreacion = DateTime.Parse(dataReader["FechaCreacion"].ToString());
                        objModDetConfBandejaLinkParametro.FechaModificacion = DateTime.Parse(dataReader["FechaModificacion"].ToString());

                        lstModDetConfBandejaLinkParmetro.Add(objModDetConfBandejaLinkParametro);
                    }
                }

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
            return lstModDetConfBandejaLinkParmetro;
        }

        public List<clsModDetConfBandejaLinkParametro> CargarParametros()
        {
            List<clsModDetConfBandejaLinkParametro> lstModDetConfBandejaLinkParmetro = new List<clsModDetConfBandejaLinkParametro>();
            clsModDetConfBandejaLinkParametro objModDetConfBandejaLinkParametro = null;

            SqlDataReader dataReader = null;
            try
            {
                dataReader = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdDetConfBandejaLinkParametroCargarParametros");
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        objModDetConfBandejaLinkParametro = new clsModDetConfBandejaLinkParametro();

                        objModDetConfBandejaLinkParametro.IdDetConfBandejaLinkParametro = int.Parse(dataReader["IdDetConfBandejaLinkParametro"].ToString());
                        objModDetConfBandejaLinkParametro.IdEncConfBandeja = int.Parse(dataReader["@IdEncConfBandeja"].ToString());
                        objModDetConfBandejaLinkParametro.Parametro = dataReader["Parametro"].ToString();
                        objModDetConfBandejaLinkParametro.Valor = dataReader["Valor"].ToString();
                        objModDetConfBandejaLinkParametro.IdTipoParametro = int.Parse(dataReader["IdTipoParametro"].ToString());
                        objModDetConfBandejaLinkParametro.IdTipoEnvioParametro = int.Parse(dataReader["IdTipoEnvioParametro"].ToString());
                        objModDetConfBandejaLinkParametro.IdUsuarioCreacion = int.Parse(dataReader["IdUsuarioCreacion"].ToString());
                        objModDetConfBandejaLinkParametro.IdUsuarioModificacion = int.Parse(dataReader["IdUsuarioModificacion"].ToString());
                        objModDetConfBandejaLinkParametro.FechaCreacion = DateTime.Parse(dataReader["FechaCreacion"].ToString());
                        objModDetConfBandejaLinkParametro.FechaModificacion = DateTime.Parse(dataReader["FechaModificacion"].ToString());

                        lstModDetConfBandejaLinkParmetro.Add(objModDetConfBandejaLinkParametro);
                    }
                }

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
            return lstModDetConfBandejaLinkParmetro;
        }
        #endregion

        #region Insertar Parametros

        public clsModErrorBase InsertarParametros(clsModDetConfBandejaLinkParametro objCapaModelo)
        {
            clsModErrorBase objModErrorBase = null;


            SqlParameter[] arrpar = {
                                        new SqlParameter("@IdEncConfBandeja",SqlDbType.Int){ Value = int.Parse(objCapaModelo.IdEncConfBandeja.ToString())},
                                        new SqlParameter("@Parametro", SqlDbType.VarChar){ Value = objCapaModelo.Parametro.ToString()},
                                        new SqlParameter("@Valor",SqlDbType.VarChar) { Value = objCapaModelo.Valor.ToString()},
                                        new SqlParameter("@IdTipoParametro", SqlDbType.Int) {Value = int.Parse(objCapaModelo.IdTipoParametro.ToString())},
                                        new SqlParameter("@IdTipoEnvioParametro", SqlDbType.Int){ Value = int.Parse(objCapaModelo.IdTipoEnvioParametro.ToString())},
                                        new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int) { Value = int.Parse(objCapaModelo.IdUsuarioCreacion.ToString())},
                                        new SqlParameter("@IdUsuarioModificacion",SqlDbType.Int){Value = int.Parse(objCapaModelo.IdUsuarioModificacion.ToString())},
                                    };
            try
            {
                object objId = SqlHelper.ExecuteScalar(this._strConexion, CommandType.StoredProcedure, "NzControles.spdDetConfBandejaLinkParametroInsertarParametros", arrpar);
                if (objId != null)
                {
                    objModErrorBase = new clsModErrorBase();
                    objModErrorBase.Id = int.Parse(objId.ToString());
                }
            }
            catch (Exception ex)
            {
                objModErrorBase = new clsModErrorBase();
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }
        #endregion

        #region Cargar parametros X ID

        public clsModDetConfBandejaLinkParametro CargarParametroXId(int id, out clsModErrorBase objModErrorBase)
        {
            clsModDetConfBandejaLinkParametro objCapaModelo = new clsModDetConfBandejaLinkParametro();
            objModErrorBase = new clsModErrorBase();

            SqlParameter[] arrpar = {
                                        new SqlParameter("@IdDetConfBandejaLinkParametro",SqlDbType.Int) { Value = id}
                                    };
            SqlDataReader leer = null;
            try
            {
                leer = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdDetConfBandejaLinkParametroCargarXIdParametros", arrpar);
                if (leer != null)
                {
                    while (leer.Read())
                    {
                        objCapaModelo.IdDetConfBandejaLinkParametro = int.Parse(leer["IdDetConfBandejaLinkParametro"].ToString());
                        objCapaModelo.IdEncConfBandeja = int.Parse(leer["IdEncConfBandeja"].ToString());
                        objCapaModelo.Parametro = leer["Parametro"].ToString();
                        objCapaModelo.Valor = leer["Valor"].ToString();
                        objCapaModelo.IdTipoParametro = int.Parse(leer["IdTipoParametro"].ToString());
                        objCapaModelo.IdTipoEnvioParametro = int.Parse(leer["IdTipoEnvioParametro"].ToString());
                        objCapaModelo.IdUsuarioCreacion = int.Parse(leer["IdUsuarioCreacion"].ToString());
                        objCapaModelo.IdUsuarioModificacion = int.Parse(leer["IdUsuarioModificacion"].ToString());
                        objCapaModelo.FechaCreacion = DateTime.Parse(leer["FechaCreacion"].ToString());
                        objCapaModelo.FechaModificacion = DateTime.Parse(leer["FechaModificacion"].ToString());
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
            return objCapaModelo;
        }

        #endregion

        #region Actualizar Parametros
        public clsModErrorBase ActualizarParametros(clsModDetConfBandejaLinkParametro objCapaModelo)
        {

            clsModErrorBase objModErrorBase = new clsModErrorBase();

            SqlParameter[] arrpar = {
                                        new SqlParameter("@IdDetConfBandejaLinkParametro", SqlDbType.Int) { Value = int.Parse(objCapaModelo.IdDetConfBandejaLinkParametro.ToString())},

                                        new SqlParameter("@IdEncConfBandeja",SqlDbType.Int){ Value = int.Parse(objCapaModelo.IdEncConfBandeja.ToString())},

                                        new SqlParameter("@Parametro", SqlDbType.VarChar){ Value = objCapaModelo.Parametro.ToString()},

                                        new SqlParameter("@Valor",SqlDbType.VarChar) { Value = objCapaModelo.Valor.ToString()},

                                        new SqlParameter("@IdTipoParametro", SqlDbType.Int) {Value = int.Parse(objCapaModelo.IdTipoParametro.ToString())},

                                        new SqlParameter("@IdTipoEnvioParametro", SqlDbType.Int){ Value = int.Parse(objCapaModelo.IdTipoEnvioParametro.ToString())},
                                       
                                        new SqlParameter("@IdUsuarioModificacion",SqlDbType.Int){Value = int.Parse(objCapaModelo.IdUsuarioModificacion.ToString())},
        
                                    };
            try
            {
               SqlHelper.ExecuteScalar(this._strConexion, CommandType.StoredProcedure, "NzControles.spdDetConfBandejaLinkParametroActualizarParametros", arrpar);
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            return objModErrorBase;
        }
        #endregion

        #region Eliminar Parametros
        public clsModErrorBase EliminarParametro(int id)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            SqlParameter[] arrpar = { new SqlParameter("@IdDetConfBandejaLinkParametro",SqlDbType.Int) {Value = id}};
            try
            {
                SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.StoredProcedure, "NzControles.spdDetConfBandejaLinkParametroEliminarParametros", arrpar);
            }
            catch(Exception ex){
                objModErrorBase.MsgError = ex.Message;
            }
            return objModErrorBase;
        }
        #endregion
    }
}
