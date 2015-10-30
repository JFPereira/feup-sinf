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
        public List<TopClientsItem> TopClients()
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
        /*public HttpResponseMessage TopClients()
        {
            int i = 49;
            var json = new JavaScriptSerializer().Serialize(i);
            var response = Request.CreateResponse(HttpStatusCode.OK, json);
            return response;
        }*/

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

        [System.Web.Http.HttpGet]
        public List<Lib_Primavera.Model.CabecDoc> DailyPurchases(string id, string month, string year)
        {
            string dateStart, dateEnd;

            List<string> dates = new List<string>();

            if (checkMonth(month))
            {
                dates = processDates(month, year);

                dateStart = dates.ElementAt(0);
                dateEnd = dates.ElementAt(1);
            }
            else
                return null;

            List<Lib_Primavera.Model.CabecDoc> docs = Lib_Primavera.PriIntegration.getDailyPurchases(id, dateStart, dateEnd);

            return docs;
        }

        static List<string> months = new List<string>() { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };

        public List<string> processDates(string month, string year)
        {
            List<string>dates = new List<string>();

            if (month != null)
            {
                dates.Add("" + year + "-" + (months.IndexOf(month) + 1) + "-" + "01");

                if (month == "january" || month == "march" || month == "may" || month == "july" || month == "august" || month == "october" || month == "december")
                    dates.Add("" + year + "-" + (months.IndexOf(month) + 1).ToString() + "-31");
                else if (month == "april" || month == "june" || month == "september" || month == "november")
                    dates.Add("" + year + "-" + (months.IndexOf(month) + 1).ToString() + "-30");
                else
                    dates.Add("" + year + "-" + (months.IndexOf(month) + 1).ToString() + "-28");
            }
            else
            {
                dates.Add("" + year + "-01-01");
                dates.Add("" + year + "-12-31");
            }

            return dates;
        }

        public bool checkMonth(string month)
        {
            bool result = (months.Exists(e => e == month)) ?  true :  false;

            return result;
        }

        //--------------- REST Methods ---------------//
        public HttpResponseMessage Post(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.InsereClienteObj(cliente);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, cliente);
                string uri = Url.Link("DefaultApi", new { CodCliente = cliente.CodCliente });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage Put(string id, Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();

            try
            {
                erro = Lib_Primavera.PriIntegration.UpdCliente(cliente);
                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }
            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }

        public HttpResponseMessage Delete(string id)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();

            try
            {
                erro = Lib_Primavera.PriIntegration.DelCliente(id);

                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }
            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }
    }
}
