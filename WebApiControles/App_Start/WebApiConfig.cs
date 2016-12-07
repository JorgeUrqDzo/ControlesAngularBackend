using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApiControles
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute(
                "http://localhost:48603," +
                "http://localhost:2397," +
                "http://localhost:3330," +
                "http://localhost:9000," +
                "http://localhost:9001,",
                "*", "*"
            ));
            //config.EnableCors();

            config.MapHttpAttributeRoutes();
            config.EnableCors();


            config.Routes.MapHttpRoute(
             name: "DefaultApi2",
             routeTemplate: "api/{controller}/{action}/{id}",
             defaults: new { id = RouteParameter.Optional }
            );
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

        }
    }
}
