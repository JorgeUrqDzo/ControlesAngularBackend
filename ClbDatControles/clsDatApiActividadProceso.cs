using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using ClbModControles.Api;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace ClbDatControles
{
   public class clsDatApiActividadProceso
    {
        private string strConexion = "";

        public clsDatApiActividadProceso()
        {
            this.strConexion = Common.DataBaseConnection.Instance.GetConnectionString();
        }
        private clsModApiActividadProceso LlenarObjeto(SqlDataReader sqlDataReader)
        {
            clsModApiActividadProceso objModApiActividadProceso = new clsModApiActividadProceso()
            {
              
                IdTablaDocumento = (int)(sqlDataReader["IdTablaDocumento"] != DBNull.Value ? sqlDataReader["IdTabladocumento"] : 0),
                IdDocumento = (int)(sqlDataReader["IdDocumento"] != DBNull.Value ? sqlDataReader["IdDocumento"] : 0),
                IdEncProceso = (int)(sqlDataReader["IdEncProceso"] != DBNull.Value ? sqlDataReader["IdEncProceso"] : 0),
                IdEncActividad = (int)(sqlDataReader["IdEncActividad"] != DBNull.Value ? sqlDataReader["IdEncActividad"] : 0),
                Aprobado = (bool)(sqlDataReader["Aprobado"] != DBNull.Value ? sqlDataReader["Aprobado"] : bool.FalseString)

            };
            return objModApiActividadProceso;
        }

        public clsModApiActividadProceso CargarActividades(int IdTablaDocumento, int IdDocumento, int IdEncActividad, int IdEncProceso, bool Aprobado, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            clsModApiActividadProceso objModApiActividadProceso = null;
            SqlParameter[] arParms = new SqlParameter[6];
            arParms[0] = new SqlParameter("@IdTablaDocumento", SqlDbType.Int);
            arParms[0].Value = IdTablaDocumento;
            arParms[1] = new SqlParameter("@IdDocumento", SqlDbType.Int);
            arParms[1].Value = IdDocumento;
            arParms[2] = new SqlParameter("@IdEncActividad", SqlDbType.Int);
            arParms[2].Value = IdEncActividad;
            arParms[3] = new SqlParameter("@IdEncProceso", SqlDbType.Int);
            arParms[3].Value = IdEncProceso;
            arParms[4] = new SqlParameter("@Aprobado", SqlDbType.Bit);
            arParms[4].Value = Aprobado;
            arParms[5] = new SqlParameter("@IdUsuario", SqlDbType.Int);
            arParms[5].Value = 1;

            SqlDataReader leer = null;
            try
            {

                leer = SqlHelper.ExecuteReader(strConexion, CommandType.StoredProcedure, "NzControles.spdCatApiActividadProcesoCargar", arParms);
                objModErrorBase.MsgError = "Estado Actualizado";
                if (leer != null)
                {
                    while (leer.Read())
                    {
                        objModApiActividadProceso = this.LlenarObjeto(leer);

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
            return objModApiActividadProceso;
        }

    }
}
