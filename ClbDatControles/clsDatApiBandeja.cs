using ClbDatControles.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using ClbModControles.Api;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Collections.ObjectModel;

namespace ClbDatControles
{
    public class clsDatApiBandeja
    {
         private string _StrConnection;

         public clsDatApiBandeja()
        {
            this._StrConnection = DataBaseConnection.Instance.GetConnectionString();
        }
          
         public clsModApiBandeja CargarCampoConsulta(string strUUID, int pagina, List<clsModApiFiltros> listaFiltros, out clsModErrorBase objModErrorBase, out clsModPaginacion objModPaginacion)
         {
             List<object> listTables = new List<object>();
             ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
             lstSqlParameters.Add(new SqlParameter("@UUID", SqlDbType.VarChar) { Value = strUUID });
             objModErrorBase = new clsModErrorBase();
             SqlDataReader sqlLeer = null;
             objModPaginacion = new clsModPaginacion();
             clsModApiBandeja objModApiBandeja = new clsModApiBandeja();
             List<clsModCatBandejaColumna> listbandejaColumnba = new List<clsModCatBandejaColumna>();
             try
             {
                 DataTable tabla1 = new DataTable();
                 DataTable tabla2 = new DataTable();
                 sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdApiBandeja", lstSqlParameters.ToArray());
                 if (sqlLeer.HasRows) 
                 {
                     while (sqlLeer.Read())
                         objModApiBandeja.bandeja = LlenarObjetoBandeja(sqlLeer);
                     ICollection<SqlParameter> lstSqlParametersFiltros = new List<SqlParameter>();
                     lstSqlParametersFiltros.Add(new SqlParameter("@Pagina", null) { Value = pagina });
                     lstSqlParametersFiltros.Add(new SqlParameter("@RegistrosPorPagina", null) { Value = objModApiBandeja.bandeja.NumeroPaginas });
                     foreach(var x in listaFiltros)
                         lstSqlParametersFiltros.Add(new SqlParameter('@' + x.nombre, null) { Value = x.valor });
                     sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, objModApiBandeja.bandeja.Consulta, lstSqlParametersFiltros.ToArray());
                     if (sqlLeer.HasRows)
                     {
                         tabla2.Load(sqlLeer);
                         objModApiBandeja.consulta = tabla2;
                         objModPaginacion.Asignar(objModApiBandeja.bandeja.NumeroPaginas, decimal.Parse(tabla2.Rows[0]["Registros"].ToString()));
                         objModApiBandeja.paginacion = objModPaginacion;
                         sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdApiBandejaColumnaDatosUUID", lstSqlParameters.ToArray());
                         if (sqlLeer.HasRows)
                         {
                             while (sqlLeer.Read())
                                 listbandejaColumnba.Add(LlenarObjetoBandejaColumna(sqlLeer));
                             objModApiBandeja.bandejaColumnba = listbandejaColumnba;
                             sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdApiBandejaLinkParametrosDatosUUID", lstSqlParameters.ToArray());
                             if (sqlLeer.HasRows)
                             {
                                 tabla1.Load(sqlLeer);
                                 objModApiBandeja.LinkParametros = tabla1;
                             }
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
             return objModApiBandeja;
         }



         public string Cargar(string strUUID, out clsModErrorBase objModErrorBase)
         {
             ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
             lstSqlParameters.Add(new SqlParameter("@UUID", SqlDbType.VarChar) { Value = strUUID });
             objModErrorBase = new clsModErrorBase();
             SqlDataReader sqlLeer = null;
             clsModApiBandeja objModApiBandeja = new clsModApiBandeja();
             string strUUIDFormulario = "";

             try
             {
                 object objFormulario = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "[NzControles].[SpdApiBandejaGetUUIDFormulario]", lstSqlParameters.ToArray());
                 strUUIDFormulario = objFormulario.ToString();
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

             return strUUIDFormulario;
         }

         private clsModCatBandejas LlenarObjetoBandeja(SqlDataReader sqlDataReader)
         {
             clsModCatBandejas objModBandeja = new clsModCatBandejas()
             {
                 Nombre = (string)(sqlDataReader["Nombre"] != DBNull.Value ? sqlDataReader["Nombre"] : ""),
                 IdEncConfBandeja = (int)(sqlDataReader["IdEncConfBandeja"] != DBNull.Value ? sqlDataReader["IdEncConfBandeja"] : 0),
                 Descripcion = (string)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"] : ""),
                 NumeroPaginas = (int)(sqlDataReader["NumeroPaginas"] != DBNull.Value ? sqlDataReader["NumeroPaginas"] : 0),
                 Consulta = (string)(sqlDataReader["Consulta"] != DBNull.Value ? sqlDataReader["Consulta"] : ""),
                 IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                 IdUsuarioModificacion = (int)(sqlDataReader["IdUsuarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                 FechaCreacion = (DateTime)(sqlDataReader["FechaCreacion"] != DBNull.Value ? sqlDataReader["FechaCreacion"] : ""),
                 FechaModificacion = (DateTime)(sqlDataReader["FechaModificacion"] != DBNull.Value ? sqlDataReader["FechaModificacion"] : ""),
                 ClaseBandeja = (string)(sqlDataReader["ClaseBandeja"] != DBNull.Value ? sqlDataReader["ClaseBandeja"] : ""),
             };
             return objModBandeja;
         }
        private clsModCatBandejaColumna LlenarObjetoBandejaColumna(SqlDataReader sqlLeer)
        {
            clsModCatBandejaColumna objModBandejaColumna = new clsModCatBandejaColumna()
            {
                IdDetConfBandejaColumna = (int)(sqlLeer["IdDetConfBandejaColumna"] != DBNull.Value ? sqlLeer["IdDetConfBandejaColumna"] : 0),
                IdEncConfBandeja = (int)(sqlLeer["IdEncConfBandeja"] != DBNull.Value ? sqlLeer["IdEncConfBandeja"] : 0),
                TituloColumna = (String)(sqlLeer["TituloColumna"] != DBNull.Value ? sqlLeer["TituloColumna"] : ""),
                ColumnaBD = (String)(sqlLeer["ColumnaBD"] != DBNull.Value ? sqlLeer["ColumnaBD"] : ""),
                IdTipoColumna = (int)(sqlLeer["IdTipoColumna"] != DBNull.Value ? sqlLeer["IdTipoColumna"] : 0),
                //TipoColumna = (string)(sqlDataReader["TipoColumna"] != DBNull.Value ? sqlDataReader["TipoColumna"] : ""),
                Clase = (String)(sqlLeer["Clase"] != DBNull.Value ? sqlLeer["Clase"] : ""),
                Formato = (String)(sqlLeer["Formato"] != DBNull.Value ? sqlLeer["Formato"] : ""),
                PaginaLink = (String)(sqlLeer["PaginaLink"] != DBNull.Value ? sqlLeer["PaginaLink"] : ""),
                IdUsuarioCreacion = (int)(sqlLeer["IdUsuarioCreacion"] != DBNull.Value ? sqlLeer["IdUsuarioCreacion"] : 0),
                IdUsuarioModificacion = (int)(sqlLeer["IdUsuarioModificacion"] != DBNull.Value ? sqlLeer["IdUsuarioModificacion"] : 0),
                FechaCreacion = (DateTime)(sqlLeer["FechaCreacion"] != DBNull.Value ? sqlLeer["FechaCreacion"] : ""),
                FechaModificacion = (DateTime)(sqlLeer["FechaModificacion"] != DBNull.Value ? sqlLeer["FechaModificacion"] : ""),
                OrdenColumna = (int)(sqlLeer["OrdenColumna"] != DBNull.Value ? sqlLeer["OrdenColumna"] : 0),
                TipoLink = (bool)(sqlLeer["TipoLink"] != DBNull.Value ? sqlLeer["TipoLink"] : bool.FalseString),
            };
            return objModBandejaColumna;
        }
    }
}
