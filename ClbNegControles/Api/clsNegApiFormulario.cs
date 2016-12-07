using System.Linq;
using ClbDatControles;
using ClbModControles;
using ClbModControles.Api;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Globalization;
using System.Runtime.Remoting;
using ClbModControles.Enums;

namespace ClbNegControles.Api
{
    public class clsNegApiFormulario
    {
        /// <summary>
        /// Devuelve el listado de controles pertenecientes al formulario
        /// </summary>
        /// <param name="strUUID">UUID para identificar el formulario</param>
        /// <param name="objModErrorBase">Objeto de errror de salida</param>
        /// <returns>Regresa una collecion de controles<returns>
        public clsModApiFormulario Cargar(string strUUID, string Key, out clsModErrorBase objModErrorBase)
        {
            clsNegApiDataSource objNegApiDataSource = new clsNegApiDataSource();
            clsDatApiFormulario objDatApiFormulario = new clsDatApiFormulario();
            clsModApiFormulario objModApiFormulario = null;

            objModApiFormulario = objDatApiFormulario.Cargar(strUUID, out objModErrorBase);
            ICollection<clsModApiControl> lstApiControlDataSource = null;
            objModErrorBase = new clsModErrorBase();

            if (objModApiFormulario != null && objModApiFormulario.LstModApiSeccion != null)
            {
                foreach (var item in objModApiFormulario.LstModApiSeccion)
                {
                    //Obtiene los controles que tienen configurado un origen de datos
                    try
                    {
                        lstApiControlDataSource = item.LstModApiControl.Where(i => (i.DataSource)).ToList();
                        if (lstApiControlDataSource != null && lstApiControlDataSource.Count > 0)
                        {
                            foreach (clsModApiControl objApiControlDataSource in lstApiControlDataSource)
                            {
                                ///Si la lista contiene algo se tiene que obtener el datasource para cada elemento configurado
                                objApiControlDataSource.ObjDataSource =
                                        objNegApiDataSource.Cargar(objApiControlDataSource.IdSeccionControl, out objModErrorBase);
                            }
                        }

                        if (Key != "undefined")
                        {
                            clsDatApiControles objDatApiControles = new clsDatApiControles();
                            Hashtable ht = objDatApiControles.CargarDatosFormularioXId(out objModErrorBase, Key, strUUID);

                            foreach (clsModApiSeccion objModApi in objModApiFormulario.LstModApiSeccion)
                            {
                                foreach (clsModApiControl objModApiCtrl in objModApi.LstModApiControl.OrderBy(i => i.IdSeccionControlPadre))
                                {
                                    if (ht.ContainsKey(objModApiCtrl.IdSeccionControl.ToString()))
                                    {
                                        if (objModApiCtrl.IdTipoControl == (int)TipoControl.Fecha)
                                        {
                                            //string pattern = objModApiFormulario.FormatoFecha;
                                            //DateTime parsedDate;
                                            string fecha = ht[objModApiCtrl.IdSeccionControl.ToString()].ToString();
                                            DateTime dt = Convert.ToDateTime(fecha);
                                            //string x = dt.Date.ToString(pattern);

                                            //if (DateTime.TryParseExact(x
                                            //    , pattern,
                                            //    null,
                                            //    DateTimeStyles.None, out parsedDate))
                                            //{
                                            //    objModApiCtrl.ValorDefault = parsedDate.ToString(pattern);
                                            //}

                                            objModApiCtrl.ValorDefault = dt.ToString("yyyy/MM/dd");

                                        }
                                        else
                                        {
                                            objModApiCtrl.ValorDefault =
                                              ht[objModApiCtrl.IdSeccionControl.ToString()].ToString();
                                        }
                                        
                                        if (objModApiCtrl.IdSeccionControlPadre != 0)
                                        {
                                            clsDatApiDataSource ojbMoDatApiFormulario = new clsDatApiDataSource();

                                            int ValuePadre = int.Parse(ht[objModApiCtrl.IdSeccionControlPadre.ToString()].ToString());
                                            int IdControlHijo = int.Parse(objModApiCtrl.IdSeccionControl.ToString());
                                            
                                            objModApiCtrl.ObjDataSource = ojbMoDatApiFormulario.GetDataSource(IdControlHijo, ValuePadre, out objModErrorBase);
                                        }

                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        objModErrorBase.MsgError = ex.Message;
                    }
                }
            }

            return objModApiFormulario;
        }

        public clsModApiDataSource GetDataSource(int IdHijo, int Value, out clsModErrorBase objModErrorBase)
        {
            clsDatApiDataSource ojbMoDatApiFormulario = new clsDatApiDataSource();
            return ojbMoDatApiFormulario.GetDataSource(IdHijo, Value, out objModErrorBase);
        }
    }
}



