using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using ClbDatControles.Common;
using System.Collections.ObjectModel;
using ClbModControles.Api;

namespace ClbDatControles
{
    public class clsDatCatFormularios
    {
        /// <summary>
        /// Variable de conexión para la clase
        /// </summary>
        private string _StrConnection;
        /// <summary>
        /// Constructor que inicializa la cadena de conexión
        /// </summary>
        public clsDatCatFormularios()
        {
            _StrConnection = Common.DataBaseConnection.Instance.GetConnectionString();
        }
        #region Metodo para guardar formulario
        /// <summary>
        /// Guarda formularios en base de datos
        /// </summary>
        /// <param name="objModCatFormularios">Obtiene el modelo de formularios</param>
        /// <returns value="objModErrorBase">Devuelve error si existe alguno.</returns>
        public clsModErrorBase Agregar(clsModCatFormularios objModCatFormularios)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = objModCatFormularios.Nombre });
                lstSqlParameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = objModCatFormularios.Descripcion });
                lstSqlParameters.Add(new SqlParameter("@IdUsuario", SqlDbType.Int) { Value = objModCatFormularios.IdusuarioCreacion });
                lstSqlParameters.Add(new SqlParameter("@IdTipoFormulario", SqlDbType.Int) {Value = objModCatFormularios.IdTipoFormulario});
                lstSqlParameters.Add(new SqlParameter("@FormatoFecha", SqlDbType.VarChar) { Value = objModCatFormularios.FormatoFecha });

                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.spdCatFormularioGuardar", lstSqlParameters.ToArray());
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
        #endregion

        #region Metodo para Modificar Formulario
        /// <summary>
        /// Actualiza Formularios en base de datos
        /// </summary>
        /// <param name="objModCatFormularios">Modelo de Catálogo de Formulario</param>
        /// <returns>Devuelve error si exite alguno</returns>
        public clsModErrorBase Actualizar(clsModCatFormularios objModCatFormularios)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@IdFormulario", SqlDbType.Int) { Value = objModCatFormularios.IdFormulario });
                //lstSqlParameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = objModCatFormularios.Nombre });
                //lstSqlParameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar) { Value = objModCatFormularios.Descripcion });
                //lstSqlParameters.Add(new SqlParameter("@ActivoFormulario", SqlDbType.Bit) { Value = objModCatFormularios.ActivoFormulario });
                //lstSqlParameters.Add(new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int) { Value = objModCatFormularios.IdusuarioCreacionFormulario });
                //lstSqlParameters.Add(new SqlParameter("@IdUsuarioModificacion", SqlDbType.Int) { Value = objModCatFormularios.IdusuarioModificaciónFormulario });
                //lstSqlParameters.Add(new SqlParameter("@FechaCreacion", SqlDbType.DateTime) { Value = objModCatFormularios.FechaCreacionFormulario });
                //lstSqlParameters.Add(new SqlParameter("@FechaModificacion", SqlDbType.DateTime) { Value = objModCatFormularios.FechaModificacionFormulario });

                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.spdCatFormularioActualizarActivacion", lstSqlParameters.ToArray());
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
        #endregion

        #region Metodo para obtener formularios
        /// <summary>
        /// Lista de formularios registrados en BD
        /// </summary>
        /// <param name="objErrorBase">objeto de modelo error base</param>
        /// <returns>Regresa una lista de objetos ModCatFormulario</returns>
        public ICollection<clsModCatFormularios> CargarFomularios(out clsModErrorBase objErrorBase, string strTextoBusqueda, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatFormularios> lstCargarFormularios = new Collection<clsModCatFormularios>();

            SqlParameter[] arrParameters = new SqlParameter[3];
            arrParameters[0] = new SqlParameter("@TextoBusqueda", SqlDbType.VarChar)
            {
                Value = strTextoBusqueda
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
            clsModCatFormularios objModCatFormularios = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatFormularioCargarBusqueda", arrParameters);
                if (sqlDataReader != null)
                {

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            if(objModPaginacion.TotalRegistros == 0) {
                                objModPaginacion.Asignar(intRegistrosPorPagina, decimal.Parse(sqlDataReader["Registros"].ToString()));
                            }
                                objModCatFormularios = this.LlenarObjetoPaginacion(sqlDataReader);
                            lstCargarFormularios.Add(objModCatFormularios);

                            
                        }
                    }
                    
                   
                    
                   // lstCargarFormularios = this.GetCatFormulariosFromReader(sqlDataReader);
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
            return lstCargarFormularios;
        }
        #endregion

        #region Metodo para obtener formularios por paginación

        public ICollection<clsModCatFormularios> CargaPagFormularios(out clsModErrorBase objErrorBase, string strTextoBusqueda, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatFormularios> lstModCatFormularios = new Collection<clsModCatFormularios>();
            SqlParameter[] arrParameters = new SqlParameter[3];
            arrParameters[0] = new SqlParameter("@TextoBusqueda", SqlDbType.VarChar) { Value = strTextoBusqueda };
            arrParameters[1] = new SqlParameter("@Pagina", SqlDbType.Int) { Value = intPagina };
            arrParameters[2] = new SqlParameter("@RegistrosPorPagina", SqlDbType.Int) { Value = intRegistrosPorPagina };
            SqlDataReader sqlDataReader = null;
            clsModCatFormularios objModCatFormularios = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatFormularioPagCargar", arrParameters);
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
                                objModCatFormularios = this.LlenarObjetoPaginacion(sqlDataReader);
                                lstModCatFormularios.Add(objModCatFormularios);
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
            return lstModCatFormularios;
        }
        #endregion

        #region Metodo para obtener un formulario por ID
        public clsModCatFormularios CargarFormularioPorId(out clsModErrorBase objErrorBase, int intIdPrograma)
        {
            objErrorBase = new clsModErrorBase();
            clsModCatFormularios objModCatFormulario = null;
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@IdFormulario", SqlDbType.Int) { Value = intIdPrograma});
                SqlDataReader sqlDataReader = null;

                try
                {
                    sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatFormularioCargaPorId", lstSqlParameters.ToArray());
                    if (sqlDataReader != null)
                    {
                        while (sqlDataReader.Read())
                        {
                            objModCatFormulario = this.LlenarObjetoFormularioId(sqlDataReader);
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
        #endregion

        #region Metedo para obtener un objeto Modelo
        /// <summary>
        /// Genera una lista de objetos de modelo formularios
        /// </summary>
        /// <param name="sqlDataReader">Reader que contiene los datos de formularios</param>
        /// <returns>Lista de objetos ModCatFormularios</returns>
        private ICollection<clsModCatFormularios> GetCatFormulariosFromReader(SqlDataReader sqlDataReader)
        {
            ICollection<clsModCatFormularios> lstModFormularios = new Collection<clsModCatFormularios>();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    clsModCatFormularios objModFomularios = new clsModCatFormularios()
                    {
                        IdFormulario = (int)(sqlDataReader["IdFormulario"] != DBNull.Value ? sqlDataReader["IdFormulario"] : 0),
                        Nombre = (String)(sqlDataReader["Formulario"] != DBNull.Value ? sqlDataReader["Formulario"] : ""),
                        Descripcion = (String)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"] : ""),
                        Activo = (bool)(sqlDataReader["Activo"] != DBNull.Value ? sqlDataReader["Activo"] :bool.FalseString),
                    };
                    lstModFormularios.Add(objModFomularios);
                }
            }
            return lstModFormularios;
        }
        #endregion

        #region Metodo para llenar objeto paginación
        /// <summary>
        /// Lector SQL de lista de Formularios
        /// </summary>
        /// <param name="sqlDataReader">Lector SQL Stored Procecure</param>
        /// <returns>Modelo Formularios</returns>
        private clsModCatFormularios LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {
            clsModCatFormularios objModFomularios = new clsModCatFormularios()
            {
                IdFormulario = (int)(sqlDataReader["IdFormulario"] != DBNull.Value ? sqlDataReader["IdFormulario"] : 0),
                Nombre = (String)(sqlDataReader["Nombre"] != DBNull.Value ? sqlDataReader["Nombre"] : ""),
                UUID = (String)(sqlDataReader["UUID"] != DBNull.Value ? sqlDataReader["UUID"].ToString() : ""),
                Descripcion = (String)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"] : ""),
                Activo = (bool)(sqlDataReader["Activo"] != DBNull.Value ? sqlDataReader["Activo"] : bool.FalseString),
                IdTipoFormulario = (int)(sqlDataReader["IdTipoFormulario"] != DBNull.Value ? sqlDataReader["IdTipoFormulario"] : 0),
                FormatoFecha = (String)(sqlDataReader["FormatoFecha"] != DBNull.Value ? sqlDataReader["FormatoFecha"] : "")
            };
            return objModFomularios;
        }
        #endregion

        #region Metodo para lleganr objeto por ID
        private clsModCatFormularios LlenarObjetoFormularioId(SqlDataReader sqlDataReader)
        {
            clsModCatFormularios objModCatFormularios = new clsModCatFormularios()
            {
                IdFormulario = (int)(sqlDataReader["IdFormulario"] != DBNull.Value ? sqlDataReader["IdFormulario"]: 0),
                Nombre = (string)(sqlDataReader["Nombre"] != DBNull.Value ? sqlDataReader["Nombre"]: 0),
                Descripcion = (string)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"]: 0),
                UUID = (string)(sqlDataReader["UUID"] != DBNull.Value ? sqlDataReader["UUID"] : 0),
                IdTipoFormulario =  (int)(sqlDataReader["IdTipoFormulario"] != DBNull.Value ? sqlDataReader["IdTipoFormulario"]:0)
            };
            return objModCatFormularios;
        }


        #endregion

        /// <summary>
        /// Devuelve el listado de controles pertenecientes al formulario
        /// </summary>
        /// <param name="strUUID">UUID para identificar el formulario</param>
        /// <param name="objModErrorBase">Objeto de errror de salida</param>
        /// <returns>Regresa una collecion de controles<returns>
        public clsModApiFormulario CargarArbol(int intIdFormulario, out clsModErrorBase objModErrorBase)
        {
            clsModApiFormulario objModApiFormulario = null;

            ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            lstSqlParameters.Add(new SqlParameter("@IdFormulario", SqlDbType.Int) { Value = intIdFormulario });
            objModErrorBase = new clsModErrorBase();
            clsModApiControl objModApiControl = null;
            clsModApiSeccion objModApiSeccion = null;
            SqlDataReader sqlLeer = null;
            int intIdSeccion = 0;
            try
            {
                sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdFormularioCargarArbol", lstSqlParameters.ToArray());

                if (sqlLeer.HasRows)
                {

                    while (sqlLeer.Read())
                    {
                        //Se crea la lista de secciones
                        if (objModApiFormulario == null)
                        {
                            objModApiFormulario = new clsModApiFormulario();
                            objModApiFormulario.Nombre = sqlLeer["NombreFormulario"] != DBNull.Value ? sqlLeer["NombreFormulario"].ToString() : string.Empty;
                            objModApiFormulario.LstModApiSeccion = new Collection<clsModApiSeccion>();

                        }
                        //Si son secciones diferentes
                        if ((sqlLeer["IdSeccion"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccion"].ToString()) : 0) != intIdSeccion)
                        {

                            objModApiSeccion = new clsModApiSeccion()
                            {
                                IdSeccion = (sqlLeer["IdSeccion"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccion"].ToString()) : 0),
                                IdGrupo = (sqlLeer["IdGrupo"] != DBNull.Value ? int.Parse(sqlLeer["IdGrupo"].ToString()) : 0),
                                Nombre = sqlLeer["NombreSeccion"] != DBNull.Value ? sqlLeer["NombreSeccion"].ToString() : string.Empty,
                                Orden = (sqlLeer["OrdenSeccion"] != DBNull.Value ? int.Parse(sqlLeer["OrdenSeccion"].ToString()) : 0),
                                Columnas = (sqlLeer["ColumnasSeccion"] != DBNull.Value ? int.Parse(sqlLeer["ColumnasSeccion"].ToString()) : 0),
                                IdTipoSeccion = (sqlLeer["IdTipoSeccion"] != DBNull.Value ? int.Parse(sqlLeer["IdTipoSeccion"].ToString()) : 0)
                            };
                            objModApiSeccion.LstModApiControl = new Collection<clsModApiControl>();
                            objModApiFormulario.LstModApiSeccion.Add(objModApiSeccion);
                            intIdSeccion = (sqlLeer["IdSeccion"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccion"].ToString()) : 0);
                        }


                        if (sqlLeer["IdSeccionControl"] !=DBNull.Value)
                        {
                            objModApiControl = new clsModApiControl()
                            {
                                IdSeccionControl = (sqlLeer["IdSeccionControl"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccionControl"].ToString()) : 0),
                                Nombre = sqlLeer["NombreControl"] != DBNull.Value ? sqlLeer["NombreControl"].ToString() : string.Empty,
                                IdTipoControl = (sqlLeer["IdTipoControl"] != DBNull.Value ? int.Parse(sqlLeer["IdTipoControl"].ToString()) : 0),
                                Orden = (sqlLeer["OrdenControl"] != DBNull.Value ? int.Parse(sqlLeer["OrdenControl"].ToString()) : 0),
                                ClaseArbol = sqlLeer["ClaseArbol"] != DBNull.Value ? sqlLeer["ClaseArbol"].ToString() : string.Empty,
                            };

                            objModApiSeccion.LstModApiControl.Add(objModApiControl);
                        }


                    }
                }

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            finally
            {
                if (sqlLeer != null)
                {
                    sqlLeer.Close();
                }
            }
            return objModApiFormulario;
        }
      
        
        public string CargarUUIDXId(out clsModErrorBase objErrorBase, int varId)
        {
            objErrorBase = new clsModErrorBase();
            string strUUID = "";
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@IdFormulario", SqlDbType.Int) { Value = varId });


                try
                {
                    object objUUID = SqlHelper.ExecuteScalar(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatFormularioCargarUUID", lstSqlParameters.ToArray());
                    if (objUUID != null)
                    {
                        strUUID = objUUID.ToString();
                    }
                }
                catch (Exception exception)
                {
                    objErrorBase.MsgError = exception.Message;
                }
            }
            catch (Exception exception)
            {
                objErrorBase.MsgError = exception.Message;
            }
            return strUUID;
        }


        public int CargarIdPorIdSeccion(out clsModErrorBase objErrorBase, int intIdSeccion)
        {
            objErrorBase = new clsModErrorBase();
            int intIdFormulario = 0;
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@IdSeccion", SqlDbType.Int) { Value = intIdSeccion});


                try
                {
                    object intId = SqlHelper.ExecuteScalar(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatFormularioCargarXIdSeccion", lstSqlParameters.ToArray());
                    if (intId != null)
                    {
                        intIdFormulario = int.Parse(intId.ToString());
                    }
                }
                catch (Exception exception)
                {
                    objErrorBase.MsgError = exception.Message;
                }
            }
            catch (Exception exception)
            {
                objErrorBase.MsgError = exception.Message;
            }
            return intIdFormulario;
        }

        public clsModCatFormularios getIdTipoFormulario(out clsModErrorBase objModErrorBase, int id)
        {
            clsModCatFormularios objModCatFormularios = new clsModCatFormularios();
            objModErrorBase = new clsModErrorBase();
            SqlParameter[] arrpar =
            {
                new SqlParameter("@IdFormulario", SqlDbType.Int) {Value = id},
            };
            SqlDataReader dr = null;
            try
            {
                 dr = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure,
                    "NzControles.SpdCatFormularioCargarXId",arrpar);
                if (dr != null)
                {
                    while (dr.Read())
                    {

                        objModCatFormularios.IdFormulario = int.Parse(dr["IdFormulario"].ToString());
                        objModCatFormularios.Nombre = dr["Nombre"].ToString();
                        objModCatFormularios.Descripcion = dr["Descripcion"].ToString();
                        objModCatFormularios.Activo = bool.Parse(dr["Activo"].ToString());
                        objModCatFormularios.IdTipoFormulario = int.Parse(dr["IdTipoFormulario"].ToString());
                        objModCatFormularios.FormatoFecha = dr["FormatoFecha"].ToString();
                        objModCatFormularios.IdusuarioCreacion = int.Parse(dr["IdUsuarioCreacion"].ToString());
                        objModCatFormularios.IdusuarioModificacion= int.Parse(dr["IdUsuarioModificacion"].ToString());
                        objModCatFormularios.FechaCreacion = DateTime.Parse(dr["FechaCreacion"].ToString());
                        objModCatFormularios.FechaModificacion = DateTime.Parse(dr["FechaModificacion"].ToString());
                        objModCatFormularios.UUID = dr["UUID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModCatFormularios;
        }

        public List<clsModCatSecciones> CargarSeccionesPorFormulario(int IdForm, out clsModErrorBase objModErrorBase)
        {
            List<clsModCatSecciones> lstModCatSeccioneses = new List<clsModCatSecciones>();
            objModErrorBase = new clsModErrorBase();


            SqlParameter[] arrpar =
            {
                new SqlParameter("@IdFormulario",SqlDbType.Int){Value = IdForm}, 
            };

            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure,
                    "NzControles.SpdSeccionesDeFormulario", arrpar);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clsModCatSecciones objModCatSecciones = new clsModCatSecciones();
                        objModCatSecciones.IdSeccion = int.Parse(dr["IdSeccion"].ToString());
                        objModCatSecciones.Tabla = dr["Tabla"].ToString();
                        objModCatSecciones.IdFormulario = int.Parse(dr["IdFormulario"].ToString());
                        objModCatSecciones.primaryKeyName = dr["primaryKeyName"].ToString();
                        objModCatSecciones.Nombre = dr["Nombre"].ToString();
                        objModCatSecciones.IdTipoFormulario = int.Parse(dr["IdTipoFormulario"].ToString());

                        lstModCatSeccioneses.Add(objModCatSecciones);
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstModCatSeccioneses;
        }
    }
}
