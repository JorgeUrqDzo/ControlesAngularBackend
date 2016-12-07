using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;


namespace ClbDatControles
{
    public class clsDatCatActividad
    {
        private string strConexion = "";

        public clsDatCatActividad()
        {
            this.strConexion = Common.DataBaseConnection.Instance.GetConnectionString();
        }

        #region Agregar Actividad

        //Agregauna nueva actividad a la tabla
        public clsModErrorBase AgregarCatACtividad(clsModCatActividad objModCatActividad)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            int identity = 0;
            
    
            try
            {
                SqlParameter[] arrPar = new SqlParameter[7];
             
                arrPar[0] = new SqlParameter("@IdTipoProceso", SqlDbType.Int)
                {
                    Value = objModCatActividad.IdTipoProceso
                };
                arrPar[1] = new SqlParameter("@Orden", SqlDbType.Int)
                {

                    Value = objModCatActividad.Orden
                };
                arrPar[2] = new SqlParameter("@Activo", SqlDbType.Bit)
                {
                    Value = objModCatActividad.Activo
                };
                arrPar[3] = new SqlParameter("@Actividad", SqlDbType.VarChar)
                {
                    Value = objModCatActividad.Actividad
                };
                arrPar[4] = new SqlParameter("@UrlActividad", SqlDbType.VarChar)
                {
                    Value = objModCatActividad.UrlActividad
                };
                arrPar[5] = new SqlParameter("@UrlDestinoAlTerminar", SqlDbType.VarChar)
                {
                    Value = objModCatActividad.UrlDestinoAlTerminar
                };
                arrPar[6] = new SqlParameter("@TiempoPromedioObjetivo", SqlDbType.Int)
                {
                    Value = objModCatActividad.TiempoPromedioObjetivo
                };
             
                object idActividad = SqlHelper.ExecuteScalar(strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdEncActividadAgregar", arrPar);
                if (idActividad != null)
                {
                    int.TryParse(idActividad.ToString(), out identity);
                    objModErrorBase.MsgError = "Actividad Creada";
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            //objModErrorBase.Id = identity;
            return objModErrorBase;
        }
        #endregion

        #region CargarActividades
        //Metodo para obtener los datos de bandejas
        public ICollection<clsModCatActividad> CargarActividad(out clsModErrorBase objErrorBase, string strTextoBuscar,
            out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatActividad> listCargarActividad = new Collection<clsModCatActividad>();

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
            clsModCatActividad objModCatActividad = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdEncActividadCargarX", arrParameters);
                if (sqlDataReader != null)
                {

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            if (objModPaginacion.TotalRegistros == 0)
                            {
                                objModPaginacion.Asignar(intRegistrosPorPagina,
                                    decimal.Parse(sqlDataReader["Registros"].ToString()));
                            }
                            objModCatActividad = this.LlenarObjetoPaginacion(sqlDataReader);
                            listCargarActividad.Add(objModCatActividad);

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
            return listCargarActividad;
        }


        #endregion

        #region Llenar objeto Paginacion
        private clsModCatActividad LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {

            clsModCatActividad objModCatActividad = new clsModCatActividad()
            {
                IdEncActividad =
                    (int) (sqlDataReader["IdEncActividad"] != DBNull.Value ? sqlDataReader["IdEncActividad"] : 0),
                   
                        IdTipoProceso =
                    (int) (sqlDataReader["IdTipoProceso"] != DBNull.Value ? sqlDataReader["IdTipoProceso"] : 0),
                   
                
                Orden = (int) (sqlDataReader["Orden"] != DBNull.Value ? sqlDataReader["Orden"] : 0),
                Activo =
                    (bool)
                        (sqlDataReader["Activo"] != DBNull.Value
                            ? sqlDataReader["Activo"]
                            : bool.FalseString),
                Actividad =
                    (string) (sqlDataReader["Actividad"] != DBNull.Value ? sqlDataReader["Actividad"] : string.Empty),
                UrlActividad =
                    (string)
                        (sqlDataReader["UrlActividad"] != DBNull.Value ? sqlDataReader["UrlActividad"] : string.Empty),
                UrlDestinoAlTerminar =
                    (string)
                        (sqlDataReader["UrlDestinoAlTerminar"] != DBNull.Value
                            ? sqlDataReader["UrlDestinoAlTerminar"]
                            : string.Empty),
                TiempoPromedioObjetivo =
                    (int)
                        (sqlDataReader["TiempoPromedioObjetivo"] != DBNull.Value
                            ? sqlDataReader["TiempoPromedioObjetivo"]
                            : 0)
            };
            return objModCatActividad;
        }

        #endregion

        #region CargaPagActividad
        public ICollection<clsModCatActividad> CargaPagActividad(out clsModErrorBase objErrorBase, string strTextoBuscar,
            out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatActividad> listModCatActividad = new Collection<clsModCatActividad>();
            SqlParameter[] arrPar = new SqlParameter[3];
            arrPar[0] = new SqlParameter("@TextoBuscar", SqlDbType.VarChar) {Value = strTextoBuscar};
            arrPar[1] = new SqlParameter("@Pagina", SqlDbType.Int) {Value = intPagina};
            arrPar[2] = new SqlParameter("@RegistrosPorPagina", SqlDbType.Int) {Value = intRegistrosPorPagina};
            SqlDataReader sqlDataReader = null;
            clsModCatActividad objModCatActividad = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdEncActividadCargar", arrPar);
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
                                    objModPaginacion.Asignar(intRegistrosPorPagina,
                                        decimal.Parse(sqlDataReader["Registros"].ToString()));

                                }

                                objModCatActividad = this.LlenarObjetoPaginacion(sqlDataReader);
                                listModCatActividad.Add(objModCatActividad);
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
            return listModCatActividad;
        }

        #endregion

        #region getCatActividadFromReader
        //Obtener Modelo de las Actividades
        public ICollection<clsModCatActividad> GetCatActividadFromReader(SqlDataReader sqlDataReader)
        {
            ICollection<clsModCatActividad> listModActividad = new Collection<clsModCatActividad>();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    clsModCatActividad objModCatActividad = new clsModCatActividad()
                    {
                        IdEncActividad =
                            (int)
                                (sqlDataReader["IdEncActividad"] != DBNull.Value ? sqlDataReader["IdEncActividad"] : 0),
                        IdTipoProceso =
                            (int) (sqlDataReader["IdTipoProceso"] != DBNull.Value ? sqlDataReader["IdTipoProceso"] : 0),
                        Orden = (int) (sqlDataReader["Orden"] != DBNull.Value ? sqlDataReader["Orden"] : 0),
                        Activo =
                            (bool)
                                (sqlDataReader["Activo"] != DBNull.Value
                                    ? sqlDataReader["Activo"]
                                    : bool.FalseString),
                        Actividad =
                            (string)
                                (sqlDataReader["Actividad"] != DBNull.Value ? sqlDataReader["Actividad"] : string.Empty),
                        UrlActividad =
                            (string)
                                (sqlDataReader["UrlActividad"] != DBNull.Value
                                    ? sqlDataReader["UrlActividad"]
                                    : string.Empty),
                        UrlDestinoAlTerminar =
                            (string)
                                (sqlDataReader["UrlDestinoAlTerminar"] != DBNull.Value
                                    ? sqlDataReader["UrlDestinoAlTerminar"]
                                    : string.Empty),
                        TiempoPromedioObjetivo =
                            (int)
                                (sqlDataReader["TiempoPromedioObjetivo"] != DBNull.Value
                                    ? sqlDataReader["TiempoPromedioObjetivo"]
                                    : 0)

                    };
                    listModActividad.Add(objModCatActividad);
                }
            }
            return listModActividad;
        }

