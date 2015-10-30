using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project.Lib_Primavera.Model;
using project.Items;

namespace project.Controllers
{
    public class FinancialController : ApiController
    {

        // GET api/financial
        [System.Web.Http.HttpGet]
        public string Index()
        {
            return "test only financial";
        }

        // GET api/financial/global
        [System.Web.Http.HttpGet]
        public List<GlobalFinancialItem> Global()
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
                    Compras = sale,
                    Vendas = purchase

                });
            }

            global = global.OrderBy(e => e.Mes).ToList();
            return global;
        }

        // GET api/financial/ytd/{year}
        [System.Web.Http.HttpGet]
        public FinancialInfo FinancialYtD(int year)
        {
            return Lib_Primavera.PriIntegration.getFinancialYtD(year);
        }

        // GET api/financial/purchases
        [System.Web.Http.HttpGet]
        public double Purchases()
        {
            return Lib_Primavera.PriIntegration.getPurchasesTotal();
        }

        // GET api/financial/purchases/yoy/{year}
        [System.Web.Http.HttpGet]
        public List<List<double>> PurchasesYoY(int year)
        {
            return Lib_Primavera.PriIntegration.getPurchasesYoY(year);
        }

        // GET api/financial/sales
        [System.Web.Http.HttpGet]
        public double Sales()
        {
            return Lib_Primavera.PriIntegration.getSalesTotal();
        }

        // GET api/financial/sales/yoy/{year}
        [System.Web.Http.HttpGet]
        public List<List<double>> SalesYoY(int year)
        {
            return Lib_Primavera.PriIntegration.getSalesYoY(year);
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
            return Lib_Primavera.PriIntegration.getSalesBookingReg("year",year,null,null);
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
