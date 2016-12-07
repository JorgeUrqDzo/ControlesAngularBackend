using ClbModControles;
using ClbDatControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbNegControles
{
    public class clsNegCatTipoProceso
    {
        #region Metodo para llamar al CargarPorId
        /// <summary>
        /// Metodo para mandar a llamar el CargarPorId en la capa de datos.
        /// </summary>
        /// <param name="IdTipoProceso">Id del TipoProceso que se esta buscando.</param>
        /// <param name="objModErrorBase">Objeto de error base.</param>
        /// <returns>Regresa el objeto TipoProceso que coincidio con el id.</returns>
        public clsModCatTipoProceso CargarPorId(int IdTipoProceso, out clsModErrorBase objModErrorBase)
        {
            
            clsDatCatTipoProceso objDatCatTipoProceso = new clsDatCatTipoProceso();
            clsModCatTipoProceso objModCatTipoProceso = objDatCatTipoProceso.CargarPorId(IdTipoProceso, out objModErrorBase);

            return objModCatTipoProceso;
        }
        #endregion

        #region Metodo para llamar a CargarTodos
        /// <summary>
        /// Metodo para mandar a llamar la funcion CargarTodos de la capa de datos.
        /// </summary>
        /// <param name="objModErrorBase">Objeto de error base.</param>
        /// <returns>Regresa una lista con todos los registros de CatTipoProceso.</returns>
        public ICollection<clsModCatTipoProceso> CargarTodos(out clsModErrorBase objModErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
           
            clsDatCatTipoProceso objDatCatTipoProceso = new clsDatCatTipoProceso();
            ICollection<clsModCatTipoProceso> lstModCatTipoProceso = objDatCatTipoProceso.CargarTodos(out objModErrorBase, strTextoBuscar, out objModPaginacion, intPagina, intRegistrosPorPagina);

            return lstModCatTipoProceso;
        }
        #endregion

        #region Metodo para llamar a Agregar
        /// <summary>
        /// Metodo que manda a llamar el agregar de la capa de datos.
        /// </summary>
        /// <param name="objModCatTipoProceso">Objeto CatTipoProceso con los datos a agregar.</param>
        /// <returns>Regresa un objeto clsModErrorBase con el id si Agrego correctamente o un mensaje de error si fallo.</returns>
        public clsModErrorBase Agregar(clsModCatTipoProceso objModCatTipoProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsDatCatTipoProceso objDatCatTipoProceso = new clsDatCatTipoProceso();

            try
            {
                if (objModCatTipoProceso.IdTipoProceso > 0)
                {
                    objModErrorBase = objDatCatTipoProceso.Actualizar(objModCatTipoProceso);
                }
                else
                {
                    objModErrorBase = objDatCatTipoProceso.Agregar(objModCatTipoProceso);
                }

                if(!String.IsNullOrEmpty(objModErrorBase.MsgError))
                {
                    throw new Exception(objModErrorBase.MsgError);
                }
            }
            catch (Exception ex)
            {
                objModErrorBase.MsgError = ex.Message;
            }
            return objModErrorBase;
        }
        #endregion


        #region ActualizarEstado
        public clsModErrorBase ActualizarEstado(clsModCatTipoProceso objModCatTipoProceso)
        {
            clsDatCatTipoProceso objDatCatTipoProceso = new clsDatCatTipoProceso();
            return objDatCatTipoProceso.ActualizarEstado(objModCatTipoProceso);
        }
        #endregion

        #region Metodo para llamar a Actualizar
        /// <summary>
        /// Metodo que manda a llamar al actualizar de la capa de datos.
        /// </summary>
        /// <param name="objModCatTipoProceso">Objeto CatTipoProceso con los datos a actualizar.</param>
        /// <returns>Regresa un objeto clsModErrorBase con el id si Agrego correctamente o un mensaje de error si fallo.</returns>
        public clsModErrorBase Actualizar(clsModCatTipoProceso objModCatTipoProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsDatCatTipoProceso objDatCatTipoProceso = new clsDatCatTipoProceso();

            objModErrorBase = objDatCatTipoProceso.Actualizar(objModCatTipoProceso);
            return objModErrorBase;
        }
        #endregion

        #region Metodo para llamar a desactivar
        /// <summary>
        /// Metodo que manda a llamar al desactivar de la capa de datos.
        /// </summary>
        /// <param name="objModCatTipoProceso">Objeto CatTipoProceso con el id del TipoProceso a desactivar.</param>
        /// <returns>Regresa un objeto clsModErrorBase con el id si Agrego correctamente o un mensaje de error si fallo.</returns>
        public clsModErrorBase Desactivar(clsModCatTipoProceso objModCatTipoProceso)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsDatCatTipoProceso objDatCatTipoProceso = new clsDatCatTipoProceso();

            objModErrorBase = objDatCatTipoProceso.Desactivar(objModCatTipoProceso);
            return objModErrorBase;
        }
        #endregion
    }
}
