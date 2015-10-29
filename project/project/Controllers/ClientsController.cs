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
        public IEnumerable<Lib_Primavera.Model.Client> Get()
        {
            return Lib_Primavera.PriIntegration.ListaClientes();
        }

        // GET api/clients/{id}
        public Client Get(string id)
        {
            Lib_Primavera.Model.Client cliente = Lib_Primavera.PriIntegration.GetCliente(id);
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
            List<Lib_Primavera.Model.Sale> sales = Lib_Primavera.PriIntegration.getSales();
            List<TopClientsItem> result = new List<TopClientsItem>();

            foreach (Sale sale in sales)
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
                        name = sale.Nome,
                        nif = sale.NumContribuinte,
                        salesVolume = sale.TotalMerc + sale.TotalIva,
                        percentage = "",
                        numPurchases = 1
                    });
                }
            }

            result = result.OrderBy(e => e.salesVolume).Reverse().Take(10).ToList();

            double sum = 0;
            foreach (TopClientsItem client in result)
                sum += client.salesVolume;

            foreach (TopClientsItem client in result)
                client.percentage += Math.Round(client.salesVolume / sum * 100, 2) + " %";

            return result;
        }
        /*public HttpResponseMessage TopClients()
        {
            int i = 49;
            var json = new JavaScriptSerializer().Serialize(i);
            var response = Request.CreateResponse(HttpStatusCode.OK, json);
            return response;
        }*/

        //--------------- REST Methods ---------------//
        public HttpResponseMessage Post(Lib_Primavera.Model.Client cliente)
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

        public HttpResponseMessage Put(string id, Lib_Primavera.Model.Client cliente)
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
