using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using ClbDatControles;

namespace ClbNegControles
{
  public  class clsNegCatMenuIcono
    {


      //public clsModCatMenuIcono CargarIconos(int IdMenuIcono, out clsModErrorBase objModErroBase)
      //{


      //    clsDatCatMenuIcono objDatCatMenuIcono = new clsDatCatMenuIcono();
      //    clsModCatMenuIcono objCatMenuIcono = objDatCatMenuIcono.CargarIconos(IdMenuIcono, out objModErroBase);
      //    return objCatMenuIcono;

      //}


      public List<clsModCatMenuIcono> CargarIconos(clsModCatMenuIcono objModCatMenuIcono, out clsModErrorBase objModErrorBase)
      {
          objModErrorBase = new clsModErrorBase();
          List<clsModCatMenuIcono> lstModCatMenuIcono = new List<clsModCatMenuIcono>();
          clsDatCatMenuIcono objDatCatMenuIcono = new clsDatCatMenuIcono();

          lstModCatMenuIcono = objDatCatMenuIcono.CargarIconos(int.Parse(objModCatMenuIcono.IdMenuIcono.ToString()), out objModErrorBase);

          return lstModCatMenuIcono;
      }

    }
}