        #endregion


        #region Actualiza
        public clsModErrorBase ActualizaCatActividad(clsModCatActividad objModCatActividad)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            SqlParameter[] arrPar = new SqlParameter[8];
            arrPar[0] = new SqlParameter("@IdEncActividad", SqlDbType.Int)
            {
                Value = objModCatActividad.IdEncActividad

            };
            arrPar[1] = new SqlParameter("@IdTipoProceso", SqlDbType.Int)
            {
                Value = objModCatActividad.IdTipoProceso
            };
            arrPar[2] = new SqlParameter("@Orden", SqlDbType.Int)
            {
                Value = objModCatActividad.Orden
            };
            arrPar[3] = new SqlParameter("@Activo", SqlDbType.Bit)
            {
                Value = objModCatActividad.Activo
            };
            arrPar[4] = new SqlParameter("@Actividad", SqlDbType.VarChar)
            {
                Value = objModCatActividad.Actividad
            };
            arrPar[5] = new SqlParameter("@UrlActividad", SqlDbType.VarChar)
            {
                Value = objModCatActividad.UrlActividad
            };
            arrPar[6] = new SqlParameter("@UrlDestinoAlTerminar", SqlDbType.VarChar)
            {
                Value = objModCatActividad.UrlDestinoAlTerminar
            };
            arrPar[7] = new SqlParameter("@TiempoPromedioObjetivo", SqlDbType.Int)
            {
                Value = objModCatActividad.TiempoPromedioObjetivo
            };
            

            try
            {
                SqlHelper.ExecuteNonQuery(this.strConexion, CommandType.StoredProcedure, "NzControles.SpdEncActividadActualizar", arrPar);
                objModErrorBase.MsgError = "Actividad Actualizada";
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;


            }
            return objModErrorBase;
        }
        #endregion


        #region CargaXId
        public clsModCatActividad CargarXId(int IdActividad, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsModCatActividad objModCatActividad = null;
            SqlParameter[] arrPar = new SqlParameter[1];
            arrPar[0]= new SqlParameter("IdEncActividad", SqlDbType.Int){Value= IdActividad};
            SqlDataReader leer = null;
            try
            {

               leer=SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure,"NzControles.SpdEncActividadCargarXId", arrPar);
                if (leer != null)
                {
                    while (leer.Read())
                    {
                        objModCatActividad = this.LlenarObjetoPaginacion(leer);

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
            return objModCatActividad;
        }

        #endregion

         #region Metodo Actualizar Estado
        public clsModErrorBase ActualizarEstado(clsModCatActividad objModCatActividad)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            SqlParameter[] arrpar = {
                                        new SqlParameter("@IdEncActividad",SqlDbType.Int) {Value= objModCatActividad.IdEncActividad},
                                        new SqlParameter("@Activo",SqlDbType.Bit){Value=objModCatActividad.Activo}
                                    };
            try
            {
                SqlHelper.ExecuteNonQuery(strConexion, CommandType.StoredProcedure, "NzControles.spdCatEncActividadActualizarEstado", arrpar);
                objModErrorBase.MsgError = "Estado Actualizado";

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            return objModErrorBase;
        }
        #endregion


      
    }
}
