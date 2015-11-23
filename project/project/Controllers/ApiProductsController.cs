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
using System.Globalization;

namespace project.Controllers
{
    public class ApiProductsController : ApiController
    {
        // GET: api/products/
        public IEnumerable<Lib_Primavera.Model.Artigo> Get()
        {
            return Lib_Primavera.PriIntegration.ListaArtigos();
        }

        // GET api/products/{id}
        public Artigo GetProduct(string id)
        {
            List<double> precos = new List<double>();
            double sumPrecos = 0;
            Lib_Primavera.Model.Artigo artigo = Lib_Primavera.PriIntegration.GetArtigo(id);
            if (artigo == null)
            {
                return null;
                //throw new HttpResponseException(
                 // Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                artigo = Lib_Primavera.PriIntegration.GetPrecoArtigo(artigo);
                precos = Lib_Primavera.PriIntegration.GetTodosPrecosArtigo(artigo);
                foreach (double preco in precos)
                {
                    sumPrecos += preco;
                }
                artigo.PrecoMedio = sumPrecos / precos.Count;
                artigo = Lib_Primavera.PriIntegration.GetVendasArtigo(artigo);
                artigo = Lib_Primavera.PriIntegration.GetComprasArtigo(artigo);
                artigo = Lib_Primavera.PriIntegration.GetTopClientesArtigo(artigo);
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
                artigo.VendasComprasMes = global;
                return artigo;
            }
        }

        // GET api/products/top
        [System.Web.Http.HttpGet]
        public HttpResponseMessage TopProducts()
        {
            List<Lib_Primavera.Model.LinhaDocVenda> products = Lib_Primavera.PriIntegration.getProductSales();
            List<TopProductsItem> result = new List<TopProductsItem>();
            double totalSalesVolume = 0;

            foreach (LinhaDocVenda product in products)
            {
                string cod = product.Artigo;
                    if (result.Exists(e => e.codArtigo == cod))
                    {
                        result.Find(e => e.codArtigo == cod).salesVolume += (product.PrecoLiquido);
                        result.Find(e => e.codArtigo == cod).quantity += product.Quantidade;
                    }
                    else
                    {
                        result.Add(new TopProductsItem
                        {
                            description = product.Descricao,
                            codArtigo = cod,
                            salesVolume = product.TotalILiquido,
                            quantity = product.Quantidade,
                            percentage = ""
                        });
                    }
                    totalSalesVolume += product.TotalILiquido;
                }
            

            result = result.OrderBy(e => e.salesVolume).Reverse().Take(10).ToList();

            foreach (TopProductsItem product in result)
                product.percentage += Math.Round(product.salesVolume / totalSalesVolume * 100, 2).ToString(CultureInfo.GetCultureInfo("en-GB"));

            var json = new JavaScriptSerializer().Serialize(result);

            return Request.CreateResponse(HttpStatusCode.OK, json);
           
        }

        // GET api/products/shipments
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Shipments()
        {
            double delayed = Lib_Primavera.PriIntegration.GetShipments();

            var json = new JavaScriptSerializer().Serialize(delayed);

            return Request.CreateResponse(HttpStatusCode.OK, json);

        }

        // GET api/products/shipments/{id}
        [System.Web.Http.HttpGet]
        public HttpResponseMessage ProductShipments(string id)
        {
            double delayed = Lib_Primavera.PriIntegration.GetProductShipments(id);

            var json = new JavaScriptSerializer().Serialize(delayed);

            return Request.CreateResponse(HttpStatusCode.OK, json);

        }


    }
}
