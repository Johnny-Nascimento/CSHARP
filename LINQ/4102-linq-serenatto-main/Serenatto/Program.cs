using SerenattoEnsaio.Dados;
using Microsoft.Data.SqlClient;
using Serenatto.Dados;
using SerenattoEnsaio.Modelos;
using SerenattoPreGravacao.Dados;
using System.Configuration;

IEnumerable<Cliente> clientes = DadosClientes.GetClientes().ToList();
IEnumerable<string> formasPagamento = DadosFormaDePagamento.FormasDePagamento;
IEnumerable<Produto> cardapioLoja = DadosCardapio.GetProdutos();
IEnumerable<Produto> cardapioDelivery = DadosCardapio.CardapioDelivery();
IEnumerable<int> totalPedidosMes = DadosPedidos.QuantidadeItensPedidosPorDia.SelectMany(lista => lista);
IEnumerable<Produto> carrinho = DadosCarrinho.GetProdutosCarrinho();
string connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

Console.WriteLine("RELATÓRIO DE DADOS CLIENTES");
foreach (var cliente in clientes)
    using (var context = new SerenattoContext())
    {
        Console.WriteLine($"{cliente.Id} | {cliente.Nome} | {cliente.Endereco} | {cliente.Telefone}");
        while (true)
        {
            Console.Clear();
            Console.WriteLine("----- Boas vindas ao Serenatto -----");
            Console.WriteLine("1. Cadastrar produto");
            Console.WriteLine("2. Listar produtos");
            Console.WriteLine("3. Atualizar produto");
            Console.WriteLine("4. Excluir produto");
            Console.WriteLine("5. Sair");
            Console.Write("Opção: ");

            if (!int.TryParse(Console.ReadLine(), out int opcao))
            {
                Console.WriteLine("Opção inválida!");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    CadastrarProduto(connectionString);
                    break;
                case 2:
                    ListarProdutos(connectionString);
                    break;
                case 3:
                    AtualizarProduto(connectionString);
                    break;
                case 4:
                    ExcluirProduto(connectionString);
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

        }

        static void CadastrarProduto(string connectionString)
        {
            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();
            Console.Write("Preço: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
            {
                Console.WriteLine("Preço inválido!");
                return;
            }
            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Produtos(Id, Nome, Preco, Descricao) VALUES(@Id, @Nome, @Preco, @Descricao)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                    command.Parameters.AddWithValue("@Nome",
         nome);
                    command.Parameters.AddWithValue("@Preco", preco);
                    command.Parameters.AddWithValue("@Descricao", descricao);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Produto cadastrado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Erro ao cadastrar o produto.");
                    }
                }
            }
        }

        static void ListarProdutos(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Produtos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())

                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("Não há produtos cadastrados.");
                        }
                        else
                        {
                            Console.WriteLine("Lista de produtos:");
                            Console.WriteLine("------------------");
                            Console.WriteLine("ID\tNome\tPreço\tDescrição");

                            while (reader.Read())
                            {
                                Guid id = reader.GetGuid(0);
                                string nome = reader.GetString(1);
                                decimal preco = reader.GetDecimal(2);
                                string descricao = reader.GetString(3);

                                Console.WriteLine($"{id}\t{nome}\t{preco}\t{descricao}");
                            }
                        }
                    }
                }
            }
        }

        static void AtualizarProduto(string connectionString)
        {
            Console.Write("Digite o ID do produto a ser atualizado: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid produtoId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            Console.Write("Novo nome (deixe em branco para manter): ");
            string novoNome = Console.ReadLine();
            Console.Write("Novo preço (deixe em branco para manter): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal novoPreco))
            {
                novoPreco = 0;
            }
            Console.Write("Nova descrição (deixe em branco para manter): ");
            string novaDescricao = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"UPDATE Produtos SET Nome = @NovoNome, Preco = @NovoPreco, Descricao = @NovaDescricao WHERE Id = @ProdutoId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProdutoId", produtoId);
                    command.Parameters.AddWithValue("@NovoNome", novoNome);
                    command.Parameters.AddWithValue("@NovoPreco", novoPreco);
                    command.Parameters.AddWithValue("@NovaDescricao", novaDescricao);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Produto atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado ou não houve alterações.");
                    }
                }
            }
        }

        static void ExcluirProduto(string connectionString)
        {
            Console.Write("Digite o ID do produto a ser excluído: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid produtoId))
            {
                Console.WriteLine("ID inválido!");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Produtos WHERE Id = @ProdutoId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProdutoId", produtoId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Produto excluído com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Produto não encontrado.");
                    }
                }
            }
        }
    }

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO FORMAS DE PAGAMENTO");
var pesquisa = from p in formasPagamento
               where p.Contains('c')
               select p;

