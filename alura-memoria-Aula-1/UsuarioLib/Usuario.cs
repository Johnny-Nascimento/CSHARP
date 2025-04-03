using System.Diagnostics;

namespace UsuarioLib;

public class Usuario
{

    public Usuario(string nome, string email, List<string> telefone)
    {
        Nome = nome;
        Email = email;
        Telefones = telefone;

        ChavesAcesso = new List<int>();
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        ChavesAcesso.Add(0);
        for (int i = 1; i < 1000000; i++)
        {
            ChavesAcesso.Insert(1, i);
        }
        stopwatch.Stop();

        Console.WriteLine($"Total tempo ms: {stopwatch.Elapsed.TotalMilliseconds}");
        // -------- // ---------------- // ----------------

        ChavesAcessoLinkedList = new LinkedList<int>();
        ChavesAcessoLinkedList.AddFirst(0);
        Stopwatch stopwatchLL = new Stopwatch();
        stopwatchLL.Start();
        
        for (int i = 1; i < 1000000; i++)
        {
            ChavesAcessoLinkedList.AddAfter(ChavesAcessoLinkedList.First, i);
        }
        stopwatchLL.Stop();

        Console.WriteLine($"Total tempo LL ms: {stopwatchLL.Elapsed.TotalMilliseconds}");

        // -------- // ---------------- // ----------------

        Console.WriteLine(ChavesAcesso.SequenceEqual(ChavesAcessoLinkedList.ToList()));
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public List<string> Telefones { get; set; }
    public List<int> ChavesAcesso { get; set; }
    public LinkedList<int> ChavesAcessoLinkedList { get; set; }

    public void PadronizaTelefones()
    {
        // Sem a atribuição de "Telefones = ..." a lista não era alterada por referência.
        Telefones = Telefones.Select(telefone =>
            telefone.Length == 8 ?
            telefone = "9" + telefone :
            telefone
        ).ToList();
    }

    public void ExibeTelefones()
    {
        Telefones.ForEach(telefone => Console.WriteLine(telefone));
    }
}
