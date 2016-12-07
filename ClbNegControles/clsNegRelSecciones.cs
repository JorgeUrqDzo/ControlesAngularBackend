using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles;

namespace ClbNegControles
{
    public class clsNegRelSecciones
    {
        public clsModErrorBase GuardarRelacion(clsModRelSecciones objModRelSecciones)
        {
            clsDatRelSecciones objDatRelSecciones = new clsDatRelSecciones();
            return objDatRelSecciones.GuardarRelacion(objModRelSecciones);
        }

        public List<clsModRelSecciones> CargarRelaciones(int idFormulario, out clsModErrorBase objModErrorBase)
        {
            clsDatRelSecciones objDatRelSecciones = new clsDatRelSecciones();
            return objDatRelSecciones.CargarRelaciones(idFormulario,out objModErrorBase);
        }

        public List<clsModRelSecciones> EliminarRelacion(clsModRelSecciones objModRelSecciones, out clsModErrorBase objModErrorBase)
        {
            clsDatRelSecciones objRelSecciones = new clsDatRelSecciones();
            return objRelSecciones.EliminarRelacion(objModRelSecciones.IdRelSecciones, out objModErrorBase);
        }
        public clsModErrorBase CambiarOrden(clsModRelSecciones objRelSecciones)
        {
            clsDatRelSecciones objDatRelSecciones = new clsDatRelSecciones();
            return objDatRelSecciones.CambiarOrden(objRelSecciones);
        }
    }
}
