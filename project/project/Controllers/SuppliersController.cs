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
        public List<TopSuppliersItem> TopSuppliers()
        {
            List<Lib_Primavera.Model.DocCompra> purchases = Lib_Primavera.PriIntegration.getPurchases();
            List<TopSuppliersItem> result = new List<TopSuppliersItem>();

            double totalSalesVolume = 0;

            foreach (DocCompra purchase in purchases)
            {
                if (result.Exists(e => e.Nif == purchase.NumContribuinte))
                {
                    result.Find(e => e.Nif == purchase.NumContribuinte).VolumeCompras += Math.Abs(purchase.TotalMerc + purchase.TotalIva);
                    result.Find(e => e.Nif == purchase.NumContribuinte).NrCompras++;
                }
                else
                {
                    result.Add(new TopSuppliersItem
                    {
                        CodFornecedor = purchase.Entidade,
                        Nome = purchase.Nome,
                        Nif = purchase.NumContribuinte,
                        VolumeCompras = Math.Abs(purchase.TotalMerc + purchase.TotalIva),
                        Percentagem = "",
                        NrCompras = 1
                    });
                }

                totalSalesVolume += Math.Abs(purchase.TotalMerc + purchase.TotalIva);
            }

            result = result.OrderBy(e => e.VolumeCompras).Reverse().Take(10).ToList();

            foreach (TopSuppliersItem supplier in result)
                supplier.Percentagem += Math.Round(supplier.VolumeCompras / totalSalesVolume * 100, 2) + " %";

            return result;
        }
    }
}

