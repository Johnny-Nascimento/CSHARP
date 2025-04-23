using SerenattoEnsaio.Dados;
using SerenattoEnsaio.Modelos;
using SerenattoPreGravacao.Dados;

const string ID       = "ID";
const string NOME     = "NOME";
const string ENDERECO = "ENDERECO";
const string TELEFONE = "TELEFONE";
const string ESPACO_SEPARADOR = " | ";

List<Cliente> listaClientes = DadosClientes.GetClientes();

var maiorId       = Math.Max(ID.Length       , listaClientes.Max(x => x.Id.ToString().Length));
var maiorNome     = Math.Max(NOME.Length     , listaClientes.Max(x => x.Nome.Length));
var maiorEndereco = Math.Max(ENDERECO.Length , listaClientes.Max(x => x.Endereco.Length));
var maiorTelefone = Math.Max( TELEFONE.Length, listaClientes.Max(x => x.Telefone.Length));

void FinalizaLinha()
{
    Console.WriteLine();
}

void Justifica(string texto, int espaco)
{
    Console.Write(texto.PadRight(espaco));
}

void LinhaSeparadora(List<int> espacos)
{
    var totalEspacos = espacos.Sum();

    Console.WriteLine(new string('-', totalEspacos));
}

void MontaCabecalhoClientes()
{
    Console.WriteLine("RELATORIO DE CLIENTES");


    var espacoEspacador = ESPACO_SEPARADOR.Length;

    Justifica(ID      , maiorId       + espacoEspacador);
    Justifica(NOME    , maiorNome     + espacoEspacador);
    Justifica(ENDERECO, maiorEndereco + espacoEspacador);
    Justifica(TELEFONE, maiorTelefone + espacoEspacador);

    FinalizaLinha();

    LinhaSeparadora(new List<int> { maiorId, maiorNome, maiorEndereco, maiorTelefone, ESPACO_SEPARADOR.Length * 4 });
}

void ListaDados()
{
    MontaCabecalhoClientes();

    foreach (var cliente in listaClientes)
    {
        Justifica(cliente.Id.ToString(), maiorId);
        Console.Write(ESPACO_SEPARADOR);

        Justifica(cliente.Nome, maiorNome);
        Console.Write(ESPACO_SEPARADOR);

        Justifica(cliente.Endereco, maiorEndereco);
        Console.Write(ESPACO_SEPARADOR);

        Justifica(cliente.Telefone, maiorTelefone);
        Console.Write(ESPACO_SEPARADOR);

        FinalizaLinha();
    }
}

ListaDados();


FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO DE FORMA DE PAGAMENTO");

var formasPagamento = DadosFormaDePagamento.FormasDePagamento;

var pesquisa = from p in formasPagamento
               where p.Contains('c') 
               select p;

Console.WriteLine(string.Join(" ", pesquisa));

var pesquisa2 = formasPagamento.Where(p => p.Contains('c'));
Console.WriteLine(string.Join(" ", pesquisa2));

var pesquisaCliente = listaClientes.Where(p => p.Nome.StartsWith('A')).Select(p => p.Nome);
Console.WriteLine(string.Join(" ", pesquisaCliente));

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO GERAL DE PRODUTOS");

var produtos = DadosCardapio.GetProdutos();

foreach (var produto in produtos)
{
    Console.WriteLine(produto.Nome + " " + produto.Descricao + " " + produto.Preco);
}

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO NOME PRODUTO");

var produtosNome = DadosCardapio.GetProdutos().Select(p => p.Nome);

Console.WriteLine(string.Join(" ", produtosNome));

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO NOME PRECO DE PRODUTOS");

var produtosNomePreco = DadosCardapio.GetProdutos().Select(p => new { Nome = p.Nome, Preco = p.Preco});

foreach (var produto in produtosNomePreco)
{
    Console.WriteLine(produto.Nome + " " + produto.Preco);
}

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO COMPRE 4 PAGUE 3 PRODUTOS");

var produtosCompre4Pague3 = DadosCardapio.GetProdutos().Select(p => new { Nome = p.Nome, Preco = p.Preco * 3 });

foreach (var produto in produtosCompre4Pague3)
{
    Console.WriteLine(produto.Nome + " " + produto.Preco);
}

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO TODOS PEDIDOS MES");

var pedidos = DadosPedidos.QuantidadeItensPedidosPorDia.SelectMany(lista => lista);

foreach (var pedido in pedidos)
{
    Console.Write(pedido + " ");
}

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO PEDIDOS COM ITEM UNICO NO MES");

var pedidosItemUnico = DadosPedidos.QuantidadeItensPedidosPorDia.SelectMany(lista => lista).Count(p => p == 1); 
Console.Write(pedidosItemUnico);

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO TODAS AS QUANTIDADES DE PEDIDOS DIFERENTES");

// Distinct: quando você deseja remover elementos duplicados de uma sequência, com base em uma chave de comparação;
var pedidosQuantidadesDiferentes = DadosPedidos.QuantidadeItensPedidosPorDia.SelectMany(lista => lista).Distinct();

foreach (var pedido in pedidosQuantidadesDiferentes)
{
    Console.Write(pedido + " ");
}


FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO PRODUTOS QUE ESTÃO NO CARDAPIO E NÃO NO DELIVERY");

var produtosCardapio = DadosCardapio.GetProdutos().Select(p => p.Nome);
var produtosDelivery = DadosCardapio.CardapioDelivery().Select(p => p.Nome);

// Except: quando você deseja encontrar elementos que estão presentes em uma sequência, mas não em outra, com base em uma chave de comparação; 
var excecoes = produtosCardapio.Except(produtosDelivery);

foreach (var produto in excecoes)
{
    Console.Write(produto + " ");
}

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO PRODUTOS QUE ESTÃO NOS DOIS CARDAPIOS");

// Intersect: quando você deseja encontrar elementos que estão presentes em ambas as sequências, com base em uma chave de comparação;
var intersect = produtosCardapio.Intersect(produtosDelivery);

foreach (var produto in intersect)
{
    Console.WriteLine(produto);
}

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO TDOOS PRODUTOS DA LOJA");

// Union: quando você deseja combinar elementos de duas sequências, removendo duplicados com base em uma chave de comparação.
var unioes = produtosCardapio.Union(produtosDelivery);

foreach (var produto in unioes)
{
    Console.WriteLine(produto);
}

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO PRODUTOS ORDENADOS POR NOME E PRECO");

var produtosOrdenados = DadosCardapio.GetProdutos().OrderBy(p => p.Nome).ThenBy(p => p.Preco);

foreach (var produto in produtosOrdenados)
{
    Console.WriteLine(produto.Nome + " " + produto.Preco);
}

FinalizaLinha();
Console.WriteLine("-------------------------------");
Console.WriteLine("RELATORIO CARRINHO DE COMPRAS");

var carrinho = DadosCarrinho.GetProdutosCarrinho().Select(p => p.Nome);
var precoCarrinho = DadosCarrinho.GetProdutosCarrinho().Select(p => p.Preco);

string resultado = carrinho.Aggregate((p1, p2) => p1 + ", " + p2);
var valorFinal = precoCarrinho.Sum();
var quantidadeItensCarrinho = carrinho.Count();

Console.WriteLine(resultado + " " + valorFinal + " " + quantidadeItensCarrinho);

var produtosPorNome = DadosCarrinho.GetProdutosCarrinho().GroupBy(p => p.Nome);

foreach (var produto in produtosPorNome)
{
    Console.WriteLine(produto.Key);
    Console.WriteLine(produto.Count());
}
