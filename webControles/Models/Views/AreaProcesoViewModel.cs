using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webControles.Models.Views
{
    public class AreaProcesoViewModel
    {

        public ICollection<AreaProcesoModel> lstAreaProcesoModel { get; set; }

        public PaginacionModel objPaginacionModel { get; set; }

        public AreaProcesoModel objAreaProcesoModel { get; set; }
    }
}