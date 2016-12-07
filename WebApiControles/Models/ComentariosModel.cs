using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiControles.Models
{
    public class ComentariosModel
    {
        public int IdComentario { get; set; }

        public string Comentario { get; set; }

        public int IdTablaDocumento { get; set; }
        public int IdDocumento { get; set; }

        public DateTime FechaCreacion { get; set; }
        public int IdUsuario { get; set; }
    }
}