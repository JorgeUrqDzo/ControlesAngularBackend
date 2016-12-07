using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace webControles.Models
{
    public class BusquedaBandejaModel 
    {

        //Identificador de la tabla de bandejas
        public int IdEncConfBandeja { get; set; }


        //Nombre de la bandeja
        public String Nombre { get; set; }

        //Descripcion de bandeja
        public String Descripcion { get; set; }

        //Nmero de total de paginas de la bandeja
        public int NumeroPaginas { get; set; }

        //Nombre de consulta
        public String Consulta { get; set; }

        //Identificador de la creacion de usuario
        public int IdUsuarioCreacion { get; set; }

        //Identificador de la modificacion de usuario
        public int IdUsuarioModificacion { get; set; }

        //Fecha de la creacion de bandeja
        public DateTime FechaCreacion { get; set; }

        //Fecha de la modificacion de la bandeja
        public DateTime FechaModificacion { get; set; }

        public string UUID { get; set; }
	}
}