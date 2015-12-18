using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using project.Items;
using project.Lib_Primavera.Model;

namespace project.Controllers
{
    public class SalesController : Controller
    {
        public static Dictionary<string, List<TopSalesItem>> top10SalesCache = new Dictionary<string, List<TopSalesItem>>();
        public static Dictionary<string, List<SalesBookingItem>> salesBookingCache = new Dictionary<string, List<SalesBookingItem>>();
        public static Dictionary<string, List<RegionalSSItem>> salesRSSCache = new Dictionary<string, List<RegionalSSItem>>();
        public static Dictionary<string, List<SalesGrowthItem>> salesGrowthCache = new Dictionary<string, List<SalesGrowthItem>>();

        public ActionResult Index()
        {
            return View();
        }

    }
}
