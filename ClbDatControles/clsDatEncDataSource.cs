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
    public class clsDatEncDataSource
    {

         private string _StrConnection;

         public clsDatEncDataSource()
        {
            this._StrConnection = ClbDatControles.Common.DataBaseConnection.Instance.GetConnectionString();
        }

        /// <summary>
        /// Agrega un nuevo elemento a la tabla de seccionControl
        /// </summary>
        /// <param name="objModCatSeccionControl">Objeto a guardar</param>
        /// <returns>Regresa el objeto de error conteniendo el id guardado o mensaje de errror generado</returns>
         public clsModErrorBase Agregar(clsModEncDataSource objModEncDataSource)
         {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            lstSqlParameters.Add(new SqlParameter("@BaseDatos", SqlDbType.Bit) { Value = objModEncDataSource.BaseDatos });
            lstSqlParameters.Add(new SqlParameter("@Consulta", SqlDbType.VarChar) { Value = objModEncDataSource.Consulta ?? "" });
            lstSqlParameters.Add(new SqlParameter("@IdSeccionControl", SqlDbType.Int) { Value = objModEncDataSource.IdSeccionControl });
            lstSqlParameters.Add(new SqlParameter("@TextField", SqlDbType.VarChar) { Value = objModEncDataSource.TextField ?? "" });
            lstSqlParameters.Add(new SqlParameter("@ValueField", SqlDbType.VarChar) { Value = objModEncDataSource.ValueField ?? "" });
            try {

                object objId = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdEncDataSourceAgregar", lstSqlParameters.ToArray());
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
         /// Agrega un nuevo elemento a la tabla de seccionControl
         /// </summary>
         /// <param name="objModCatSeccionControl">Objeto a guardar</param>
         /// <returns>Regresa el objeto de error conteniendo el id guardado o mensaje de errror generado</returns>
         public clsModErrorBase Actualizar(clsModEncDataSource objModEncDataSource)
         {
             clsModErrorBase objModErrorBase = new clsModErrorBase();

             ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
             lstSqlParameters.Add(new SqlParameter("@IdEncDataSource", SqlDbType.Int) { Value = objModEncDataSource.IdEncDataSource });
             lstSqlParameters.Add(new SqlParameter("@BaseDatos", SqlDbType.Bit) { Value = objModEncDataSource.BaseDatos });
             lstSqlParameters.Add(new SqlParameter("@Consulta", SqlDbType.VarChar) { Value = objModEncDataSource.Consulta });
             lstSqlParameters.Add(new SqlParameter("@IdSeccionControl", SqlDbType.Int) { Value = objModEncDataSource.IdSeccionControl });
             lstSqlParameters.Add(new SqlParameter("@TextField", SqlDbType.VarChar) { Value = objModEncDataSource.TextField });
             lstSqlParameters.Add(new SqlParameter("@ValueField", SqlDbType.VarChar) { Value = objModEncDataSource.ValueField });
             try
             {

                 SqlHelper.ExecuteNonQuery(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatSeccionControlActualizar", lstSqlParameters.ToArray());

             }
             catch (Exception ex)
             {
                 objModErrorBase = new clsModErrorBase();
                 objModErrorBase.MsgError = ex.Message;
             }

             return objModErrorBase;
         }




         /// <summary>
         /// Agrega un nuevo elemento a la tabla de seccionControl
         /// </summary>
         /// <param name="objModCatSeccionControl">Objeto a guardar</param>
         /// <returns>Regresa el objeto de error conteniendo el id guardado o mensaje de errror generado</returns>
         public ICollection<clsModEncDataSource> Cargar(int intIdEncDataSource, out clsModErrorBase objModErrorBase)
         {
             objModErrorBase = new clsModErrorBase();
             ICollection<clsModEncDataSource> lstModEncDataSource = new Collection<clsModEncDataSource>();
             clsModEncDataSource objModEncDataSource = null;

             ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
             lstSqlParameters.Add(new SqlParameter("@IdEncDataSource", SqlDbType.Int) { Value = intIdEncDataSource });
             SqlDataReader sqlLeer = null;
             try
             {

                 sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatSeccionControlActualizar", lstSqlParameters.ToArray());
                 if (sqlLeer.HasRows) {

                     while (sqlLeer.Read()) {

                         objModEncDataSource = new clsModEncDataSource() {

                             IdEncDataSource = (sqlLeer["IdEncDataSource"] != DBNull.Value ? int.Parse(sqlLeer["IdEncDataSource"].ToString()) : 0),
                             Consulta = sqlLeer["Consulta"] != DBNull.Value ? sqlLeer["Consulta"].ToString() : string.Empty,
                             BaseDatos = (sqlLeer["BaseDatos"] != DBNull.Value ? bool.Parse(sqlLeer["BaseDatos"].ToString()) : false),
                             IdSeccionControl = (sqlLeer["IdSeccionControl"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccionControl"].ToString()) : 0),
                             TextField = sqlLeer["TextField"] != DBNull.Value ? sqlLeer["TextField"].ToString() : string.Empty,
                             ValueField = sqlLeer["ValueField"] != DBNull.Value ? sqlLeer["ValueField"].ToString() : string.Empty
                         };


                         lstModEncDataSource.Add(objModEncDataSource);
                     }
                 }

             }
             catch (Exception ex)
             {
                 objModErrorBase = new clsModErrorBase();
                 objModErrorBase.MsgError = ex.Message;
             }
             finally {

                 if (sqlLeer != null) sqlLeer.Close();
             }

             return lstModEncDataSource;
         }


         public clsModEncDataSource CargarXControl(int intIdSeccionControl, out clsModErrorBase objModErrorBase)
         {
             objModErrorBase = new clsModErrorBase();
             clsModEncDataSource objModEncDataSource = null;
             ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
             lstSqlParameters.Add(new SqlParameter("@IdSeccionControl", SqlDbType.Int) { Value = intIdSeccionControl });
             SqlDataReader sqlLeer = null;
             try
             {

                 sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdEncDataSourceCargarXId", lstSqlParameters.ToArray());
                 if (sqlLeer.HasRows)
                 {

                     while (sqlLeer.Read())
                     {

                         objModEncDataSource = new clsModEncDataSource()
                         {

                             IdEncDataSource = (sqlLeer["IdEncDataSource"] != DBNull.Value ? int.Parse(sqlLeer["IdEncDataSource"].ToString()) : 0),
                             Consulta = sqlLeer["Consulta"] != DBNull.Value ? sqlLeer["Consulta"].ToString() : string.Empty,
                             BaseDatos = (sqlLeer["BaseDatos"] != DBNull.Value ? bool.Parse(sqlLeer["BaseDatos"].ToString()) : false),
                             IdSeccionControl = (sqlLeer["IdSeccionControl"] != DBNull.Value ? int.Parse(sqlLeer["IdSeccionControl"].ToString()) : 0),
                             TextField = sqlLeer["TextField"] != DBNull.Value ? sqlLeer["TextField"].ToString() : string.Empty,
                             ValueField = sqlLeer["ValueField"] != DBNull.Value ? sqlLeer["ValueField"].ToString() : string.Empty
                         };
                     }
                 }

             }
             catch (Exception ex)
             {
                 objModErrorBase = new clsModErrorBase();
                 objModErrorBase.MsgError = ex.Message;
             }
             finally
             {

                 if (sqlLeer != null) sqlLeer.Close();
             }

             return objModEncDataSource;
         }


    }
}
