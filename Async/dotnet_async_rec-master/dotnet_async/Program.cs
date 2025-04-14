using System.Text.Encodings.Web;
using System.Text.Json;

object chave = new object();

Task<string> conteudoTask;
lock (chave)
{
    conteudoTask = Task.Run(() => File.ReadAllTextAsync("voos.txt", System.Text.Encoding.Latin1));
}

void ExibirRelatorio()
{
    Task.Delay(2000).GetAwaiter().GetResult();

    Console.WriteLine("Executando relatorio!");
}

void LerArquivo()
{
    try
    {
        Task.Delay(2000).GetAwaiter().GetResult();

        Console.WriteLine($"Conteúdo: \n{conteudoTask.Result}");
    }
    catch (AggregateException ex)
    {
        Console.WriteLine(ex.InnerException.Message);
    }
}

// Thread t = new Thread(LerArquivo);
// t.Start();
// 
// Thread t2 = new Thread(ExibirRelatorio);
// t2.Start();

// Task task1 = Task.Run(() => { LerArquivo(); });
// Task task2 = Task.Run(() => { ExibirRelatorio(); });
// 
// while (task1.Status == TaskStatus.Running  
//     || task2.Status == TaskStatus.Running)
// {
//     Console.WriteLine(".");
//     Task.Delay(500).GetAwaiter().GetResult(); ;
// }

async Task ExibirRelatorioAsync()
{
    await Task.Delay(5000);

    Console.WriteLine("Executando relatorio!");
}

async Task LerArquivoAsync()
{
    try
    {
        await Task.Delay(5000);

        Console.WriteLine($"Conteúdo: \n{conteudoTask.Result}");
    }
    catch (AggregateException ex)
    {
        Console.WriteLine(ex.InnerException.Message);
    }
}

// LerArquivoAsync();
// await ExibirRelatorioAsync();

async Task ExibirRelatorioAsync_TASK(CancellationToken token)
{
    try
    {
        await Task.Delay(5000, token);

        Console.WriteLine("Executando relatorio!");

        token.ThrowIfCancellationRequested();
    }
    catch (AggregateException ex)
    {
        Console.WriteLine(ex.InnerException.Message);
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine($"Tarefa cancelada {ex.Message}");
    }
}

async Task<int> LerArquivoAsync_TASK(CancellationToken token)
{
    try
    {
        await Task.Delay(5000, token);

        Console.WriteLine($"Conteúdo: \n{conteudoTask.Result}");
        token.ThrowIfCancellationRequested();
    }
    catch (AggregateException ex)
    {
        Console.WriteLine(ex.InnerException.Message);
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine($"Tarefa cancelada {ex.Message}");
    }


    return 9;
}


CancellationTokenSource tokenCancelamento = new CancellationTokenSource();

// int i = await LerArquivoAsync_TASK();
// await ExibirRelatorioAsync_TASK();

// Task tarefa = Task.WhenAll(LerArquivoAsync_TASK(tokenCancelamento.Token), ExibirRelatorioAsync_TASK(tokenCancelamento.Token));
// 
// await Task.Delay(1000).ContinueWith(_ =>  tokenCancelamento.Cancel());
// 
// Console.WriteLine("Outras operações.");
// 
// await tarefa;

List<Voo> listaVoos;

using (var stream = new FileStream("..\\..\\..\\voos.json", FileMode.Open, FileAccess.Read))
{
    listaVoos = await JsonSerializer.DeserializeAsync<List<Voo>>(stream);
}

foreach (var voo in listaVoos)
{
    Console.WriteLine(voo.Id);
    Console.WriteLine(voo.Origem);
    Console.WriteLine(voo.Destino);
    Console.WriteLine(voo.Preco);
    Console.WriteLine(voo.MilhasNecessaria);
}
