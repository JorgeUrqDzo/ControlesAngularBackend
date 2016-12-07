using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace webControles.Models.Views
{
    public class ConfigurarActividadViewModel
    {
        public DetActividadModel objDetActividadModel { get; set; }
        public EncProcesoModel objEncProcesoModel { get; set; }
        public ActividadModel objEncActividadModel { get; set; }
        public TipoProcesoModel objTipoProcesoModel { get; set; }

        public PaginacionModel objPaginacionModel { get; set; }

        public ICollection<DetActividadModel> lstDetActividadModel { get; set; }
        public ICollection<EncProcesoModel> lstEncProcesoModel { get; set; }
        public ICollection<ActividadModel> lstEncActividadModel { get; set; }
        public ICollection<TipoProcesoModel> lstTipoProcesoModel { get; set; }

        public ConfigurarActividadViewModel()
        {
            objEncProcesoModel = new EncProcesoModel();
            objDetActividadModel = new DetActividadModel();
            objEncActividadModel = new ActividadModel();
            objTipoProcesoModel = new TipoProcesoModel();
            lstDetActividadModel = new Collection<DetActividadModel>();
            lstEncProcesoModel = new Collection<EncProcesoModel>();
            lstEncActividadModel = new Collection<ActividadModel>();
            lstTipoProcesoModel = new Collection<TipoProcesoModel>();
        }

    }
}