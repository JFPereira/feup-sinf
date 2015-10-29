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
        public IEnumerable<Lib_Primavera.Model.Product> Get()
        {
            return Lib_Primavera.PriIntegration.ListaArtigos();
        }

        // GET api/products/{id}    
        public Product GetProduct(string id)
        {
            Lib_Primavera.Model.Product artigo = Lib_Primavera.PriIntegration.GetArtigo(id);
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
        /*[System.Web.Http.HttpGet]
        public List<TopProductsItem> TopProducts()
        {
            List<Lib_Primavera.Model.Sale> sales = Lib_Primavera.PriIntegration.ListaCompras();
            List<TopProductsItem> result = new List<TopProductsItem>();

            foreach (Sale sale in sales)
            {
                foreach (string cod in sale.codArtigo)
                {
                    if (result.Exists(e => e.codArtigo == cod))
                    {
                        result.Find(e => e.codArtigo == cod).salesVolume += (sale.TotalMerc + sale.TotalIva);
                    }
                    else
                    {
                        Product pro = Get(cod);
                        result.Add(new TopProductsItem
                        {
                            name = pro.DescArtigo,
                            codArtigo = cod,
                            salesVolume = sale.TotalMerc + sale.TotalIva,
                            percentage = ""
                        });
                    }
                }
            }

            result = result.OrderBy(e => e.salesVolume).Reverse().Take(10).ToList();

            double sum = 0;
            foreach (TopProductsItem product in result)
                sum += product.salesVolume;

            foreach (TopProductsItem product in result)
                product.percentage += Math.Round(product.salesVolume / sum * 100, 2) + " %";

            return result;
        }*/
    }
}
