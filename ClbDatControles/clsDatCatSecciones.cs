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

namespace ClbDatControles
{
    public class clsDatCatSecciones
    {
        /// <summary>
        /// Variable de conexión para la clase
        /// </summary>
        private string _StrConnection;
        /// <summary>
        /// Constructor que inicializa la cadena de conexión
        /// </summary>
        public clsDatCatSecciones()
        {
            _StrConnection = Common.DataBaseConnection.Instance.GetConnectionString();
        }

        #region Metodo para guardar Sección
        /// <summary>
        /// Guarda secciones en base de datos
        /// </summary>
        /// <param name="objModCatSecciones">Obtiene el modelo de secciones</param>
        /// <returns>Devuelve error si existe alguno.</returns>
        public clsModErrorBase GuardarSeccion(clsModCatSecciones objModCatSecciones)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                //Parametros de tabla CatSeccion
                lstSqlParameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar){Value = objModCatSecciones.Nombre});
                lstSqlParameters.Add(new SqlParameter("@Icono", SqlDbType.Int) { Value = (objModCatSecciones.IdSeccionIcono == null ? 0 : objModCatSecciones.IdSeccionIcono) });
                lstSqlParameters.Add(new SqlParameter("@IdFormulario", SqlDbType.Int) { Value = objModCatSecciones.IdFormulario});
                lstSqlParameters.Add(new SqlParameter("@IdTipoSeccion", SqlDbType.Int) { Value = objModCatSecciones.IdTipoSeccion});
                lstSqlParameters.Add(new SqlParameter("@IdGrupo", SqlDbType.VarChar) { Value = (objModCatSecciones.IdGrupo == 0) ? null : objModCatSecciones.IdGrupo.ToString() });
                lstSqlParameters.Add(new SqlParameter("@Orden", SqlDbType.Int) { Value = objModCatSecciones.Orden });
                lstSqlParameters.Add(new SqlParameter("@Columnas", SqlDbType.Int) { Value = objModCatSecciones.Columnas });
                lstSqlParameters.Add(new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int) { Value = objModCatSecciones.IdUsuarioCreacion });
                lstSqlParameters.Add(new SqlParameter("@IdUsuarioModificacion", SqlDbType.Int) { Value = objModCatSecciones.IdUsuarioModificacion });
                lstSqlParameters.Add(new SqlParameter("@Tabla", SqlDbType.VarChar) { Value = objModCatSecciones.Tabla });
                lstSqlParameters.Add(new SqlParameter("@PrimaryKeyName", SqlDbType.VarChar) { Value = objModCatSecciones.primaryKeyName });

                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.spdCatSeccionAgregar", lstSqlParameters.ToArray());
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


        #region Metodo para Guardar Grupos Nuevos de Formulario

        public clsModErrorBase AgregarGrupo(clsModCatSecciones objModSecciones)
        {
            clsModErrorBase objModErrorBase = null;
            SqlParameter[] arrpar = new SqlParameter[2];
            arrpar[0] = new SqlParameter("@IdFormulario", SqlDbType.Int);
            arrpar[0].Value = objModSecciones.IdFormulario;
            arrpar[1] = new SqlParameter("@Grupo", SqlDbType.VarChar);
            arrpar[1].Value = objModSecciones.Grupo;

            try
            {
                object objId = SqlHelper.ExecuteScalar(_StrConnection, CommandType.StoredProcedure, "NzControles.spAgregarGrupo", arrpar);
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



        #region Metodo para Cargar Grupos de Formulario

      

        private ICollection<clsModCatSecciones> ObtenerListaGruposReader(SqlDataReader sqlDataReader)
        {
            ICollection<clsModCatSecciones> lstModCatsecciones = new Collection<clsModCatSecciones>();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    clsModCatSecciones objModCatGrupo = new clsModCatSecciones()
                    {

                        IdGrupo = (int)(sqlDataReader["IdGrupo"] != DBNull.Value ? sqlDataReader["IdGrupo"] : 0),
                        Grupo = (sqlDataReader["Grupo"] != DBNull.Value ? sqlDataReader["Grupo"].ToString() : "")
                    };
                    lstModCatsecciones.Add(objModCatGrupo);
                }
            }
            return lstModCatsecciones;
        }


        #endregion

        #region Metodo para Modificar Formulario
        /// <summary>
        /// Actualiza Formularios en base de datos
        /// </summary>
        /// <param name="objModCatFormularios">Modelo de Catálogo de Formulario</param>
        /// <returns>Devuelve error si exite alguno</returns>
        public clsModErrorBase ActualizarSeccion(clsModCatSecciones objModCatSecciones)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@IdSeccion", SqlDbType.Int) { Value = objModCatSecciones.IdSeccion });
                lstSqlParameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = objModCatSecciones.Nombre });
                lstSqlParameters.Add(new SqlParameter("@Icono", SqlDbType.Int) { Value = (objModCatSecciones.IdSeccionIcono == null ? 0 : objModCatSecciones.IdSeccionIcono) });
                lstSqlParameters.Add(new SqlParameter("@IdTipoSeccion", SqlDbType.Int) { Value = objModCatSecciones.IdTipoSeccion });
                lstSqlParameters.Add(new SqlParameter("@IdGrupo", SqlDbType.Int) { Value = (objModCatSecciones.IdGrupo == 0) ? null : objModCatSecciones.IdGrupo.ToString() });
                lstSqlParameters.Add(new SqlParameter("@Columnas", SqlDbType.Int) { Value = objModCatSecciones.Columnas });
                lstSqlParameters.Add(new SqlParameter("@IdUsuarioModificacion", SqlDbType.Int) { Value = objModCatSecciones.IdUsuarioModificacion});
                lstSqlParameters.Add(new SqlParameter("@Tabla", SqlDbType.VarChar) {Value = objModCatSecciones.Tabla});
                lstSqlParameters.Add(new SqlParameter("@PrimaryKeyName", SqlDbType.VarChar) { Value = objModCatSecciones.primaryKeyName });

                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.spdCatSeccionActualizar", lstSqlParameters.ToArray());
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

        #region Metodo para obtener secciones por paginación
        /// <summary>
        /// Metodo para obtener lista de datos mediante paginación
        /// </summary>
        /// <param name="objErrorBase">Error en dado caso que se tenga</param>
        /// <param name="strTextoBusqueda">String de busqueda</param>
        /// <param name="objModPaginacion">objeto modelo de paginación</param>
        /// <param name="intPagina">Pagina con la que se iniciara</param>
        /// <param name="intRegistrosPorPagina">Registros que se mostrará por pagina</param>
        /// <returns></returns>
        public ICollection<clsModCatSecciones> CargarPagSecciones(out clsModErrorBase objErrorBase, string strTextoBusqueda, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatSecciones> lstModCatSecciones = new Collection<clsModCatSecciones>();
            SqlParameter[] arrParameters = new SqlParameter[3];
            arrParameters[0] = new SqlParameter("@TextoBusqueda", SqlDbType.VarChar) { Value = strTextoBusqueda };
            arrParameters[1] = new SqlParameter("@Pagina", SqlDbType.Int) { Value = intPagina };
            arrParameters[2] = new SqlParameter("@RegistrosPorPagina", SqlDbType.Int) { Value = intRegistrosPorPagina };
            SqlDataReader sqlDataReader = null;
            clsModCatSecciones objModCatSeccion = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "SpdCatSeccionPagCargar", arrParameters);
                if (sqlDataReader != null)
                {
                    //Paginación
                    try
                    {
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                if (objModPaginacion.TotalRegistros == 0)
                                {
                                    objModPaginacion.Asignar(intRegistrosPorPagina, decimal.Parse(sqlDataReader["Registros"].ToString()));
                                }
                                objModCatSeccion = this.LlenarObjetoPaginacion(sqlDataReader);
                                lstModCatSecciones.Add(objModCatSeccion);
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
            return lstModCatSecciones;
        }


        #endregion

        #region Metodo para obtener Seccion por ID
        public clsModCatSecciones CargarSeccionPorId(out clsModErrorBase objErrorBase, int intIdPrograma)
        {
            objErrorBase = new clsModErrorBase();
            clsModCatSecciones objModCatSeccion = null;
            try
            {
                ICollection<SqlParameter> lstParameters = new List<SqlParameter>();
                lstParameters.Add(new SqlParameter("@IdSeccion", SqlDbType.Int) { Value = intIdPrograma });
                SqlDataReader sqlDataReader = null;

                try
                {
                    sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatSeccionCargaPorId", lstParameters.ToArray());
                    if (sqlDataReader != null)
                    {
                        while (sqlDataReader.Read())
                        {
                            objModCatSeccion = this.LlenarObjetoSeccionId(sqlDataReader);
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
            return objModCatSeccion;
        }

        #endregion


        #region Metodo para llenar objeto paginación

        private clsModCatSecciones LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {
            clsModCatSecciones objModSecciones = new clsModCatSecciones()
            {
                IdSeccion = (int)(sqlDataReader["IdSeccion"] != DBNull.Value ? sqlDataReader["IdSeccion"]:0),
                Nombre = (string)(sqlDataReader["Nombre"] != DBNull.Value ? sqlDataReader["Nombre"]: ""),
                Icono = (string)(sqlDataReader["Icono"] != DBNull.Value ? sqlDataReader["Icono"]: ""),
                Orden = (int)(sqlDataReader["Orden"] != DBNull.Value ? sqlDataReader["Orden"]: 0)
                //TODO: Joel Hernandez, Agregar los demas campos que faltan, estos datos son de prueba para verificar funcionamiento
            };
            return objModSecciones;
        }

        #endregion

        #region Metodo para llenar objeto por ID
        private clsModCatSecciones LlenarObjetoSeccionId(SqlDataReader sqlDataReader)
        {
            clsModCatSecciones objModCatSeccion = new clsModCatSecciones()
            {
                IdSeccion = (int)(sqlDataReader["IdSeccion"] != DBNull.Value ? sqlDataReader["IdSeccion"]:0),
                Nombre = (string)(sqlDataReader["Nombre"] != DBNull.Value ? sqlDataReader["Nombre"]:""),
                IdTipoSeccion = (int)(sqlDataReader["IdTipoSeccion"] != DBNull.Value ? sqlDataReader["IdTipoSeccion"]:0),
                IdGrupo = (int)(sqlDataReader["IdGrupo"] != DBNull.Value ? sqlDataReader["IdGrupo"]:0),
                Columnas = (int)(sqlDataReader["Columnas"] != DBNull.Value ? sqlDataReader["Columnas"] : 3),
                Tabla = (string)(sqlDataReader["Tabla"] != DBNull.Value ? sqlDataReader["Tabla"] : ""),
                primaryKeyName = (string)(sqlDataReader["PrimaryKeyName"] != DBNull.Value ? sqlDataReader["PrimaryKeyName"] : ""),
                IdSeccionIcono = (int)(sqlDataReader["IdSeccionIcono"] != DBNull.Value ? sqlDataReader["IdSeccionIcono"] : 0)
            };
            return objModCatSeccion;
        }


        #endregion



        public clsModErrorBase ActualizarOrden(int intIdSeccion, int intOrden)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                //Parametros de tabla CatSeccion
                lstSqlParameters.Add(new SqlParameter("@IdSeccion", SqlDbType.Int) { Value = intIdSeccion });
                lstSqlParameters.Add(new SqlParameter("@Orden", SqlDbType.Int) { Value = intOrden});

                SqlHelper.ExecuteNonQuery(this._StrConnection, CommandType.StoredProcedure, "NzControles.spdCatSeccionActualizarOrden", lstSqlParameters.ToArray());
            }
            catch (Exception exception)
            {
                objModErrorBase.MsgError = exception.Message;
            }
            return objModErrorBase;
        }


    }
}
