using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project.Lib_Primavera.Model;
using project.Items;
using System.Globalization;

namespace project.Controllers
{
    public class ApiClientsController : ApiController
    {
        // GET: api/clients/
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            // return all clients
            IEnumerable<Lib_Primavera.Model.Cliente> clients = Lib_Primavera.PriIntegration.ListaClientes();


            var json = new JavaScriptSerializer().Serialize(clients);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // GET api/clients/{entity}
        [System.Web.Http.HttpGet]
        public Cliente GetClient(string entity)
        {
            // return the target client through entity
            Lib_Primavera.Model.Cliente cliente = Lib_Primavera.PriIntegration.GetCliente(entity);
            if (cliente == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return cliente;
            }
        }

        // GET api/clients/top
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopClients()
        {
            List<TopClientsItem> result;

            if (HomeController.top10ClientsCache != null)
            {
                result = HomeController.top10ClientsCache;
            }
            else
            {
                result = new List<TopClientsItem>();

                // get all the sales docs
                List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSales();

                double totalSalesVolume = 0;

                foreach (CabecDoc sale in sales)
                {
                    // if the entity already exists then update the target values
                    if (result.Exists(e => e.entity == sale.Entidade))
                    {
                        result.Find(e => e.entity == sale.Entidade).salesVolume += (sale.TotalMerc + sale.TotalIva);
                        result.Find(e => e.entity == sale.Entidade).numPurchases++;
                    }
                    else // else create new item and add it to the list
                    {
                        result.Add(new TopClientsItem
                        {
                            entity = sale.Entidade,
                            name = sale.Nome,
                            nif = sale.NumContribuinte,
                            salesVolume = sale.TotalMerc + sale.TotalIva,
                            percentage = "",
                            numPurchases = 1
                        });
                    }

                    totalSalesVolume += (sale.TotalMerc + sale.TotalIva);
                }

                // take the top 10 clientes of the list and order them by sales volume
                result = result.OrderBy(e => e.salesVolume).Reverse().Take(10).ToList();

                // calculate the percentage of each one
                foreach (TopClientsItem client in result)
                    client.percentage += Math.Round(client.salesVolume / totalSalesVolume * 100, 2).ToString(CultureInfo.GetCultureInfo("en-GB"));

                HomeController.top10ClientsCache = result;
            }

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // GET api/clients/{entity}/top-products
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopProducts(string entity)
        {
            // gets all lines in sales docs with all the products sold to the client
            List<Lib_Primavera.Model.LinhaDocVenda> allProducts = Lib_Primavera.PriIntegration.getSalesDocLinesByClient(entity);

            List<TopProductsItem> result = new List<TopProductsItem>();

            double totalProductSalesVolume = 0;

            foreach (LinhaDocVenda product in allProducts)
            {
                // if the product already exists then update the target values
                if (result.Exists(e => e.codArtigo == product.Artigo))
                {
                    result.Find(e => e.codArtigo == product.Artigo).quantity += product.Quantidade;
                    result.Find(e => e.codArtigo == product.Artigo).salesVolume += product.PrecoLiquido;
                }
                else // else create new item and add it to the list
                {
                    result.Add(new TopProductsItem
                    {
                        codArtigo = product.Artigo,
                        description = product.Descricao,
                        quantity = product.Quantidade,
                        salesVolume = product.PrecoLiquido,
                        percentage = ""
                    });
                }

                totalProductSalesVolume += product.PrecoLiquido;
            }

            // take the top 10 clientes of the list and order them by sales volume
            result = result.OrderBy(e => e.salesVolume).Reverse().Take(10).ToList();

            // calculate the percentage of each one
            foreach (TopProductsItem product in result)
                product.percentage += Math.Round(product.salesVolume / totalProductSalesVolume * 100, 2).ToString(CultureInfo.GetCultureInfo("en-GB"));

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // static variable to the total elements of the purchases graphics
        public int totalElems = 0;

        // GET api/clients/{entity}/daily-purchases/{month}/{year}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage DailyPurchases(string entity, string month, string year)
        {
            // date interval between the target sales documents
            string dateStart, dateEnd;

            // list to save the dates which would be process on route
            List<string> dates = new List<string>();

            // list with all the target sales docs
            List<Lib_Primavera.Model.CabecDoc> docs = new List<CabecDoc>();

            // list with the final core view items
            List<Items.DailyPurchasesItem> days = new List<DailyPurchasesItem>();

            // processing dates
            dates = processDates(month, year);

            dateStart = dates.ElementAt(0);
            dateEnd = dates.ElementAt(1);

            // get all the target sales docs
            docs = Lib_Primavera.PriIntegration.getClientPurchasesBetween(entity, dateStart, dateEnd);

            foreach (Lib_Primavera.Model.CabecDoc doc in docs)
            {
                if (days.Exists(e => e.day == doc.Data.Day))
                {
                    days.Find(e => e.day == doc.Data.Day).numPurchase++;
                    days.Find(e => e.day == doc.Data.Day).salesVolume += (doc.TotalIva + doc.TotalMerc);
                }
                else
                {
                    days.Add(new DailyPurchasesItem
                    {
                        day = doc.Data.Day,
                        numPurchase = 1,
                        salesVolume = (doc.TotalMerc + doc.TotalIva)
                    });
                }
            }

            //adding remaining days that have not a purchase
            for (int day = 1; day <= totalElems; day++)
            {
                if (!days.Exists(e => e.day == day))
                {
                    days.Add(new DailyPurchasesItem
                    {
                        day = day,
                        numPurchase = 0,
                        salesVolume = 0
                    });
                }
            }

            days = days.OrderBy(e => e.day).ToList();

            var json = new JavaScriptSerializer().Serialize(days);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // GET api/clients/{entity}/monthly-purchases/{year}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage MonthlyPurchases(string entity, string year)
        {
            // date interval between the target sales documents
            string dateStart, dateEnd;

            // list to save the dates which would be process on route
            List<string> dates = new List<string>();

            // list with all the target sales docs
            List<Lib_Primavera.Model.CabecDoc> docs = new List<CabecDoc>();

            // list with the final core view items
            List<Items.MonthlyPurchasesItem> months = new List<MonthlyPurchasesItem>();

            // processing dates
            dates = processDates(null, year);

            // start date for search
            dateStart = dates.ElementAt(0);

            // end date for search
            dateEnd = dates.ElementAt(1);

            // get all the target sales docs
            docs = Lib_Primavera.PriIntegration.getClientPurchasesBetween(entity, dateStart, dateEnd);

            foreach (Lib_Primavera.Model.CabecDoc doc in docs)
            {
                if (months.Exists(e => e.month == doc.Data.Month.ToString()))
                {
                    months.Find(e => e.month == doc.Data.Month.ToString()).numPurchase++;
                    months.Find(e => e.month == doc.Data.Month.ToString()).salesVolume += (doc.TotalIva + doc.TotalMerc);
                }
                else
                {
                    months.Add(new MonthlyPurchasesItem
                    {
                        month = doc.Data.Month.ToString(),
                        numPurchase = 1,
                        salesVolume = (doc.TotalMerc + doc.TotalIva)
                    });
                }
            }

            //adding remaining months that have not a purchase
            for (int month = 1; month <= totalElems; month++)
            {
                string month_str;

                if (month < 10)
                    month_str = "0" + month.ToString();
                else
                    month_str = month.ToString();

                if (!months.Exists(e => e.month == month.ToString()))
                {
                    months.Add(new MonthlyPurchasesItem
                    {
                        month = month_str,
                        numPurchase = 0,
                        salesVolume = 0
                    });
                }
            }

            months = months.OrderBy(e => e.month).ToList();

            var json = new JavaScriptSerializer().Serialize(months);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // process the date interval and assign the total elements that the WS DailyPurchases/MonthlyPurchases will need
        public List<string> processDates(string month, string year)
        {
            List<string> dates = new List<string>();

            if (month != null)
            {
                dates.Add("" + year + "-" + month + "-" + "01");

                if (month == "01" || month == "03" || month == "05" || month == "07" || month == "08" || month == "10" || month == "12")
                {
                    dates.Add("" + year + "-" + month + "-31");
                    totalElems = 31;
                }
                else if (month == "04" || month == "06" || month == "09" || month == "11")
                {
                    dates.Add("" + year + "-" + month + "-30");
                    totalElems = 30;
                }
                else
                {
                    dates.Add("" + year + "-02-28");
                    totalElems = 28;
                }
            }
            else
            {
                dates.Add("" + year + "-01-01");
                dates.Add("" + year + "-12-31");
                totalElems = 12;
            }

            return dates;
        }

        // GET api/clients/{entity}/apc
        [System.Web.Http.HttpGet]
        public HttpResponseMessage AveragePurchaseCost(string entity)
        {
            APCItem result = new APCItem();

            double totalPurchaseVolume = 0;

            // get all the purchases of the client
            result.details = Lib_Primavera.PriIntegration.getClientPurchases(entity);

            // get the total number of purchases of the client
            result.numPurchases = result.details.Count();

            // sum the total value of each client purchase
            for (int i = 0; i < result.numPurchases; i++)
                totalPurchaseVolume += (result.details.ElementAt(i).TotalMerc + result.details.ElementAt(i).TotalIva);

            // calculate the average of total value and number of purchases
            result.averagePurchaseCost = totalPurchaseVolume / result.numPurchases;

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }

        // GET api/clients/{entity}/ce
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CostsVsEarnings(string entity)
        {
            // gets all lines in sales docs with all the products sold to the client
            List<LinhaDocVenda> allSoldProducts = Lib_Primavera.PriIntegration.getSalesDocLinesByClient(entity);
            CostsVsEarningsItem result = new CostsVsEarningsItem();

            double tCost = 0, tEarning = 0, prof = 0;

            foreach (LinhaDocVenda product in allSoldProducts)
            {
                tCost += (product.PrecoCustoMedio * product.Quantidade);
                tEarning += (product.PrecoUnitario * product.Quantidade);
            }

            // calculate the diff between total earnings and total costs
            prof = tEarning - tCost;

            result.totalCost = tCost.ToString(CultureInfo.GetCultureInfo("en-GB"));
            result.totalEarning = tEarning.ToString(CultureInfo.GetCultureInfo("en-GB"));
            result.profit = prof.ToString(CultureInfo.GetCultureInfo("en-GB"));
            result.total = tEarning + tCost;

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
        }
    }
}
