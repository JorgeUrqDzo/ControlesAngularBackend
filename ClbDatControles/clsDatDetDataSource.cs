using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using System.Collections.ObjectModel;

namespace ClbDatControles
{
    public class clsDatDetDataSource
    {


        private string _StrConnection;

        public clsDatDetDataSource()
        {
            this._StrConnection = ClbDatControles.Common.DataBaseConnection.Instance.GetConnectionString();
        }

        /// <summary>
        /// Agrega un nuevo registro en la tabla DetDataSource
        /// </summary>
        public clsModErrorBase Agregar(clsModDetDataSource objModDetDataSource)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();

            ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            lstSqlParameters.Add(new SqlParameter("@IdEncDataSource", SqlDbType.Int) { Value = objModDetDataSource.IdEncDataSource });
            lstSqlParameters.Add(new SqlParameter("@Value", SqlDbType.VarChar) { Value = objModDetDataSource.Value });
            lstSqlParameters.Add(new SqlParameter("@TextField", SqlDbType.VarChar) { Value = objModDetDataSource.TextField });
            try
            {

                SqlHelper.ExecuteNonQuery(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdDetDataSourceAgregar", lstSqlParameters.ToArray());

            }
            catch (Exception ex)
            {
                objModErrorBase = new clsModErrorBase();
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }





        public string CargarXControl(int intIdEncDataSource, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            string strValores = "";
            ICollection<SqlParameter> lstSqlParameters = new List<SqlParameter>();
            lstSqlParameters.Add(new SqlParameter("@IdEncDataSource", SqlDbType.Int) { Value = intIdEncDataSource });
            try
            {

                object objResultado = SqlHelper.ExecuteScalar(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdDetDataSourceCargarXId", lstSqlParameters.ToArray());
                strValores = objResultado.ToString();

            }
            catch (Exception ex)
            {
                objModErrorBase = new clsModErrorBase();
                objModErrorBase.MsgError = ex.Message;
            }

            return strValores;
        }


    }
}
