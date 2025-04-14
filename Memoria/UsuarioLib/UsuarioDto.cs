namespace UsuarioLib;

public readonly record struct FormularioDto(string Nome, string Cpf, int Idade, string Cargo);

public record UsuarioDto
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public List<string> Telefones { get; set; }

    public virtual bool Equals(UsuarioDto? usuario)
    {
        return this.Nome == usuario.Nome && this.Email == usuario.Email && this.Telefones.SequenceEqual(usuario.Telefones);
    }
}