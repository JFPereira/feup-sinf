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
        //Clients/Show/{id}
        public async Task<ActionResult> Show(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:49328/api/clients/" + id);

            var client = await response.Content.ReadAsAsync<Cliente>();

            ViewData["entity"] = id;

            return View(client);
        }

        public ActionResult List()
        {
            return View();
        }
    }
}
