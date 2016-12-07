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
    public class clsDatCatMenu
    {
         private string strConexion = "";

        public clsDatCatMenu()
        {
            this.strConexion = Common.DataBaseConnection.Instance.GetConnectionString();
        }

        #region Agregar Menu
        //Agrega un nuevo menu a la tabla CatMenu
        public clsModErrorBase AgregarCatMenu(clsModCatMenu objModCatMenu)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            int identity = 0;
            
            try
            {
                SqlParameter[] arrPar = new SqlParameter[9];
             
                arrPar[0] = new SqlParameter("@IdTipoMenu", SqlDbType.Int)
                {
                    Value = objModCatMenu.IdTipoMenu
                };
                arrPar[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar)
                {

                    Value = objModCatMenu.Descripcion
                };
                arrPar[2] = new SqlParameter("@Menu", SqlDbType.VarChar)
                {
                    Value = objModCatMenu.Menu
                };
                arrPar[3] = new SqlParameter("@Url", SqlDbType.VarChar)
                {
                    Value = objModCatMenu.Url
                };
                arrPar[4] = new SqlParameter("@IdMenuIcono", SqlDbType.Int)
                {
                    Value = objModCatMenu.IdMenuIcono
                };
                arrPar[5] = new SqlParameter("@IdMenuPadre", SqlDbType.Int)
                {
                    Value = objModCatMenu.IdMenuPadre 
                };
                arrPar[6] = new SqlParameter("@Activo", SqlDbType.Bit)
                {
                    Value = objModCatMenu.Activo
                };

                arrPar[7] = new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int)
                {
                    Value = objModCatMenu.IdUsuarioCreacion
                };
                arrPar[8] = new SqlParameter("@IdUsuarioModificacion", SqlDbType.Int)
                {
                    Value = objModCatMenu.IdUsuarioModificacion
                };
          
             
             
                object idMenu = SqlHelper.ExecuteScalar(strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdCatMenuAgregar", arrPar);
                if (idMenu != null)
                {
                    int.TryParse(idMenu.ToString(), out identity);
                    objModErrorBase.MsgError = "Menu Creado";
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

        #region CargarMenu
        //Metodo para obtener los datos de la tabla CatMenu
        public ICollection<clsModCatMenu> CargarMenu(out clsModErrorBase objErrorBase, string strTextoBuscar,
            out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatMenu> listCargarMenu = new Collection<clsModCatMenu>();

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
            clsModCatMenu objModCatMenu = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdCatMenuCargarX", arrParameters);
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
                            objModCatMenu = this.LlenarObjetoPaginacion(sqlDataReader);
                            listCargarMenu.Add(objModCatMenu);

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
            return listCargarMenu;
        }


        #endregion

        #region Llenar objeto Paginacion
        private clsModCatMenu LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {
            clsModCatMenu objModCatMenu = new clsModCatMenu()
            {
                IdMenu =(int)(sqlDataReader["IdMenu"] != DBNull.Value ? sqlDataReader["IdMenu"] : 0),
                IdTipoMenu =(int)(sqlDataReader["IdTipoMenu"] != DBNull.Value ? sqlDataReader["IdTipoMenu"] : 0),
                Descripcion = (string)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"] : string.Empty),
                Menu =(string)(sqlDataReader["Menu"] != DBNull.Value ? sqlDataReader["Menu"]: string.Empty),
                Url =(string)(sqlDataReader["Url"] != DBNull.Value ? sqlDataReader["Url"] : string.Empty),
                Activo= (bool)(sqlDataReader["Activo"] != DBNull.Value ? sqlDataReader["Activo"] : bool.FalseString),
                IdMenuIcono =(int)(sqlDataReader["IdMenuIcono"] != DBNull.Value ? sqlDataReader["IdMenuIcono"] : 0),
                IdMenuPadre =(int)(sqlDataReader["IdMenuPadre"] != DBNull.Value? sqlDataReader["IdMenuPadre"]: 0),
                IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                IdUsuarioModificacion = (int)(sqlDataReader["IdUsuarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                Icono = (string)(sqlDataReader["Icono"] != DBNull.Value ? sqlDataReader["Icono"] : "")

            };
            return objModCatMenu;
        }

        #endregion

        #region CargaPagMenu
        public ICollection<clsModCatMenu> CargaPagMenu(out clsModErrorBase objErrorBase, string strTextoBuscar,
            out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            objErrorBase = new clsModErrorBase();
            objModPaginacion = new clsModPaginacion();
            ICollection<clsModCatMenu> listModCatMenu = new Collection<clsModCatMenu>();
            SqlParameter[] arrPar = new SqlParameter[3];
            arrPar[0] = new SqlParameter("@TextoBuscar", SqlDbType.VarChar) { Value = strTextoBuscar };
            arrPar[1] = new SqlParameter("@Pagina", SqlDbType.Int) { Value = intPagina };
            arrPar[2] = new SqlParameter("@RegistrosPorPagina", SqlDbType.Int) { Value = intRegistrosPorPagina };
            SqlDataReader sqlDataReader = null;
            clsModCatMenu objModCatMenu = null;
            try
            {
                sqlDataReader = SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure,
                    "NzControles.SpdCatMenuCargar", arrPar);
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

                                objModCatMenu = this.LlenarObjetoPaginacion(sqlDataReader);
                                listModCatMenu.Add(objModCatMenu);
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
            return listModCatMenu;
        }

        #endregion

        #region getCatMenuFromReader

        //Obtener Modelos de Menu
        public ICollection<clsModCatMenu> GetCatMenuFromReader(SqlDataReader sqlDataReader)
        {
            ICollection<clsModCatMenu> listModMenu = new Collection<clsModCatMenu>();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    clsModCatMenu objModCatMenu = new clsModCatMenu()
                    {
                        IdMenu = (int)(sqlDataReader["IdMenu"] != DBNull.Value ? sqlDataReader["IdMenu"] : 0),
                        IdTipoMenu = (int)(sqlDataReader["IdTipoMenu"] != DBNull.Value ? sqlDataReader["IdTipoMenu"] : 0),
                        Descripcion = (string)(sqlDataReader["Descripcion"] != DBNull.Value ? sqlDataReader["Descripcion"] : string.Empty),
                        Menu = (string)(sqlDataReader["Menu"] != DBNull.Value ? sqlDataReader["Menu"] : string.Empty),
                        Url = (string)(sqlDataReader["Url"] != DBNull.Value ? sqlDataReader["Url"] : string.Empty),
                        Activo = (bool)(sqlDataReader["Activo"] != DBNull.Value ? sqlDataReader["Activo"] : bool.FalseString),
                        IdMenuIcono = (int)(sqlDataReader["IdMenuIcono"] != DBNull.Value ? sqlDataReader["IdMenuIcono"] : 0),
                        IdMenuPadre = (int)(sqlDataReader["IdMenuPadre"] != DBNull.Value ? sqlDataReader["IdMenuPadre"] : 0),
                        IdUsuarioCreacion = (int)(sqlDataReader["IdUsuarioCreacion"] != DBNull.Value ? sqlDataReader["IdUsuarioCreacion"] : 0),
                        IdUsuarioModificacion = (int)(sqlDataReader["IdUsuarioModificacion"] != DBNull.Value ? sqlDataReader["IdUsuarioModificacion"] : 0),
                        Icono = (string)(sqlDataReader["Icono"] != DBNull.Value ? sqlDataReader["Icono"] : "")

                    };
                    listModMenu.Add(objModCatMenu);
                }
            }
            return listModMenu;
        }

        #endregion

        #region Actualiza

        //Actualiza los datos de la tabla CatMenu
        public clsModErrorBase ActualizaCatMenu(clsModCatMenu objModCatMenu)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            SqlParameter[] arrPar = new SqlParameter[10];
            arrPar[0] = new SqlParameter("@IdMenu", SqlDbType.Int)
            {
                Value = objModCatMenu.IdMenu
            };
            arrPar[1] = new SqlParameter("@IdTipoMenu", SqlDbType.Int)
            {
                Value = objModCatMenu.IdTipoMenu
            };
            arrPar[2] = new SqlParameter("@Descripcion", SqlDbType.VarChar)
            {
                Value = objModCatMenu.Descripcion
            };
            arrPar[3] = new SqlParameter("@Menu", SqlDbType.VarChar)
            {
                Value = objModCatMenu.Menu
            };
            arrPar[4] = new SqlParameter("@Url", SqlDbType.VarChar)
            {
                Value = objModCatMenu.Url
            };
            arrPar[5] = new SqlParameter("@IdMenuIcono", SqlDbType.Int)
            {
                Value = objModCatMenu.IdMenuIcono
            };
            arrPar[6] = new SqlParameter("@IdMenuPadre", SqlDbType.Int)
            {
                Value = objModCatMenu.IdMenuPadre
            };
            arrPar[7] = new SqlParameter("@Activo", SqlDbType.Bit)
            {
                Value = objModCatMenu.Activo
            };

            arrPar[9] = new SqlParameter("@IdUsuarioCreacion", SqlDbType.Int)
            {
                Value = objModCatMenu.IdUsuarioCreacion
            };
            arrPar[9] = new SqlParameter("@IdUsuarioModificacion", SqlDbType.Int)
            {
                Value = objModCatMenu.IdUsuarioModificacion
            };

            try
            {
                SqlHelper.ExecuteNonQuery(this.strConexion, CommandType.StoredProcedure, "NzControles.SpdCatMenuActualizar", arrPar);
                objModErrorBase.MsgError = "Menu Actualizado";
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;


            }
            return objModErrorBase;
        }
        #endregion


        #region CargaXId
        //Carga datos en la tabla buscando por id de la tabla CatMenu
        public clsModCatMenu CargarXId(int IdMenus, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsModCatMenu objModCatMenu = null;
            SqlParameter[] arrPar = new SqlParameter[1];
            arrPar[0] = new SqlParameter("IdMenu", SqlDbType.Int) { Value = IdMenus };
            SqlDataReader leer = null;
            try
            {

                leer = SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure, "NzControles.SpdCatMenuCargarXId", arrPar);
                if (leer != null)
                {
                    while (leer.Read())
                    {
                        objModCatMenu = this.LlenarObjetoPaginacion(leer);

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
            return objModCatMenu;
        }

        #endregion

        #region Metodo Actualizar Estado
        public clsModErrorBase ActualizarEstado(clsModCatMenu objModCatMenu)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            SqlParameter[] arrpar = {
                                        new SqlParameter("@IdMenu",SqlDbType.Int) {Value= objModCatMenu.IdMenu},
                                        new SqlParameter("@Activo",SqlDbType.Bit){Value=objModCatMenu.Activo}
                                    };
            try
            {
                SqlHelper.ExecuteNonQuery(strConexion, CommandType.StoredProcedure, "NzControles.SpdCatMenuActualizarEstado", arrpar);
                objModErrorBase.MsgError = "Estado Actualizado";

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            return objModErrorBase;
        }
        #endregion


        public List<clsModCatMenuIcono> CargarIconos(clsModErrorBase objModErrorBase)
        {
            List<clsModCatMenuIcono> lstCatMenuIcono = new List<clsModCatMenuIcono>();
            clsModCatMenuIcono objCatMenuIcono = null;
            objModErrorBase = new clsModErrorBase();

            SqlDataReader dr = null;

            try
            {
                dr = SqlHelper.ExecuteReader(this.strConexion, CommandType.StoredProcedure, "NzControles.SpdCatMenuCargarIconos");

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        objCatMenuIcono = new clsModCatMenuIcono();
                        objCatMenuIcono.objCatMenu = new clsModCatMenu();

                        objCatMenuIcono.Icono = dr["Icono"].ToString();
                        objCatMenuIcono.IdMenuIcono = int.Parse(dr["IdMenuIcono"].ToString());
                        objCatMenuIcono.objCatMenu.IdMenuIcono = int.Parse(dr["IdMenuIcono"].ToString());

                        lstCatMenuIcono.Add(objCatMenuIcono);
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstCatMenuIcono;
        }
        
}
}