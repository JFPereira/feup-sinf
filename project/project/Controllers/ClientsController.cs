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
    public class ClientsController : ApiController
    {
        // GET: api/clients/
        public IEnumerable<Lib_Primavera.Model.Cliente> Get()
        {
            return Lib_Primavera.PriIntegration.ListaClientes();
        }

        // GET api/clients/{id}
        [System.Web.Http.HttpGet]
        public Cliente GetClient(string id)
        {
            Lib_Primavera.Model.Cliente cliente = Lib_Primavera.PriIntegration.GetCliente(id);
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
        public List<Items.TopClientsItem> TopClients()
        {
            List<Lib_Primavera.Model.CabecDoc> sales = Lib_Primavera.PriIntegration.getSales();
            List<TopClientsItem> result = new List<TopClientsItem>();

            double totalSalesVolume = 0;

            foreach (CabecDoc sale in sales)
            {
                if (result.Exists(e => e.nif == sale.NumContribuinte))
                {
                    result.Find(e => e.nif == sale.NumContribuinte).salesVolume += (sale.TotalMerc + sale.TotalIva);
                    result.Find(e => e.nif == sale.NumContribuinte).numPurchases++;
                }
                else
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

            result = result.OrderBy(e => e.salesVolume).Reverse().Take(10).ToList();

            foreach (TopClientsItem client in result)
                client.percentage += Math.Round(client.salesVolume / totalSalesVolume * 100, 2) + " %";

            return result;
        }

        // GET api/clients/{id}/top-products
        [System.Web.Http.HttpGet]
        public List<Items.TopProductsItem> TopProducts(string id)
        {
            List<Lib_Primavera.Model.LinhaDocVenda> allProducts = Lib_Primavera.PriIntegration.topClientProducts(id);
            List<TopProductsItem> result = new List<TopProductsItem>();

            double totalProductSalesVolume = 0;

            foreach (LinhaDocVenda product in allProducts)
            {
                if (result.Exists(e => e.codArtigo == product.CodArtigo))
                {
                    result.Find(e => e.codArtigo == product.CodArtigo).quantity += product.Quantidade;
                    result.Find(e => e.codArtigo == product.CodArtigo).salesVolume += product.PrecoLiquido;
                }
                else
                {
                    result.Add(new TopProductsItem
                    {
                        codArtigo = product.CodArtigo,
                        description = product.DescArtigo,
                        quantity = product.Quantidade,
                        salesVolume = product.PrecoLiquido,
                        percentage = ""
                    });
                }

                totalProductSalesVolume += product.PrecoLiquido;
            }

            result = result.OrderBy(e => e.salesVolume).Reverse().Take(10).ToList();

            foreach (TopProductsItem product in result)
                product.percentage += Math.Round(product.salesVolume / totalProductSalesVolume * 100, 2) + " %";

            return result;
        }

        // static variable to the total elements of the purchases graphics
        public int totalElems = 0;

        // GET api/clients/{id}/daily-purchases/{month}/{year}
        [System.Web.Http.HttpGet]
        public List<Items.DailyPurchasesItem> DailyPurchases(string id, string month, string year)
        {
            // date interval between the target sales documents
            string dateStart, dateEnd;

            // list to save the dates which would be process on route
            List<string> dates = new List<string>();

            // list with all the target sales docs
            List<Lib_Primavera.Model.CabecDoc> docs = new List<CabecDoc>();

            // list with the final core view items
            List<Items.DailyPurchasesItem> days = new List<DailyPurchasesItem>();

            // validate month obtained in route
            if (checkMonth(month))
            {
                dates = processDates(month, year);

                dateStart = dates.ElementAt(0);
                dateEnd = dates.ElementAt(1);
            }
            else
                return null;

            // get all the target sales docs
            docs = Lib_Primavera.PriIntegration.getClientDailyPurchases(id, dateStart, dateEnd);

            foreach (Lib_Primavera.Model.CabecDoc doc in docs) {
                if (days.Exists(e => e.day == doc.Datatime.Day))
                {
                    days.Find(e => e.day == doc.Datatime.Day).numPurchase++;
                    days.Find(e => e.day == doc.Datatime.Day).salesVolume += (doc.TotalIva + doc.TotalMerc);
                }
                else
                {
                    days.Add(new DailyPurchasesItem
                    {
                        day = doc.Datatime.Day,
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

            return days.OrderBy(e => e.day).ToList(); ;
        }

        // static variable to the list of available months
        static List<string> months = new List<string>() { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };

        // process the date interval and assign the total elements that will have the WS DailyPurchases/MonthlyPurchases
        public List<string> processDates(string month, string year)
        {
            List<string>dates = new List<string>();

            if (month != null)
            {
                dates.Add("" + year + "-" + (months.IndexOf(month) + 1) + "-" + "01");

                if (month == "january" || month == "march" || month == "may" || month == "july" || month == "august" || month == "october" || month == "december") {
                    dates.Add("" + year + "-" + (months.IndexOf(month) + 1).ToString() + "-31");
                    totalElems = 31;
                }
                else if (month == "april" || month == "june" || month == "september" || month == "november") {
                    dates.Add("" + year + "-" + (months.IndexOf(month) + 1).ToString() + "-30");
                    totalElems = 30;
                }
                else {
                    dates.Add("" + year + "-" + (months.IndexOf(month) + 1).ToString() + "-28");
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

        public bool checkMonth(string month)
        {
            bool result = (months.Exists(e => e == month)) ?  true :  false;

            return result;
        }

        // GET api/clients/{id}/apc
        [System.Web.Http.HttpGet]
        public Items.APCItem AveragePurchaseCost(string id)
        {
            Items.APCItem result = new Items.APCItem();



            return result;
        }
    }
}
