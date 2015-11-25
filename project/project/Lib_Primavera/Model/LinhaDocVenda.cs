using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Lib_Primavera.Model
{
    public class LinhaDocVenda
    {
        public DateTime Data
        {
            get;
            set;
        }

        public string Artigo
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }

        public string IdCabecDoc
        {
            get;
            set;
        }

        public double Quantidade
        {
            get;
            set;
        }

        public string Unidade
        {
            get;
            set;
        }

        public double DescontoComercial
        {
            get;
            set;
        }

        public double PrecoUnitario
        {
            get;
            set;
        }

        public double TotalILiquido
        {
            get;
            set;
        }

        public double TotalIva
        {
            get;
            set;
        }

        public double PrecoLiquido
        {
            get;
            set;
        }

        public double PrecoCustoMedio
        {
            get;
            set;
        }
    }
}