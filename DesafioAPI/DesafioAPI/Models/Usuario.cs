namespace DesafioAPI.Models
{
    public class Project
    {
        public int TeamId { get; set; }
        public int Id { get; set; }
        public string? Nome { get; set; }
        public bool Concluido { get; set; }
    }

    public class Team
    {
        public Guid UsuarioId { get; set; }
        public int Id { get; set; }
        public string? Nome { get; set; }
        public bool Lider { get; set; }
        public List<Project>? Projetos { get; set; }
    }

    public class Log
    {
        public Guid UsuarioId { get; set; }
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string? Acao { get; set; }
    }

    public class Usuario
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public int Score { get; set; }
        public bool Ativo { get; set; }
        public string? Pais { get; set; }
        public Team? Equipe { get; set; }
        public List<Log>? Logs { get; set; }
    }
}
