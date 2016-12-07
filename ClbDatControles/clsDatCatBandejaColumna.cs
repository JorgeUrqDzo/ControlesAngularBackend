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
    public class clsDatCatBandejaColumna
    {
        /// <summary>
        /// Variable de conexión para la clase
        /// </summary>
        private string _StrConnection;
        /// <summary>
        /// Constructor que inicializa la cadena de conexión
        /// </summary>
        public clsDatCatBandejaColumna()
        {
            _StrConnection = Common.DataBaseConnection.Instance.GetConnectionString();
        }
        public ICollection<clsModCatBandejaColumna> CargaPagBandejaColumna(out clsModErrorBase objErrorBase, int intIdEncConfBandeja)
        {
            objErrorBase = new clsModErrorBase();
            
            ICollection<clsModCatBandejaColumna> lstModCatBandejaColumna = new Collection<clsModCatBandejaColumna>();
            SqlParameter[] arrParameters = new SqlParameter[3];
            arrParameters[0] = new SqlParameter("@IdEncConfBandeja", SqlDbType.Int) { Value = intIdEncConfBandeja };
            SqlDataReader sqlDataReader = null;
            clsModCatBandejaColumna objModCatBandejaColumna = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdDentConfBandejaColumnaCargarPorPagina", arrParameters);
                if (sqlDataReader != null)
                {
                    //Paginación
                    try
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                objModCatBandejaColumna = this.LlenarObjetoPaginacion(sqlDataReader);
                                lstModCatBandejaColumna.Add(objModCatBandejaColumna);
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
            return lstModCatBandejaColumna;
        }

        private clsModCatBandejaColumna LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {
            clsModCatBandejaColumna objModBandejaColumna = new clsModCatBandejaColumna()
            {
                IdDetConfBandejaColumna = (int)(sqlDataReader["IdDetConfBandejaColumna"] != DBNull.Value ? sqlDataReader["IdDetConfBandejaColumna"] : 0),
                IdEncConfBandeja = (int)(sqlDataReader["IdEncConfBandeja"] != DBNull.Value ? sqlDataReader["IdEncConfBandeja"] : 0),
                TituloColumna = (String)(sqlDataReader["TituloColumna"] != DBNull.Value ? sqlDataReader["TituloColumna"] : ""),
                ColumnaBD = (String)(sqlDataReader["ColumnaBD"] != DBNull.Value ? sqlDataReader["ColumnaBD"] : ""),
                IdTipoColumna = (int)(sqlDataReader["IdTipoColumna"] != DBNull.Value ? sqlDataReader["IdTipoColumna"] : 0),
                TipoColumna = (string)(sqlDataReader["TipoColumna"] != DBNull.Value ? sqlDataReader["TipoColumna"] : ""),
                Clase = (String)(sqlDataReader["Clase"] != DBNull.Value ? sqlDataReader["Clase"] : ""),
                TipoLink = (bool)(sqlDataReader["TipoLink"] != DBNull.Value ? sqlDataReader["TipoLink"] : ""),
                Formato = (String)(sqlDataReader["Formato"] != DBNull.Value ? sqlDataReader["Formato"] : ""),
                PaginaLink = (String)(sqlDataReader["PaginaLink"] != DBNull.Value ? sqlDataReader["PaginaLink"] : ""),
                IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                IdUsuarioModificacion = (int)(sqlDataReader["IdUsuarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                FechaCreacion = (DateTime)(sqlDataReader["FechaCreacion"] != DBNull.Value ? sqlDataReader["FechaCreacion"] : ""),
                FechaModificacion = (DateTime)(sqlDataReader["FechaModificacion"] != DBNull.Value ? sqlDataReader["FechaModificacion"] : "")
            };
            return objModBandejaColumna;
        }

        public clsModCatBandejaColumna CargarBandejaColumnaPorId(out clsModErrorBase objErrorBase, int intIdBandejaColumna)
        {
            objErrorBase = new clsModErrorBase();
            clsModCatBandejaColumna objModCatFormulario = null;
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@IdBandejaColumna ", SqlDbType.Int) { Value = intIdBandejaColumna });
                SqlDataReader sqlDataReader = null;

                try
                {
                    sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdDentConfBandejaColumnaCargaPorId", lstSqlParameters.ToArray());
                    if (sqlDataReader != null)
                    {
                        while (sqlDataReader.Read())
                        {
                            objModCatFormulario = this.LlenarObjetoBandejaColumnaId(sqlDataReader);
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

        private clsModCatBandejaColumna LlenarObjetoBandejaColumnaId(SqlDataReader sqlDataReader)
        {
            clsModCatBandejaColumna objModBandejaColumna = new clsModCatBandejaColumna()
            {
                IdDetConfBandejaColumna = (int)(sqlDataReader["IdDetConfBandejaColumna"] != DBNull.Value ? sqlDataReader["IdDetConfBandejaColumna"] : 0),
                IdEncConfBandeja = (int)(sqlDataReader["IdEncConfBandeja"] != DBNull.Value ? sqlDataReader["IdEncConfBandeja"] : 0),
                TituloColumna = (String)(sqlDataReader["TituloColumna"] != DBNull.Value ? sqlDataReader["TituloColumna"] : ""),
                ColumnaBD = (String)(sqlDataReader["ColumnaBD"] != DBNull.Value ? sqlDataReader["ColumnaBD"] : ""),
                IdTipoColumna = (int)(sqlDataReader["IdTipoColumna"] != DBNull.Value ? sqlDataReader["IdTipoColumna"] : 0),
                Clase = (String)(sqlDataReader["Clase"] != DBNull.Value ? sqlDataReader["Clase"] : ""),
                Formato = (String)(sqlDataReader["Formato"] != DBNull.Value ? sqlDataReader["Formato"] : ""),
                TipoLink = (bool)(sqlDataReader["TipoLink"] != DBNull.Value ? sqlDataReader["TipoLink"] : false),
                PaginaLink = (String)(sqlDataReader["PaginaLink"] != DBNull.Value ? sqlDataReader["PaginaLink"] : ""),
                IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                IdUsuarioModificacion = (int)(sqlDataReader["IdUsuarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                FechaCreacion = (DateTime)(sqlDataReader["FechaCreacion"] != DBNull.Value ? sqlDataReader["FechaCreacion"] : ""),
                FechaModificacion = (DateTime)(sqlDataReader["FechaModificacion"] != DBNull.Value ? sqlDataReader["FechaModificacion"] : "")
            };
            return objModBandejaColumna;
        }

        public clsModErrorBase ActualizarBandejaColumna(clsModCatBandejaColumna objModCatBandejaColumna)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@idBandejaColumna", SqlDbType.Int) { Value = objModCatBandejaColumna.IdDetConfBandejaColumna });
                lstSqlParameters.Add(new SqlParameter("@tituloColumna", SqlDbType.VarChar) { Value = objModCatBandejaColumna.TituloColumna });
                lstSqlParameters.Add(new SqlParameter("@columnaBD", SqlDbType.VarChar) { Value = objModCatBandejaColumna.ColumnaBD });
                lstSqlParameters.Add(new SqlParameter("@idTipoColumna", SqlDbType.Int) { Value = objModCatBandejaColumna.IdTipoColumna });
                lstSqlParameters.Add(new SqlParameter("@clase", SqlDbType.VarChar) { Value = objModCatBandejaColumna.Clase });
                lstSqlParameters.Add(new SqlParameter("@formato", SqlDbType.VarChar) { Value = objModCatBandejaColumna.Formato });
                lstSqlParameters.Add(new SqlParameter("@paginaLink", SqlDbType.VarChar) { Value = objModCatBandejaColumna.PaginaLink });
                lstSqlParameters.Add(new SqlParameter("@tipoLink", SqlDbType.Bit) { Value = objModCatBandejaColumna.TipoLink });

                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdDentConfBandejaColumnaActualizarPorId", lstSqlParameters.ToArray());
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

        public clsModErrorBase EliminarBandejaColumna(clsModCatBandejaColumna objModCatBandejaColumna)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@idBandejaColumna", SqlDbType.Int) { Value = objModCatBandejaColumna.IdDetConfBandejaColumna });
               
                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdDentConfBandejaColumnaEliminarPorId", lstSqlParameters.ToArray());
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

        public clsModErrorBase AgregarBandejaColumna(clsModCatBandejaColumna objModCatBandejaColumna)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@tituloColumna", SqlDbType.VarChar) { Value = objModCatBandejaColumna.TituloColumna });
                lstSqlParameters.Add(new SqlParameter("@columnaBD", SqlDbType.VarChar) { Value = objModCatBandejaColumna.ColumnaBD });
                lstSqlParameters.Add(new SqlParameter("@idTipoColumna", SqlDbType.Int) { Value = objModCatBandejaColumna.IdTipoColumna });
                lstSqlParameters.Add(new SqlParameter("@clase", SqlDbType.VarChar) { Value = objModCatBandejaColumna.Clase });
                lstSqlParameters.Add(new SqlParameter("@formato", SqlDbType.VarChar) { Value = objModCatBandejaColumna.Formato });
                lstSqlParameters.Add(new SqlParameter("@paginaLink", SqlDbType.VarChar) { Value = objModCatBandejaColumna.PaginaLink });
                lstSqlParameters.Add(new SqlParameter("@tipoLink", SqlDbType.Bit) { Value = objModCatBandejaColumna.TipoLink });
                lstSqlParameters.Add(new SqlParameter("@IdEncConfBandeja", SqlDbType.Int) { Value = objModCatBandejaColumna.IdEncConfBandeja });
                lstSqlParameters.Add(new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int) { Value = objModCatBandejaColumna.IdUsuarioCreacion });
                lstSqlParameters.Add(new SqlParameter("@IdUsuarioModificacion", SqlDbType.Int) { Value = objModCatBandejaColumna.IdUsuarioModificacion });
                lstSqlParameters.Add(new SqlParameter("@OrdenColumna", SqlDbType.Int) { Value = objModCatBandejaColumna.OrdenColumna });
 
                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.spdDentConfBandejaColumnaAgregar", lstSqlParameters.ToArray());
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

        public clsModErrorBase CambiarOrden(clsModCatBandejaColumna objModCatBandejaColumna)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            SqlParameter[] arrpar =
            {
                new SqlParameter("@IdDetConfBandejaColumna", SqlDbType.Int){Value = objModCatBandejaColumna.IdDetConfBandejaColumna}, 
                new SqlParameter("@OrdenColumna", SqlDbType.Int){Value = objModCatBandejaColumna.OrdenColumna}, 
            };

            try
            {
                SqlHelper.ExecuteNonQuery(this._StrConnection, CommandType.StoredProcedure,
                    "NzControles.spdDetConfBandejaColumnaCambiarOrden", arrpar);
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }
    }
}
