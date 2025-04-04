using System.Diagnostics;
using UsuarioLib;

/*
Usuario usuario = 
    new Usuario(
        "Daniel", 
        "daniel@email.com",
        new List<string>() {"12345678"});

//12345678
usuario.ExibeTelefones();

//efetuar a padronização
usuario.PadronizaTelefones();

//912345678
usuario.ExibeTelefones();


Stopwatch sw = Stopwatch.StartNew();
sw.Start();

for (int i = 0; i < 1000000; i++)
{
    Coordenada coordenada = new Coordenada(123.456, -123.456);
    var latitude = coordenada.Latitude;
    var longitude = coordenada.Longitude;
}

sw.Stop();

Console.WriteLine($"Tempo: {sw.Elapsed.TotalMilliseconds}");


FormularioDto dto = new FormularioDto("11111111111", 50, "Teste");
dto.Nome = "Nome";
Console.WriteLine(dto.Nome);
dto.Nome = "Nome";
Console.WriteLine(dto.Nome);

FormularioDto dto2 = new FormularioDto();
dto2.Nome = "Teste";
dto2.Idade = 50;
dto2.Cargo = "Teste";
dto2.Cpf = "11111111111";

Console.WriteLine(dto == dto2);
*/

Stopwatch sw = Stopwatch.StartNew();
sw.Start();

for (int i = 0; i < 1000000000; i++)
{
    Coordenada coordenada = new Coordenada(123.456, -123.456);
    var latitude = coordenada.Latitude;
    var longitude = coordenada.Longitude;
}

sw.Stop();
Console.WriteLine($"Tempo: {sw.Elapsed.TotalMilliseconds}");


sw.Restart();

for (int i = 0; i < 1000000000; i++)
{
    FormularioDto dto = new FormularioDto("Teste", "11111111", 25, "Dess");

    var nome = dto.Nome;
    var cpf = dto.Cpf;
}

sw.Stop();
Console.WriteLine($"Tempo: {sw.Elapsed.TotalMilliseconds}");
