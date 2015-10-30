using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS800;
using Interop.StdPlatBS800;
using Interop.StdBE800;
using Interop.GcpBE800;
using ADODB;
using Interop.IGcpBS800;
//using Interop.StdBESql800;
//using Interop.StdBSSql800;

using project.Items;

namespace project.Lib_Primavera
{
    public class PriIntegration
    {
        # region Helpers

        private static bool initCompany()
        {
            return PriEngine.InitializeCompany(
                project.Properties.Settings.Default.Company.Trim(),
                project.Properties.Settings.Default.User.Trim(),
                project.Properties.Settings.Default.Password.Trim());
        }

        # endregion

        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {
            StdBELista objList;

            List<Model.Cliente> listClientes = new List<Model.Cliente>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, Fac_Mor AS campo_exemplo FROM  CLIENTES");


                while (!objList.NoFim())
                {
                    listClientes.Add(new Model.Cliente
                    {
                        CodCliente = objList.Valor("Cliente"),
                        NomeCliente = objList.Valor("Nome"),
                        Moeda = objList.Valor("Moeda"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        Morada = objList.Valor("campo_exemplo")
                    });
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {
            GcpBECliente objCli = new GcpBECliente();

            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    myCli.Morada = objCli.get_Morada();
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);
                        objCli.set_Morada(cliente.Morada);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }

        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);
                    myCli.set_Morada(cli.Morada);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }

        public static int numPurchases(string entity)
        {
            int numPurchases = 0;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                numPurchases = PriEngine.Engine.Consulta("SELECT count(*) as numPurchases FROM CabecDoc WHERE Entidade = " + entity).Valor("numPurchases");
                return numPurchases;

            }
            return -1;
        }

        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------

