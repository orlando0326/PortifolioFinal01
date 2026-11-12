using Microsoft.EntityFrameworkCore;
using PortfolioFinal01.Model;
using PortfolioFinal01.Data;

namespace PortfolioFinal01.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Turma> Turmas { get; set; }

        public DbSet<Projeto> Projetos { get; set; }
    }
}