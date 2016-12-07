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
using ClbModControles.Api;


namespace ClbDatControles
{
    public class clsDatApiMenuDinamico
    {
        private string _StrConnection;
        /// <summary>
        /// Constructor que inicializa la cadena de conexión
        /// </summary>

        public clsDatApiMenuDinamico()
        {
            _StrConnection = Common.DataBaseConnection.Instance.GetConnectionString();
        }

        public List<clsModApiMenuDinamico> CargarMenuDinamico(out clsModErrorBase objErrorBase, int tipoMenu)
        {
            objErrorBase = new clsModErrorBase();
            List<clsModApiMenuDinamico> lista = new List<clsModApiMenuDinamico>();
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@TipoMenu", SqlDbType.Int) { Value = tipoMenu });
                SqlDataReader sqlDataReader = null;
                
                try
                {
                    sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdApiMenuDinamicoTipoMenu", lstSqlParameters.ToArray());
                    if (sqlDataReader != null)
                        while (sqlDataReader.Read()) 
                            lista.Add(this.LlenarObjetoMenu(sqlDataReader));
                }
                catch (Exception exception)
                {
                    objErrorBase.MsgError = exception.Message;
                }
                finally
                {
                    if (sqlDataReader != null)
                        sqlDataReader.Close();
                }
            }
            catch (Exception exception)
            {
                objErrorBase.MsgError = exception.Message;
            }
            return lista;
        }


        private clsModApiMenuDinamico LlenarObjetoMenu(SqlDataReader sqlDataReader)
        {
            clsModApiMenuDinamico objModMenu = new clsModApiMenuDinamico()
            {
                IdMenu = (int)(sqlDataReader["IdMenu"] != DBNull.Value ? sqlDataReader["IdMenu"] : 0),
                IdTipoMenu = (int)(sqlDataReader["IdTipoMenu"] != DBNull.Value ? sqlDataReader["IdTipoMenu"] : 0),
                Descripcion = (string)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"] : ""),
                Menu = (string)(sqlDataReader["Menu"] != DBNull.Value ? sqlDataReader["Menu"] : ""),
                Url = (string)(sqlDataReader["Url"] != DBNull.Value ? sqlDataReader["Url"] : ""),
                IdMenuIcono = (int)(sqlDataReader["IdMenuIcono"] != DBNull.Value ? sqlDataReader["IdMenuIcono"] : 0),
                IdMenuPadre = (int)(sqlDataReader["IdMenuPadre"] != DBNull.Value ? sqlDataReader["IdMenuPadre"] : 0),
                FechaCreacion = (DateTime)(sqlDataReader["FechaCreacion"] != DBNull.Value ? sqlDataReader["FechaCreacion"] : ""),
                FechaModificacion = (DateTime)(sqlDataReader["FechaModificacion"] != DBNull.Value ? sqlDataReader["FechaModificacion"] : ""),
                IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                IdUsuarioModificacion = (int)(sqlDataReader["IdUsuarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                Icono = (string)(sqlDataReader["Icono"] != DBNull.Value ? sqlDataReader["Icono"] : ""),
                TipoMenu = (string)(sqlDataReader["TipoMenu"] != DBNull.Value ? sqlDataReader["TipoMenu"] : ""),
            };
            return objModMenu;
        }
    }
}
