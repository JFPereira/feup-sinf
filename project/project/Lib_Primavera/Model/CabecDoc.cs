using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Lib_Primavera.Model
{
    public class CabecDoc
    {
        public string Entidade
        {
            get;
            set;
        }

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

        public string Id
        {
            get;
            set;
        }

        public DateTime Datatime
        {
            get;
            set;
        }

        public string Serie
        {
            get;
            set;
        }

        public List<Model.LinhaDocVenda> LinhasDoc
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }
    }
}