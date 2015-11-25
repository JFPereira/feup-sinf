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
using project.Lib_Primavera.Model;

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
           Cliente cliente = new Cliente();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objCli = PriEngine.Engine.Consulta(
                    "SELECT Clientes.Cliente, Clientes.Nome, Clientes.NumContrib, Clientes.Fac_Mor, Clientes.Moeda FROM Clientes WHERE Clientes.Cliente = " + codCliente);
                if (objCli.NoFim()) return null;
                else {
                    cliente = new Cliente
                    {
                        CodCliente = objCli.Valor("Cliente"),
                        NomeCliente = objCli.Valor("Nome"),
                        NumContribuinte = objCli.Valor("NumContrib"),
                        Morada = objCli.Valor("Fac_Mor"),
                        Moeda = objCli.Valor("Moeda")
                    };
                }
            }

            return cliente;
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

        //returns the number of purchases of an entity 
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

        public static List<double> GetTodosPrecosArtigo(Model.Artigo artigo)
        {
            List<double> precos = new List<double>();
            StdBELista objList;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT LinhasDoc.PrecUnit from LinhasDoc WHERE Artigo = '" + artigo.CodArtigo + "'");

                while (!objList.NoFim())
                {
                    precos.Add(objList.Valor("PrecUnit"));
                    objList.Seguinte();
                }

                return precos;
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
                        Entidade = objList.Valor("Entidade"),
                        Nome = objList.Valor("Nome"),
                        NumDoc = objList.Valor("NumDoc"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                        Data = objList.Valor("Data"),
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
        public static double GetShipments()
        {
            double delayed = 0;
            StdBELista objList;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT LinhasInternos.QntSatisfeita, LinhasInternos.Quantidade from LinhasInternos");

                while (!objList.NoFim())
                {
                    double satis = objList.Valor("QntSatisfeita");
                    double quant = objList.Valor("Quantidade");
                    delayed += quant - satis;
                    objList.Seguinte();
                }

                return delayed;
            }
            else
                return 0;
        }

        public static double GetProductShipments(string id)
        {
            double delayed = 0;
            StdBELista objList;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT LinhasInternos.QntSatisfeita, LinhasInternos.Quantidade from LinhasInternos WHERE Artigo = '"+ id +"'");

                while (!objList.NoFim())
                {
                    double satis = objList.Valor("QntSatisfeita");
                    double quant = objList.Valor("Quantidade");
                    delayed += quant - satis;
                    objList.Seguinte();
                }

                return delayed;
            }
            else
                return 0;
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
                        lindc.Artigo = objListLin.Valor("Artigo");
                        lindc.Descricao = objListLin.Valor("Descricao");
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
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.Artigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
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

        public static List<Model.DocCompra> getPurchases()
        {
            List<Model.DocCompra> purchases = new List<Model.DocCompra>();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecCompras.DataDoc, CabecCompras.Entidade, CabecCompras.Nome, CabecCompras.NumDoc, CabecCompras.NumContribuinte, CabecCompras.TotalMerc, CabecCompras.TotalIva FROM CabecCompras");

                while (!objList.NoFim())
                {
                    purchases.Add(new Model.DocCompra
                    {
                        id = null,
                        Data = objList.Valor("DataDoc"),
                        Entidade = objList.Valor("Entidade"),
                        Nome = objList.Valor("Nome"),
                        NumDoc = objList.Valor("NumDoc"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                        LinhasCompras = null,
                        Serie = null
                    });

                    objList.Seguinte();
                }
            }

            return purchases;
        }

        public static double getMonthlyPurchases(int month, int year)
        {
            double total = 0;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecCompras.TotalMerc, CabecCompras.TotalIva FROM CabecCompras WHERE MONTH(CabecCompras.DataDoc) = " + month + " AND YEAR(CabecCompras.DataDoc) = " + year);

                while (!objList.NoFim())
                {
                    total += objList.Valor("TotalMerc");
                    total += objList.Valor("TotalIVA");

                    objList.Seguinte();
                }
            }

            return total;
        }

        public static double getProductMonthlyPurchases(int month, int year, string cod)
        {
            double total = 0;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecCompras.TotalMerc, CabecCompras.TotalIva FROM CabecCompras WHERE LinhasCompras.IdCabecCompras = CabecCompras.Id AND LinhasCompras.Artigo = '" + cod + "' AND MONTH(CabecCompras.DataDoc) = " + month + " AND YEAR(CabecCompras.DataDoc) = " + year);

                while (!objList.NoFim())
                {
                    total += objList.Valor("TotalMerc");
                    total += objList.Valor("TotalIVA");

                    objList.Seguinte();
                }
            }

            return total;
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

        // get all the CabecDocs on the datebase
        public static List<Model.CabecDoc> getSales()
        {
            List<Model.CabecDoc> sales = new List<Model.CabecDoc>();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecDoc.Data, CabecDoc.Entidade, CabecDoc.Nome, CabecDoc.NumDoc, CabecDoc.NumContribuinte, CabecDoc.TotalMerc, CabecDoc.TotalIva FROM CabecDoc");

                while (!objList.NoFim())
                {
                    sales.Add(new Model.CabecDoc
                    {
                        Data = objList.Valor("Data"),
                        Entidade = objList.Valor("Entidade"),
                        Nome = objList.Valor("Nome"),
                        NumDoc = objList.Valor("NumDoc"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                    });

                    objList.Seguinte();
                }
            }

            return sales;
        }

        public static List<Model.CabecDoc> getSalesBy(string controller, string year, string month, string day)
        {
            List<Model.CabecDoc> sales = new List<Model.CabecDoc>();
            StdBELista objList;

            bool companyInitialized = initCompany();

            List<string> months = new List<string>() { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };

            if (month == "january" || month == "march" || month == "may" || month == "july" || month == "august" || month == "october" || month == "december" || month == "april" || month == "june" || month == "september" || month == "november" || month == "february")
                month = (months.IndexOf(month) + 1).ToString();
            else month = null;

            if (companyInitialized)
            {
                if (controller == "year")
                {
                   objList = PriEngine.Engine.Consulta(
                    "SELECT CabecDoc.Data, CabecDoc.Entidade, CabecDoc.Nome, CabecDoc.NumDoc, CabecDoc.NumContribuinte, CabecDoc.TotalMerc, CabecDoc.TotalIva, LinhasDoc.Quantidade as quantity FROM CabecDoc, LinhasDoc WHERE CabecDoc.Id = LinhasDoc.IdCabecDoc AND DATEPART(year, CabecDoc.Data) = " + year);
                }

                else if (controller == "month")
                {
                    if (month == null) return null;
                    objList = PriEngine.Engine.Consulta(
                    "SELECT CabecDoc.Data, CabecDoc.Entidade, CabecDoc.Nome, CabecDoc.NumDoc, CabecDoc.NumContribuinte, CabecDoc.TotalMerc, CabecDoc.TotalIva, LinhasDoc.Quantidade as quantity FROM CabecDoc, LinhasDoc WHERE CabecDoc.Id = LinhasDoc.IdCabecDoc AND DATEPART(mm,CabecDoc.Data) = " + month + " AND DATEPART(yyyy,CabecDoc.Data) = " + year);
                }
                else if (controller == "day")
                {
                    if (month == null) return null;
                    objList = PriEngine.Engine.Consulta(
                   "SELECT CabecDoc.Data, CabecDoc.Entidade, CabecDoc.Nome, CabecDoc.NumDoc, CabecDoc.NumContribuinte, CabecDoc.TotalMerc, CabecDoc.TotalIva, LinhasDoc.Quantidade as quantity FROM CabecDoc, LinhasDoc WHERE CabecDoc.Id = LinhasDoc.IdCabecDoc AND DATEPART(dd,CabecDoc.Data) = " + day + " AND DATEPART(mm,CabecDoc.Data) = " + month + " AND DATEPART(yyyy,CabecDoc.Data) = " + year);
                }
                else return null;

                while (!objList.NoFim())
                {
                    sales.Add(new Model.CabecDoc
                    {
                        Data = objList.Valor("Data"),
                        Entidade = objList.Valor("Entidade"),
                        Nome = objList.Valor("Nome"),
                        NumDoc = objList.Valor("NumDoc"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                    });

                    objList.Seguinte();
                }
            }

            return sales;
        }
        //returns total amount of money made from selling the item prod, with contraints year,month and day
        public static double getSalesProd(string controller, string prod, string year, string month, string day)
        {
            StdBELista objList;

            double quantity = 0;
            bool companyInitialized = initCompany();

            List<string> months = new List<string>() { "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" };

            if (month == "january" || month == "march" || month == "may" || month == "july" || month == "august" || month == "october" || month == "december" || month == "april" || month == "june" || month == "september" || month == "november" || month == "february")
                month = (months.IndexOf(month) + 1).ToString();
            else month = null;

            if (companyInitialized)
            {
                if (controller == "year")
                {
                    objList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Quantidade AS quantity, LinhasDoc.PrecUnit AS price FROM LinhasDoc,CabecDoc WHERE DATEPART(year, CabecDoc.Data) = " + year + " AND CabecDoc.Id = LinhasDoc.IdCabecDoc AND LinhasDoc.Artigo = " + prod);
                    while (!objList.NoFim())
                    {
                        quantity += objList.Valor("quantity") * objList.Valor("price");
                        objList.Seguinte();
                    }
                }
                else if (controller == "month")
                {
                    if (month == null) { return 0; }
                    objList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Quantidade AS quantity, LinhasDoc.PrecUnit AS price FROM LinhasDoc,CabecDoc WHERE DATEPART(mm,CabecDoc.Data) = " + month + " AND DATEPART(yyyy,CabecDoc.Data) = " + year + " AND CabecDoc.Id = LinhasDoc.IdCabecDoc AND LinhasDoc.Artigo = " + prod);
                    while (!objList.NoFim())
                    {
                        quantity += objList.Valor("quantity") * objList.Valor("price");
                        objList.Seguinte();
                    }
                }
                else if (controller == "day")
                {
                    if (month == null) { return 0; }
                    objList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Quantidade AS quantity, LinhasDoc.PrecUnit AS price FROM LinhasDoc,CabecDoc WHERE DATEPART(dd,CabecDoc.Data) = " + day + " AND DATEPART(mm,CabecDoc.Data) = " + month + " AND DATEPART(yyyy,CabecDoc.Data) = " + year + " AND CabecDoc.Id = LinhasDoc.IdCabecDoc AND LinhasDoc.Artigo = " + prod);
                    while (!objList.NoFim())
                    {
                        quantity += objList.Valor("quantity") * objList.Valor("price");
                        objList.Seguinte();
                    }
                }



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
                    lindv.Artigo = objList.Valor("Artigo");
                    lindv.Descricao = objList.Valor("Descricao");
                    lindv.Quantidade = objList.Valor("Quantidade");
                    lindv.Unidade = objList.Valor("Unidade");
                    lindv.DescontoComercial = objList.Valor("Desconto1");
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

        public static double getMonthlySales(int month, int year)
        {
            double total = 0;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecDoc.TotalMerc, CabecDoc.TotalIva FROM CabecDoc WHERE MONTH(CabecDoc.Data) = " + month + " AND YEAR(CabecDoc.Data) = " + year);

                while (!objList.NoFim())
                {
                    total += objList.Valor("TotalMerc");
                    total += objList.Valor("TotalIVA");

                    objList.Seguinte();
                }
            }

            return total;
        }

        public static double getProductMonthlySales(int month, int year, string cod)
        {
            double total = 0;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                StdBELista objList = PriEngine.Engine.Consulta(
                    "SELECT CabecDoc.TotalMerc, CabecDoc.TotalIva FROM CabecDoc WHERE LinhasDoc.IdCabecDoc = CabeckDoc.Id AND LinhasDoc.Artigo = '" + cod + "' AND MONTH(CabecDoc.Data) = " + month + " AND YEAR(CabecDoc.Data) = " + year);

                while (!objList.NoFim())
                {
                    total += objList.Valor("TotalMerc");
                    total += objList.Valor("TotalIVA");

                    objList.Seguinte();
                }
            }

            return total;
        }

        // get all the client CabecDocs between two dates
        public static List<Model.CabecDoc> getClientPurchasesBetween(string client_id, string dateStart, string dateEnd)
        {
            StdBELista objList;

            List<Model.CabecDoc> sales = new List<Model.CabecDoc>();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT CabecDoc.TotalMerc, CabecDoc.TotalIva, CabecDoc.Data, CabecDoc.Nome, CabecDoc.NumDoc, CabecDoc.NumContribuinte FROM CabecDoc WHERE '" + dateStart + "' <= CONVERT(DATE, CabecDoc.Data) AND CONVERT(DATE, CabecDoc.Data) <= '" + dateEnd + "' AND CabecDoc.Entidade = '" + client_id + "'");
                while (!objList.NoFim())
                {
                    sales.Add(new Model.CabecDoc
                    {
                        Entidade = client_id,
                        Nome = objList.Valor("Nome"),
                        NumDoc = objList.Valor("NumDoc"),
                        NumContribuinte = objList.Valor("NumContribuinte"),
                        TotalMerc = objList.Valor("TotalMerc"),
                        TotalIva = objList.Valor("TotalIva"),
                        LinhasDoc = null,
                        Data = objList.Valor("Data"),
                    });

                    objList.Seguinte();
                }

                return sales;
            }
            else
                return null;
        }
		
        //checks if month string is correct
        public static string checkMonth(string month)
        {

            if (month != "01" && month != "03" && month != "05" && month != "07" && month != "08" && month != "10" && month != "12" && month != "04" && month != "06" && month != "09" && month != "11" && month != "02")
				month = null;

            return month;

        }

        //returns a list for sales booking per region
        public static List<RegSalesBookingItem> getSalesBookingReg(string controller, string year, string month, string day)
        {
            List<RegSalesBookingItem> returnList = new List<RegSalesBookingItem>();
            StdBELista objList;

            bool companyInitialized = initCompany();

            month = checkMonth(month);

            if (companyInitialized)
            {
                if (controller == "year")
                {
                    objList = PriEngine.Engine.Consulta(
                            "SELECT Clientes.Pais, LinhasDoc.Quantidade, LinhasDoc.PrecUnit FROM Clientes, CabecDoc, LinhasDoc WHERE CabecDoc.Entidade = Clientes.Cliente AND LinhasDoc.IdCabecDoc = CabecDoc.Id AND DATEPART(year, CabecDoc.Data) = " + year);
                }
                else if (controller == "month")
                {
                    if (month == null) return null;
                    objList = PriEngine.Engine.Consulta(
                           "SELECT Clientes.Pais, LinhasDoc.Quantidade, LinhasDoc.PrecUnit FROM Clientes, CabecDoc, LinhasDoc WHERE CabecDoc.Entidade = Clientes.Cliente AND LinhasDoc.IdCabecDoc = CabecDoc.Id AND DATEPART(year, CabecDoc.Data) = " + year + " AND DATEPART(month, CabecDoc.Data) = " + month);
                }
                else if (controller == "day")
                {
                    if (month == null) return null;
                    objList = PriEngine.Engine.Consulta(
                            "SELECT Clientes.Pais, LinhasDoc.Quantidade, LinhasDoc.PrecUnit FROM Clientes, CabecDoc, LinhasDoc WHERE CabecDoc.Entidade = Clientes.Cliente AND LinhasDoc.IdCabecDoc = CabecDoc.Id AND DATEPART(year, CabecDoc.Data) = " + year + " AND DATEPART(month, CabecDoc.Data) = " + month + " AND DATEPART(day, CabecDoc.Data) = " + day);
                }
                else return null;

                while (!objList.NoFim())
                {
                    if (returnList.Exists(e => e.pais == objList.Valor("Pais")))
                    {
                        returnList.Find(e => e.pais == objList.Valor("Pais")).valorVendas += objList.Valor("Quantidade") * objList.Valor("PrecUnit");
                    }
                    else
                        returnList.Add(new RegSalesBookingItem
                        {
                            pais = objList.Valor("Pais"),
                            valorVendas = objList.Valor("Quantidade") * objList.Valor("PrecUnit")
                        });
                    objList.Seguinte();
                }

                returnList = returnList.OrderBy(e => e.valorVendas).Reverse().ToList();

            }

            return returnList;
        }

        public static List<Model.CabecDoc> getClientPurchases(string entity)
        {
            List<Model.CabecDoc> docs = new List<Model.CabecDoc>();

            StdBELista docsList;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                docsList = PriEngine.Engine.Consulta("SELECT CabecDoc.Id, CabecDoc.TotalMerc, CabecDoc.TotalIva, CabecDoc.Data, CabecDoc.Nome, CabecDoc.NumDoc, CabecDoc.NumContribuinte FROM CabecDoc WHERE CabecDoc.Entidade = '" + entity + "'");
                while (!docsList.NoFim())
                {
                    docs.Add(new Model.CabecDoc
                    {
                        id = docsList.Valor("Id"),
                        Entidade = entity,
                        Nome = docsList.Valor("Nome"),
                        NumDoc = docsList.Valor("NumDoc"),
                        NumContribuinte = docsList.Valor("NumContribuinte"),
                        TotalMerc = docsList.Valor("TotalMerc"),
                        TotalIva = docsList.Valor("TotalIva"),
                        Data = docsList.Valor("Data"),
                        LinhasDoc = getSalesDocLines(docsList.Valor("Id"))
                    });

                    docsList.Seguinte();
                }

                return docs;
            }
            else
                return null;
        }

        // get all the product lines of one CabecDoc
        public static List<Model.LinhaDocVenda> getSalesDocLines(string doc_id)
        {
            List<Model.LinhaDocVenda> linesDoc = new List<Model.LinhaDocVenda>();

            StdBELista linesList;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                linesList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Artigo, LinhasDoc.Descricao, LinhasDoc.Quantidade, LinhasDoc.Unidade, LinhasDoc.DescontoComercial, LinhasDoc.PrecUnit, LinhasDoc.TotalILiquido, LinhasDoc.TotalIva, LinhasDoc.PrecoLiquido FROM LinhasDoc WHERE LinhasDoc.IdCabecDoc = '" + doc_id + "'");

                while (!linesList.NoFim())
                {
                    linesDoc.Add(new Model.LinhaDocVenda
                    {
                        Artigo = linesList.Valor("Artigo"),
                        Descricao = linesList.Valor("Descricao"),
                        Quantidade = linesList.Valor("Quantidade"),
                        Unidade = linesList.Valor("Unidade"),
                        DescontoComercial = linesList.Valor("DescontoComercial"),
                        PrecoUnitario = linesList.Valor("PrecUnit"),
                        TotalILiquido = linesList.Valor("TotalILiquido"),
                        TotalIva = linesList.Valor("TotalIva"),
                        PrecoLiquido = linesList.Valor("PrecoLiquido")
                    });

                    linesList.Seguinte();
                }

                return linesDoc;
            }
            else
                return null;
        }

        // get all the product lines of the all clients CabecDocs
        public static List<Model.LinhaDocVenda> getSalesDocLinesByClient(string entity)
        {
            List<Model.LinhaDocVenda> linesDoc = new List<Model.LinhaDocVenda>();

            StdBELista linesList;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                linesList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Data, LinhasDoc.Artigo, LinhasDoc.Descricao, LinhasDoc.Quantidade, LinhasDoc.Unidade, LinhasDoc.DescontoComercial, LinhasDoc.PrecUnit, LinhasDoc.TotalILiquido, LinhasDoc.TotalIva, LinhasDoc.PrecoLiquido, LinhasDoc.PCM FROM LinhasDoc, CabecDoc WHERE LinhasDoc.IdCabecDoc = CabecDoc.Id AND CabecDoc.Entidade = '" + entity + "'");

                while (!linesList.NoFim())
                {
                    linesDoc.Add(new Model.LinhaDocVenda
                    {
                        Data = linesList.Valor("Data"),
                        Artigo = linesList.Valor("Artigo"),
                        Descricao = linesList.Valor("Descricao"),
                        Quantidade = linesList.Valor("Quantidade"),
                        Unidade = linesList.Valor("Unidade"),
                        DescontoComercial = linesList.Valor("DescontoComercial"),
                        PrecoUnitario = linesList.Valor("PrecUnit"),
                        TotalILiquido = linesList.Valor("TotalILiquido"),
                        TotalIva = linesList.Valor("TotalIva"),
                        PrecoLiquido = linesList.Valor("PrecoLiquido"),
                        PrecoCustoMedio = linesList.Valor("PCM"),

                    });

                    linesList.Seguinte();
                }

                return linesDoc;
            }
            else
                return null;
        }

        public static double getPercentage(string controller, string year, string month, string day, string pais)
        {
            StdBELista objList;
            double returnDouble = 0;

            bool companyInitialized = initCompany();

            month = checkMonth(month);

            if (companyInitialized)
            {
                if (controller == "year")
                {
                    objList = PriEngine.Engine.Consulta(
                            "SELECT Clientes.Pais, LinhasDoc.Quantidade, LinhasDoc.PrecUnit FROM Clientes, CabecDoc, LinhasDoc WHERE CabecDoc.Entidade = Clientes.Cliente AND LinhasDoc.IdCabecDoc = CabecDoc.Id AND DATEPART(year, CabecDoc.Data) = " + year + " AND Clientes.Pais = '" + pais + "'");
                }
                else if (controller == "month")
                {
                    if (month == null) return 0;
                    objList = PriEngine.Engine.Consulta(
                           "SELECT Clientes.Pais, LinhasDoc.Quantidade, LinhasDoc.PrecUnit FROM Clientes, CabecDoc, LinhasDoc WHERE CabecDoc.Entidade = Clientes.Cliente AND LinhasDoc.IdCabecDoc = CabecDoc.Id AND DATEPART(year, CabecDoc.Data) = " + year + " AND DATEPART(month, CabecDoc.Data) = " + month + " AND Clientes.Pais = '" + pais + "'");
                }
                else if (controller == "day")
                {
                    if (month == null) return 0;
                    objList = PriEngine.Engine.Consulta(
                            "SELECT Clientes.Pais, LinhasDoc.Quantidade, LinhasDoc.PrecUnit FROM Clientes, CabecDoc, LinhasDoc WHERE CabecDoc.Entidade = Clientes.Cliente AND LinhasDoc.IdCabecDoc = CabecDoc.Id AND DATEPART(year, CabecDoc.Data) = " + year + " AND DATEPART(month, CabecDoc.Data) = " + month + " AND DATEPART(day, CabecDoc.Data) = " + day + " AND Clientes.Pais = '" + pais + "'");
                }
                else return 0;

                while (!objList.NoFim())
                {
                    returnDouble += objList.Valor("Quantidade") * objList.Valor("PrecUnit");
                    objList.Seguinte();
                }
            }

            return returnDouble;
        }

        public static List<string> getAllCountries()
        {
            StdBELista objList;
            List<string> countries = new List<string>();

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta(
                           "SELECT DISTINCT Clientes.Pais FROM Clientes");
                while (!objList.NoFim())
                {
                    if (objList.Valor("Pais") != "")
                        countries.Add(objList.Valor("Pais"));
                    objList.Seguinte();
                }

            }
            return countries;
        }

        public static int numUnits(int NumDoc)
        {
            int numUnits = 0;
            StdBELista objList;

            bool companyInitialized = initCompany();

            if (companyInitialized)
            {
                objList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Quantidade as quantity FROM CabecDoc,LinhasDoc WHERE LinhasDoc.IdCabecDoc = CabecDoc.Id AND CabecDoc.NumDoc = " + NumDoc);

                while (!objList.NoFim())
                {
                    numUnits += objList.Valor("quantity");

                    objList.Seguinte();
                }

                return numUnits;

            }
            return -1;
        }

        #endregion DocsVenda

    }

}
