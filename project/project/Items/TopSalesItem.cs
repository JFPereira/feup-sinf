using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Items
{
    public class TopSalesItem
    {
        public string entity
        {
            get;
            set;
        }

        public int numDoc
        {
            get;
            set;
        }


        public double purchaseValue
        {
            get;
            set;
        }

        public DateTime date
        {
            get;
            set;
        }

        public int numPurchases
        {
            get;
            set;
        }
    }
}