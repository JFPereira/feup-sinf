using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using project.Items;
using project.Lib_Primavera.Model;

namespace project.Controllers
{
    public class ClientsController : Controller
    {
        //Clients/Index/{id}
        public ActionResult Index(string id)
        {
            ApiClientsController apiClients = new ApiClientsController();
            Cliente clientSearch = apiClients.GetClient(id);

            return View(clientSearch);
        }

        // GET: /Clients/List

        public async Task<ActionResult> List()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:49328/api/clients");

            var clients = await response.Content.ReadAsAsync<IEnumerable<Cliente>>();

            string test = "ola";

            ViewData["test"] = test;

            return View(clients);
        }
    }
}
