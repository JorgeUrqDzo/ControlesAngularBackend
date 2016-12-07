using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles.Common;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;

namespace ClbDatControles
{

        public class clsDatCatBandejas
        {
            //Variable para la conexion de datos
            string strConexion;

            //Constructor para inicializar la cadena de conexion
            public clsDatCatBandejas()
            {
                strConexion = DataBaseConnection.Instance.GetConnectionString();
            }

            //Metodo para obtener los datos de bandejas
             public ICollection<clsModCatBandejas> CargarBandejas(out clsModErrorBase objErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatBandejas> lstCargarBandejas = new Collection<clsModCatBandejas>();

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
            clsModCatBandejas objModCatBandejas = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure, "NzControles.SpdEncConfBandejaCargarX", arrParameters);
                if (sqlDataReader != null)
                {

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            if(objModPaginacion.TotalRegistros == 0) {
                                objModPaginacion.Asignar(intRegistrosPorPagina, decimal.Parse(sqlDataReader["Registros"].ToString()));
                            }
                                objModCatBandejas = this.LlenarObjetoPaginacion(sqlDataReader);
                            lstCargarBandejas.Add(objModCatBandejas);
    
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
            return lstCargarBandejas;
        }
            //Metodo para llenar el objeto paginacion con los datos de el store
             public clsModCatBandejas LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
             {
                 clsModCatBandejas objModBandejas = new clsModCatBandejas()
                 {
                     IdEncConfBandeja = (int)(sqlDataReader["IdEncConfBandeja"] != DBNull.Value ? sqlDataReader["IdEncConfBandeja"] : 0),
                     Nombre = (String)(sqlDataReader["Nombre"] != DBNull.Value ? sqlDataReader["Nombre"] : ""),
                     Descripcion = (String)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"] : ""),
                     NumeroPaginas = (int)(sqlDataReader["NumeroPaginas"] != DBNull.Value ? sqlDataReader["NumeroPaginas"] : 0),
                     Consulta = (String)(sqlDataReader["Consulta"] != DBNull.Value ? sqlDataReader["Consulta"] : ""),
                     IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                     IdUsuarioModificacion = (int)(sqlDataReader["IdUsuarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                     FechaCreacion = (DateTime)(sqlDataReader["FechaCreacion"] != DBNull.Value ? sqlDataReader["FechaCreacion"] : ""),
                    FechaModificacion = (DateTime)(sqlDataReader["FechaModificacion"] != DBNull.Value ? sqlDataReader["FechaModificacion"] : ""),
                      UUID = (sqlDataReader["UUID"] != DBNull.Value ? sqlDataReader["UUID"] : "0").ToString(),
                 };
                 return objModBandejas;
             }

            //Modelo para cargar Bandejas por paginacion
             public ICollection<clsModCatBandejas> CargaPagBandejas(out clsModErrorBase objErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
             {
                 objErrorBase = new clsModErrorBase();
                 objModPaginacion = new clsModPaginacion();
                 ICollection<clsModCatBandejas> lstModCatBandejas = new Collection<clsModCatBandejas>();
                 SqlParameter[] arrParameters = new SqlParameter[3];
                 arrParameters[0] = new SqlParameter("@TextoBuscar", SqlDbType.VarChar) { Value = strTextoBuscar };
                 arrParameters[1] = new SqlParameter("@Pagina", SqlDbType.Int) { Value = intPagina };
                 arrParameters[2] = new SqlParameter("@RegistrosPorPagina", SqlDbType.Int) { Value = intRegistrosPorPagina };
                 SqlDataReader sqlDataReader = null;
                 clsModCatBandejas objModCatBandejas = null;
                 try
                 {
                     sqlDataReader = SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure, "NzControles.SpdEncConfBandejaCargar", arrParameters);
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
                                     
                                     objModCatBandejas = this.LlenarObjetoPaginacion(sqlDataReader);
                                     lstModCatBandejas.Add(objModCatBandejas);
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
                 return lstModCatBandejas;
             }

            
            //Obtener Modelo de las bandejas
             public ICollection<clsModCatBandejas> GetCatBandejasFromReader(SqlDataReader sqlDataReader)
             {
                 ICollection<clsModCatBandejas> lstModBandejas = new Collection<clsModCatBandejas>();
                 if (sqlDataReader.HasRows)
                 {
                     while (sqlDataReader.Read())
                     {
                         clsModCatBandejas objModCatBandejas = new clsModCatBandejas()
                         {
                             IdEncConfBandeja = (int)(sqlDataReader["IdEncConfBandeja"] != DBNull.Value ? sqlDataReader["IdEncConfBandeja"] : 0),
                             Nombre = (String)(sqlDataReader["Nombre"] != DBNull.Value ? sqlDataReader["Nombre"] : ""),
                             Descripcion = (String)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"] : ""),
                             NumeroPaginas = (int)(sqlDataReader["NumeroPaginas"] != DBNull.Value ? sqlDataReader["NumeroPaginas"] : 0),
                             Consulta = (String)(sqlDataReader["Consulta"] != DBNull.Value ? sqlDataReader["Consulta"] : ""),
                             IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                             IdUsuarioModificacion = (int)(sqlDataReader["IdUduarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                             FechaCreacion = (DateTime)(sqlDataReader["FechaCreacion"] != DBNull.Value ? sqlDataReader["FechaCreacion"] : ""),
                             FechaModificacion = (DateTime)(sqlDataReader["FechaModificacion"] != DBNull.Value ? sqlDataReader["FechaModificacion"] : ""),
                           
                         };
                         lstModBandejas.Add(objModCatBandejas);
                     }
                 }
                 return lstModBandejas;
             }


        }
    }

