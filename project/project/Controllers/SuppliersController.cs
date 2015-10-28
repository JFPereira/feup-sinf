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
    public class SuppliersController : ApiController
    {
        // GET: api/suppliers/
        public IEnumerable<Lib_Primavera.Model.Fornecedor> Get()
        {
            return Lib_Primavera.PriIntegration.ListaFornecedores();
        }

        // GET api/suppliers/{id}    
        public Fornecedor Get(string id)
        {
            Lib_Primavera.Model.Fornecedor fornecedor = Lib_Primavera.PriIntegration.GetFornecedor(id);
            if (fornecedor == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return fornecedor;
            }
        }

        // GET api/suppliers/top
        [System.Web.Http.HttpGet]
        public IEnumerable<string> TopSuppliers()
        {
            return new string[] { "Cabras", "Vacas", "Porcas", "Galinhas" };
        }
    }
}

