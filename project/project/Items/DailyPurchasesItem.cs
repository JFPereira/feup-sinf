using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Items
{
    public class DailyPurchasesItem
    {
        public int day
        {
            get;
            set;
        }

        public int numPurchase
        {
            get;
            set;
        }

        public double salesVolume
        {
            get;
            set;
        }
    }
}