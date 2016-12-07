using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles;
using ClbModControles.Api;

namespace ClbNegControles.Api
{
    public class clsNegApiBandeja
    {
        clsDatApiBandeja _objBandeja = null;
        public clsNegApiBandeja()
        {
            _objBandeja = new clsDatApiBandeja();
        }

        public clsModApiBandeja CargarCampoConsulta(string strUUID, List<clsModApiFiltros> listaFiltros, int pagina, out clsModErrorBase objModErrorBase, out clsModPaginacion objModPaginacion)
        {
            return _objBandeja.CargarCampoConsulta(strUUID, pagina, listaFiltros, out objModErrorBase, out objModPaginacion);
        }
        public string Cargar(string strUUID, out clsModErrorBase objModErrorBase)
        {
            return _objBandeja.Cargar(strUUID, out objModErrorBase);
        }
    }
}
