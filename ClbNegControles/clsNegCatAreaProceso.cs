using ClbDatControles;
using ClbModControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbNegControles
{
    public class clsNegCatAreaProceso
    {
        #region Cargar
        public List<clsModCatAreaProceso> Cargar(out clsModErrorBase objModErroBase)
        {
            clsDatCatAreaProceso objCatAreaProceso = new clsDatCatAreaProceso();

            return objCatAreaProceso.CargarAreaProceso(out objModErroBase);
        }
        #endregion

        #region Crear
        public clsModErrorBase Crear(clsModCatAreaProceso objModCatAreaProceso)
        {
            clsDatCatAreaProceso objDatCatAreaProceso = new clsDatCatAreaProceso();

            int id = int.Parse(objModCatAreaProceso.IdAreaProceso.ToString());

            if (id == 0)
            {
                //Crear Nuevo
                return objDatCatAreaProceso.Crear(objModCatAreaProceso);
            }
            else
            {
                //Actualizar Existente
                return objDatCatAreaProceso.Actualizar(objModCatAreaProceso);
            }
        }
        #endregion

        #region CargarXId
        public clsModCatAreaProceso CargarXId(clsModCatAreaProceso objModCatAreaProceso, out clsModErrorBase objModErrorBase)
        {
            int id = int.Parse(objModCatAreaProceso.IdAreaProceso.ToString());

            clsDatCatAreaProceso objNegCatAreaProceso = new clsDatCatAreaProceso();
            return objNegCatAreaProceso.CargarXId(id, out objModErrorBase);
        }
        #endregion

        #region Actualizar
        public clsModErrorBase Actualizar(clsModCatAreaProceso objModCatAreaProceso)
        {
            clsDatCatAreaProceso objDatCatAreaProceso = new clsDatCatAreaProceso();
            return objDatCatAreaProceso.Actualizar(objModCatAreaProceso);
        }
        #endregion

        #region Paginacion
        public ICollection<clsModCatAreaProceso> CargarCatAreaProceso(out clsModErrorBase objErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            clsDatCatAreaProceso    objDatCatAreaProceso = new clsDatCatAreaProceso();

            ICollection<clsModCatAreaProceso> lstBandejas = objDatCatAreaProceso.CargarBandejas(out objErrorBase, strTextoBuscar, out objModPaginacion, intPagina, intRegistrosPorPagina);

            return lstBandejas;

        }
        #endregion

        #region ActualizarEstado
        public clsModErrorBase ActualizarEstado(clsModCatAreaProceso objModCatAreaProceso)
        {
            clsDatCatAreaProceso objDatCatAreaProceso = new clsDatCatAreaProceso();
            return objDatCatAreaProceso.ActualizarEstado(objModCatAreaProceso);
        }
        #endregion

    }
}
