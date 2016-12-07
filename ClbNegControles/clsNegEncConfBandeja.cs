using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles;

namespace ClbNegControles
{
    public class clsNegEncConfBandeja
    {
        #region Metodo para Agregar Formulario
        public clsModErrorBase AgregarFormulario(clsModEncConfBandeja objModEncConfBandeja)
        {
            if (chekSQL(objModEncConfBandeja.Consulta.ToString()))
            {
                clsDatEncConfBandeja objDatEncConfBandeja = new clsDatEncConfBandeja();
                if (objModEncConfBandeja.IdEncConfBandeja.ToString().Equals("0"))
                {
                    //Agregar Nuevo formulario
                    return objDatEncConfBandeja.AgregarFormulaio(objModEncConfBandeja);

                }
                else
                {
                    //Actualizar Formulario Existente
                    return objDatEncConfBandeja.Actualizar(objModEncConfBandeja);
                }
            }
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            objModErrorBase.MsgError = "Solo se permite consultas con SELECT";
            return objModErrorBase;
        }
        #endregion


        #region Metodo para Validar Consulta SQL
        public clsModErrorBase ValidarSQL(clsModEncConfBandeja objModEncConfBandeja)
        {
            clsDatEncConfBandeja objDatEncConfBandeja = new clsDatEncConfBandeja();
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            if (objModEncConfBandeja.IdTipoConsulta == 1)
            {
                if (chekSQL(objModEncConfBandeja.Consulta))
                {
                    return objDatEncConfBandeja.ValidarSQL(objModEncConfBandeja);
                }
            }
            else if (objModEncConfBandeja.IdTipoConsulta == 2)
            {
                if (chekSQL_SPD(objModEncConfBandeja.Consulta))
                {
                    return new clsModErrorBase();
                }
                else
                {
                    objModErrorBase.MsgError = "Solo se permiten llamadas a Store Procedures";
                    return objModErrorBase;
                }
            }
            
            objModErrorBase.MsgError = "Solo se permiten Consultas con SELECT";
            return objModErrorBase;

        }
        #endregion


        #region Metodo para Checkar palabras claves en SQL
        private bool chekSQL(string sql)
        {
            string[] array = {
                "DELETE",
                "INSERT",
                "ALTER",
                "DROP",
                "CREATE",
                "TRUNCATE",
                "REPLACE",
                "UPDATE"
            };

            foreach (string i in array)
            {
                if (sql.ToUpper().Contains(i))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Metodo para Checkar palabras claves en STORE PROCEDURE
        private bool chekSQL_SPD(string sql)
        {
            string[] array = {
                "DELETE",
                "INSERT",
                "ALTER",
                "DROP",
                "CREATE",
                "TRUNCATE",
                "REPLACE",
                "UPDATE",
                "SELECT"
            };

            foreach (string i in array)
            {
                if (sql.ToUpper().Contains(i))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion


        #region Metodo para Cargar por ID
        public clsModEncConfBandeja CargarXId(clsModEncConfBandeja objModEncConfBandeja, out clsModErrorBase objModErroBase)
        {
            int id = int.Parse(objModEncConfBandeja.IdEncConfBandeja.ToString());

            clsDatEncConfBandeja objDatEncConfBandeja = new clsDatEncConfBandeja();
            return objDatEncConfBandeja.CargarXId(id, out objModErroBase);
        }
        #endregion  

        #region Metodo para obtener nombres de las columnas
        public List<clsModEncConfBandeja> NombreColumnas(clsModEncConfBandeja objModEncConfBandeja, out clsModErrorBase objModErrorBase)
        {
            objModErrorBase = new clsModErrorBase();
            List<clsModEncConfBandeja> lstModEncConfBandeja = new List<clsModEncConfBandeja>();
            clsDatEncConfBandeja objDatEncConfBandeja = new clsDatEncConfBandeja();

            lstModEncConfBandeja = objDatEncConfBandeja.NombreColumna(int.Parse(objModEncConfBandeja.IdEncConfBandeja.ToString()), out objModErrorBase);

            return lstModEncConfBandeja;
        }
        #endregion
    }
}
