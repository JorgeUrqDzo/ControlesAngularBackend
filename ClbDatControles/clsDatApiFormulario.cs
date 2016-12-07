using ClbDatControles.Common;
using ClbModControles;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;
using ClbModControles.Api;
using System.Collections.ObjectModel;

namespace ClbDatControles
{
    public class clsDatApiFormulario
    {
        private string _StrConnection;

        public clsDatApiFormulario()
        {
            this._StrConnection = DataBaseConnection.Instance.GetConnectionString();
        }


        /// <summary>
        /// Devuelve el listado de controles pertenecientes al formulario
        /// </summary>
        /// <param name="strUUID">UUID para identificar el formulario</param>
        /// <param name="objModErrorBase">Objeto de errror de salida</param>
        /// <returns>Regresa una collecion de controles<returns>
        public clsModApiFormulario Cargar(string strUUID, out clsModErrorBase objModErrorBase)
        {
            clsModApiFormulario objModApiFormulario = null;

            ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            lstSqlParameters.Add(new SqlParameter("@UUID", SqlDbType.VarChar) { Value = strUUID});
            objModErrorBase = new clsModErrorBase();
            clsModApiControl objModApiControl = null;
            clsModApiSeccion objModApiSeccion = null;
            SqlDataReader sqlLeer = null;
            int intIdSeccion = 0;
            try
            {
                sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdApiFormularioCargar", lstSqlParameters.ToArray());

                if (sqlLeer.HasRows)
                {
                    
                    while (sqlLeer.Read())
                    {
                        //Se crea la lista de secciones
                        if (objModApiFormulario == null)
                        {
                            objModApiFormulario = new clsModApiFormulario();
                            objModApiFormulario.Nombre = sqlLeer["NombreFormulario"] != DBNull.Value ? sqlLeer["NombreFormulario"].ToString() : string.Empty;
                            objModApiFormulario.FormatoFecha = sqlLeer["FormatoFecha"] != DBNull.Value ? sqlLeer["FormatoFecha"].ToString() : string.Empty;
                            objModApiFormulario.LstModApiSeccion = new Collection<clsModApiSeccion>();

                        }
                        //Si son secciones diferentes
                        if ((sqlLeer["IdSeccion"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccion"].ToString()) : 0) != intIdSeccion) {

                            objModApiSeccion = new clsModApiSeccion()
                            {
                                IdSeccion = (sqlLeer["IdSeccion"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccion"].ToString()) : 0),
                                IdGrupo = (sqlLeer["IdGrupo"] != DBNull.Value ? int.Parse(sqlLeer["IdGrupo"].ToString()) : 0),
                                Nombre = sqlLeer["NombreSeccion"] != DBNull.Value ? sqlLeer["NombreSeccion"].ToString() : string.Empty,
                                Orden = (sqlLeer["OrdenSeccion"] != DBNull.Value ? int.Parse(sqlLeer["OrdenSeccion"].ToString()) : 0),
                                Columnas = (sqlLeer["Columnas"] != DBNull.Value ? int.Parse(sqlLeer["Columnas"].ToString()) : 0),
                                Icono = sqlLeer["Icono"] != DBNull.Value ? sqlLeer["Icono"].ToString() : string.Empty,
                                IdTipoSeccion = (sqlLeer["IdTipoSeccion"] != DBNull.Value ? int.Parse(sqlLeer["IdTipoSeccion"].ToString()) : 0)
                            };
                            objModApiSeccion.LstModApiControl = new Collection<clsModApiControl>();
                            objModApiFormulario.LstModApiSeccion.Add(objModApiSeccion);
                            intIdSeccion = (sqlLeer["IdSeccion"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccion"].ToString()) : 0);
                        }

                        

                        objModApiControl = new clsModApiControl()
                        {
                            IdSeccionControl = (sqlLeer["IdSeccionControl"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccionControl"].ToString()) : 0),
                            IdSeccionControlPadre = (sqlLeer["IdSeccionControlPadre"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccionControlPadre"].ToString()) : 0),
                            Nombre = sqlLeer["NombreControl"] != DBNull.Value ? sqlLeer["NombreControl"].ToString() : string.Empty,
                            IdTipoControl = (sqlLeer["IdTipoControl"] != DBNull.Value ? int.Parse(sqlLeer["IdTipoControl"].ToString()) : 0),
                            Longitud = (sqlLeer["Longitud"] != DBNull.Value ? int.Parse(sqlLeer["Longitud"].ToString()) : 0),
                            Formato = sqlLeer["Formato"] != DBNull.Value ? sqlLeer["Formato"].ToString() : string.Empty,
                            Requerido = (sqlLeer["Requerido"] != DBNull.Value ? bool.Parse(sqlLeer["Requerido"].ToString()) : false),
                            Orden = (sqlLeer["OrdenControl"] != DBNull.Value ? int.Parse(sqlLeer["OrdenControl"].ToString()) : 0),
                            ValorDefault = sqlLeer["ValorDefault"] != DBNull.Value ? sqlLeer["ValorDefault"].ToString() : string.Empty,
                            Caption = sqlLeer["Caption"] != DBNull.Value ? sqlLeer["Caption"].ToString() : string.Empty,
                            NombreColumna = sqlLeer["NombreColumna"] != DBNull.Value ? sqlLeer["NombreColumna"].ToString() : string.Empty,
                            CssClass = sqlLeer["CssClass"] != DBNull.Value ? sqlLeer["CssClass"].ToString() : string.Empty,
                            HelpText = sqlLeer["HelpText"] != DBNull.Value ? sqlLeer["HelpText"].ToString() : string.Empty,
                            TextoSeleccionado = sqlLeer["TextoSeleccionado"] != DBNull.Value ? sqlLeer["TextoSeleccionado"].ToString() : string.Empty,
                            TextoNoSeleccionado = sqlLeer["TextoNoSeleccionado"] != DBNull.Value ? sqlLeer["TextoNoSeleccionado"].ToString() : string.Empty,
                            DataSource = (sqlLeer["DataSource"] != DBNull.Value ? bool.Parse(sqlLeer["DataSource"].ToString()) : false),
                            TextAreaHeight = (sqlLeer["TextAreaHeight"] != DBNull.Value ? int.Parse(sqlLeer["TextAreaHeight"].ToString()) : 0)
                        };

                        objModApiSeccion.LstModApiControl.Add(objModApiControl);


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

    }
}
