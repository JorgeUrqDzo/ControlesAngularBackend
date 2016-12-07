using ClbModControles;
using ClbDatControles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbNegControles
{
    public class clsNegDetConfBandejaLinkParametro
    {  
        #region Metodos para Cargar Parametros
        public List<clsModDetConfBandejaLinkParametro> CargarParametros(out clsModErrorBase objModErroBase, int id)
        {
            clsDatDetConfBandejaLinkParametro objCapaDatos = new clsDatDetConfBandejaLinkParametro();
            return objCapaDatos.CargarParametros(out objModErroBase, id);
        }

        public List<clsModDetConfBandejaLinkParametro> CargarParametros()
        {
            clsDatDetConfBandejaLinkParametro objCapaDatos = new clsDatDetConfBandejaLinkParametro();
            return objCapaDatos.CargarParametros();
        }
        #endregion

        #region Metodo para Cargar Parametros X Id
        public clsModDetConfBandejaLinkParametro CargarParametroXId(int id, out clsModErrorBase objModErrorBase)
        {
            clsDatDetConfBandejaLinkParametro objCapaDatos = new clsDatDetConfBandejaLinkParametro();
            return objCapaDatos.CargarParametroXId(id, out objModErrorBase);
        }
        #endregion

        #region Metodo para Agregar Parametros
        public clsModErrorBase InsertarParametros(clsModDetConfBandejaLinkParametro objCapaModelo)
        {
            clsDatDetConfBandejaLinkParametro objCapaDatos = new clsDatDetConfBandejaLinkParametro();

            if (objCapaModelo.IdDetConfBandejaLinkParametro.ToString().Equals("0"))
            {
                //Agregar Parametro
                return objCapaDatos.InsertarParametros(objCapaModelo);
            }
            else
            {
                //Actualizar Parametro existente
                return objCapaDatos.ActualizarParametros(objCapaModelo);
            }
        }
        #endregion

        #region Metodo para Eliminar Parametros
        public clsModErrorBase EliminarParametro(int id)
        {
            clsDatDetConfBandejaLinkParametro objCapaDatos = new clsDatDetConfBandejaLinkParametro();
            return objCapaDatos.EliminarParametro(id);
        }
        #endregion

        #region Metodo para Actualizar Parametros
        public clsModErrorBase ActualizarParametros(clsModDetConfBandejaLinkParametro objCapaModelo)
        {
            clsDatDetConfBandejaLinkParametro objCapaDatos = new clsDatDetConfBandejaLinkParametro();
            return objCapaDatos.ActualizarParametros(objCapaModelo);
        }
        #endregion
    }
}
