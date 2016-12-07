using System;
using System.Collections.Generic;
using ClbDatControles;
using ClbModControles;
using System.Transactions;

namespace ClbNegControles
{
    public class clsNegCatSecciones
    {
        private readonly clsDatCatSecciones _objDatCatSecciones;
        private readonly clsNegCatSeccionControl _objNegCatSeccionControl;

        public clsModErrorBase AgregarGrupo(clsModCatSecciones objModSecciones)
        {
            clsDatCatSecciones objDatSecciones = new clsDatCatSecciones();
            return objDatSecciones.AgregarGrupo(objModSecciones);
        }



        public clsNegCatSecciones()
        {
            _objDatCatSecciones = new clsDatCatSecciones();
            _objNegCatSeccionControl = new clsNegCatSeccionControl();
        }
        #region Guarda o actualiza sección en base de datos
        /// <summary>
        /// Metodo para guardar una seccion en base de datos
        /// </summary>
        /// <param name="objSecciones">Objetos que seran guardados en base de datos</param>
        /// <returns>Error en dado caso, sino sigue con el metodo</returns>
        public clsModErrorBase GuardarSeccion(clsModCatSecciones objSecciones)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                if (objSecciones.IdTipoSeccion == (int)TipoSeccion.Panel) objSecciones.IdGrupo = 0;
                if (objSecciones.IdSeccion > 0)
                {
                    objModErrorBase = _objDatCatSecciones.ActualizarSeccion(objSecciones);
                    objModErrorBase.Id = objSecciones.IdSeccion;
                }
                else
                {
                    objModErrorBase = _objDatCatSecciones.GuardarSeccion(objSecciones);
                }

                if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
                {
                    throw new Exception(objModErrorBase.MsgError);
                }
                
            }
            catch (Exception exception)
            {
                objModErrorBase.MsgError = exception.Message;
            }
            return objModErrorBase;
        }


        public clsModErrorBase Guardar(clsModCatSecciones objModCatSecciones)
        {
            clsModErrorBase objModErrorBase = null;
            try
            {
                using (TransactionScope objTransactionScope = new TransactionScope())
                {
                    //Guarda la seccion
                    objModErrorBase = GuardarSeccion(objModCatSecciones);

                    if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
                    {
                        throw new Exception(objModErrorBase.MsgError);
                    }
                    //Asigna id de seccion generado
                    objModCatSecciones.IdSeccion = objModErrorBase.Id;

                    objModErrorBase = _objNegCatSeccionControl.Guardar(objModCatSecciones);
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

        #region Metodo para obtener formularios con paginación
        /// <summary>
        /// Listado de secciones con paginación
        /// </summary>
        /// <param name="objModErrorBase">Error Base</param>
        /// <param name="strTextoBusqueda">Texto de busqueda de secciones</param>
        /// <param name="objModPaginacion">Modelo de paginación</param>
        /// <param name="intPagina">Número de pagina enviado</param>
        /// <param name="intRegistroPorPagina">Número de registros por paginador</param>
        /// <returns>Listado de secciones</returns>
        public ICollection<clsModCatSecciones> CargarPagSecciones(out clsModErrorBase objModErrorBase, string strTextoBusqueda, out clsModPaginacion objModPaginacion, int intPagina, int intRegistroPorPagina)
        {
            ICollection<clsModCatSecciones> lst = _objDatCatSecciones.CargarPagSecciones(out objModErrorBase, strTextoBusqueda, out objModPaginacion, intPagina, intRegistroPorPagina);
            return lst;
        }

        #endregion

        #region Metodo para obtener formularios por Id
        public clsModCatSecciones CargarSeccionPorId(out clsModErrorBase objModErrorBase, int intIdPrograma)
        {
            clsModCatSecciones objCatSeccion = _objDatCatSecciones.CargarSeccionPorId(out objModErrorBase, intIdPrograma);
            return objCatSeccion;
        }
        #endregion

        /// <summary>
        /// Actualiza el orden de la seccion enviada
        /// </summary>
        public clsModErrorBase ActualizarOrden(int intIdSeccion, int intOrden)
        {
            return _objDatCatSecciones.ActualizarOrden(intIdSeccion, intOrden);
        }
    }
}
