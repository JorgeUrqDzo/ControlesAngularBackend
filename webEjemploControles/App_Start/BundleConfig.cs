using System.Web;
using System.Web.Optimization;

namespace webEjemploControles
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, consulte http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                   "~/Scripts/jQuery-2.1.4.min.js",
                                   "~/Scripts/jquery.validate.js",
                                   "~/Scripts/jquery-ui.min.js",
                                   "~/Scripts/bootstrap.min.js",
                                   "~/Scripts/app.min.js",
                                    "~/Scripts/Services/MenuDinamicoService.js",
                                    "~/Scripts/Common/Nz/MenuDinamico.js"
                                   ));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-switch.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/AdminLTE.min.css",
                      "~/Content/skins/skin-blue-light.min.css",
                      "~/Content/Site.css"
                      ));
        }
    }
}
