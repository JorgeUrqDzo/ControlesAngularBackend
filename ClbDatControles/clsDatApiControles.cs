using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using ClbModControles.Api;
using Microsoft.ApplicationBlocks.Data;

namespace ClbDatControles
{
    public class clsDatApiControles
    {
        private string strConexion = null;

        public clsDatApiControles()
        {
            this.strConexion = Common.DataBaseConnection.Instance.GetConnectionString();
        }

        public clsModErrorBase GuardarDatosFormulario(Hashtable objDatos, string Fk, int value)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            DataTable dt = new DataTable();

            dt.Columns.Add("IdControl", typeof(int));
            dt.Columns.Add("Valor", typeof(string));

            foreach (Object dato in objDatos.Keys)
            {
                dt.Rows.Add(int.Parse(dato.ToString()), objDatos[dato].ToString());
            }

            SqlParameter[] arrpar =
            {
                new SqlParameter("@tablaColumnas", SqlDbType.Structured) {Value = dt}, 
                new SqlParameter("@Fk", SqlDbType.VarChar) {Value = Fk}, 
                new SqlParameter("@ValorFk", SqlDbType.Int) {Value = value}
            };

            try
            {
                objModErrorBase.Id = int.Parse(SqlHelper.ExecuteScalar(this.strConexion, CommandType.StoredProcedure, "NzControles.spdApiControlesInsertar", arrpar).ToString());
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;

            }


            return objModErrorBase;
        }

        public Hashtable CargarDatosFormularioXId(out clsModErrorBase objModErrorBase, String Key, String UUID)
        {
            objModErrorBase = new clsModErrorBase();
            clsModApiControles objModApiControles = null;

            SqlParameter[] arrpar =
            {
                new SqlParameter("@Key",SqlDbType.VarChar) { Value = Key}, 
                new SqlParameter("@UUID",SqlDbType.VarChar) { Value = UUID}, 
            };

            SqlDataReader dr = null;
            DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();
            List<String> lstColumnName = new List<string>();

            try
            {
                dr = SqlHelper.ExecuteReader(this.strConexion, CommandType.StoredProcedure,
                    "NzControles.spdApiControlesCargar", arrpar);

                dt.Load(dr);

                foreach (DataColumn item in dt.Columns)
                {
                    string columnName = item.ColumnName;

                    lstColumnName.Add(columnName);
                }

                int x = lstColumnName.Count;

                foreach (DataRow datdaRow in dt.Rows)
                {
                    for (int i = 0; i < x; i++)
                    {
                        string column = lstColumnName.First();
                        ht.Add(column, datdaRow[i].ToString());
                        lstColumnName.Remove(column);
                    }
                }

            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            finally
            {
                if (dr != null) dr.Dispose();
            }

            return ht;
        }

        public clsModErrorBase ActualizarDatosFormulario(Hashtable objDatos, string key)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            DataTable dt = new DataTable();

            dt.Columns.Add("IdControl", typeof(int));
            dt.Columns.Add("Valor", typeof(string));

            foreach (Object dato in objDatos.Keys)
            {
                dt.Rows.Add(int.Parse(dato.ToString()), objDatos[dato].ToString());
            }

            SqlParameter[] arrpar =
            {
                new SqlParameter("@tablaColumnas", SqlDbType.Structured) {Value = dt}, 
                new SqlParameter("@Key", SqlDbType.VarChar) {Value = key}, 
            };

            try
            {
                SqlHelper.ExecuteNonQuery(this.strConexion, CommandType.StoredProcedure, "NzControles.spdApiControlesActualizar", arrpar);
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }

            return objModErrorBase;
        }

        public List<clsModApiRelaciones> OrdenarSecciones(Hashtable objDatos)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            DataTable dt = new DataTable();
            List<clsModApiRelaciones> lstModApiRelaciones = new List<clsModApiRelaciones>();

            dt.Columns.Add("IdControl", typeof(int));
            dt.Columns.Add("Valor", typeof(string));

            foreach (Object dato in objDatos.Keys)
            {
                dt.Rows.Add(int.Parse(dato.ToString()), objDatos[dato].ToString());
            }

            SqlParameter[] arrpar =
            {
                new SqlParameter("@tablaColumnas", SqlDbType.Structured) {Value = dt}, 
            };
            SqlDataReader dr = null;
            try
            {
               dr = SqlHelper.ExecuteReader(this.strConexion, CommandType.StoredProcedure, "NzControles.spdApiControlesOrdenar", arrpar);

                if (dr.HasRows)
                {
                    while (dr.Read()) 
                    {
                        clsModApiRelaciones objModApiRelaciones = new clsModApiRelaciones();

                        objModApiRelaciones.IdControl = int.Parse(dr["IdControl"].ToString());
                        objModApiRelaciones.IdSeccion = int.Parse(dr["IdSeccion"].ToString());
                        objModApiRelaciones.IsHijo = int.Parse(dr["Hijo"].ToString()) == 1 ? true : false;
                        objModApiRelaciones.IsPadre = int.Parse(dr["Padre"].ToString()) == 1 ? true : false; ;
                        objModApiRelaciones.KeyHijo = dr["KeyHijo"].ToString();
                        objModApiRelaciones.KeyPadre = dr["KeyPadre"].ToString();
                        objModApiRelaciones.Orden = int.Parse(dr["Orden"].ToString());
                        objModApiRelaciones.Valor = dr["Valor"].ToString();

                        lstModApiRelaciones.Add(objModApiRelaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;

            }

            return lstModApiRelaciones.OrderBy(x=> x.Orden).ToList();
        }
    }
}
