using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClbModControles;
using ClbDatControles;

namespace ClbNegControles
{
    public class clsNegCatMenu
    {
        
        public clsNegCatMenu ()
        {
            
        }

        //Metodo para Agregar o Actualizar un dato en tabla CatMenu
        public clsModErrorBase AgregarCatMenu(clsModCatMenu objModCatMenu)
        {
            clsModErrorBase objModErrorBase = new clsModErrorBase();
            clsDatCatMenu objDatCatMenu = new clsDatCatMenu();

            try
            {
                if (objModCatMenu.IdMenu > 0)
                {
                    objModErrorBase = objDatCatMenu.ActualizaCatMenu(objModCatMenu);
                }
                else
                {
                    objModErrorBase = objDatCatMenu.AgregarCatMenu(objModCatMenu);
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

        public ICollection<clsModCatMenu> CargarMenu(out clsModErrorBase objModErrorBase, string strTextoBuscar, out clsModPaginacion objModPaginacion, int intPagina, int intRegistrosPorPagina)
        {
            clsDatCatMenu objDatCatMenu = new clsDatCatMenu();

            ICollection<clsModCatMenu> lst = objDatCatMenu.CargarMenu(out objModErrorBase, strTextoBuscar, out objModPaginacion, intPagina, intRegistrosPorPagina);

            return lst;
        }



        public clsModCatMenu CargarXId(int IdMenus, out clsModErrorBase objModErroBase)
        {


            clsDatCatMenu objDatCatMenu = new clsDatCatMenu();
            clsModCatMenu objCatMenu = objDatCatMenu.CargarXId(IdMenus, out objModErroBase);
            return objCatMenu;

        }

        #region ActualizarEstado
        public clsModErrorBase ActualizarEstado(clsModCatMenu objModCatMenu)
        {
            clsDatCatMenu objDatCatMenu = new clsDatCatMenu();
            return objDatCatMenu.ActualizarEstado(objModCatMenu);
        }
        #endregion

        public List<clsModCatMenuIcono> CargarIconos(clsModErrorBase objModErrorBase)
        {
            clsDatCatMenu objDatCatMenu = new clsDatCatMenu();

            return objDatCatMenu.CargarIconos(objModErrorBase);
        }
    }


    }

