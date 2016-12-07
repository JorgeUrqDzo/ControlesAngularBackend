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
   public class clsDatCatMenuIcono
    {
        private string _strConexion;

         public clsDatCatMenuIcono()
        {
            this._strConexion = DataBaseConnection.Instance.GetConnectionString();
        }

         private clsModCatMenuIcono LlenarObjetoPaginacion(SqlDataReader sqlDataReader)
        {
             clsModCatMenuIcono objModCatMenuIcono = new clsModCatMenuIcono()
            {
                 IdMenuIcono = (int)(sqlDataReader["IdMenu"] != DBNull.Value ? sqlDataReader["IdMenu"] : 0),
                Icono = (string)(sqlDataReader["Icono"] != DBNull.Value ? sqlDataReader["Icono"] : string.Empty)
          
             };
             return objModCatMenuIcono;
        }
         //public clsModCatMenuIcono CargarIconos(int IdMenuIcono, out clsModErrorBase objModErrorBase)
         //{
         //    objModErrorBase = new clsModErrorBase();
         //    clsModCatMenuIcono objModCatMenuIcono = null;
         //    SqlParameter[] arrPar = new SqlParameter[1];
         //    arrPar[0] = new SqlParameter("IdMenuIcono", SqlDbType.Int) { Value = IdMenuIcono };
         //    SqlDataReader leer = null;
         //    try
         //    {

         //        leer = SqlHelper.ExecuteReader(_strConexion, CommandType.StoredProcedure, "SpdCatMenuIconoCargarXId", arrPar);
         //        if (leer != null)
         //        {
         //            while (leer.Read())
         //            {
         //                objModCatMenuIcono = this.LlenarObjetoPaginacion(leer);

         //            }
         //        }
         //    }
         //    catch (Exception ex)
         //    {
         //        objModErrorBase.MsgError = ex.Message;
         //    }
         //    finally
         //    {
         //        if (leer != null)
         //        {
         //            leer.Close();
         //        }
         //    }
         //    return objModCatMenuIcono;
         //}

         public List<clsModCatMenuIcono> CargarIconos(int IdMenuIcono, out clsModErrorBase objModErrorBase)
         {
             objModErrorBase = new clsModErrorBase();
             clsModCatMenuIcono objModCatMenuIcono = null;
             List<clsModCatMenuIcono> lstModCatMenuIcono = new List<clsModCatMenuIcono>();

             SqlParameter[] arrpar = { new SqlParameter("IdMenuIcono", SqlDbType.Int) { Value = IdMenuIcono } };


             SqlDataReader leer = null;
           
             try
             {
                 leer = SqlHelper.ExecuteReader(this._strConexion, CommandType.StoredProcedure, "NzControles.SpdCatMenuIconoCargarXId", arrpar);
                 if (leer != null)
                 {
                     while (leer.Read())
                     {
                         objModCatMenuIcono = this.LlenarObjetoPaginacion(leer);

                     }
                 }
             }
             catch (Exception ex)
             {
                 objModErrorBase.MsgError = ex.Message;
             }

             return lstModCatMenuIcono;
         }
    }
}
