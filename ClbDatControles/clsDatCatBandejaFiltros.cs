using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles.Common;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using ClbModControles;

namespace ClbDatControles
{
    public class clsDatCatBandejaFiltros
    {
        /// <summary>
        /// Variable de conexión para la clase
        /// </summary>
        private string _StrConnection;
        /// <summary>
        /// Constructor que inicializa la cadena de conexión
        /// </summary>
        public clsDatCatBandejaFiltros()
        {
            _StrConnection = Common.DataBaseConnection.Instance.GetConnectionString();
        }
        public ICollection<clsModCatBandejaFiltros> CargaPagBandejaFiltros(out clsModErrorBase objErrorBase, string strTextoBusqueda, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatBandejaFiltros> lstModCatBandejaFiltros = new Collection<clsModCatBandejaFiltros>();
            SqlParameter[] arrParameters = new SqlParameter[3];
            arrParameters[0] = new SqlParameter("@TextoBusqueda", SqlDbType.VarChar) { Value = strTextoBusqueda };
            arrParameters[1] = new SqlParameter("@Pagina", SqlDbType.Int) { Value = intPagina };
            arrParameters[2] = new SqlParameter("@RegistrosPorPagina", SqlDbType.Int) { Value = intRegistrosPorPagina };
            SqlDataReader sqlDataReader = null;
            clsModCatBandejaFiltros objModCatBandejaFiltros = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdDentConfBandejaFiltrosCargarPorPagina", arrParameters);
                if (sqlDataReader != null)
                {
                    //Paginación
                    try
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                //Solo lo debe de hacer la primera vez
                                if (objModPaginacion.TotalRegistros == 0)
                                {
                                    objModPaginacion.Asignar(intRegistrosPorPagina, decimal.Parse(sqlDataReader["Registros"].ToString()));

                                }
                                //TODO: Joel Hernandez, Crear lista de objetos para añadirlos a la lista 
                                objModCatBandejaFiltros = this.LlenarObjetoPaginacion(sqlDataReader);
                                lstModCatBandejaFiltros.Add(objModCatBandejaFiltros);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        objErrorBase.MsgError = exception.Message;
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
            return lstModCatBandejaFiltros;
        }
        private clsModCatBandejaFiltros LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {
            clsModCatBandejaFiltros objModBandejaFiltros = new clsModCatBandejaFiltros()
            {
                IdDentConfBandejaFiltros = (int)(sqlDataReader["IdDentConfBandejaFiltros"] != DBNull.Value ? sqlDataReader["IdDentConfBandejaFiltros"] : 0),
                IdEncConfBandeja = (int)(sqlDataReader["IdEncConfBandeja"] != DBNull.Value ? sqlDataReader["IdEncConfBandeja"] : 0),
                Filtro = (string)(sqlDataReader["Filtro"] != DBNull.Value ? sqlDataReader["Filtro"] : ""),
                IdTipoControl = (int)(sqlDataReader["IdTipoControl"] != DBNull.Value ? sqlDataReader["IdTipoControl"] : 0),
                IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                IdUsuarioModificacion = (int)(sqlDataReader["IdUsuarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                FechaCreacion = (DateTime)(sqlDataReader["FechaCreacion"] != DBNull.Value ? sqlDataReader["FechaCreacion"] : ""),
                FechaModificacion = (DateTime)(sqlDataReader["FechaModificacion"] != DBNull.Value ? sqlDataReader["FechaModificacion"] : ""),
                TipoControl = (string)(sqlDataReader["TipoControl"] != DBNull.Value ? sqlDataReader["TipoControl"] : ""),
            };
            return objModBandejaFiltros;
        }
        public clsModCatBandejaFiltros CargarBandejaFiltrosPorId(out clsModErrorBase objErrorBase, int intIdBandejaColumna)
        {
            objErrorBase = new clsModErrorBase();
            clsModCatBandejaFiltros objModCatFormulario = null;
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@IdBandejaFiltros", SqlDbType.Int) { Value = intIdBandejaColumna });
                SqlDataReader sqlDataReader = null;

                try
                {
                    sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdDentConfBandejaFiltrosCargaPorId", lstSqlParameters.ToArray());
                    if (sqlDataReader != null)
                    {
                        while (sqlDataReader.Read())
                        {
                            objModCatFormulario = this.LlenarObjetoBandejaFiltrosId(sqlDataReader);
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
            }
            catch (Exception exception)
            {
                objErrorBase.MsgError = exception.Message;
            }
            return objModCatFormulario;
        }
        private clsModCatBandejaFiltros LlenarObjetoBandejaFiltrosId(SqlDataReader sqlDataReader)
        {
            clsModCatBandejaFiltros objModBandejaColumna = new clsModCatBandejaFiltros()
            {
                IdDentConfBandejaFiltros = (int)(sqlDataReader["IdDentConfBandejaFiltros"] != DBNull.Value ? sqlDataReader["IdDentConfBandejaFiltros"] : 0),
                IdEncConfBandeja = (int)(sqlDataReader["IdEncConfBandeja"] != DBNull.Value ? sqlDataReader["IdEncConfBandeja"] : 0),
                Filtro = (string)(sqlDataReader["Filtro"] != DBNull.Value ? sqlDataReader["Filtro"] : ""),
                IdTipoControl = (int)(sqlDataReader["IdTipoControl"] != DBNull.Value ? sqlDataReader["IdTipoControl"] : 0),
                IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                IdUsuarioModificacion = (int)(sqlDataReader["IdUsuarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                FechaCreacion = (DateTime)(sqlDataReader["FechaCreacion"] != DBNull.Value ? sqlDataReader["FechaCreacion"] : ""),
                FechaModificacion = (DateTime)(sqlDataReader["FechaModificacion"] != DBNull.Value ? sqlDataReader["FechaModificacion"] : "")
            };
            return objModBandejaColumna;
        }
        public clsModErrorBase ActualizarBandejaFiltros(clsModCatBandejaFiltros objModCatBandejaFiltros)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@idBandejaFiltro", SqlDbType.VarChar) { Value = objModCatBandejaFiltros.IdDentConfBandejaFiltros });
                lstSqlParameters.Add(new SqlParameter("@filtro", SqlDbType.VarChar) { Value = objModCatBandejaFiltros.Filtro });
                lstSqlParameters.Add(new SqlParameter("@idTipoControl", SqlDbType.Int) { Value = objModCatBandejaFiltros.IdTipoControl });

                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdDentConfBandejaFiltrosActualizarPorId", lstSqlParameters.ToArray());
                if (objId != null)
                {
                    int intIdentity;
                    int.TryParse(objId.ToString(), out intIdentity);
                    objModErrorBase.Id = intIdentity;
                }
            }
            catch (Exception exception)
            {
                objModErrorBase.MsgError = exception.Message;
            }
            return objModErrorBase;
        }
        public clsModErrorBase EliminarBandejaFiltros(clsModCatBandejaFiltros objModCatBandejaFiltros)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@idBandejaFiltro", SqlDbType.Int) { Value = objModCatBandejaFiltros.IdDentConfBandejaFiltros });
                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdDentConfBandejaFiltrosEliminarPorId", lstSqlParameters.ToArray());
                if (objId != null)
                {
                    int intIdentity;
                    int.TryParse(objId.ToString(), out intIdentity);
                    objModErrorBase.Id = intIdentity;
                }
            }
            catch (Exception exception)
            {
                objModErrorBase.MsgError = exception.Message;
            }
            return objModErrorBase;
        }
        public clsModErrorBase AgregarBandejaFiltros(clsModCatBandejaFiltros objModCatBandejaFiltros)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@IdTipoControl", SqlDbType.Int) { Value = objModCatBandejaFiltros.IdTipoControl });
                lstSqlParameters.Add(new SqlParameter("@Filtro", SqlDbType.VarChar) { Value = objModCatBandejaFiltros.Filtro });
                lstSqlParameters.Add(new SqlParameter("@IdEncConfBandeja", SqlDbType.Int) { Value = objModCatBandejaFiltros.IdEncConfBandeja });
                lstSqlParameters.Add(new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int) { Value = objModCatBandejaFiltros.IdUsuarioCreacion });
                lstSqlParameters.Add(new SqlParameter("@IdUsuarioModificacion", SqlDbType.Int) { Value = objModCatBandejaFiltros.IdUsuarioModificacion });

                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.spdDentConfBandejaFiltroAgregar", lstSqlParameters.ToArray());
                if (objId != null)
                {
                    int intIdentity;
                    int.TryParse(objId.ToString(), out intIdentity);
                    objModErrorBase.Id = intIdentity;
                }
            }
            catch (Exception exception)
            {
                objModErrorBase.MsgError = exception.Message;
            }
            return objModErrorBase;
        }
    }
}
