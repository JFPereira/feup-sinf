using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project.Lib_Primavera;
using project.Items;

namespace project.Controllers
{
    public class SalesController : ApiController
    {
        // GET api/sales/top
        [System.Web.Http.HttpGet]
        public List<TopSalesItem> TopSales()
        {
            List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSales();
            List<TopSalesItem> result = new List<TopSalesItem>();

            foreach (Lib_Primavera.Model.CabecDoc sale in sales)
            {
                result.Add(new TopSalesItem
                    {
                        entity = sale.Entidade,
                        numDoc = sale.NumDoc,
                        purchaseValue = sale.TotalMerc + sale.TotalIva,
                        date = sale.Data,
                        numPurchases = Lib_Primavera.PriIntegration.numPurchases(sale.Entidade)
                    });
            }

            result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

            return result;
        }

        // GET api/sales/psb/{year}
        [System.Web.Http.HttpGet]
        public List<SalesBookingItem> SalesBookingY(string year)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
                returnList.Add(new SalesBookingItem
                    {
                        nome = prod.DescArtigo,
                        valorVendas = Lib_Primavera.PriIntegration.getSalesProd("year", prod.CodArtigo, year, null, null)
                    });
            }
            returnList = returnList.OrderBy(e => e.valorVendas).Reverse().Take(10).ToList();

            return returnList;
        }

        // GET api/sales/psb/{year}/{month}
        [System.Web.Http.HttpGet]
        public List<SalesBookingItem> SalesBookingM(string year, string month)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
                
                returnList.Add(new SalesBookingItem
                {
                    nome = prod.DescArtigo,
                    valorVendas = Lib_Primavera.PriIntegration.getSalesProd("month", prod.CodArtigo, year, month, null)
                });
                
            }
            returnList = returnList.OrderBy(e => e.valorVendas).Reverse().Take(10).ToList();

            return returnList;
        }

        // GET api/sales/psb/{year}/{month}/{day}
        [System.Web.Http.HttpGet]
        public List<SalesBookingItem> SalesBookingD(string year, string month, string day)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
               
                returnList.Add(new SalesBookingItem
                {
                    nome = prod.DescArtigo,
                    valorVendas = Lib_Primavera.PriIntegration.getSalesProd("day", prod.CodArtigo, year, month, day)
                });
            }
            returnList = returnList.OrderBy(e => e.valorVendas).Reverse().Take(10).ToList();

            return returnList;
        }
    }
}
