using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClbModControles
{
   public class clsModEncConfBandeja
    {

       public int IdEncConfBandeja { get; set; }

       /// <summary>
       /// Titulo/ nombre del Formulario
       /// </summary>
       public string Nombre { get; set; }
       /// <summary>
       /// Descripcion del Formulario
       /// </summary>
       public string Descripcion { get; set; }
       /// <summary>
       /// Numero de paginas que se cargaran
       /// </summary>
       public int NumeroPaginas { get; set; }
       /// <summary>
       /// Consulta SQL que ocupara el Formulario para su funcionamiento
       /// </summary>
       public string Consulta { get; set; }

       /// <summary>
       /// Id del usuario que crea el formulario
       /// </summary>
       public int IdUsuarioCreacion {get; set;}
       /// <summary>
       /// Id del usuario que modifica el formulario
       /// </summary>
       public int IdUsuarioModificacion { get; set; }
       /// <summary>
       /// Fecha de cuando se crea el formulario, esta sera tomada por SQL SERVER con GETDATE
       /// </summary>
       public DateTime FechaCreacion { get; set; }
       /// <summary>
       /// Fecha de Modificacion del formulario.
       /// </summary>
       public DateTime FechaModificacion { get; set; }

       public string ClaseBandeja { get; set; }

       public int IdColumna { get; set; }

       public string NombreColumna { get; set; }

       public int IdFormulario { get; set; }

       public int IdTipoConsulta { get; set; }
    }
}
