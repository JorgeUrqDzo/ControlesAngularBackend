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
    public class MenuModel
    {
        private DateTime _FechaCreacion;
        private DateTime _FechaModificacion;
        //Declara los campos de la tabla CatMenu que se requieren en la vista
        public int IdMenu{ get; set; }

        [Required(ErrorMessage = "*Seleccione un tipo de Menu")]
        public int IdTipoMenu { get; set; }


        [Required(ErrorMessage = "*Campo Requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "*Campo Requerido")]
        public string Menu { get; set; }

        [Required(ErrorMessage = "*Campo Requerido")]
        public string Url { get; set; }

        [Required(ErrorMessage = "*Seleccione una opcion")]

        public int IdMenuIcono { get; set; }
        public string Icono { get; set; }

        public int IdMenuPadre { get; set; }

        public bool Activo{ get; set; }
        [Required(ErrorMessage = "*Campo Requerido")]
        public string FechaModificacionStr { get; set; }
        [Required(ErrorMessage = "*Campo Requerido")]
        public string FechaCreacionStr { get; set; }

         [Required(ErrorMessage = "*Campo Requerido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaCreacion
        {
            get
            {
                return _FechaCreacion;
            }
            set
            {
                _FechaCreacion = value;
                FechaCreacionStr = _FechaCreacion.ToString("dd/MM/yyyy");
            }
        }
         [Required(ErrorMessage = "*Campo Requerido")]
         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaModificacion {
            get
            {
                return _FechaModificacion;
            }
            set 
            {
                _FechaModificacion = value;
                FechaModificacionStr = _FechaModificacion.ToString("dd/MM/yyyy");
            }
        }

        [Required(ErrorMessage = "*Campo Requerido")]
        public int IdUsuarioCreacion { get; set; }
        
        [Required(ErrorMessage = "*Campo Requerido")]
      
        public int IdUsuarioModificacion { get; set; }

        public PaginacionModel objPaginacionModel { get; set; }


        public ICollection<MenuModel> listMenuModel { get; set; }


        public MenuModel objMenuModel { get; set; }
        public MenuIconoModel objMenuIconoModel { get; set; }

        [Required(ErrorMessage = "*Selecciona una opcion")]
        public int IdTipoPagina { get; set; }
    }
}