using SerenattoEnsaio.Dados;
using SerenattoEnsaio.Modelos;

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

    Console.WriteLine(new string('_', totalEspacos));
}

void MontaCabecalhoClientes()
{
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
