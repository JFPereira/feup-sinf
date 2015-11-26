using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project.Lib_Primavera.Model;
using project.Items;

namespace project.Controllers
{
    public class ApiFinancialController : ApiController
    {

        // GET api/financial
        [System.Web.Http.HttpGet]
        public string Index()
        {
            return "test only financial";
        }

        // GET api/financial/global
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Global()
        {
            List<GlobalFinancialItem> global = new List<GlobalFinancialItem>();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();
            int m = Int32.Parse(month);
            int y = Int32.Parse(year);
            for (int i = 1; i < m + 1; i++)
            {
                double purchase = Lib_Primavera.PriIntegration.getMonthlyPurchases(i, y);
                double sale = Lib_Primavera.PriIntegration.getMonthlySales(i, y);
                global.Add(new GlobalFinancialItem
                {
                    Ano = y,
                    Mes = i,
                    Compras = purchase,
                    Vendas = sale

                });
            }

            global = global.OrderBy(e => e.Mes).ToList();
            var json = new JavaScriptSerializer().Serialize(global);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // GET api/financial/ytd/{year}
        [System.Web.Http.HttpGet]
        public FinancialYearInfo FinancialYtD(int year)
        {
            FinancialYearInfo result = new FinancialYearInfo();

            // DB queries
            List<CabecDoc> sales = Lib_Primavera.PriIntegration.getSales();
            List<DocCompra> purchases = Lib_Primavera.PriIntegration.getPurchases();

            // Sales
            foreach (var entry in sales)
                if (entry.Data.Year == year)
                    result.sales += entry.TotalMerc + entry.TotalIva;

            // Purchases
            foreach (var entry in purchases)
                if (entry.Data.Year == year)
                    result.purchases -= entry.TotalMerc + entry.TotalIva;

            // Revenue
            result.revenue = result.sales - result.purchases;

            return result;
        }

        // GET api/financial/purchases
        [System.Web.Http.HttpGet]
        public double Purchases()
        {
            double result = 0;

            // DB query
            List<DocCompra> purchases = Lib_Primavera.PriIntegration.getPurchases();

            // Purchases total amount
            foreach (var entry in purchases)
                result -= entry.TotalMerc + entry.TotalIva;

            return result;
        }

        // GET api/financial/purchases/yoy/{year}
        [System.Web.Http.HttpGet]
        public List<List<double>> PurchasesYoY(int year)
        {
            List<List<double>> result = new List<List<double>>();
            for (int i = 0; i < 12; i++)
                result.Add(new List<double> { 0, 0 });

            // DB query
            List<DocCompra> purchases = Lib_Primavera.PriIntegration.getPurchases();

            // Purchases total amount
            foreach (var entry in purchases)
            {
                if (entry.Data.Year == year || entry.Data.Year == year - 1)
                {
                    double amount = -1 * (entry.TotalMerc + entry.TotalIva);

                    result[entry.Data.Month - 1][entry.Data.Year - year + 1] += amount;
                }
            }

            return result;
        }

        // GET api/financial/sales
        [System.Web.Http.HttpGet]
        public double Sales()
        {
            double result = 0;

            // DB query
            List<CabecDoc> sales = Lib_Primavera.PriIntegration.getSales();

            // Purchases total amount
            foreach (var entry in sales)
                result += entry.TotalMerc + entry.TotalIva;

            return result;
        }

        // GET api/financial/sales/yoy/{year}
        [System.Web.Http.HttpGet]
        public List<List<double>> SalesYoY(int year)
        {
            List<List<double>> result = new List<List<double>>();
            for (int i = 0; i < 12; i++)
                result.Add(new List<double> { 0, 0 });

            // DB query
            List<CabecDoc> sales = Lib_Primavera.PriIntegration.getSales();

            // Purchases total amount
            foreach (var entry in sales)
            {
                if (entry.Data.Year == year || entry.Data.Year == year - 1)
                {
                    double amount = entry.TotalMerc + entry.TotalIva;

                    result[entry.Data.Month - 1][entry.Data.Year - year + 1] += amount;
                }
            }

            return result;
        }

        // GET api/financial/top10sales
        [System.Web.Http.HttpGet]
        public List<TopSalesCountry> Top10SalesCountries()
        {
            return Lib_Primavera.PriIntegration.getTop10SalesCountries();
        }

        // GET api/financial/salesbooking/{year}
        [System.Web.Http.HttpGet]
        public List<RegSalesBookingItem> SalesBookingRegY(string year)
        {
            return Lib_Primavera.PriIntegration.getSalesBookingReg("year", year, null, null);
        }

        // GET api/financial/salesbooking/{year}/{month}
        [System.Web.Http.HttpGet]
        public List<RegSalesBookingItem> SalesBookingRegM(string year, string month)
        {
            return Lib_Primavera.PriIntegration.getSalesBookingReg("month", year, month, null);
        }

        // GET api/financial/salesbooking/{year}/{month}/{day}
        [System.Web.Http.HttpGet]
        public List<RegSalesBookingItem> SalesBookingRegD(string year, string month, string day)
        {
            return Lib_Primavera.PriIntegration.getSalesBookingReg("day", year, month, day);
        }

    }

}
