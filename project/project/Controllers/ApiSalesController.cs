using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project.Lib_Primavera;
using project.Items;

namespace project.Controllers
{
    public class ApiSalesController : ApiController
    {

        //returns 10 top sales of all time
        // GET api/sales/top
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopSales()
        {
            List<TopSalesItem> result;

            if (HomeController.topSalesCache != null)
            {
                result = HomeController.topSalesCache;
            }
            else
            {
                result = new List<TopSalesItem>();

                List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSales();

                foreach (Lib_Primavera.Model.CabecDoc sale in sales)
                {
                    result.Add(new TopSalesItem
                        {
                            entity = sale.Entidade,
                            numDoc = sale.NumDoc,
                            purchaseValue = sale.TotalMerc + sale.TotalIva,
                            date = sale.Data.ToString("dd/MM/yyyy"),
                            numPurchases = Lib_Primavera.PriIntegration.numPurchases(sale.Entidade)
                        });
                }

                result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

                HomeController.topSalesCache = result;
            }

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns 10 top sales of a certain year and month
        // GET api/sales/top/{year}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopSalesY(string year)
        {
            List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSalesBy("year", year, null, null);
            List<TopSalesItem> result = new List<TopSalesItem>();

            foreach (Lib_Primavera.Model.CabecDoc sale in sales)
            {
                result.Add(new TopSalesItem
                {
                    entity = sale.Entidade,
                    numDoc = sale.NumDoc,
                    purchaseValue = sale.TotalMerc + sale.TotalIva,
                    date = sale.Data.ToString("dd/MM/yyyy"),
                    numPurchases = Lib_Primavera.PriIntegration.numUnits(sale.NumDoc)
                });
            }

            result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns 10 top sales of a certain year and month
        // GET api/sales/top/{year}/{month}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopSalesM(string year, string month)
        {
            List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSalesBy("month", year, month, null);
            List<TopSalesItem> result = new List<TopSalesItem>();

            foreach (Lib_Primavera.Model.CabecDoc sale in sales)
            {
                result.Add(new TopSalesItem
                {
                    entity = sale.Entidade,
                    numDoc = sale.NumDoc,
                    purchaseValue = sale.TotalMerc + sale.TotalIva,
                    date = sale.Data.ToString("dd/MM/yyyy"),
                    numPurchases = Lib_Primavera.PriIntegration.numUnits(sale.NumDoc)
                });
            }

            result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns 10 top sales of a certain year, month and day
        // GET api/sales/top/{year}/{month}/{day}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopSalesD(string year, string month, string day)
        {
            List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSalesBy("day", year, month, day);
            List<TopSalesItem> result = new List<TopSalesItem>();

            foreach (Lib_Primavera.Model.CabecDoc sale in sales)
            {
                result.Add(new TopSalesItem
                {
                    entity = sale.Entidade,
                    numDoc = sale.NumDoc,
                    purchaseValue = sale.TotalMerc + sale.TotalIva,
                    date = sale.Data.ToString("dd/MM/yyyy"),
                    numPurchases = Lib_Primavera.PriIntegration.numUnits(sale.NumDoc)
                });
            }

            result = result.OrderBy(e => e.purchaseValue).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);

        }

        //returns a list with the 10 top products and volume of sales in a year
        // GET api/sales/psb/{year}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SalesBookingY(string year)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
                List<double> sp = Lib_Primavera.PriIntegration.getSalesProd("year", prod.CodArtigo, year, null, null);
                returnList.Add(new SalesBookingItem
                    {
                        codArtigo = prod.CodArtigo,
                        nome = prod.DescArtigo,
                        valorVendas = sp[0],
                        quantidade = sp[1]
                    });
            }
            returnList = returnList.OrderBy(e => e.valorVendas).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns a list with the 10 top products and volume of sales in a certain year and month
        // GET api/sales/psb/{year}/{month}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SalesBookingM(string year, string month)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
                List<double> sp = Lib_Primavera.PriIntegration.getSalesProd("month", prod.CodArtigo, year, month, null);
                returnList.Add(new SalesBookingItem

                {
                    codArtigo = prod.CodArtigo,
                    nome = prod.DescArtigo,
                    valorVendas = sp[0],
                    quantidade = sp[1]
                });
                
            }
             
