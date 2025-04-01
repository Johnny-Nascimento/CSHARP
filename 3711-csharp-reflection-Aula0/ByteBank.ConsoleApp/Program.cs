using ByteBank.Common;
using System.Reflection;

MostrarBanner();

while (true)
{
    MostrarMenu();

    if (int.TryParse(Console.ReadLine(), out int escolha))
    {
        ExecutarEscolha(escolha);
    }
    else
    {
        Console.WriteLine("Opção inválida. Tente novamente.");
    }
}

static void MostrarBanner()
{
    Console.WriteLine(@"


    ____        __       ____              __      
   / __ )__  __/ /____  / __ )____ _____  / /__    
  / __  / / / / __/ _ \/ __  / __ `/ __ \/ //_/    
 / /_/ / /_/ / /_/  __/ /_/ / /_/ / / / / ,<       
/_____/\__, /\__/\___/_____/\__,_/_/ /_/_/|_|      
      /____/                                       
                                
        ");
}

static void MostrarMenu()
{
    Console.WriteLine("\nEscolha uma opção:");
    Console.WriteLine();
    Console.WriteLine("1. Ler arquivo de boletos");
    Console.WriteLine();
    Console.WriteLine("2. Gravar boletos agrupados");
    Console.WriteLine();
    Console.WriteLine("3. Lista de teste");
    Console.WriteLine();
    Console.Write("Digite o número da opção desejada: ");
}

static void ExecutarEscolha(int escolha)
{
    switch (escolha)
    {
        case 1:
            LerArquivoBoletos();
        break;

        case 2:
            GravarArquivosAgrupados();
        break;

        case 3:
            GeraRelatorioDeTeste();
        break;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }
}

static void LerArquivoBoletos()
{
    Console.WriteLine("Lendo arquivo de boletos...");

    var leitorDeBoleto = new LeitorDeBoleto();
    List<Boleto> boletos = leitorDeBoleto.LerBoletos("Boletos.csv");

    foreach (var boleto in boletos)
    {
        Console.WriteLine($"Cedente: {boleto.CedenteNome}, Valor: {boleto.Valor:#0.00}, Vencimento: {boleto.DataVencimento} Multa: {boleto.Multa}");
    }
}

static void GravarArquivosAgrupados()
{
    Console.WriteLine("Gravando arquivos...");

    var leitorDeBoleto = new LeitorDeBoleto();
    List<Boleto> boletos = leitorDeBoleto.LerBoletos("Boletos.csv");

    string[] nomeParametroConstrutor = { "nomeArquivoSaida", "dataRelatorio" };
    object[] valoresParametrosConstrutor = { "BoletosAgrupados.csv", new DateTime(1998, 02, 27) };
    var nomeMetodo = "Processar";
    Type tipoRelatorio = typeof(RelatorioDeBoleto);

    ProcessarDinamicamente<Boleto>(tipoRelatorio, nomeParametroConstrutor, valoresParametrosConstrutor, nomeMetodo, boletos);
}

static void ProcessarDinamicamente<T>(Type tipoRelatorio, string[] nomeParametroConstrutor, object[] valoresParametrosConstrutor, string nomeMetodo, List<T> parametroMetodo)
{
    if (nomeParametroConstrutor.Length != valoresParametrosConstrutor.Length)
        throw new Exception("Quantidade de nomes de parametros é diferente da quantidade de parametros.");

    var construtores = tipoRelatorio.GetConstructors();

    var construtor = construtores
        .Single(c => c.GetParameters().Length == nomeParametroConstrutor.Length &&
        c.GetParameters().Select(p => p.Name).SequenceEqual(nomeParametroConstrutor));

    var instanciaClasse = construtor.Invoke(valoresParametrosConstrutor);

    var metodoProcessar = tipoRelatorio.GetMethod(nomeMetodo, new Type[] { parametroMetodo.GetType() });
    if (metodoProcessar == null) 
        throw new Exception("Erro na tentativa de encontrar um método válido.");

   if (metodoProcessar.GetParameters().Any(p => p.ParameterType == parametroMetodo.GetType()) == false)
        throw new Exception("Tipo de parâmetro do método é diferente do esperado.");

    metodoProcessar.Invoke(instanciaClasse, new object[] { parametroMetodo });
}

static void GeraRelatorioDeTeste()
{
    Console.WriteLine("Este é um relatorio de teste");

    string[] nomeParametroConstrutor = { "nomeArquivoSaida"};
    object[] valoresParametrosConstrutor = { "BoletosAgrupados.csv" };
    var nomeMetodo = "Processar";
    Type tipoRelatorio = typeof(RelatorioDeBoleto);

    List<string> listaTeste = new List<string> { "teste1", "teste2", "teste3" };

    ProcessarDinamicamente<string>(tipoRelatorio, nomeParametroConstrutor, valoresParametrosConstrutor, nomeMetodo, listaTeste);
}