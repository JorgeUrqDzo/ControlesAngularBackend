using ClbDatControles;
using ClbModControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ClbNegControles
{
    public class clsNegEncProceso
    {

        #region Paginacion
        public ICollection<clsModEncProceso> CargarEncProceso(out clsModErrorBase objErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            clsDatEncProceso objDatEncProceso = new clsDatEncProceso();

            ICollection<clsModEncProceso> lstBandejas = objDatEncProceso.CargarBandejas(out objErrorBase, strTextoBuscar, out objModPaginacion, intPagina, intRegistrosPorPagina);

            return lstBandejas;

        }
        #endregion

        #region CargarXId
        public clsModEncProceso CargarXId(clsModEncProceso objModEncProceso, out clsModErrorBase objModErrorBase)
        {
            clsDatEncProceso objDatEncProceso = new clsDatEncProceso();
            clsDatDetActividad objDatDetActividad = new clsDatDetActividad();
            clsModEncProceso capaModelo = new clsModEncProceso();

            capaModelo = objDatEncProceso.CargarXId(objModEncProceso, out objModErrorBase);

            capaModelo.lstModDetActividad = objDatDetActividad.Cargar(objModEncProceso.IdEncProceso, out objModErrorBase);
            return capaModelo;
        }
        #endregion

        #region Actividades Por Tipo Proceso

        public List<clsModCatActividad> ActividadesXTipoProceso(clsModErrorBase objModErrorBase)
        {
            clsDatEncProceso objDatEncProceso = new clsDatEncProceso();

            return objDatEncProceso.ActividadesXTipoProceso(objModErrorBase);
        }
        #endregion

        #region Guardar Cambios

        public clsModErrorBase Guardar(clsModEncProceso objModEncProceso)
        {
            clsModErrorBase objModErrorBase = null;
            clsDatEncProceso objDatEncProceso = new clsDatEncProceso();
            clsDatDetActividad objDatDetActividad = new clsDatDetActividad();
            int idEncProceso = 0;
            try
            {
                using (TransactionScope objTransactionScope = new TransactionScope())
                {
                    if (objModEncProceso.IdEncProceso == 0)
                    {
                        //llamar Funcion Guardar capa Datos EncProceso para insertar un registro nuevo
                        objModErrorBase = objDatEncProceso.Guardar(objModEncProceso);
                        idEncProceso = objModErrorBase.Id;
                    }
                    else
                    {
                        //llamar funcion Actualizar capa Datos EncProceso
                        objModErrorBase = objDatEncProceso.Actualizar(objModEncProceso);
                        idEncProceso = objModEncProceso.IdEncProceso;
                    }


                    if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
                    {
                        throw new Exception(objModErrorBase.MsgError);
                    }

                    //llamar funcion Guardar capa Datos DetActividad
                    objModErrorBase = objDatDetActividad.Guardar(objModEncProceso.lstModDetActividad, idEncProceso);

                    if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
                    {
                        throw new Exception(objModErrorBase.MsgError);
                    }
                    objTransactionScope.Complete();
                }
            }
            catch (Exception exception)
            {
                objModErrorBase.MsgError = exception.Message;
            }
            return objModErrorBase;
        }
        #endregion
    }
}