            returnList = returnList.OrderBy(e => e.valorVendas).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns a list with the 10 top products and volume of sales in a certain year, month and day
        // GET api/sales/psb/{year}/{month}/{day}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SalesBookingD(string year, string month, string day)
        {
            List<SalesBookingItem> returnList = new List<SalesBookingItem>();
            List<Lib_Primavera.Model.Artigo> allProd = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (Lib_Primavera.Model.Artigo prod in allProd)
            {
                List<double> sp = Lib_Primavera.PriIntegration.getSalesProd("day", prod.CodArtigo, year, month, day);
                returnList.Add(new SalesBookingItem
                {
                    codArtigo = prod.CodArtigo,
                    nome = prod.DescArtigo,
                    valorVendas = sp[0],
                    quantidade = sp[1]
                });
            }
            returnList = returnList.OrderBy(e => e.valorVendas).Reverse().Take(10).ToList();

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns the regional sales status, which is the percentage of volume of sales each region has in a certain year
        // GET api/sales/rss/{year}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RegionalSSY(string year)
        {
            List<RegionalSSItem> returnList = new List<RegionalSSItem>();
            List<string> countries = Lib_Primavera.PriIntegration.getAllCountries();

            double totalValue = 0;
            double thisValue = 0;

            foreach (string country in countries)
            {
                thisValue = Lib_Primavera.PriIntegration.getPercentage("year", year, null, null, country);
                totalValue += thisValue;
                returnList.Add(new RegionalSSItem
                {
                    pais = country,
                    percentagem = thisValue,
                    valor = thisValue

                });
            }

            foreach (RegionalSSItem item in returnList)
            {
                if (item.valor == 0)
                {
                    item.percentagem = 0;
                }
                else
                {
                    item.percentagem = 100.0 * item.percentagem / totalValue;
                }
            }

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns the regional sales status, which is the percentage of volume of sales each region has in a certain year and month
        // GET api/sales/rss/{year}/{month}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RegionalSSM(string year, string month)
        {
            List<RegionalSSItem> returnList = new List<RegionalSSItem>();
            List<string> countries = Lib_Primavera.PriIntegration.getAllCountries();
            double totalValue = 0;
            double thisValue = 0;

            foreach (string country in countries)
            {
                thisValue = Lib_Primavera.PriIntegration.getPercentage("month", year, month, null, country);
                totalValue += thisValue;
                returnList.Add(new RegionalSSItem
                {
                    pais = country,
                    percentagem = thisValue,
                    valor = thisValue

                });
            }

            foreach (RegionalSSItem item in returnList)
            {
                if (item.valor == 0)
                {
                    item.percentagem = 0;
                }
                else
                {
                    item.percentagem = 100.0 * item.percentagem / totalValue;
                }
            }

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns the regional sales status, which is the percentage of volume of sales each region has in a certain year, month and day
        // GET api/sales/rss/{year}/{month}/{day}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage RegionalSSD(string year, string month, string day)
        {
            List<RegionalSSItem> returnList = new List<RegionalSSItem>();
            List<string> countries = Lib_Primavera.PriIntegration.getAllCountries();
            double totalValue = 0;
            double thisValue = 0;

            foreach (string country in countries)
            {
                thisValue = Lib_Primavera.PriIntegration.getPercentage("day", year, month, day, country);
                totalValue += thisValue;
                returnList.Add(new RegionalSSItem
                {
                    pais = country,
                    percentagem = thisValue,
                    valor = thisValue

                });
            }
            foreach (RegionalSSItem item in returnList)
            {
                if (item.valor == 0)
                {
                    item.percentagem = 0;
                }
                else
                {
                    item.percentagem = 100.0 * item.percentagem / totalValue;
                }
            }

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        //returns sales growth, which is the percentage of comparing volume of sales of two certain times
        // GET api/sales/sg/{year}/{month}/{year}/{month}
        /*[System.Web.Http.HttpGet]
        public HttpResponseMessage SalesGrowth(string year1, string month1, string year2, string month2)
        {
            List<Lib_Primavera.Model.CabecDoc> sales1 = new List<Lib_Primavera.Model.CabecDoc>();
            List<Lib_Primavera.Model.CabecDoc> sales2 = new List<Lib_Primavera.Model.CabecDoc>();
            List<SalesGrowthItem> returnList = new List<SalesGrowthItem>();
            string name = "";

            if (month1 == "None")
            {
                sales1 = Lib_Primavera.PriIntegration.getSalesBy("year", year1, null, null);
                name += "Growth from " + year1 + " to ";
            }
            else
            {
                sales1 = Lib_Primavera.PriIntegration.getSalesBy("month", year1, month1, null);
                name += "Growth from " + month1 + " of " + year1 + " to ";
            }

            if (month2 == "None")
            {
                sales2 = Lib_Primavera.PriIntegration.getSalesBy("year", year2, null, null);
                name += year2;
            }
            else
            {
                sales2 = Lib_Primavera.PriIntegration.getSalesBy("month", year2, month2, null);
                name += month2 + " of " + year2;
            }



            double totalValue1 = 0;
            double totalValue2 = 0;
            double percentage = 0;

            foreach (Lib_Primavera.Model.CabecDoc doc in sales1)
            {
                totalValue1 += doc.TotalMerc + doc.TotalIva;
            }
            foreach (Lib_Primavera.Model.CabecDoc doc in sales2)
            {
                totalValue2 += doc.TotalMerc + doc.TotalIva;
            }

            if (totalValue1 != 0 && totalValue2 != 0)
            {
                percentage = (totalValue2 - totalValue1) / totalValue1 * 100;
            }
            else
            {
                if (totalValue1 == 0)
                {
                    percentage = totalValue2;
                }
                if (totalValue2 == 0)
                {
                    percentage = -totalValue1;
                }
            }

            returnList.Add(new SalesGrowthItem
            {

                Nome = name,
                Percentagem = percentage,
                Valor = totalValue2 - totalValue1

            });

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);
                        
        } */

        //returns sales growth, which is the percentage of comparing volume of sales of two years
        // GET api/sales/sg/{year}/{year}

        [System.Web.Http.HttpGet]
        public HttpResponseMessage SalesGrowth(string year1, string year2)
        {
            List<Lib_Primavera.Model.CabecDoc> sales1 = new List<Lib_Primavera.Model.CabecDoc>();
            List<Lib_Primavera.Model.CabecDoc> sales2 = new List<Lib_Primavera.Model.CabecDoc>();
            List<SalesGrowthItem> returnList = new List<SalesGrowthItem>();
            
            sales1 = Lib_Primavera.PriIntegration.getSalesBy("year", year1, null, null);
            sales2 = Lib_Primavera.PriIntegration.getSalesBy("year", year2, null, null);
           
            //[i][0] totalValue1;
            //[i][1] totalValue2;
            //[i][2] percentage;
            //[i][3] dif;
            List<List<double>> quarters = new List<List<double>>();
            for (int i = 0; i < 4; i++)
            {
                List<double> l = new List<double>();
                for (int j = 0; j < 4; j++)
                {
                    l.Add(0);
                }
                quarters.Add(l);
            }

            foreach (var entry in sales1)
            {
                if (entry.Data.Month < 4)
                {
                    quarters[0][0] += entry.TotalMerc + entry.TotalIva;
                }
                else if (entry.Data.Month < 7)
                {
                    quarters[1][0] += entry.TotalMerc + entry.TotalIva;
                }
                else if (entry.Data.Month < 10)
                {
                    quarters[2][0] += entry.TotalMerc + entry.TotalIva;
                }
                else if (entry.Data.Month < 13)
                {
                    quarters[3][0] += entry.TotalMerc + entry.TotalIva;
                }
            }

            foreach (var entry in sales2)
            {
                if (entry.Data.Month < 4)
                {
                    quarters[0][1] += entry.TotalMerc + entry.TotalIva;

                    //result[entry.Data.Month - 1][entry.Data.Year - year + 1] += amount;
                }
                else if (entry.Data.Month < 7)
                {
                    quarters[1][1] += entry.TotalMerc + entry.TotalIva;
                }
                else if (entry.Data.Month < 10)
                {
                    quarters[2][1] += entry.TotalMerc + entry.TotalIva;
                }
                else if (entry.Data.Month < 13)
                {
                    quarters[3][1] += entry.TotalMerc + entry.TotalIva;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                quarters[i][3] = quarters[i][1] - quarters[i][0];

                if (quarters[i][0] != 0 && quarters[i][1] != 0)
                {
                    quarters[i][2] = (quarters[i][1] - quarters[i][0]) / quarters[i][1] * 100;
                }
                else
                {
                    if (quarters[i][0] == 0)
                    {
                        quarters[i][2] = quarters[i][1];
                    }
                    else if (quarters[i][1] == 0)
                    {
                        quarters[i][2] = -quarters[i][0];
                    }
                }

                returnList.Add(new SalesGrowthItem
                {

                    Valor1 = quarters[i][0],
                    Valor2 = quarters[i][1],
                    Percentagem = quarters[i][2],
                    Dif = quarters[i][3]

                });
            }

            var json = new JavaScriptSerializer().Serialize(returnList);

            return Request.CreateResponse(HttpStatusCode.OK, json);

        } 
    }
}