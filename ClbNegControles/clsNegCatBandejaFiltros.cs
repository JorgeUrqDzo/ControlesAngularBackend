using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using ClbDatControles;

namespace ClbNegControles
{
    public class clsNegCatBandejaFiltros
    {
        private readonly clsDatCatBandejaFiltros _objDatCatBandejaFiltros = null;
        public clsNegCatBandejaFiltros()
        {
            _objDatCatBandejaFiltros = new clsDatCatBandejaFiltros();
        }
        public clsModErrorBase AgregarBandejaFiltro(clsModCatBandejaFiltros bandejaFiltros)
        {
            return _objDatCatBandejaFiltros.AgregarBandejaFiltros(bandejaFiltros);
        }
        public ICollection<clsModCatBandejaFiltros> CargarBandejaFiltros(out clsModErrorBase objErrorBase, string strTextoBusqueda, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            return _objDatCatBandejaFiltros.CargaPagBandejaFiltros(out objErrorBase, strTextoBusqueda, out objModPaginacion, intPagina, intRegistrosPorPagina);
        }
        public clsModCatBandejaFiltros CargarBandejaFiltrosPorId(out clsModErrorBase objModErrorBase, int intIdBandejaColumna)
        {
            return _objDatCatBandejaFiltros.CargarBandejaFiltrosPorId(out objModErrorBase, intIdBandejaColumna);
        }
        public clsModErrorBase ActualizarBandejaFiltros(clsModCatBandejaFiltros bandejaColumna)
        {
            return _objDatCatBandejaFiltros.ActualizarBandejaFiltros(bandejaColumna);
        }
        public clsModErrorBase EliminarBandejaFiltros(clsModCatBandejaFiltros bandejaFiltros)
        {
            return _objDatCatBandejaFiltros.EliminarBandejaFiltros(bandejaFiltros);
        }
    }
}
