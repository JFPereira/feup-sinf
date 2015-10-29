using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Lib_Primavera.Model
{
    public class Sale
    {
        public int NumDoc
        {
            get;
            set;
        }


        public string Nome
        {
            get;
            set;
        }

        public string NumContribuinte
        {
            get;
            set;
        }

        public double TotalMerc  
        {
            get;
            set;
        }

        public double TotalIva
        {
            get;
            set;
        }

        public List<string> codArtigo
        {
            get;
            set;
        }
    }
}