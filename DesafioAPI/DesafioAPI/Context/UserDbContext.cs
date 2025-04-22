using DesafioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Logs)
            .WithOne()
            .HasForeignKey(l => l.UsuarioId)
            .IsRequired(false);

            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Equipe)
            .WithOne()
            .HasForeignKey<Team>(t => t.UsuarioId)
            .IsRequired(false);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Projetos)
                .WithOne()
                .HasForeignKey(p => p.TeamId)
                .IsRequired(false);
        }

        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Team> times { get; set; }
        public DbSet<Project> projetos { get; set; }
        public DbSet<Log> logs { get; set; }
    }
}
