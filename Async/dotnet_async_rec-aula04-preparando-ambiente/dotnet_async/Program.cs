#region assync
//object chave = new object();
//Task<string> conteudoTask;
//lock(chave)
//{
//    conteudoTask = Task.Run(() => File.ReadAllTextAsync("voos.txt"));
//}

//async Task LerArquivoAsync(CancellationToken token)
//{
//    try
//    {
//        await Task.Delay(new Random().Next(300, 8000));
//        token.ThrowIfCancellationRequested();
//        Console.WriteLine($"Conteúdo: \n{conteudoTask.Result}");

//    }
//    catch (OperationCanceledException ex)
//    {
//       Console.WriteLine($"Tarefa cancelada: {ex.Message}");
//    }
//    catch (AggregateException ex)
//    {
//        Console.WriteLine($"Aconteceu o erro: {ex.InnerException.Message}");
//    }

//}

//async Task ExibirRelatorioAsync(CancellationToken token)
//{
//	try
//	{
//        Console.WriteLine("Executando relatório de compra de passagens!");
//        await Task.Delay(new Random().Next(300, 8000));
//        token.ThrowIfCancellationRequested();
//    }
//	catch(OperationCanceledException ex)
//	{

//        Console.WriteLine($"Tarefa cancelada: {ex.Message}");
//	}

//}

//CancellationTokenSource tokenDeCancelamento = new CancellationTokenSource();

//Task tarefa= Task.WhenAll(LerArquivoAsync(tokenDeCancelamento.Token), ExibirRelatorioAsync(tokenDeCancelamento.Token));

//await Task.Delay(1000).ContinueWith(_ => tokenDeCancelamento.Cancel());

//Console.WriteLine("Outras operações.");
//Console.ReadKey();
#endregion

using dotnet_async.Client;
using dotnet_async.Modelos;

var client = new JornadaMilhasClient(new JornadaMilhasClientFactory().CreateClient());

async Task ProcessarConsultasDeVooAsync()
{
    try
    {

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        var voos = await client.ConsultarVoosAsync(cancellationTokenSource.Token);


        foreach (var voo in voos)
        {
            Console.WriteLine(voo.ToString());
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

async Task CompraPassagemAsync()
{
    CompraPassagemRequest compraPassagem = new CompraPassagemRequest();
    compraPassagem.Origem = "Vitória";
    compraPassagem.Destino = "São Paulo";
    compraPassagem.Milhas = 15000;

    var resposta = await client.ComprarPassagemAsync(compraPassagem);
    Console.WriteLine(resposta);
}

await ProcessarConsultasDeVooAsync();
await CompraPassagemAsync();