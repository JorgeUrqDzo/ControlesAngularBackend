using ClbDatControles.Common;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbDatControles
{
    public class clsDatCatTipoProceso
    {
        #region String de conexion
        private string _StrConnection;
        public clsDatCatTipoProceso()
        {
            this._StrConnection = DataBaseConnection.Instance.GetConnectionString();
        }
        #endregion

        #region Metodo para cargar TipoProceso por id
        /// <summary>
        /// Carga un objeto TipoProceso por Id.
        /// </summary>
        /// <param name="IdTipoCliente">Id de TipoProceso</param>
        /// <param name="objModErrorBase">Objeto de error basse</param>
        /// <returns>Devuelve un objeto TipoProceso</returns>
        public clsModCatTipoProceso CargarPorId(int IdTipoProceso, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsModCatTipoProceso objModCatTipoProceso = null;
            SqlParameter[] arrPar = new SqlParameter[1];
            arrPar[0] = new SqlParameter("IdTipoProceso", SqlDbType.Int) { Value = IdTipoProceso };
            SqlDataReader leer = null;
            try
            {

                leer = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatTipoProcesoCargarXId", arrPar);
                if (leer != null)
                {
                    while (leer.Read())
                    {
                        objModCatTipoProceso = this.LlenarObjetoPaginacion(leer);

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
            return objModCatTipoProceso;
        }
        #endregion

        #region Metodo para cargar todos los TipoProceso
        /// <summary>
        /// Cargar toda la lista de TipoProceso.
        /// <param name="objModErrorBase">Objeto de error base</param>
        /// <returns>Regresa una lista de objetos TipoProceso</returns>
        /// </summary>
         public ICollection<clsModCatTipoProceso> CargarTodos(out clsModErrorBase objErrorBase, string strTextoBuscar,
            out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatTipoProceso> listCargarProcesos = new Collection<clsModCatTipoProceso>();

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
            clsModCatTipoProceso objModCatTipoProceso = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(_StrConnection, CommandType.StoredProcedure,
                    "NzControles.SpdCatTipoProcesoCargarX", arrParameters);
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
                            objModCatTipoProceso = this.LlenarObjetoPaginacion(sqlDataReader);
                            listCargarProcesos.Add(objModCatTipoProceso);

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
            return listCargarProcesos;
        }
        
        #endregion

        #region LlenarObjetoPaginacion 
     private clsModCatTipoProceso LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {

            clsModCatTipoProceso objModCatTipoProceso = new clsModCatTipoProceso()
            {
                IdTipoProceso =(int) (sqlDataReader["IdTipoProceso"] != DBNull.Value ? sqlDataReader["IdTipoProceso"] : 0),
 
                IdAreaProceso = (int) (sqlDataReader["IdAreaProceso"] != DBNull.Value ? sqlDataReader["IdAreaProceso"] : 0),
                TipoProceso =(string) (sqlDataReader["TipoProceso"] != DBNull.Value ? sqlDataReader["TipoProceso"] : string.Empty),
                Activo =(bool) (sqlDataReader["Activo"] != DBNull.Value ? sqlDataReader["Activo"]: bool.FalseString),
                
               
            };
            return objModCatTipoProceso;
        }

#endregion


        #region Metodo para Agregar un TipoProceso
        /// <summary>
        /// Guarda un objeto de tipo TipoProceso.
        /// </summary>
        /// <param name="objModCatTipoProceso"> Objeto tipo TipoProceso que se guardara.</param>
        /// <returns>Regresa un objeto ModErrorBase con el resultado.</returns>
     public clsModErrorBase Agregar(clsModCatTipoProceso objModCatTipoProceso)
     {
         clsModErrorBase objModErrorBase = new clsModErrorBase();
         int identity = 0;

         try
         {
             SqlParameter[] arrPar = new SqlParameter[3];

             arrPar[0] = new SqlParameter("@IdAreaProceso", SqlDbType.Int)
             {
                 Value = objModCatTipoProceso.IdAreaProceso
             };
             arrPar[1] = new SqlParameter("@TipoProceso", SqlDbType.VarChar)
             {

                 Value = objModCatTipoProceso.TipoProceso
             };
             arrPar[2] = new SqlParameter("@Activo", SqlDbType.Bit)
             {
                 Value = objModCatTipoProceso.Activo
             };

             object idProceso = SqlHelper.ExecuteScalar(_StrConnection, CommandType.StoredProcedure,
                 "NzControles.SpdCatTipoProcesoAgregar", arrPar);
             if (idProceso != null)
             {
                 int.TryParse(idProceso.ToString(), out identity);
                 objModErrorBase.MsgError = "Proceso Creado";
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

     #region Metodo Actualizar Estado
     public clsModErrorBase ActualizarEstado(clsModCatTipoProceso objModCatTipoProceso)
     {
         clsModErrorBase objModErrorBase = new clsModErrorBase();
         SqlParameter[] arrpar = {
                                        new SqlParameter("@IdTipoProceso",SqlDbType.Int) {Value= objModCatTipoProceso.IdTipoProceso},
                                        new SqlParameter("@Activo",SqlDbType.Bit){Value=objModCatTipoProceso.Activo}
                                    };
         try
         {
             SqlHelper.ExecuteNonQuery(_StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatTipoProcesoActualizarEstado", arrpar);
             objModErrorBase.MsgError = "Estado Actualizado";

         }
         catch (Exception ex)
         {
             objModErrorBase.MsgError = ex.Message;
         }
         return objModErrorBase;
     }
     #endregion

   

        #region Metodo para Actualizar un TipoProceso
        /// <summary>
        ///  Metodo para Actualizar un objeto de TipoProceso
        /// </summary>
        /// <param name="objModCatTipoProceso">Es el objeto con los datos nuevos. Debe contener el id.</param>
        /// <returns>regresa un objModErrorBase con el id o un mensaje de error si fallo.</returns>
        public clsModErrorBase Actualizar(clsModCatTipoProceso objModCatTipoProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            SqlParameter[] arrPar = new SqlParameter[4];
            arrPar[0] = new SqlParameter("@IdTipoProceso", SqlDbType.Int);
            arrPar[0].Value = objModCatTipoProceso.IdTipoProceso;
            arrPar[1] = new SqlParameter("@IdAreaProceso", SqlDbType.Int);
            arrPar[1].Value = objModCatTipoProceso.IdAreaProceso;
            arrPar[2] = new SqlParameter("@TipoProceso", SqlDbType.VarChar);
            arrPar[2].Value = objModCatTipoProceso.TipoProceso;
            arrPar[3] = new SqlParameter("@Activo", SqlDbType.Bit);
            arrPar[3].Value = objModCatTipoProceso.Activo;

            try
            {
                SqlHelper.ExecuteNonQuery(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatTipoProcesoActualizar", arrPar);
                objModErrorBase.MsgError ="Proceso Actualizado";
            }

            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }
        #endregion

        #region Metodo para desactivar un TipoProceso
        /// <summary>
        /// Metodo para desactivar un objeto TipoProceso
        /// </summary>
        /// <param name="objModCatTipoProceso">un objeto TipoProceso con el id del TipoProceso a desactivar</param>
        /// <returns>regresa un objeto clsModErrorBase con el id si lo desactivo o un mensaje de error si no.</returns>
        public clsModErrorBase Desactivar(clsModCatTipoProceso objModCatTipoProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            SqlParameter IdTipoProceso = new SqlParameter("@IdTipoProceso",SqlDbType.Int);

            try
            {
                object idTipoProceso = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.spdCatTipoProcesoDesactivar");

                if (idTipoProceso != null)
                {
                    int id;
                    int.TryParse(idTipoProceso.ToString(), out id);
                    objModErrorBase.Id = id;
                }
            }
            catch(Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            return objModErrorBase;
        }
        #endregion
    }
}
