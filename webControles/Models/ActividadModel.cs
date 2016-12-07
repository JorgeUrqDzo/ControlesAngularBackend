using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace webControles.Models
{
    public class ActividadModel
    {
        
        //Identificador de la tabla de Actividades
    
        public int IdEncActividad { get; set; }

        //Identificador del tipo de proceso
        [Required(ErrorMessage = "*Seleccione un tipo de proceso")]
        public int IdTipoProceso { get; set; }

        //orden de la actividad
        [Required(ErrorMessage = "*Campo Requerido")]
        public int Orden { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        //Indicador de si se inicio o no el proceso
        public bool Activo { get; set; }

        [Required(ErrorMessage = "*Campo Requerido")]
        //Nombre de la actividad
        public String Actividad { get; set; }

        [Required(ErrorMessage = "*Campo Requerido")]
        //Url de la Actividad
        
        public string UrlActividad { get; set; }

    

        //Url del destino en donde termina la actividad
        public string UrlDestinoAlTerminar { get; set; }

        [Required(ErrorMessage = "*Campo Requerido")]
        //El promedio del tiempo que se tiene fijada la duracion de la actividad
        public int TiempoPromedioObjetivo { get; set; }

        public string ActividadSiguiente { get; set; }
     
        public PaginacionModel objPaginacionModel { get; set; }

      
        public ICollection<ActividadModel> listActividadModel { get; set; }


        public ActividadModel objActividadModel { get; set; }

        public TipoProcesoModel objTipoProceso { get; set; }

    }
}