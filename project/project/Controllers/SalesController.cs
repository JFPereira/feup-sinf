using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    public class SalesController : Controller
    {
        //
        // GET: /Sales/{ano}/{mes}/{dia}

        public ActionResult Index(string year, string month, string day)
        {
            ViewData["year"] = year;
            ViewData["month"] = month;
            ViewData["day"] = day;
            
            return View();
        }

    }
}
