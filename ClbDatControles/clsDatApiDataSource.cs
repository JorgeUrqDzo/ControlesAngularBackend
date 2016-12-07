using ClbModControles;
using ClbModControles.Api;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbDatControles
{
    public class clsDatApiDataSource
    {
        private string _StrConnection;

        public clsDatApiDataSource()
        {
            this._StrConnection = ClbDatControles.Common.DataBaseConnection.Instance.GetConnectionString();
        }

        public clsModApiDataSource Cargar(int intIdSeccionControl, out clsModErrorBase objModErrorBase)
        {
            clsModApiDataSource objModApiDataSource = new clsModApiDataSource();

            objModErrorBase = new clsModErrorBase();
            SqlDataReader sqlLeer = null;
            try
            {
                ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
                lstSqlParameters.Add(new SqlParameter("@IdSeccionControl", SqlDbType.Int) { Value = intIdSeccionControl });
                sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdEncDataSourceCargar", lstSqlParameters.ToArray());
                if (sqlLeer.HasRows)
                {

                    while (sqlLeer.Read())
                    {
                        objModApiDataSource.TextField = sqlLeer["TextField"] != DBNull.Value ? sqlLeer["TextField"].ToString() : string.Empty;
                        objModApiDataSource.ValueField = sqlLeer["ValueField"] != DBNull.Value ? sqlLeer["ValueField"].ToString() : string.Empty;
                    }
                    sqlLeer.Close();

                    sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdApiEncDataSourceCargarPorControl", lstSqlParameters.ToArray());
                    if (sqlLeer.HasRows)
                    {
                        DataTable dtTabla = new DataTable();
                        dtTabla.Load(sqlLeer);
                        
                        DataRow dr = dtTabla.NewRow();
                        foreach (DataColumn dataColumn in dtTabla.Columns)
                        {

                            if (dataColumn.ColumnName.Equals(objModApiDataSource.TextField))
                            {
                                dr[dataColumn.ColumnName] = "Seleccione...";
                            }
                            //else if (dataColumn.ColumnName.Equals(objModApiDataSource.ValueField))
                            //{
                            //    dr[dataColumn.ColumnName] = -2;
                            //}
                            else
                            {
                                dr[dataColumn.ColumnName] = 0;
                            }
                            
                        }
                        dtTabla.Rows.Add(dr);
                        objModApiDataSource.DataSource = dtTabla;
                        sqlLeer.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                objModErrorBase.MsgError = ex.Message;
            }
            finally
            {
                if (sqlLeer != null) sqlLeer.Dispose();
            }

            return objModApiDataSource;
        }


        public clsModApiDataSource GetDataSource(int IdHijo, int Value, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsModApiDataSource objModApiDataSource = new clsModApiDataSource();

            SqlParameter[] arrpar = 
            {
                new SqlParameter("@IdSeccionControl", SqlDbType.Int) {Value = IdHijo},
            };

            SqlParameter[] arrpar2 = {
                                         new SqlParameter("@IdValue", SqlDbType.Int){Value = Value},
                                         new SqlParameter("@IdSeccionControl", SqlDbType.Int) {Value = IdHijo},

                                     };

            DataTable dt = new DataTable();
            SqlDataReader dr = null;
            try
            {

                dr = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdEncDataSourceCargar", arrpar);
                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        objModApiDataSource.TextField = dr["TextField"] != DBNull.Value
                            ? dr["TextField"].ToString()
                            : string.Empty;
                        objModApiDataSource.ValueField = dr["ValueField"] != DBNull.Value
                            ? dr["ValueField"].ToString()
                            : string.Empty;
                    }
                    dr.Close();

                    dr = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure,
                        "NzControles.SpdApiDataSourceControlHijo", arrpar2);

                    if (dr.HasRows)
                    {
                        dt.Load(dr);

                        objModApiDataSource.DataSource = dt;
                        dr.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModApiDataSource;
        }
    }
}


