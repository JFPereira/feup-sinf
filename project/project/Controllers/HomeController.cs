using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using project.Items;

namespace project.Controllers
{
    public class HomeController : Controller
    {
        public static List<TopClientsItem> top10ClientsCache;
        public static List<TopProductsItem> top10ProductsCache;

        public ActionResult Index()
        {
            return View();
        }
    }
}
