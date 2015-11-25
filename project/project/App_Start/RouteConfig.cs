using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Sales",
              url: "Sales/{year}/{month}/{day}",
              defaults: new
              {
                  controller = "Sales",
                  action = "Index",
                  year = UrlParameter.Optional,
                  month = UrlParameter.Optional,
                  day = UrlParameter.Optional
              }
            );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "index", id = UrlParameter.Optional }
            );

            

        }
    }
}