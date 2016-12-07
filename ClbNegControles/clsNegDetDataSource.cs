using ClbDatControles;
using ClbModControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbNegControles
{
    public class clsNegDetDataSource
    {
        /// <summary>
        /// Agrega los registros en la tabla de detDataSource
        /// </summary>
        public clsModErrorBase Agregar(string strValores, int intIdEncDataSource)
        {
            clsModErrorBase objModErrorBase = null;
            clsDatDetDataSource objDatDetDataSource = new clsDatDetDataSource();

            string[] arrValores = strValores.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (arrValores != null && arrValores.Length > 0)
            {
                foreach (string str in arrValores)
                {
                    objModErrorBase = objDatDetDataSource.Agregar(new clsModDetDataSource()
                    {
                        IdEncDataSource = intIdEncDataSource,
                        TextField = str,
                        Value = str
                    });

                    if (!String.IsNullOrEmpty(objModErrorBase.MsgError)) {
                        throw new Exception(objModErrorBase.MsgError);
                    }

                }
            }
            
            return objModErrorBase;
        }


        public string CargarXControl(int intIdEncDataSource, out clsModErrorBase objModErrorBase)
        {
            clsDatDetDataSource objDatDetDataSource = new clsDatDetDataSource();

            return objDatDetDataSource.CargarXControl(intIdEncDataSource, out objModErrorBase);
        }
    }
}
