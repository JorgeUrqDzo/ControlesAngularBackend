using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;

namespace webControles.Models.Views
{
    public class BusquedaBandejaViewModel
    {
        //
        // GET: /BusquedaBandejaViewModel/
        public BusquedaBandejaViewModel()
        {
            objBusquedaBandejaModel = new BusquedaBandejaModel();
            objBandejaModel = new BandejaModel();
            objBandejaColumnaModel = new BandejaColumnaModel();
            lstBandejaColumnaModel = new Collection<BandejaColumnaModel>();
            parametrosViewModel = new ParametrosViewModel();
            parametrosViewModel.lstParametrosModel = new List<ParametrosModel>();
        }
        
        ///<summary>
        /// Contiene el id de la bandeja
        /// <summary>

        public int IdEncConfBandeja { get; set; }

        ///<summary>
        /// Contiene todas las variables del modelo de bandeja
        /// <summary>

        public BusquedaBandejaModel objBusquedaBandejaModel { get; set; }

        public BandejaModel objBandejaModel { get; set; }

        ///<summary>
        /// objeto modelo de paginacion de bandejas
        /// <summary>

        public PaginacionModel objPaginacionModel { get; set; }

        public BandejaColumnaModel objBandejaColumnaModel { get; set; }

        ///<summary>
        /// Lista de modelo de bandejas
        /// <summary>

        public ICollection<BusquedaBandejaModel> lstBusquedaBandejaModel

        { get; set; }

        public ICollection<BandejaColumnaModel> lstBandejaColumnaModel { get; set; }

        public ParametrosViewModel parametrosViewModel { get; set; }
    }
}