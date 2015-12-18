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
        public static Dictionary<String, Dictionary<int, List<TopClientsItem>>> top10ClientsCache = new Dictionary<String, Dictionary<int, List<TopClientsItem>>>();
        public static Dictionary<String, int?> lateShipmentsCache = new Dictionary<String, int?>();
        public static Dictionary<String, Dictionary<int, ProductSalesItem>> totalUnitsSoldCache = new Dictionary<String, Dictionary<int, ProductSalesItem>>();
        public static Dictionary<String, Dictionary<int, List<GlobalFinancialItem>>> productFinancialCache = new Dictionary<String, Dictionary<int, List<GlobalFinancialItem>>>();

        //Products/Show/{id}
        public async Task<ActionResult> Show(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:49328/api/products/" + id);

            var product = await response.Content.ReadAsAsync<Artigo>();
            ViewData["product"] = id;

            return View(product);
        }

        //Products/List
        public async Task<ActionResult> List()
        {
            return View();
        }
    }
}