Console.WriteLine(string.Join(" ", pesquisa));

var pesquisa2 = formasPagamento.Where(p => p.StartsWith('d'));

Console.WriteLine(string.Join(" ", pesquisa2));

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO DADOS CARDÁPIO LOJA");

cardapioLoja.ToList();

foreach (var item in cardapioLoja)
{
    Console.WriteLine($"{item.Id} | {item.Nome} | {item.Descricao} | {item.Preco}");
}

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS DO CARDÁPIO POR NOME");

var produtosPorNome = cardapioLoja.Select(p => p.Nome);

foreach (var item in produtosPorNome)
{
    Console.WriteLine(item);
}

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS POR PREÇO");

var produtosPreco = cardapioLoja.Select(c => new
{
    NomeProduto = c.Nome,
    PrecoProduto = c.Preco
});

foreach (var item in produtosPreco)
{
    Console.WriteLine($"{item.NomeProduto} | {item.PrecoProduto}");
}

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS POR PREÇO NO COMBO LEVE 4 E PAGUE 3");

var produtosPrecoCombo = cardapioLoja.Select(c => new
{
    NomeProduto = c.Nome,
    PrecoCombo = c.Preco * 3
});

foreach (var item in produtosPrecoCombo)
{
    Console.WriteLine($"{item.NomeProduto} | {item.PrecoCombo}");
}

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO QUANTIDADE PRODUTOS PEDIDOS NO MÊS");

foreach (var pedido in totalPedidosMes)
{
    Console.Write($"{pedido} ");
}

Console.WriteLine(" ");
Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO TOTAL DE PEDIDOS INDIVIDUAIS NO MÊS");

var pedidosIndividuais = totalPedidosMes
    .Count(numero => numero == 1);

Console.WriteLine($"O total de pedidos individuais foi: {pedidosIndividuais}");

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO PEDIDOS COM QUANTIDADES DIFERENTES DE ITENS");

IEnumerable<int> totalPedidosDiferentesMes = totalPedidosMes.Distinct();

foreach (var pedido in totalPedidosDiferentesMes)
{
    Console.Write($"{pedido} ");
}

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS SOMENTE LOJA");

IEnumerable<string> produtoCardapioLoja = cardapioLoja.Select(p => p.Nome);
IEnumerable<string> produtoCardapioDelivery = cardapioDelivery.Select(p => p.Nome);

var produtosSomenteLoja = produtoCardapioLoja.Except(produtoCardapioDelivery).ToList();

foreach (var produto in produtosSomenteLoja)
{
    Console.WriteLine(produto);
}

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS LOJA E DELIVERY");

var listaProdutosLojaEDelivery = produtoCardapioLoja.Intersect(produtoCardapioDelivery).ToList();

foreach (var item in listaProdutosLojaEDelivery)
{
    Console.WriteLine(item);
}

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO TODOS OS PRODUTOS CARDÁPIO");

var listaProdutosGeral = produtoCardapioLoja.Union(produtoCardapioDelivery).ToList();

foreach (var item in listaProdutosGeral)
{
    Console.WriteLine(item);
}

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS ORDENADOS POR NOME E PREÇO");

var cardapioOrdenado = cardapioLoja
    .OrderBy(p => p.Nome)
    .ThenBy(p => p.Preco);

foreach (var item in cardapioOrdenado)
{
    Console.WriteLine($"{item.Nome} | {item.Preco}");
}

Console.WriteLine("--------------------------------");
Console.WriteLine("RELATÓRIO CARRINHO DE COMPRAS");

IEnumerable<string> nomeProdutos = carrinho.Select(c => c.Nome);
IEnumerable<decimal> precoProdutos = carrinho.Select(p => p.Preco);

string resultado = nomeProdutos.Aggregate((p1, p2) => p1 + ", " + p2);
//decimal valorFinal = precoProdutos.Aggregate((n1, n2) => n1 + n2);
decimal valorFinal = precoProdutos.Sum();

var numeroProdutos = nomeProdutos.Count();

var grupoPorNome = carrinho.GroupBy(p => p.Nome);

Console.WriteLine(resultado);
Console.WriteLine($"Total de produtos do carrinho: {numeroProdutos}");

foreach (var grupo in grupoPorNome)
{
    Console.WriteLine($"Nome do produto: {grupo.Key}");
    Console.WriteLine($"Numero de produtos: {grupo.Count()}");
}


Console.WriteLine($"Valor total da compra: {valorFinal}");
