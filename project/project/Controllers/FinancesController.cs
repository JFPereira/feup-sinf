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
    public class FinancesController : Controller
    {
        public static Dictionary<int, FinancialYearInfo> kpisCache = new Dictionary<int, FinancialYearInfo>();
        public static Dictionary<int, List<List<double>>> purchasesCache = new Dictionary<int, List<List<double>>>();

        // finances/index
        public ActionResult index()
        {
            return View();
        }
    }
}
