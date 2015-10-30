using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project.Lib_Primavera.Model
{
    public class Artigo
    {
        public string CodArtigo
        {
            get;
            set;
        }

        public string DescArtigo
        {
            get;
            set;
        }

        public string Tipo
        {
            get;
            set;
        }

        public double Preco
        {
            get;
            set;
        }

        public double Custo
        {
            get;
            set;
        }

        public double Stock
        {
            get;
            set;
        }

        public double StockReposicao
        {
            get;
            set;
        }

        public double emFalta
        {
            get;
            set;
        }

        public double Vendidos
        {
            get;
            set;
        }

        public double Vendas
        {
            get;
            set;
        }

        public double Comprados
        {
            get;
            set;
        }
        public double Compras
        {
            get;
            set;
        }

        public double Margem
        {
            get;
            set;
        }

        public List<Items.TopClientsItem> TopClientes
        {
            get;
            set;
        }

        public List<Items.GlobalFinancialItem> VendasComprasMes
        {
            get;
            set;
        }

    }
}