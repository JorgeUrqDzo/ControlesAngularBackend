using System;
using System.Collections.Generic;
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
    public class clsDatGetTableColumnsNames
    {
        private string _StrConnection;

        public clsDatGetTableColumnsNames()
        {
            this._StrConnection = DataBaseConnection.Instance.GetConnectionString();
        }

        public List<clsModGetTableColumnsNames> getTableColumnNames(out clsModErrorBase objModErrorBase, clsModGetTableColumnsNames objModGetTableColumnsNames)
        {
            String[] split = objModGetTableColumnsNames.columnName.Split('.');
            string tableSchema = split[0];
            string tableName = split[1];
            objModErrorBase = new clsModErrorBase();

            SqlParameter[] arrpar =
            {
                //new SqlParameter("@TableName", SqlDbType.VarChar) {Value = objModGetTableColumnsNames.columnName }, 
                new SqlParameter("@TableSchema", SqlDbType.VarChar) {Value = tableSchema }, 
                new SqlParameter("@TableName", SqlDbType.VarChar) {Value = tableName }, 
            };

            List<clsModGetTableColumnsNames> lstModGetTableColumnsNames = new List<clsModGetTableColumnsNames>();
            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure,
                    "NzControles.spdGetTableColumnsNames", arrpar);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clsModGetTableColumnsNames capaModelo = new clsModGetTableColumnsNames();
                        capaModelo.columnName = dr["COLUMN_NAME"].ToString();
                        lstModGetTableColumnsNames.Add(capaModelo);
                    }
                }

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstModGetTableColumnsNames;
        }
    }
}
