using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles;
using System.Transactions;
using ClbModControles.Api;

namespace ClbNegControles
{
    public class clsNegCatFormularios
    {
        private readonly clsDatCatFormularios _objDatCatFormularios;

        /// <summary>
        /// Constructor interno de la clase de datos de formularios
        /// </summary>
        public clsNegCatFormularios()
        {
            _objDatCatFormularios = new clsDatCatFormularios();
        }

        
        #region Metodo para guardar Formulario en BD
        /// <summary>
        /// Metodo que permite guardar formularios en base de datos
        /// </summary>
        /// <param name="objFormularios">Objetos que seran guardados en base de datos</param>
        /// <returns>Error en dado caso que la transacción no sera exitosa</returns>
        public clsModErrorBase GuardarFormulario(clsModCatFormularios objFormularios)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            //clsDatCatFormularios objDatCatFormularios = new clsDatCatFormularios();
            try
            {
                if (objFormularios.IdFormulario > 0)
                {
                    objModErrorBase = _objDatCatFormularios.Actualizar(objFormularios);
                }
                else
                {
                    objModErrorBase = _objDatCatFormularios.Agregar(objFormularios);
                    ///Se asigna el id del formulario generado
                    objFormularios.IdFormulario = objModErrorBase.Id;
                }


                if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
                {
                    throw new Exception(objModErrorBase.MsgError);
                }
                else
                {

                    if (objFormularios.IdFormulario > 0)
                    {
                        //objFormularios.
                        objFormularios.UUID = CargarUUIDXId(out objModErrorBase, objFormularios.IdFormulario);
                        //Se devuelve el id del formulario obtenido en el metodo anterior
                        objModErrorBase.Id = objFormularios.IdFormulario;
                    }
                }
            }
            catch (Exception exception)
            {
                objModErrorBase.MsgError = exception.Message;
            }
            return objModErrorBase;
        }
        #endregion

