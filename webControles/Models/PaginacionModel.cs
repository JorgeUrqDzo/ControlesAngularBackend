using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webControles.Models
{
    public class PaginacionModel
    {
        public PaginacionModel() 
        {

        }
        #region Variables
        public int TotalPaginas { get; set; }
        public int LimitePaginas { get; set; }
        public int PaginaActual { get; set; }
        public int TotalElementos { get; set; }
        public List<int> LstPaginas { get; set; }
        public bool MostrarPaginacion { get; set; }
        public int PrimeraHoja { get; set; }
        public int UltimaHoja { get; set; }
        public int PaginasVista { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        #endregion
        #region Metodos
        /// <summary>
        /// Metodo Model Paginación
        /// </summary>
        /// <param name="strController">Controller que afectara</param>
        /// <param name="strAction">Acción que realizara</param>
        /// <param name="intTotalPaginas">Total de paginas que mostrará</param>
        /// <param name="IntPaginaActual">Pagina actual que comenzará</param>
        /// <param name="intTotalElementos">Total de elementos que mostrará</param>
        /// <param name="intLimitePaginas">Limite de páginas que </param>
        public PaginacionModel(string strController, string strAction, int intTotalPaginas, int IntPaginaActual, int intTotalElementos, int intLimitePaginas = 10)
        {
            this.Controller = strController;
            this.Action = strAction;
            this.TotalElementos = intTotalElementos;
            this.PaginaActual = IntPaginaActual;
            this.TotalPaginas = intTotalPaginas;
            this.LimitePaginas = intLimitePaginas;
            this.PaginasVista = 10;
            this.CalcularPaginas();
        }
        /// <summary>
        /// Metodo para calcular las paginas que se necesitaran 
        /// </summary>
        private void CalcularPaginas()
        {
            int intPaginaInicial = 1;
            int intPaginasTotales = 1;

            //Caso 1.- Solo una pagina, entonces no se crea la paginación
            if (TotalPaginas > 1)
            {
                decimal intTotal = PaginaActual / decimal.Parse(PaginasVista.ToString());
                intPaginaInicial = ((int)intTotal) * PaginasVista;
                if (intPaginaInicial == 0) intPaginaInicial = 1;
                if ((intPaginaInicial + PaginasVista) > TotalPaginas)
                {
                    intPaginasTotales = TotalPaginas;
                }
                else
                {
                    intPaginasTotales = intPaginaInicial + PaginasVista;
                }
                AgregarPaginas(intPaginaInicial, intPaginasTotales);
            }
            else
            {
                MostrarPaginacion = false;
            }
        }
        /// <summary>
        ///Metodo para agregar mas paginas 
        /// </summary>
        /// <param name="intPaginaInicial">Pagina inicial a mostrar</param>
        /// <param name="intTotalPaginas">Total de paginas a mostrar</param>
        private void AgregarPaginas(int intPaginaInicial, int intTotalPaginas)
        {
            if (!(intPaginaInicial == 1 && intTotalPaginas == 1))
            {
                MostrarPaginacion = true;
                LstPaginas = new List<int>();
                for (int i = intPaginaInicial; i <= intTotalPaginas; i++)
                {
                    LstPaginas.Add(i);
                }
            }
            else
            {
                MostrarPaginacion = false;
            }
        }
        #endregion 
    }
}