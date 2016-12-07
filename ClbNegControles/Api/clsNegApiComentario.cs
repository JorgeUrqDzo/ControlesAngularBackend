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
   public class clsNegApiComentario
    {
       clsDatApiComentario _objComentarioDat = null;
        public clsNegApiComentario()
        {
            _objComentarioDat = new clsDatApiComentario();
        }

        public clsModErrorBase AgregarCatComentario(clsModApiComentario objModApiComentario)
        {
            //clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsDatApiComentario objDatApiComentario = new clsDatApiComentario();

                 return objDatApiComentario.AgregarCatComentario(objModApiComentario);
        }
   

        public List<clsModApiComentario> CargarComentarios(int IdTablaDocumento, int IdDocumento, int IdUsuario, out clsModErrorBase objModErrorBase)
        {
            return _objComentarioDat.CargarComentarios(IdTablaDocumento, IdDocumento, IdUsuario, out objModErrorBase);
        }
    }
}
