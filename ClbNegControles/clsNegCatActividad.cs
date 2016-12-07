using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using ClbDatControles;

namespace ClbNegControles
{
    public class clsNegCatActividad
    {
        public clsNegCatActividad ()
        {
            
        }

        public clsModErrorBase AgregarCatActividad(clsModCatActividad objModCatActividad)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsDatCatActividad objDatCatActividad = new clsDatCatActividad();

            try
            {
                if (objModCatActividad.IdEncActividad > 0)
                {
                    objModErrorBase = objDatCatActividad.ActualizaCatActividad(objModCatActividad);
                }
                else
                {
                    objModErrorBase = objDatCatActividad.AgregarCatACtividad(objModCatActividad);
                }

                //Se valida que el id obtenido sea diferente de cero, en caso contrario se genera una excepcion para detener la transaccion
                if (!String.IsNullOrEmpty(objModErrorBase.MsgError))
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

        public ICollection<clsModCatActividad> CargarActividad(out clsModErrorBase objModErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            clsDatCatActividad objDatCatActividad = new clsDatCatActividad();

            ICollection<clsModCatActividad> lst = objDatCatActividad.CargarActividad(out objModErrorBase, strTextoBuscar, out objModPaginacion, intPagina, intRegistrosPorPagina);

            return lst;
        }



        public clsModCatActividad CargarXId(int IdActividad, out clsModErrorBase objModErroBase)
        {


            clsDatCatActividad objDatCatActividad = new clsDatCatActividad();
            clsModCatActividad objCatActividad = objDatCatActividad.CargarXId(IdActividad, out objModErroBase);
            return objCatActividad;

        }

        #region ActualizarEstado
        public clsModErrorBase ActualizarEstado(clsModCatActividad objModCatActividad)
        {
            clsDatCatActividad objDatCatActividad = new clsDatCatActividad();
            return objDatCatActividad.ActualizarEstado(objModCatActividad);
        }
        #endregion

        //public clsModErrorBase DeshabilitarCatActividad(int intIdActividad, bool bolActivo)
        //{
        //    clsDatCatActividad objDatCatActividad = new clsDatCatActividad();

        //    clsModErrorBase objModErrorBase = objDatCatActividad.DeshabilitarCatActividad(intIdActividad, bolActivo);

        //    return objModErrorBase;
        //}
    }
}
