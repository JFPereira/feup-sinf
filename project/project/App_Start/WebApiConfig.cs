using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace project
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //--------------- Products ---------------//

            // GET api/products/
            config.Routes.MapHttpRoute(
                name: "Products",
                routeTemplate: "api/products",
                defaults: new { controller = "Products", action = "Get" }
            );

            // GET api/products/top
            config.Routes.MapHttpRoute(
                name: "TopProducts",
                routeTemplate: "api/products/top",
                defaults: new { controller = "Products", action = "TopProducts" }
            );

            //--------------- Clients ---------------//

            // GET api/clients/
            config.Routes.MapHttpRoute(
                name: "Clients",
                routeTemplate: "api/clients",
                defaults: new { controller = "Clients", action = "Get" }
            );

            // GET api/clients/top
            config.Routes.MapHttpRoute(
                name: "TopClients",
                routeTemplate: "api/clients/top",
                defaults: new { controller = "Clients", action = "TopClients" }
                );

            // GET api/clients/{id}/topProducts
            config.Routes.MapHttpRoute(
                name: "Monthly Purchases",
                routeTemplate: "api/clients/{id}/top-products",
                defaults: new { controller = "Clients", action = "TopProducts" },
                constraints: new { id = "[0-9]+" }
            );

            //--------------- Suppliers ---------------//

            // GET api/suppliers/
            config.Routes.MapHttpRoute(
                name: "Suppliers",
                routeTemplate: "api/suppliers",
                defaults: new { controller = "Suppliers", action = "Get" }
            );

            // GET api/suppliers/top
            config.Routes.MapHttpRoute(
                name: "TopSuppliers",
                routeTemplate: "api/suppliers/top",
                defaults: new { controller = "Suppliers", action = "TopSuppliers" }
            );

            //--------------- Sales ---------------//

            // GET api/sales/top
            config.Routes.MapHttpRoute(
                name: "SalesTop",
                routeTemplate: "api/sales/top",
                defaults: new { controller = "Sales", action = "TopSales" }
            );

            // GET api/sales/top/countries
            config.Routes.MapHttpRoute(
                name: "SalesTopCountries",
                routeTemplate: "api/sales/top/countries",
                defaults: new { controller = "Sales", action = "TopCountries" }
            );

            //--------------- Financial ---------------//

            // GET api/financial/
            config.Routes.MapHttpRoute(
                name: "Financial",
                routeTemplate: "api/financial",
                defaults: new { controller = "Financial", action = "Index"}
            );
            
            // GET api/financial/global
            config.Routes.MapHttpRoute(
                name: "GlobalFinancial",
                routeTemplate: "api/financial/global",
                defaults: new { controller = "Financial", action = "Global" }
            );

            // GET api/financial/purchases
            config.Routes.MapHttpRoute(
                name: "FinancialPurchases",
                routeTemplate: "api/financial/purchases",
                defaults: new { controller = "Financial", action = "Purchases" }
            );

            //--------------- Defaut Route ---------------//

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
