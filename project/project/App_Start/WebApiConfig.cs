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

            // GET api/products/{id}
            config.Routes.MapHttpRoute(
                name: "GetProduct",
                routeTemplate: "api/products/{id}",
                defaults: new { controller = "Products", action = "GetProduct" }
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

            // GET api/clients/{entity}
            config.Routes.MapHttpRoute(
                name: "GetClient",
                routeTemplate: "api/clients/{entity}",
                defaults: new { controller = "Clients", action = "GetClient" }
            );

            // GET api/clients/{entity}/apc
            config.Routes.MapHttpRoute(
                name: "AveragePurchaseCost",
                routeTemplate: "api/clients/{entity}/apc",
                defaults: new { controller = "Clients", action = "AveragePurchaseCost" }
                );

            // GET api/clients/{entity}/monthly-purchases/{year}
            config.Routes.MapHttpRoute(
                name: "MonthlyPurchases",
                routeTemplate: "api/clients/{entity}/monthly-purchases/{year}",
                defaults: new { controller = "Clients", action = "MonthlyPurchases" }
                );

            // GET api/clients/{entity}/daily-purchases/{month}/{year}
            config.Routes.MapHttpRoute(
                name: "DailyPurchases",
                routeTemplate: "api/clients/{entity}/daily-purchases/{month}/{year}",
                defaults: new { controller = "Clients", action = "DailyPurchases" }
                );

            // GET api/clients/{entity}/top-products
            config.Routes.MapHttpRoute(
                name: "Monthly Purchases",
                routeTemplate: "api/clients/{entity}/top-products",
                defaults: new { controller = "Clients", action = "TopProducts" }
            );

            // GET api/clients/{entity}/ce
            config.Routes.MapHttpRoute(
                name: "CostsVsEarnings",
                routeTemplate: "api/clients/{entity}/ce",
                defaults: new { controller = "Clients", action = "CostsVsEarnings" }
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

            // GET api/sales/psb/{year}
            config.Routes.MapHttpRoute(
                name: "SalesBookingYear",
                routeTemplate: "api/sales/psb/{year}",
                defaults: new { controller = "Sales", action = "SalesBookingY" }
            );

            // GET api/sales/psb/{year}/{month}
            config.Routes.MapHttpRoute(
                name: "SalesBookingMonth",
                routeTemplate: "api/sales/psb/{year}/{month}",
                defaults: new { controller = "Sales", action = "SalesBookingM" }
            );

            // GET api/sales/psb/{year}/{month}/{day}
            config.Routes.MapHttpRoute(
                name: "SalesBookingDay",
                routeTemplate: "api/sales/psb/{year}/{month}/{day}",
                defaults: new { controller = "Sales", action = "SalesBookingD" }
            );

            // GET api/sales/rss/{year}
            config.Routes.MapHttpRoute(
                name: "RegionalSalesStatusY",
                routeTemplate: "api/sales/rss/{year}",
                defaults: new { controller = "Sales", action = "RegionalSSY" }
            );

            // GET api/sales/rss/{year}/{month}
            config.Routes.MapHttpRoute(
                name: "RegionalSalesStatusYM",
                routeTemplate: "api/sales/rss/{year}/{month}",
                defaults: new { controller = "Sales", action = "RegionalSSM" }
            );

            // GET api/sales/rss/{year}/{month}/{day}
            config.Routes.MapHttpRoute(
                name: "RegionalSalesStatusYMD",
                routeTemplate: "api/sales/rss/{year}/{month}/{day}",
                defaults: new { controller = "Sales", action = "RegionalSSD" }
            );

            //--------------- Financial ---------------//

            // GET api/financial/
            config.Routes.MapHttpRoute(
                name: "Financial",
                routeTemplate: "api/financial",
                defaults: new { controller = "Financial", action = "Index" }
            );

            // GET api/financial/global
            config.Routes.MapHttpRoute(
                name: "GlobalFinancial",
                routeTemplate: "api/financial/global",
                defaults: new { controller = "Financial", action = "Global" }
            );

            // GET api/financial/ytd/{year}
            config.Routes.MapHttpRoute(
                name: "FinancialYtD",
                routeTemplate: "api/financial/ytd/{year}",
                defaults: new { controller = "Financial", action = "FinancialYtD" },
                constraints: new { year = "[0-9]+" }
            );

            // GET api/financial/purchases
            config.Routes.MapHttpRoute(
                name: "FinancialPurchases",
                routeTemplate: "api/financial/purchases",
                defaults: new { controller = "Financial", action = "Purchases" }
            );

            // GET api/financial/purchases/yoy/{year}
            config.Routes.MapHttpRoute(
                name: "FinancialPurchasesYoY",
                routeTemplate: "api/financial/purchases/yoy/{year}",
                defaults: new { controller = "Financial", action = "PurchasesYoY" },
                constraints: new { year = "[0-9]+" }
            );

            // GET api/financial/sales
            config.Routes.MapHttpRoute(
                name: "FinancialSales",
                routeTemplate: "api/financial/sales",
                defaults: new { controller = "Financial", action = "Sales" }
            );

            // GET api/financial/sales/yoy/{year}
            config.Routes.MapHttpRoute(
                name: "FinancialSalesYoY",
                routeTemplate: "api/financial/sales/yoy/{year}",
                defaults: new { controller = "Financial", action = "SalesYoY" },
                constraints: new { year = "[0-9]+" }
            );

            // GET api/financial/top10sales
            config.Routes.MapHttpRoute(
                name: "FinancialTop10Sales",
                routeTemplate: "api/financial/top10sales",
                defaults: new { controller = "Financial", action = "Top10SalesCountries" }
            );

            // GET api/financial/salesbooking/{year}
            config.Routes.MapHttpRoute(
                name: "RegSalesBookingY",
                routeTemplate: "api/financial/salesbooking/{year}",
                defaults: new { controller = "Financial", action = "SalesBookingRegY" },
                constraints: new { }
            );

            // GET api/financial/salesbooking/{year}/{month}
            config.Routes.MapHttpRoute(
                name: "RegSalesBookingM",
                routeTemplate: "api/financial/salesbooking/{year}/{month}",
                defaults: new { controller = "Financial", action = "SalesBookingRegM" }
            );

            // GET api/financial/salesbooking/{year}/{month}/{day}
            config.Routes.MapHttpRoute(
                name: "RegSalesBookingD",
                routeTemplate: "api/financial/salesbooking/{year}/{month}/{day}",
                defaults: new { controller = "Financial", action = "SalesBookingRegD" }
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
