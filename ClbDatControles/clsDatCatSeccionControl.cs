using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using ClbDatControles.Common;
using ClbModControles;
using System.Collections.ObjectModel;

namespace ClbDatControles
{
    public class clsDatCatSeccionControl
    {

        private string _StrConnection;

        public clsDatCatSeccionControl()
        {
            this._StrConnection = DataBaseConnection.Instance.GetConnectionString();
        }

        /// <summary>
        /// Agrega un nuevo elemento a la tabla de seccionControl
        /// </summary>
        /// <param name="objModCatSeccionControl">Objeto a guardar</param>
        /// <returns>Regresa el objeto de error conteniendo el id guardado o mensaje de errror generado</returns>
        public clsModErrorBase Agregar(clsModCatSeccionControl objModCatSeccionControl) {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            lstSqlParameters.Add(new SqlParameter("@IdSeccion", SqlDbType.Int) { Value = objModCatSeccionControl.IdSeccion });
            lstSqlParameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = objModCatSeccionControl.Nombre });
            lstSqlParameters.Add(new SqlParameter("@NombreColumna", SqlDbType.VarChar) { Value = objModCatSeccionControl.NombreColumna });
            lstSqlParameters.Add(new SqlParameter("@IdTipoControl", SqlDbType.Int) { Value = objModCatSeccionControl.IdTipoControl});
            lstSqlParameters.Add(new SqlParameter("@Longitud", SqlDbType.Int) { Value = objModCatSeccionControl.Longitud });
            lstSqlParameters.Add(new SqlParameter("@Formato", SqlDbType.VarChar) { Value = objModCatSeccionControl.Formato ?? "" });
            lstSqlParameters.Add(new SqlParameter("@Requerido", SqlDbType.Bit) { Value = objModCatSeccionControl.Requerido });

            lstSqlParameters.Add(new SqlParameter("@ValorDefault", SqlDbType.VarChar) { Value = objModCatSeccionControl.ValorDefault ?? "" });
            lstSqlParameters.Add(new SqlParameter("@Caption", SqlDbType.VarChar) { Value = objModCatSeccionControl.Caption ?? "" });
            lstSqlParameters.Add(new SqlParameter("@HelpText", SqlDbType.VarChar) { Value = objModCatSeccionControl.HelpText ?? "" });
            lstSqlParameters.Add(new SqlParameter("@TextoSeleccionado", SqlDbType.VarChar) { Value = objModCatSeccionControl.TextoSeleccionado ?? "" });
            lstSqlParameters.Add(new SqlParameter("@TextoNoSeleccionado", SqlDbType.VarChar) { Value = objModCatSeccionControl.TextoNoSeleccionado ?? "" });
            lstSqlParameters.Add(new SqlParameter("@TextAreaHeight", SqlDbType.Int) { Value = objModCatSeccionControl.TextAreaHeight });