        #region Metodo para actualizar datos del Formulario en BD
        /// <summary>
        /// Metodo que permite actualizar en BD
        /// </summary>
        /// <param name="objModCatFormulario">Objetos que seran actualizadas en BD</param>
        /// <returns>Error en dado caso de que exista</returns>
        public clsModErrorBase ActualizarFormulario(clsModCatFormularios objModCatFormulario)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            try
            {
                if (objModCatFormulario != null)
                {
                    if (objModCatFormulario.IdFormulario != 0)
                    {
                        
                    }
                    else
                    {
                        objModErrorBase = _objDatCatFormularios.Actualizar(objModCatFormulario);
                    }
                }
            }
            catch (Exception exception)
            {
                objModErrorBase.MsgError = exception.Message;
            }
            return objModErrorBase;
        }
        #endregion

        #region Metodo para obtener formularios de BD
        public ICollection<clsModCatFormularios> CargarFomularios(out clsModErrorBase objErrorBase, string strTextoBusqueda, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            clsDatCatFormularios objDatCatFormulario = new clsDatCatFormularios();

            ICollection<clsModCatFormularios> lst = objDatCatFormulario.CargarFomularios(out objErrorBase, strTextoBusqueda, out objModPaginacion, intPagina, intRegistrosPorPagina);

            return lst;
            //return objDatCatFormulario.CargarFomularios(out objErrorBase);
        }
        #endregion

        #region Metodo para obtener formularios con paginación
        /// <summary>
        /// Listado de formularios con paginación
        /// </summary>
        /// <param name="objModErrorBase">Error base</param>
        /// <param name="strTextoBusqueda">Texto de búsqueda por formulario</param>
        /// <param name="objModPaginacion">Modelo de paginación</param>
        /// <param name="intPagina">Número de pagina enviado</param>
        /// <param name="intRegistroPorPagina">Número de registros contemplados por paginador</param>
        /// <returns></returns>
        //public ICollection<clsModCatFormularios> CargarPagFormularios(out clsModErrorBase objModErrorBase, string strTextoBusqueda, out clsModPaginacion objModPaginacion, int intPagina, int intRegistroPorPagina)
        //{
        //    clsDatCatFormularios objDatCatFormularios = new clsDatCatFormularios();

        //    ICollection<clsModCatFormularios> lst = objDatCatFormularios.CargarFomularios(out objModErrorBase, strTextoBusqueda, out objModPaginacion, intPagina, intRegistroPorPagina);
        //    return lst;
        //}
        #endregion

        #region Metodo para obtener formularios por ID
        public clsModCatFormularios CargarFormularioPorId(out clsModErrorBase objModErrorBase, int intIdPrograma)
        {
            clsModCatFormularios objCatFormulario = _objDatCatFormularios.CargarFormularioPorId(out objModErrorBase, intIdPrograma);
            return objCatFormulario;
        }

        #endregion


        public string CargarArbol(int intIdFormulario, out clsModErrorBase objModErrorBase)
        {
            string strArbol = "";
            string strNodo = "";
            string strSubNodo = "";
            int intCont = 0, intContS = 0;

            clsModApiFormulario objModApiFormulario = _objDatCatFormularios.CargarArbol(intIdFormulario, out objModErrorBase);
            if (objModApiFormulario != null)
            {
                strArbol = "[{";
                strArbol += "\"text\":\"" + objModApiFormulario.Nombre + "\",";
                strArbol += "\"idType\":\"" + 0 + "\",";
                strArbol += "\"href\":\"" + intIdFormulario + "\"";

                
                if (objModApiFormulario.LstModApiSeccion != null && objModApiFormulario.LstModApiSeccion.Count > 0)
                {
                    strNodo = "\"nodes\":[";
                    
                    foreach (clsModApiSeccion objModApiSeccion in objModApiFormulario.LstModApiSeccion)
                    {
                        strNodo += (intCont == 0) ? "{" : ",{";
                        strNodo += "\"text\":\"" + objModApiSeccion.Nombre+ "\",";
                        strNodo += "\"order\":\"" + objModApiSeccion.Orden + "\",";
                        strNodo += "\"idType\":\"" + 1 + "\",";
                        strNodo += "\"href\":\"" + objModApiSeccion.IdSeccion + "\"";

                        //strSubRubro = getRubros(lstRubro, obj.IdRubro);
                        //if (strSubRubro != "") strRubro += "," + strSubRubro;
                        if (objModApiSeccion.LstModApiControl != null && objModApiSeccion.LstModApiControl.Count > 0) {

                            strSubNodo = "\"nodes\":[";
                            intContS = 0;
                            foreach (clsModApiControl objModApiControl in objModApiSeccion.LstModApiControl) {

                                strSubNodo += (intContS == 0) ? "{" : ",{";
                                strSubNodo += "\"text\":\"" + objModApiControl.Nombre + "\",";
                                strSubNodo += "\"order\":\"" + objModApiControl.Orden + "\",";
                                strSubNodo += "\"idType\":\"" + 2 + "\",";
                                if(!String.IsNullOrEmpty(objModApiControl.ClaseArbol)) strSubNodo += "\"icon\":\""+objModApiControl.ClaseArbol+"\",";
                                strSubNodo += "\"href\":\"" + objModApiControl.IdSeccionControl + "\"";


                                strSubNodo += "}";
                                intContS++;
                            }

                            strSubNodo += "]";

                            strNodo += "," + strSubNodo;
                            
                        }


                        strNodo += "}";
                        intCont++;
                    }

                    strNodo += "]";

                    strArbol += "," + strNodo;
                }


                strArbol += "}]";
            }
            return strArbol;
        }

        public string CargarUUIDXId(out clsModErrorBase objErrorBase, int varId)
        {
            return _objDatCatFormularios.CargarUUIDXId(out objErrorBase, varId);
        }

        /// <summary>
        /// Devuelve el id del formmulario en base al id de la seccion
        /// </summary>
        public int CargarIdPorIdSeccion(out clsModErrorBase objModErrorBase, int intIdSeccion) { 
        
            return _objDatCatFormularios.CargarIdPorIdSeccion(out objModErrorBase, intIdSeccion);
        }

        public clsModCatFormularios getIdTipoFormulario(out clsModErrorBase objModErrorBase, int id)
        {
           clsDatCatFormularios objDatCatFormularios = new clsDatCatFormularios();
            return objDatCatFormularios.getIdTipoFormulario(out objModErrorBase, id);
        }

        public List<clsModCatSecciones> CargarSeccionesPorFormulario(int idForm, out clsModErrorBase objModErrorBase)
        {
            clsDatCatFormularios objDatCatFormularios = new clsDatCatFormularios();

            return objDatCatFormularios.CargarSeccionesPorFormulario(idForm, out objModErrorBase);
        }
    }
}
