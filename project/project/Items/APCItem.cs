using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using project.Lib_Primavera;

namespace project.Items
{
    public class APCItem
    {
        public double averagePurchaseCost
        {
            get;
            set;
        }

        public int numPurchases
        {
            get;
            set;
        }

        public List<Lib_Primavera.Model.CabecDoc> docs
        {
            get;
            set;
        }
    }
}