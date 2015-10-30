using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Lib_Primavera.Model
{
    public class DocCompra
    {

        public string id
        {
            get;
            set;
        }

        public string NumDocExterno
        {
            get;
            set;
        }

        public string Nome
        {
            get;
            set;
        }


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

        public string NumContribuinte
        {
            get;
            set;
        }

        public DateTime Data
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

        public string Serie
        {
            get;
            set;
        }

        public List<Model.LinhaDocCompra> LinhasCompras
        {
            get;
            set;
        }
    }
}