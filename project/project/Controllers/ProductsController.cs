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
    public class ProductsController : Controller
    {
        //Products/Show/{id}
        public async Task<ActionResult> Show(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:49328/api/products/" + id);

            var product = await response.Content.ReadAsAsync<Artigo>();
            ViewData["product"] = id;

            return View(product);
        }

    }
}
