using ClbModControles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using System.Transactions;

namespace ClbNegControles
{
    public class clsNegCatSeccionControl
    {

        public clsModErrorBase Agregar(clsModCatSeccionControl objModCatSeccionControl)
        {
            clsDatCatSeccionControl objDatCatSeccionControl = new clsDatCatSeccionControl();

            return objDatCatSeccionControl.Agregar(objModCatSeccionControl);
        }


        public clsModErrorBase Guardar(clsModCatSecciones objModCatSecciones)
        {
            clsDatCatSeccionControl objDatCatSeccionControl = new clsDatCatSeccionControl();
            clsNegEncDataSource objNegEncDataSource = new clsNegEncDataSource();
            clsModErrorBase objModErrorBase = null;
            clsModCatSeccionControl objSeccionControl = new clsModCatSeccionControl();
            Hashtable ht = new Hashtable();
            try
            {
                //Actualiza los registros, agrega o desactiva los registros

                objModErrorBase = objDatCatSeccionControl.Eliminar(objModCatSecciones.IdSeccion);

                if (String.IsNullOrEmpty(objModErrorBase.MsgError))
                {
                    if (objModCatSecciones.LstSeccionControl != null)
                    {
                        int intOrden = 1;
                        objModCatSecciones.LstSeccionControl.OrderBy(x => objSeccionControl.IdSeccionControlPadre);

                        foreach (clsModCatSeccionControl objModSeccionControl in objModCatSecciones.LstSeccionControl)
                        {
                            objModSeccionControl.IdSeccion = objModCatSecciones.IdSeccion;
                            //objModSeccionControl.NombreColumna = GenerarNombreColumna();
                            objModSeccionControl.Orden = intOrden;

                            //Verificar si el control es padre o hijo
                            if (objModSeccionControl.IdSeccionControlPadre != 0)
                            {
                                //Control Hijo, buscar el Id nuevo que tiene el padre en el hashtable y actualizarlo
                                foreach (Object i in ht.Keys)
                                {
                                    if (objModSeccionControl.IdSeccionControlPadre == int.Parse(i.ToString()))
                                        objModSeccionControl.IdSeccionControlPadre = int.Parse(ht[i].ToString());
                                }
                            }

                            //Agregar IdSeccionControlViejo a key y IdSeccionControlNuevo Value de HashTable
                            objModErrorBase = Agregar(objModSeccionControl);
                            ht.Add(objModSeccionControl.IdSeccionControl, objModErrorBase.Id);

                            if (!string.IsNullOrEmpty(objModErrorBase.MsgError))
                            {
                                throw new Exception(objModErrorBase.MsgError);
                            }
                            //Asigna el id al elemento
                            objModSeccionControl.IdSeccionControl = objModErrorBase.Id;
                            //Manda llamar a guardar la configuracion del datasource
                            if (objModSeccionControl.objEncDataSource != null)
                            {
                                objModSeccionControl.objEncDataSource.IdSeccionControl = objModSeccionControl.IdSeccionControl;
                                objModErrorBase = objNegEncDataSource.Agregar(objModSeccionControl.objEncDataSource);
                                if (!string.IsNullOrEmpty(objModErrorBase.MsgError))
                                {
                                    throw new Exception(objModErrorBase.MsgError);
                                }
                            }

                            intOrden++;
                        }
                        //Si no hay error
                        if (!string.IsNullOrEmpty(objModErrorBase.MsgError))
                        {
                            throw new Exception(objModErrorBase.MsgError);
                        }
                    }

                }
                else
                {
                    throw new Exception(objModErrorBase.MsgError);
                }

            }
            catch (Exception ex)
            {

                objModErrorBase = new clsModErrorBase();
                objModErrorBase.MsgError = ex.Message;
            }


            return objModErrorBase;
        }

        public clsModErrorBase Actualizar(clsModCatSeccionControl objModCatSeccionControl)
        {
            clsDatCatSeccionControl objDatCatSeccionControl = new clsDatCatSeccionControl();

            return objDatCatSeccionControl.Actualizar(objModCatSeccionControl);
        }

        public clsModErrorBase Eliminar(int intIdSeccion)
        {
            clsDatCatSeccionControl objDatCatSeccionControl = new clsDatCatSeccionControl();

            return objDatCatSeccionControl.Eliminar(intIdSeccion);
        }


        public ICollection<clsModCatSeccionControl> Cargar(int intIdSeccion, out clsModErrorBase objModErrorBase)
        {

            clsDatCatSeccionControl objDatCatSeccionControl = new clsDatCatSeccionControl();
            ICollection<clsModCatSeccionControl> lstSeccionControl = objDatCatSeccionControl.Cargar(intIdSeccion, out objModErrorBase);
            clsNegEncDataSource objNegEncDataSource = new clsNegEncDataSource();
            clsNegDetDataSource objNegDetDataSource = new clsNegDetDataSource();
            foreach (clsModCatSeccionControl objSeccionControl in lstSeccionControl)
            {

                //Si el control tiene origen de datos se realiza la consulta en la tabla
                if (objSeccionControl.DataSource)
                {
                    objSeccionControl.objEncDataSource =
                            objNegEncDataSource.CargarXControl(objSeccionControl.IdSeccionControl, out objModErrorBase);

                    if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
                    {
                        throw new Exception(objModErrorBase.MsgError);
                    }


                    if (objSeccionControl.objEncDataSource != null && !objSeccionControl.objEncDataSource.BaseDatos)
                    {
                        //Si es un elemento con datasource pero no se obtiene de base de datos se carga el detalle
                        objSeccionControl.objEncDataSource.Valores =
                            objNegDetDataSource.CargarXControl(objSeccionControl.objEncDataSource.IdEncDataSource, out objModErrorBase);
                        //Reeplaza el salto de linea(\n) por <br/>
                        objSeccionControl.objEncDataSource.Valores = objSeccionControl.objEncDataSource.Valores.Replace("\\n", "\n");
                    }

                }

            }

            return lstSeccionControl;
        }

        private string GenerarNombreColumna()
        {

            return "";
        }

        public List<clsModControlesGenerados> GenerarControles(string schema, string tabla, out clsModErrorBase objModErrorBase)
        {
            clsDatCatSeccionControl objDatCatSeccionControl = new clsDatCatSeccionControl();
            return objDatCatSeccionControl.GenerarControles(tabla, schema, out objModErrorBase);
        }
    }
}
