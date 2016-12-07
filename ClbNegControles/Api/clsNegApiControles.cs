using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles;
using ClbModControles.Api;

namespace ClbNegControles.Api
{
    public class clsNegApiControles
    {
        public clsModErrorBase GuardarDatosFormulario(Hashtable objDatos, string key)
        {
            Hashtable htOrdenado = new Hashtable();
            List<clsModApiRelaciones> lstModApiRelaciones = OrdenarSecciones(objDatos);
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            int orden = 1;
            bool guardar = false;
            bool verificarPadre = true;

            //guarda el nombre de la columna padre para guardarse como key en ht Relaciones
            string padre = "";
            string Fk = "";
            int value = -1;

            //HT que guarda las relaciones (Nombre Columna Padre, Valor generado de inserccion)
            Hashtable Relaciones = new Hashtable();

            clsModApiRelaciones objModApiRelaciones = new clsModApiRelaciones();
            clsDatApiControles objDatApiControles = new clsDatApiControles();

            //Si no tiene key son valores nuevos a guardar
            if (key == null)
            {
                foreach (clsModApiRelaciones i in lstModApiRelaciones)
                {

                    if (orden == int.Parse(i.Orden.ToString()))
                    {

                        htOrdenado.Add(i.IdControl.ToString(), i.Valor);
                        objModApiRelaciones = i;
                        padre = i.KeyPadre;
                    }
                    else
                    {
                        if (objModApiRelaciones.IsHijo)
                        {
                            foreach (object dato in Relaciones.Keys)
                            {
                                if (dato.ToString().Equals(objModApiRelaciones.KeyHijo))
                                {
                                    Fk = dato.ToString();
                                    value = int.Parse(Relaciones[dato].ToString());
                                }
                            }
                        }
                        objModErrorBase = objDatApiControles.GuardarDatosFormulario(htOrdenado, Fk, value);
                        Fk = "";
                        value = -1;
                        orden = int.Parse(i.Orden.ToString());
                        htOrdenado.Clear();

                        if (orden == int.Parse(i.Orden.ToString()))
                        {
                            htOrdenado.Add(i.IdControl.ToString(), i.Valor);
                        }

                        if (objModApiRelaciones.IsPadre && !Relaciones.ContainsKey(padre))
                        {
                            Relaciones.Add(padre, objModErrorBase.Id);
                        }

                        objModApiRelaciones = i;
                    }


                }
                if (objModApiRelaciones.IsHijo)
                {
                    foreach (object dato in Relaciones.Keys)
                    {
                        if (dato.ToString().Equals(objModApiRelaciones.KeyHijo))
                        {
                            Fk = dato.ToString();
                            value = int.Parse(Relaciones[dato].ToString());
                        }
                    }
                }
                objModErrorBase = objDatApiControles.GuardarDatosFormulario(htOrdenado, Fk, value);
                return objModErrorBase;
            }
            else
            {
                //Si tiene key entonces se actualizan los valores correspondientes

                foreach (clsModApiRelaciones i in lstModApiRelaciones)
                    htOrdenado.Add(i.IdControl.ToString(), i.Valor);

                objModErrorBase = objDatApiControles.ActualizarDatosFormulario(htOrdenado, key);
                return objModErrorBase;
            }

            //clsDatApiControles objDatApiControles = new clsDatApiControles();
            //if (key == null)
            //{

            //    return objDatApiControles.GuardarDatosFormulario(htOrdenado);
            //}
            //else
            //{
            //    return objDatApiControles.ActualizarDatosFormulario(htOrdenado, key);
            //}
        }

        public List<clsModApiRelaciones> OrdenarSecciones(Hashtable objDatos)
        {
            clsDatApiControles objDatApiControles = new clsDatApiControles();
            return objDatApiControles.OrdenarSecciones(objDatos);
        }
    }
}