        #region Artigo

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {
            Model.Artigo myArt = new Model.Artigo();

            StdBELista objList;


            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT Artigo.Artigo, Artigo.Descricao, Artigo.PCMedio, Artigo.TipoArtigo, Artigo.STKActual, Artigo.STKReposicao FROM Artigo WHERE Artigo.Artigo = '" + codArtigo + "'");
                if (objList.NoFim()) return null;
                else
                {
                    //objList.Seguinte();
                    myArt = new Model.Artigo
                    {
                        CodArtigo = codArtigo,
                        DescArtigo = objList.Valor("Descricao"),
                        Custo = objList.Valor("PCMedio"),
                        Tipo = objList.Valor("TipoArtigo"),
                        Stock = objList.Valor("STKActual"),
                        StockReposicao = objList.Valor("STKReposicao")
                    };
                }

                myArt.emFalta = myArt.StockReposicao - myArt.Stock;
                if (myArt.emFalta < 0) myArt.emFalta = 0;
                return myArt;
            }
            else
                return null;

        }

        public static Lib_Primavera.Model.Artigo GetPrecoArtigo(Model.Artigo artigo)
        {

            StdBELista objList;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT ArtigoMoeda.PVP1 FROM ArtigoMoeda WHERE ArtigoMoeda.Artigo = '" + artigo.CodArtigo + "' AND ArtigoMoeda.Moeda = 'EUR'");
                if (objList.NoFim()) return null;
                else
                {
                    artigo.Preco = objList.Valor("PVP1");
                }

                return artigo;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Artigo GetVendasArtigo(Model.Artigo artigo)
        {
            StdBELista objList;
            Model.LinhaDocVenda lindv;

            List<Model.LinhaDocVenda> sales = new List<Model.LinhaDocVenda>();
            double sum = 0;
            double totalQuantity = 0;

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Quantidade, LinhasDoc.PrecUnit from LinhasDoc WHERE Artigo = '" + artigo.CodArtigo + "'");
                sales = new List<Model.LinhaDocVenda>();

                while (!objList.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.Quantidade = objList.Valor("Quantidade");
                    lindv.PrecoUnitario = objList.Valor("PrecUnit");

                    sales.Add(lindv);
                    objList.Seguinte();
                }

                foreach (Model.LinhaDocVenda sale in sales)
                {
                    sum += (sale.Quantidade * sale.PrecoUnitario);
                    totalQuantity += sale.Quantidade;
                }

                artigo.Vendas = sum;
                artigo.Vendidos = totalQuantity;

                return artigo;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Artigo GetComprasArtigo(Model.Artigo artigo)
        {
            StdBELista objList;
            Model.LinhaDocCompra lindv;

            List<Model.LinhaDocCompra> purchases = new List<Model.LinhaDocCompra>();
            double sum = 0;
            double totalQuantity = 0;

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT LinhasCompras.Quantidade, LinhasCompras.PrecUnit from LinhasCompras WHERE Artigo = '" + artigo.CodArtigo + "'");
                purchases = new List<Model.LinhaDocCompra>();

                while (!objList.NoFim())
                {
                    lindv = new Model.LinhaDocCompra();
                    lindv.Quantidade = objList.Valor("Quantidade");
                    lindv.PrecoUnitario = objList.Valor("PrecUnit");

                    purchases.Add(lindv);
                    objList.Seguinte();
                }

                foreach (Model.LinhaDocCompra purchase in purchases)
                {
                    sum += (Math.Abs(purchase.Quantidade) * purchase.PrecoUnitario);
                    totalQuantity += Math.Abs(purchase.Quantidade);
                }

                artigo.Compras = sum;
                artigo.Comprados = totalQuantity;
                artigo.Margem = artigo.Vendas + artigo.Compras;

                return artigo;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Artigo GetTopClientesArtigo(Model.Artigo artigo)
        {
            StdBELista objList;

            List<Model.CabecDoc> sales = new List<Model.CabecDoc>();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT CabecDoc.Data, CabecDoc.Entidade, CabecDoc.Nome, CabecDoc.NumDoc, CabecDoc.NumContribuinte, CabecDoc.TotalMerc, CabecDoc.TotalIva FROM LinhasDoc, CabecDoc WHERE LinhasDoc.IdCabecDoc = CabecDoc.Id AND LinhasDoc.Artigo = " + artigo.CodArtigo);
                while (!objList.NoFim())
                {
                    sales.Add(new Model.CabecDoc
                    {
                        Id = null,
                        Entidade = objList.Valor("Entidade"),
                        Nome = objList.Valor("Nome"),
                        NumDoc = objList.Valor("NumDoc"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                        LinhasDoc = null,
                        Data = objList.Valor("Data"),
                        Serie = null
                    });

                    objList.Seguinte();
                }

                List<Items.TopClientsItem> result = new List<Items.TopClientsItem>();

                double totalSalesVolume = 0;

                foreach (Model.CabecDoc sale in sales)
                {
                    if (result.Exists(e => e.nif == sale.NumContribuinte))
                    {
                        result.Find(e => e.nif == sale.NumContribuinte).salesVolume += (sale.TotalMerc + sale.TotalIva);
                        result.Find(e => e.nif == sale.NumContribuinte).numPurchases++;
                    }
                    else
                    {
                        result.Add(new Items.TopClientsItem
                        {
                            entity = sale.Entidade,
                            name = sale.Nome,
                            nif = sale.NumContribuinte,
                            salesVolume = sale.TotalMerc + sale.TotalIva,
                            percentage = "",
                            numPurchases = 1
                        });
                    }

                    totalSalesVolume += (sale.TotalMerc + sale.TotalIva);
                }

                result = result.OrderBy(e => e.salesVolume).Reverse().Take(10).ToList();

                foreach (Items.TopClientsItem client in result)
                    client.percentage += Math.Round(client.salesVolume / totalSalesVolume * 100, 2) + " %";

                artigo.TopClientes = result;


                return artigo;
            }
            else
                return null;

        }

        public static List<Model.Artigo> ListaArtigos()
        {

            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        #endregion Artigo

        #region Fornecedor

        public static List<Model.Fornecedor> ListaFornecedores()
        {
            StdBELista objList;

            List<Model.Fornecedor> listFornecedores = new List<Model.Fornecedor>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT Fornecedor, Nome, Morada, NumContrib, Tel FROM FORNECEDORES");


                while (!objList.NoFim())
                {
                    listFornecedores.Add(new Model.Fornecedor
                    {
                        CodFornecedor = objList.Valor("Fornecedor"),
                        NomeFornecedor = objList.Valor("Nome"),
                        Morada = objList.Valor("Morada"),
                        NumContribuinte = objList.Valor("NumContrib"),
                        Telefone = objList.Valor("Tel")
                    });
                    objList.Seguinte();

                }

                return listFornecedores;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Fornecedor GetFornecedor(string codFornecedor)
        {
            GcpBEFornecedor objFor = new GcpBEFornecedor();

            Model.Fornecedor myFor = new Model.Fornecedor();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codFornecedor) == true)
                {
                    objFor = PriEngine.Engine.Comercial.Fornecedores.Edita(codFornecedor);
                    myFor.CodFornecedor = objFor.get_Fornecedor();
                    myFor.NomeFornecedor = objFor.get_Nome();
                    myFor.NumContribuinte = objFor.get_NumContribuinte();
                    myFor.Morada = objFor.get_Morada();
                    return myFor;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }


        #endregion fornecedor

        #region DocCompra

        public static List<Model.DocCompra> VGR_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasCompras = listlindc;

                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }

        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasCompras;
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR, rl);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }

        public static FinancialInfo getFinancialYtD(int year)
        {
            FinancialInfo result = new FinancialInfo();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                // Purchases
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecCompras.DataVencimento, CabecCompras.TotalMerc, CabecCompras.TotalIva FROM CabecCompras");

                while (!objList.NoFim())
                {
                    DateTime date = objList.Valor("DataVencimento");

                    if (date.Year == year)
                    {
                        result.purchases -= objList.Valor("TotalMerc");
                        result.purchases -= objList.Valor("TotalIVA");
                    }

                    objList.Seguinte();
                }

                // Sales
                objList = PriEngine.Engine.Consulta(
                    "SELECT CabecDoc.DataVencimento, CabecDoc.TotalMerc, CabecDoc.TotalIva FROM CabecDoc");

                while (!objList.NoFim())
                {
                    DateTime date = objList.Valor("DataVencimento");

                    if (date.Year == year)
                    {
                        result.sales += objList.Valor("TotalMerc");
                        result.sales += objList.Valor("TotalIVA");
                    }

                    objList.Seguinte();
                }

                // Revenue
                result.revenue = result.sales - result.purchases;
            }

            return result;
        }

        public static double getPurchasesTotal()
        {
            double total = 0;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecCompras.TotalMerc, CabecCompras.TotalIva FROM CabecCompras");

                while (!objList.NoFim())
                {
                    total -= objList.Valor("TotalMerc");
                    total -= objList.Valor("TotalIVA");

                    objList.Seguinte();
                }
            }

            return total;
        }

        public static List<List<double>> getPurchasesYoY(int year)
        {
            List<List<double>> result = new List<List<double>>();

            for (int i = 0; i < 12; i++)
                result.Add(new List<double> { -1, -1 });

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecCompras.DataVencimento, CabecCompras.TotalMerc, CabecCompras.TotalIva FROM CabecCompras");

                while (!objList.NoFim())
                {
                    DateTime date = objList.Valor("DataVencimento");

                    if (date.Year == year || date.Year == year - 1)
                    {
                        double amount = -1 * (objList.Valor("TotalMerc") + objList.Valor("TotalIVA"));

                        if (result[date.Month - 1][date.Year - year + 1] == -1)
                            result[date.Month - 1][date.Year - year + 1] = amount;
                        else
                            result[date.Month - 1][date.Year - year + 1] += amount;
                    }

                    objList.Seguinte();
                }
            }

            return result;
        }

        public static List<Model.DocCompra> getPurchases()
        {
            StdBELista objList;

            List<Model.DocCompra> purchases = new List<Model.DocCompra>();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT CabecCompras.DataDoc, CabecCompras.Entidade, CabecCompras.Nome, CabecCompras.NumDoc, CabecCompras.NumContribuinte, CabecCompras.TotalMerc, CabecCompras.TotalIva FROM CabecCompras");
                while (!objList.NoFim())
                {
                    purchases.Add(new Model.DocCompra
                    {
                        id = null,
                        Entidade = objList.Valor("Entidade"),
                        Nome = objList.Valor("Nome"),
                        NumDoc = objList.Valor("NumDoc"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                        LinhasCompras = null,
                        Data = objList.Valor("DataDoc"),
                        Serie = null
                    });

                    objList.Seguinte();
                }

                return purchases;
            }
            else
                return null;
        }

        #endregion DocCompra

        #region DocsVenda

        /*public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }*/

        /*public static List<Model.DocVenda> Encomendas_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }*/

        /*public static Model.DocVenda Encomenda_Get(string numdoc)
        {


            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {


                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }*/

        public static List<Model.CabecDoc> getSales()
        {
            StdBELista objList;

            List<Model.CabecDoc> sales = new List<Model.CabecDoc>();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT CabecDoc.Data, CabecDoc.Entidade, CabecDoc.Nome, CabecDoc.NumDoc, CabecDoc.NumContribuinte, CabecDoc.TotalMerc, CabecDoc.TotalIva FROM CabecDoc");
                while (!objList.NoFim())
                {
                    sales.Add(new Model.CabecDoc
                    {
                        Id = null,
                        Entidade = objList.Valor("Entidade"),
                        Nome = objList.Valor("Nome"),
                        NumDoc = objList.Valor("NumDoc"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                        LinhasDoc = null,
                        Data = objList.Valor("Data"),
                        Serie = null
                    });

                    objList.Seguinte();
                }

                return sales;
            }
            else
                return null;
        }

        public static int getSalesProd(string controller, string name)
        {

            int quantity = 0;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                if (controller == "year")
                    quantity = PriEngine.Engine.Consulta("SELECT sum(LinhasDoc.Quantidade) as quantity FROM LinhasDoc,CabecDoc WHERE CabecDoc.Data.Year = DateTime.Now.Year AND CabecDoc.Id = LinhasDoc.IdCabecDoc AND LinhasDoc.DescArtigo = " + name).Valor("quantity");
                else if (controller == "month")
                    quantity = PriEngine.Engine.Consulta("SELECT sum(LinhasDoc.Quantidade) as quantity FROM LinhasDoc,CabecDoc WHERE CabecDoc.Data.Month = DateTime.Now.Month AND CabecDoc.Id = LinhasDoc.IdCabecDoc AND LinhasDoc.DescArtigo = " + name).Valor("quantity");
                else if (controller == "day")
                    quantity = PriEngine.Engine.Consulta("SELECT sum(LinhasDoc.Quantidade) as quantity FROM LinhasDoc,CabecDoc WHERE CabecDoc.Data.Day = DateTime.Now.Day AND CabecDoc.Id = LinhasDoc.IdCabecDoc AND LinhasDoc.DescArtigo = " + name).Valor("quantity");


                return quantity;

            }
            return -1;

        }

        public static List<Model.LinhaDocVenda> getProductSales()
        {
            StdBELista objList;
            Model.LinhaDocVenda lindv;

            List<Model.LinhaDocVenda> sales = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(project.Properties.Settings.Default.Company.Trim(), project.Properties.Settings.Default.User.Trim(), project.Properties.Settings.Default.Password.Trim()) == true)
            {
                objList = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalIva, TotalILiquido, PrecoLiquido from LinhasDoc");

                sales = new List<Model.LinhaDocVenda>();

                while (!objList.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objList.Valor("idCabecDoc");
                    lindv.CodArtigo = objList.Valor("Artigo");
                    lindv.DescArtigo = objList.Valor("Descricao");
                    lindv.Quantidade = objList.Valor("Quantidade");
                    lindv.Unidade = objList.Valor("Unidade");
                    lindv.Desconto = objList.Valor("Desconto1");
                    lindv.PrecoUnitario = objList.Valor("PrecUnit");
                    lindv.TotalILiquido = objList.Valor("TotalILiquido");
                    lindv.TotalIva = objList.Valor("TotalIva");
                    lindv.PrecoLiquido = objList.Valor("PrecoLiquido");

                    sales.Add(lindv);
                    objList.Seguinte();
                }

                return sales;
            }
            else
                return null;
        }

        public static double getSalesTotal()
        {
            double total = 0;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecDoc.TotalMerc, CabecDoc.TotalIva FROM CabecDoc");

                while (!objList.NoFim())
                {
                    total += objList.Valor("TotalMerc");
                    total += objList.Valor("TotalIVA");

                    objList.Seguinte();
                }
            }

            return total;
        }

        public static List<List<double>> getSalesYoY(int year)
        {
            List<List<double>> result = new List<List<double>>();

            for (int i = 0; i < 12; i++)
                result.Add(new List<double> { -1, -1 });

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecDoc.DataVencimento, CabecDoc.TotalMerc, CabecDoc.TotalIva FROM CabecDoc");

                while (!objList.NoFim())
                {
                    DateTime date = objList.Valor("DataVencimento");

                    if (date.Year == year || date.Year == year - 1)
                    {
                        double amount = objList.Valor("TotalMerc") + objList.Valor("TotalIVA");

                        if (result[date.Month - 1][date.Year - year + 1] == -1)
                            result[date.Month - 1][date.Year - year + 1] = amount;
                        else
                            result[date.Month - 1][date.Year - year + 1] += amount;
                    }

                    objList.Seguinte();
                }
            }

            return result;
        }

        public static List<TopSalesCountry> getTop10SalesCountries()
        {
            List<TopSalesCountry> result = new List<TopSalesCountry>();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                Dictionary<string, double> countrySalesMap = new Dictionary<string, double>();

                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT Clientes.Pais, CabecDoc.TotalMerc, CabecDoc.TotalIva FROM Clientes, CabecDoc WHERE CabecDoc.Entidade = Clientes.Cliente");

                // build country-sales dictionary
                while (!objList.NoFim())
                {
                    string country = objList.Valor("Pais");
                    double amount = objList.Valor("TotalMerc") + objList.Valor("TotalIVA");

                    if (countrySalesMap.ContainsKey(country))
                        countrySalesMap[country] += amount;
                    else
                        countrySalesMap[country] = amount;

                    objList.Seguinte();
                }

                // this is the total sales of the top 10 sales countries
                double total = 0;

                // pick the top 10
                for (int i = 0; i < 10 && countrySalesMap.Count > 0; i++)
                {
                    var countryMaxSales = countrySalesMap.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

                    result.Add(new TopSalesCountry
                    {
                        country = countryMaxSales,
                        amount = countrySalesMap[countryMaxSales],
                        percentage = 0
                    });

                    total += countrySalesMap[countryMaxSales];

                    countrySalesMap.Remove(countryMaxSales);
                }

                // fill in the percentage for each entry
                foreach (TopSalesCountry entry in result)
                {
                    entry.percentage = 100.0 * entry.amount / total;
                }
            }

            return result;
        }

        public static List<Model.LinhaDocVenda> topClientProducts(string client_id)
        {
            StdBELista objList;

            List<Model.LinhaDocVenda> listProducts = new List<Model.LinhaDocVenda>();

            bool companyInitialize = PriEngine.InitializeCompany(
                project.Properties.Settings.Default.Company.Trim(),
                project.Properties.Settings.Default.User.Trim(),
                project.Properties.Settings.Default.Password.Trim());

            if (companyInitialize)
            {
                objList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Artigo, LinhasDoc.Descricao, LinhasDoc.TotalIva, LinhasDoc.PrecoLiquido, LinhasDoc.TotalILiquido, LinhasDoc.Quantidade FROM LinhasDoc, CabecDoc WHERE LinhasDoc.IdCabecDoc = CabecDoc.Id AND CabecDoc.Entidade = " + client_id);
                while (!objList.NoFim())
                {
                    listProducts.Add(new Model.LinhaDocVenda
                    {
                        CodArtigo = objList.Valor("Artigo"),
                        DescArtigo = objList.Valor("Descricao"),
                        TotalILiquido = objList.Valor("TotalILiquido"),
                        TotalIva = objList.Valor("TotalIva"),
                        Quantidade = objList.Valor("Quantidade"),
                        Desconto = 0,
                        IdCabecDoc = "",
                        Unidade = "",
                        PrecoUnitario = 0,
                        PrecoLiquido = objList.Valor("PrecoLiquido")

                    });
                    objList.Seguinte();
                }

                return listProducts;
            }
            else
                return null;
        }

        #endregion DocsVenda
    }
}
