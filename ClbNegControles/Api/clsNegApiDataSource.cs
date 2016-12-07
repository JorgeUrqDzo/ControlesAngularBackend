using ClbModControles;
using ClbModControles.Api;
using ClbDatControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbNegControles.Api
{
    public class clsNegApiDataSource
    {
        public clsModApiDataSource Cargar(int intIdSeccionControl, out clsModErrorBase objModErrorBase)
        {
            clsDatApiDataSource objDatApiDataSource = new clsDatApiDataSource();
            return objDatApiDataSource.Cargar(intIdSeccionControl, out objModErrorBase);

        }

    }
}
