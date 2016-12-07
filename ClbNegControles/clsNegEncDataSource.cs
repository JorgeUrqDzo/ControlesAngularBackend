using ClbModControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;

namespace ClbNegControles
{
    public class clsNegEncDataSource
    {

        public clsModErrorBase Agregar(clsModEncDataSource objModEncDataSource)
        {
            clsDatEncDataSource objDatEncDataSource = new clsDatEncDataSource();
            if (!objModEncDataSource.BaseDatos) {
                objModEncDataSource.TextField = "TextField";
                objModEncDataSource.ValueField = "Value";
            }
            clsModErrorBase objModErrorBase = objDatEncDataSource.Agregar(objModEncDataSource);

            if (objModEncDataSource.Valores!= null && string.IsNullOrEmpty(objModErrorBase.MsgError))
            {
                clsNegDetDataSource objNegDetDataSource = new clsNegDetDataSource();
                objNegDetDataSource.Agregar(objModEncDataSource.Valores, objModErrorBase.Id);
            }


            return objModErrorBase;
        }



        public ICollection<clsModEncDataSource> Cargar(int intIdEncDataSource, out clsModErrorBase objModErrorBase)
        {
            clsDatEncDataSource objDatEncDataSource = new clsDatEncDataSource();

            return objDatEncDataSource.Cargar(intIdEncDataSource, out objModErrorBase);
        }

        public clsModEncDataSource CargarXControl(int intIdSeccionControl, out clsModErrorBase objModErrorBase)
        {
            clsDatEncDataSource objDatEncDataSource = new clsDatEncDataSource();

            return objDatEncDataSource.CargarXControl(intIdSeccionControl, out objModErrorBase);
        }


    }
}
