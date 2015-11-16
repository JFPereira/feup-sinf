using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project.Controllers
{
    public class UtilitiesController : Controller
    {
        [HttpGet]
        public ActionResult GetHtmlPage(string path)
        {
            return new FilePathResult(path, "text/html");
        }

    }
}
