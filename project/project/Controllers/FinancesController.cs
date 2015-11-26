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
        // finances/index
        public ActionResult index()
        {
            return View();
        }
    }
}
