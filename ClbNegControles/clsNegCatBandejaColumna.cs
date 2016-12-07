using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using ClbDatControles;

namespace ClbNegControles
{
    public class clsNegCatBandejaColumna
    {
        private readonly clsDatCatBandejaColumna _objDatCatBandejaColumna = null;
        public clsNegCatBandejaColumna()
        {
            _objDatCatBandejaColumna = new clsDatCatBandejaColumna();
        }

        public clsModErrorBase AgregarBandejaColumna(clsModCatBandejaColumna bandejaColumna)
        {
            return _objDatCatBandejaColumna.AgregarBandejaColumna(bandejaColumna);
        }

        public ICollection<clsModCatBandejaColumna> CargarBandejaColumna(out clsModErrorBase objErrorBase, int intIdEncConfBandeja)
        {
            return _objDatCatBandejaColumna.CargaPagBandejaColumna(out objErrorBase, intIdEncConfBandeja);
        }
        public clsModCatBandejaColumna CargarBandejaColumnaPorId(out clsModErrorBase objModErrorBase, int intIdBandejaColumna)
        {
            return _objDatCatBandejaColumna.CargarBandejaColumnaPorId(out objModErrorBase, intIdBandejaColumna);
        }
        public clsModErrorBase ActualizarBandejaColumna(clsModCatBandejaColumna bandejaColumna)
        {
            return _objDatCatBandejaColumna.ActualizarBandejaColumna(bandejaColumna);
        }

        public clsModErrorBase EliminarBandejaColumna(clsModCatBandejaColumna bandejaColumna)
        {
            return _objDatCatBandejaColumna.EliminarBandejaColumna(bandejaColumna);
        }

        public clsModErrorBase CambiarOrden(clsModCatBandejaColumna objCatBandejaColumna)
        {
            clsDatCatBandejaColumna objDatCatBandejaColumna = new clsDatCatBandejaColumna();
            return objDatCatBandejaColumna.CambiarOrden(objCatBandejaColumna);
        }
    }
}
