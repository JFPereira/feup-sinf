using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using project.Lib_Primavera.Model;

namespace project.Controllers
{
    public class SalesController : ApiController
    {
        // GET api/sales/top
        [System.Web.Http.HttpGet]
        public IEnumerable<string> TopSales()
        {
            return new string[] { "sale1", "sale2", "sale3", "sale4" };
        }

        // GET api/sales/countries
        [System.Web.Http.HttpGet]
        public IEnumerable<string> TopCountries()
        {
            return new string[] { "portugal", "england", "france", "spain" };
        }
    }
}
