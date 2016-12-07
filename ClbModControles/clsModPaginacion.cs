using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
    public class clsModPaginacion
    {
        #region Variables publicas
        /// <summary>
        /// Paginas a mostrar
        /// </summary>
        public int Paginas { get; set; }
        /// <summary>
        /// Total de registros
        /// </summary>
        public int TotalRegistros { get; set; }
        #endregion

        #region Metodos de Modelo Paginación
        /// <summary>
        /// Metodo para asiganr páginas a los registros
        /// </summary>
        /// <param name="intRegistrosPorPagina">Total de registros que se mostrarán por página</param>
        /// <param name="decTotalRegistros">Total de registros</param>
        public void Asignar(int intRegistrosPorPagina, decimal decTotalRegistros)
        {
            decimal paginas = decTotalRegistros / intRegistrosPorPagina;
            if ((int)paginas < paginas)
            {
                Paginas = (int)paginas + 1;
            }
            else
            {
                Paginas = (int)paginas;
            }
            TotalRegistros = int.Parse(decTotalRegistros.ToString());
        }

        #endregion
    }
}
