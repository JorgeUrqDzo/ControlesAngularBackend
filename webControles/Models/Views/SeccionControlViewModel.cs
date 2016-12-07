using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webControles.Models.Views
{
    public class SeccionControlViewModel
    {
        public ICollection<SeccionControlModel> LstSeccionControl { get; set; }

        public SeccionesModel ObjSeccionesModel { get; set; }

        public SeccionControlModel ObjSeccionControl { get; set; }
    }
}