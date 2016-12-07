using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using ClbDatControles.Common;
using ClbModControles;
using System.Collections.ObjectModel;

namespace ClbDatControles
{
    public class clsDatCatTipoControl
    {
        
        private string _StrConnection;

        public clsDatCatTipoControl()
        {
            this._StrConnection = DataBaseConnection.Instance.GetConnectionString();
        }

        /// <summary>
        /// Carga todos los controles pertenecientes a la seccion
        /// </summary>
        /// <param name="intIdSeccion">id De la seccion a cargar</param>
        /// <param name="objModErrorBase">Objeto de error basse</param>
        /// <returns>devuelve el listado de controles pertenecientes a una seccion</returns>
        public ICollection<clsModCatTipoControl> Cargar(out clsModErrorBase objModErrorBase)
        {

            ICollection<clsModCatTipoControl> lstTipoControl = new Collection<clsModCatTipoControl>();

            clsModCatTipoControl objModTipoControl = null;
            objModErrorBase = new clsModErrorBase();
            SqlDataReader sqlLeer = null;
            try
            {
                sqlLeer = SqlHelper.ExecuteReader(this._StrConnection, CommandType.StoredProcedure, "NzControles.SpdCatTipoControlCargar");

                if (sqlLeer.HasRows)
                {
                    while (sqlLeer.Read())
                    {
                        objModTipoControl = this.GetTipoControl(sqlLeer);
                        lstTipoControl.Add(objModTipoControl);

                    }
                }

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            finally
            {
                if (sqlLeer != null)
                {
                    sqlLeer.Close();
                }
            }
            return lstTipoControl;
        }


        public clsModCatTipoControl GetTipoControl(SqlDataReader sqlLeer)
        {

            clsModCatTipoControl objModTipoControl = null;

            objModTipoControl = new clsModCatTipoControl()
            {
                IdTipoControl = (sqlLeer["IdTipoControl"] != DBNull.Value ? int.Parse(sqlLeer["IdTipoControl"].ToString()) : 0),
                TipoControl = sqlLeer["TipoControl"] != DBNull.Value ? sqlLeer["TipoControl"].ToString() : string.Empty,
                CodTipoControl = sqlLeer["CodTipoControl"] != DBNull.Value ? sqlLeer["CodTipoControl"].ToString() : string.Empty,
                DataSource = (sqlLeer["DataSource"] != DBNull.Value ? bool.Parse(sqlLeer["DataSource"].ToString()) : false)
            };


            return objModTipoControl;
        }

    }
}
