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
        [System.Web.Http.HttpGet]
        public IEnumerable<string> TopProducts()
        {
            return new string[] { "product1", "product2", "product3", "product4" };
        }
    }
}
