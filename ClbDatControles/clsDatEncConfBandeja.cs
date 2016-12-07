using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using ClbDatControles.Common;
using ClbModControles;
using System.Data.SqlClient;
using System.Data;

namespace ClbDatControles
{
    public class clsDatEncConfBandeja
    {
        private string _strConexion;

        #region Metodo Constructor para inicializar la variable _strConexion
        public clsDatEncConfBandeja()
        {
            this._strConexion = DataBaseConnection.Instance.GetConnectionString();
        }
        #endregion

        #region Metodo para Agregar un nuevo formulario a la bandeja

        public clsModErrorBase AgregarFormulaio(clsModEncConfBandeja objModEncConfBandeja)
        {
            clsModErrorBase objModErrorBase = null;
            SqlParameter[] arrpar = {
                                        new SqlParameter("@Nombre",SqlDbType.VarChar) { Value = objModEncConfBandeja.Nombre},
                                        new SqlParameter ("@Descripcion",SqlDbType.VarChar){Value = objModEncConfBandeja.Descripcion},
                                        new SqlParameter("@NumeroPaginas",SqlDbType.Int){Value = objModEncConfBandeja.NumeroPaginas},
                                        new SqlParameter("@Consulta",SqlDbType.Text) {Value = objModEncConfBandeja.Consulta},
                                        new SqlParameter("@IdUsuarioCreacion",SqlDbType.Int){Value = objModEncConfBandeja.IdUsuarioCreacion},
                                        new SqlParameter("@IdUsuarioModificacion",SqlDbType.Int){Value = objModEncConfBandeja.IdUsuarioModificacion},
                                        new SqlParameter("@IdFormulario",SqlDbType.Int){Value = objModEncConfBandeja.IdFormulario}, 
                                        new SqlParameter("@IdTipoConsulta",SqlDbType.Int){Value = objModEncConfBandeja.IdTipoConsulta}, 
                                        new SqlParameter("@ClaseBandeja",SqlDbType.VarChar) { Value = objModEncConfBandeja.ClaseBandeja},

                                    };
            try
            {
                object objId = SqlHelper.ExecuteScalar(_strConexion, CommandType.StoredProcedure, "NzControles.spdEncConfBandejaAgregar", arrpar);
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

        #region Metodo para Actualizar un formulario existente
        public clsModErrorBase Actualizar(clsModEncConfBandeja objModEncConfBandeja)
        {
            clsModErrorBase objModErrorBase = null;
            SqlParameter[] arrpar =  {
                                         new SqlParameter("@IdEncConfBandeja",SqlDbType.Int) { Value = objModEncConfBandeja.IdEncConfBandeja},
                                         new SqlParameter("@Nombre",SqlDbType.VarChar) { Value = objModEncConfBandeja.Nombre},
                                        new SqlParameter ("@Descripcion",SqlDbType.VarChar){Value = objModEncConfBandeja.Descripcion},
                                        new SqlParameter("@NumeroPaginas",SqlDbType.Int){Value = objModEncConfBandeja.NumeroPaginas},
                                        new SqlParameter("@Consulta",SqlDbType.Text) { Value = objModEncConfBandeja.Consulta},
                                        new SqlParameter("@FechaModificacion",SqlDbType.DateTime){ Value = DateTime.Now.ToString()},
                                        new SqlParameter("@IdUsuarioModificacion",SqlDbType.Int){Value = objModEncConfBandeja.IdUsuarioModificacion},
                                        new SqlParameter("@IdFormulario",SqlDbType.Int){Value = objModEncConfBandeja.IdFormulario}, 
                                        new SqlParameter("@IdTipoConsulta",SqlDbType.Int){Value = objModEncConfBandeja.IdTipoConsulta},
                                        new SqlParameter("@ClaseBandeja",SqlDbType.VarChar) { Value = objModEncConfBandeja.ClaseBandeja},
                                     };
            try
            {
                 SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.StoredProcedure, "NzControles.spdEncConfBandejaActualizar", arrpar);
                    objModErrorBase = new clsModErrorBase();
                
            }
            catch (Exception ex)
            {
                objModErrorBase = new clsModErrorBase();
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }
        #endregion

        #region Metodo para Validar Consulta SQL
        public clsModErrorBase ValidarSQL(clsModEncConfBandeja objModEncConfBandeja)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            try
            {
                string strSQL =
                "SET NOEXEC ON " +
                    objModEncConfBandeja.Consulta.ToString() +
                " SET NOEXEC OFF ";

                SqlHelper.ExecuteNonQuery(this._strConexion, CommandType.Text, strSQL);
                objModErrorBase.MsgError = null;
            }
            catch (Exception ex)
            {
                //objModErrorBase.MsgError = ex.Message;
                objModErrorBase.MsgError = "Revise la sintaxis de la Consulta SQL";
            }
            return objModErrorBase;
        }
        #endregion

        #region Metodo para Cargar Formularios por Id
        public clsModEncConfBandeja CargarXId(int Id, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsModEncConfBandeja objModEncConfBandeja = new clsModEncConfBandeja();
            SqlParameter[] arrpar = { new SqlParameter("@IdEncConfBandeja", SqlDbType.Int) { Value = Id } };
            SqlDataReader leer = null;
            try
            {
                leer = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdEncConfBandejaCargarXId", arrpar);
                if (leer != null)
                {
                    while (leer.Read())
                    {
                        objModEncConfBandeja.IdEncConfBandeja = int.Parse(leer["IdEncConfBandeja"].ToString());
                        objModEncConfBandeja.Nombre = leer["Nombre"].ToString();
                        objModEncConfBandeja.Descripcion = leer["Descripcion"].ToString();
                        objModEncConfBandeja.NumeroPaginas = int.Parse(leer["NumeroPaginas"].ToString());
                        objModEncConfBandeja.Consulta = leer["Consulta"].ToString();
                        objModEncConfBandeja.IdUsuarioCreacion = int.Parse(leer["IdUsuarioCreacion"].ToString());
                        objModEncConfBandeja.IdUsuarioModificacion = int.Parse(leer["IdUsuarioModificacion"].ToString());
                        objModEncConfBandeja.FechaCreacion = DateTime.Parse(leer["FechaCreacion"].ToString());
                        objModEncConfBandeja.FechaModificacion = DateTime.Parse(leer["FechaModificacion"].ToString());
                        objModEncConfBandeja.IdTipoConsulta = int.Parse(leer["IdTipoConsulta"].ToString());
                        objModEncConfBandeja.IdFormulario = int.Parse(leer["IdFormulario"].ToString());
                        objModEncConfBandeja.ClaseBandeja = leer["ClaseBandeja"].ToString();

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
            return objModEncConfBandeja;
        }
        #endregion

        #region Metodo para Regresar Nombre de columnas
        public List<clsModEncConfBandeja> NombreColumna(int id, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsModEncConfBandeja objModEncConfBandeja = null;
            List<clsModEncConfBandeja> lstModEncConfBandeja = new List<clsModEncConfBandeja>();

            SqlParameter[] arrpar = { new SqlParameter("Id", SqlDbType.Int) { Value = id } };

            SqlDataReader leer = null;
            DataTable dt = new DataTable();
            try
            {
                leer = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.spdEncConfBandejaConsultaXId", arrpar);
                dt.Load(leer);

                foreach (DataColumn item in dt.Columns)
                {
                    objModEncConfBandeja = new clsModEncConfBandeja();

                    objModEncConfBandeja.NombreColumna = item.ColumnName.ToString();

                    lstModEncConfBandeja.Add(objModEncConfBandeja);
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstModEncConfBandeja;
        }
        #endregion
    }
}