            lstSqlParameters.Add(new SqlParameter("@Orden", SqlDbType.Int) { Value = objModCatSeccionControl.Orden });
            lstSqlParameters.Add(new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int) { Value = objModCatSeccionControl.IdUsuarioCreacion });
            lstSqlParameters.Add(new SqlParameter("@IdSeccionControlPadre", SqlDbType.Int) { Value = objModCatSeccionControl.IdSeccionControlPadre });
            try {

                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatSeccionControlAgregar", lstSqlParameters.ToArray());
                if (objId != null)
                {
                    int intIdentity;
                    int.TryParse(objId.ToString(), out intIdentity);
                    objModErrorBase.Id = intIdentity;
                }
            
            }
            catch(Exception ex) {
                objModErrorBase = new clsModErrorBase();
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }


        /// <summary>
        /// Actualiza el registro enviado
        /// </summary>
        /// <param name="objModCatSeccionControl">Objeto a actualizr</param>
        /// <returns>regresa objeto de modelo error base</returns>
        public clsModErrorBase Actualizar(clsModCatSeccionControl objModCatSeccionControl)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            lstSqlParameters.Add(new SqlParameter("@IdSeccion", SqlDbType.Int) { Value = objModCatSeccionControl.IdSeccion });
            lstSqlParameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = objModCatSeccionControl.Nombre });
            lstSqlParameters.Add(new SqlParameter("@NombreColumna", SqlDbType.VarChar) { Value = objModCatSeccionControl.NombreColumna });
            lstSqlParameters.Add(new SqlParameter("@IdTipoControl", SqlDbType.Int) { Value = objModCatSeccionControl.IdTipoControl });
            lstSqlParameters.Add(new SqlParameter("@Longitud", SqlDbType.Int) { Value = objModCatSeccionControl.Longitud });
            lstSqlParameters.Add(new SqlParameter("@Formato", SqlDbType.VarChar) { Value = objModCatSeccionControl.Formato });
            lstSqlParameters.Add(new SqlParameter("@Requerido", SqlDbType.Bit) { Value = objModCatSeccionControl.Requerido });
            lstSqlParameters.Add(new SqlParameter("@IdSeccionControlPadre", SqlDbType.Int) { Value = objModCatSeccionControl.IdSeccionControlPadre });
            
            //lstSqlParameters.Add(new SqlParameter("@ValorDefault", SqlDbType.VarChar) { Value = (objModCatSeccionControl.ValorDefault == null) ? "" : objModCatSeccionControl.ValorDefault });
            //lstSqlParameters.Add(new SqlParameter("@Caption", SqlDbType.VarChar) { Value = (objModCatSeccionControl.Caption == null) ? "" : objModCatSeccionControl.Caption });
            //lstSqlParameters.Add(new SqlParameter("@HelpText", SqlDbType.VarChar) { Value = (objModCatSeccionControl.HelpText == null) ? "" : objModCatSeccionControl.HelpText });
            //lstSqlParameters.Add(new SqlParameter("@TextoSeleccionado", SqlDbType.VarChar) { Value = (objModCatSeccionControl.TextoSeleccionado == null) ? "" : objModCatSeccionControl.TextoSeleccionado });
            //lstSqlParameters.Add(new SqlParameter("@TextoNoSeleccionado", SqlDbType.VarChar) { Value = (objModCatSeccionControl.TextoNoSeleccionado == null) ? "" : objModCatSeccionControl.TextoNoSeleccionado });
            //lstSqlParameters.Add(new SqlParameter("@TextAreaHeight", SqlDbType.Int) { Value = objModCatSeccionControl.TextAreaHeight });


            lstSqlParameters.Add(new SqlParameter("@Orden", SqlDbType.Int) { Value = objModCatSeccionControl.Orden });
            lstSqlParameters.Add(new SqlParameter("@IdUsuarioModificacion", SqlDbType.Int) { Value = objModCatSeccionControl.IdUsuarioModificacion });
            lstSqlParameters.Add(new SqlParameter("@IdSeccionControl", SqlDbType.Int) { Value = objModCatSeccionControl.IdSeccionControl });
            try
            {
                SqlHelper.ExecuteNonQuery(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatSeccionControlActualizar", lstSqlParameters.ToArray());
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }


        /// <summary>
        /// Actualiza el registro enviado
        /// </summary>
        /// <param name="objModCatSeccionControl">Objeto a actualizr</param>
        /// <returns>regresa objeto de modelo error base</returns>
        public clsModErrorBase Eliminar(int intIdSeccion)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            lstSqlParameters.Add(new SqlParameter("@IdSeccion", SqlDbType.Int) { Value = intIdSeccion });
            try
            {
                SqlHelper.ExecuteNonQuery(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatSeccionControlEliminar", lstSqlParameters.ToArray());
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }

        /// <summary>
        /// Carga todos los controles pertenecientes a la seccion
        /// </summary>
        /// <param name="intIdSeccion">id De la seccion a cargar</param>
        /// <param name="objModErrorBase">Objeto de error basse</param>
        /// <returns>devuelve el listado de controles pertenecientes a una seccion</returns>
        public ICollection<clsModCatSeccionControl> Cargar(int intIdSeccion, out clsModErrorBase objModErrorBase) {
        
            ICollection<clsModCatSeccionControl> lstSeccionControl = new Collection<clsModCatSeccionControl>();
            ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            clsModCatSeccionControl objModCatSeccionControl = null;
            lstSqlParameters.Add(new SqlParameter("@IdSeccion", SqlDbType.Int) { Value = intIdSeccion });
            objModErrorBase = new clsModErrorBase();
            SqlDataReader sqlLeer  = null;
            try
            {
                sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatSeccionControlCargar", lstSqlParameters.ToArray());

                if (sqlLeer.HasRows)
                {
                    while (sqlLeer.Read())
                    {
                        objModCatSeccionControl = this.GetSeccionControl(sqlLeer);
                        lstSeccionControl.Add(objModCatSeccionControl);

                    }
                }

            }
            catch(Exception ex) {
                objModErrorBase.MsgError = ex.Message;
            }
            finally
            {
                if (sqlLeer != null)
                {
                    sqlLeer.Close();
                }
            }
            return lstSeccionControl;
        }


        private clsModCatSeccionControl GetSeccionControl(SqlDataReader sqlLeer) {

            clsModCatSeccionControl objModCatSeccionControl = null;

            objModCatSeccionControl = new clsModCatSeccionControl()
            {
                IdSeccionControl = (sqlLeer["IdSeccionControl"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccionControl"].ToString()) : 0),
                IdSeccionControlPadre = (sqlLeer["IdSeccionControlPadre"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccionControlPadre"].ToString()) : 0),
                IdSeccion = (sqlLeer["IdSeccion"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccion"].ToString()) : 0),
                Nombre = sqlLeer["Nombre"] != DBNull.Value ? sqlLeer["Nombre"].ToString() : string.Empty,
                NombreColumna = sqlLeer["NombreColumna"] != DBNull.Value ? sqlLeer["NombreColumna"].ToString() : string.Empty,
                IdTipoControl = (sqlLeer["IdTipoControl"] != DBNull.Value ? int.Parse(sqlLeer["IdTipoControl"].ToString()) : 0),
                Longitud = (sqlLeer["Longitud"] != DBNull.Value ? int.Parse(sqlLeer["Longitud"].ToString()) : 0),
                Formato = sqlLeer["Formato"] != DBNull.Value ? sqlLeer["Formato"].ToString() : string.Empty,
                TipoControl = sqlLeer["TipoControl"] != DBNull.Value ? sqlLeer["TipoControl"].ToString() : string.Empty,
                Orden = (sqlLeer["Orden"] != DBNull.Value ? int.Parse(sqlLeer["Orden"].ToString()) : 0),
                Requerido = (sqlLeer["Requerido"] != DBNull.Value ? bool.Parse(sqlLeer["Requerido"].ToString()) : false),
                ValorDefault = sqlLeer["ValorDefault"] != DBNull.Value ? sqlLeer["ValorDefault"].ToString() : string.Empty,
                Caption = sqlLeer["Caption"] != DBNull.Value ? sqlLeer["Caption"].ToString() : string.Empty,
                HelpText = sqlLeer["HelpText"] != DBNull.Value ? sqlLeer["HelpText"].ToString() : string.Empty,
                TextoSeleccionado = sqlLeer["TextoSeleccionado"] != DBNull.Value ? sqlLeer["TextoSeleccionado"].ToString() : string.Empty,
                TextoNoSeleccionado = sqlLeer["TextoNoSeleccionado"] != DBNull.Value ? sqlLeer["TextoNoSeleccionado"].ToString() : string.Empty,
                TextAreaHeight = (sqlLeer["TextAreaHeight"] != DBNull.Value ? int.Parse(sqlLeer["TextAreaHeight"].ToString()) : 0),
                DataSource = (sqlLeer["DataSource"] != DBNull.Value ? bool.Parse(sqlLeer["DataSource"].ToString()) : false),
            };
            
            
            return objModCatSeccionControl;
        }

        public List<clsModControlesGenerados> GenerarControles(string tabla, string schema,
            out clsModErrorBase objModErrorBase)
        {
            clsModControlesGenerados objClsModControlesGenerados = new clsModControlesGenerados();
            objModErrorBase = new clsModErrorBase();

            List<clsModControlesGenerados> lstClsModControlesGeneradoses = new List<clsModControlesGenerados>();

            SqlParameter[] arrpar = 
            {
                new SqlParameter("@TableName", SqlDbType.VarChar){Value = tabla},
                new SqlParameter("@TableSchema", SqlDbType.VarChar){Value = schema}
            };

            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure,
                    "NzControles.spdGetControlesData",arrpar);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clsModControlesGenerados objModControlesGenerados = new clsModControlesGenerados();
                        objModControlesGenerados.Nombre = dr["Nombre"].ToString();
                        if (dr["Longitud"].ToString().Equals(""))
                        {
                            objModControlesGenerados.Longitud = 0;
                        }
                        else
                        {
                            objModControlesGenerados.Longitud = int.Parse(dr["Longitud"].ToString());
                        }

                        if (dr["Null"].ToString().Equals("YES"))
                        {
                            objModControlesGenerados.Null = true;
                        }
                        else
                        {
                            objModControlesGenerados.Null = false;
                        }
                        objModControlesGenerados.Tipo = dr["Tipo"].ToString();

                        objModControlesGenerados.TablaRelacion = dr["TablaRelacion"].ToString();

                        objModControlesGenerados.SchemaTablaRelacion = dr["SchemaTablaRelacion"].ToString();

                        lstClsModControlesGeneradoses.Add(objModControlesGenerados);
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstClsModControlesGeneradoses;
        }

    }
}
