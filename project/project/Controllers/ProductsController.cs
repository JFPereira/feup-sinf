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
    public class ProductsController : ApiController
    {
        // GET: api/products/
        public IEnumerable<Lib_Primavera.Model.Artigo> Get()
        {
            return Lib_Primavera.PriIntegration.ListaArtigos();
        }

        // GET api/products/{id}
        public Artigo GetProduct(string id)
        {
            Lib_Primavera.Model.Artigo artigo = Lib_Primavera.PriIntegration.GetArtigo(id);
            if (artigo == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return artigo;
            }
        }

        // GET api/products/top
        [System.Web.Http.HttpGet]
        public List<TopProductsItem> TopProducts()
        {
            List<Lib_Primavera.Model.LinhaDocVenda> products = Lib_Primavera.PriIntegration.getProductSales();
            List<TopProductsItem> result = new List<TopProductsItem>();
            double totalSalesVolume = 0;

            foreach (LinhaDocVenda product in products)
            {
                string cod = product.CodArtigo;
                    if (result.Exists(e => e.codArtigo == cod))
                    {
                        result.Find(e => e.codArtigo == cod).salesVolume += (product.PrecoLiquido);
                        result.Find(e => e.codArtigo == cod).quantity += product.Quantidade;
                    }
                    else
                    {
                        result.Add(new TopProductsItem
                        {
                            description = product.DescArtigo,
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
                product.percentage += Math.Round(product.salesVolume / totalSalesVolume * 100, 2) + " %";

            return result;
        }
    }
}
