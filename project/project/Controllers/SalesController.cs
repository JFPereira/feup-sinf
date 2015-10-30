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
                        date = sale.Datatime,
                        numPurchases = Lib_Primavera.PriIntegration.numPurchases(sale.Entidade)
                    });
            }

            result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

            return result;
        }

        // GET api/sales/booking/{id}
        [System.Web.Http.HttpGet]
        public List<SalesBookingItem> SalesBooking(string id)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
                returnList.Add(new SalesBookingItem
                    {
                        nome = prod.DescArtigo,
                        numVendas = Lib_Primavera.PriIntegration.getSalesProd(id, prod.DescArtigo)
                    });


            }
            returnList.OrderBy(e => e.numVendas).Reverse().Take(10).ToList();

            return returnList;
        }
    }
}
