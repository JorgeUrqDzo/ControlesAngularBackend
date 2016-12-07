using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbDatControles;
using ClbModControles;
using ClbModControles.Api;
namespace ClbNegControles.Api
{
   public class clsNegApiActividadProceso
    {
         clsDatApiActividadProceso _objActividadDat = null;
        public clsNegApiActividadProceso()
        {
            _objActividadDat = new clsDatApiActividadProceso();
        }

        public clsModApiActividadProceso CargarActividades(int IdTablaDocumento, int IdDocumento, int IdEncActividad, int IdEncProceso, bool Aprobado, out clsModErrorBase objModErrorBase)
        {
            return _objActividadDat.CargarActividades(IdTablaDocumento, IdDocumento, IdEncActividad, IdEncProceso, Aprobado, out objModErrorBase);
        }
    }
}
