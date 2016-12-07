using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using Microsoft.ApplicationBlocks.Data;
using ClbDatControles.Common;

namespace ClbDatControles
{
    public class clsDatGetDBTablesName
    {
        private string _StrConnection;

        public clsDatGetDBTablesName()
        {
            this._StrConnection = DataBaseConnection.Instance.GetConnectionString();
        }

        public List<clsModGetDBTablesName> getTablesDBName(out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();;

            List<clsModGetDBTablesName> lstModGetDbTablesName = new List<clsModGetDBTablesName>();
            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure,
                    "NzControles.spdGetDBTablesName");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        clsModGetDBTablesName objModGetDbTablesName = new clsModGetDBTablesName();
                        objModGetDbTablesName.tableName = dr["TABLE_NAME"].ToString();
                        lstModGetDbTablesName.Add(objModGetDbTablesName);
                    }
                }

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return lstModGetDbTablesName;
        }
    }
}
