using ERP_Ferramenteiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERP_Ferramenteiro.Infra.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios => Set<Funcionario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Funcionario>(e =>
            {
                e.ToTable("funcionarios");
                e.HasKey(f => f.Id);

                e.Property(f => f.Matricula)
                    .IsRequired()
                    .HasMaxLength(20);

                e.Property(f => f.Nome)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(f => f.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                e.HasIndex(f => f.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_Funcionarios_Email");

                e.Property(f => f.SenhaHash)
                    .IsRequired()
                    .HasMaxLength(100);   

                e.Property(f => f.Ativo)
                    .IsRequired();
            });
        }
    }
}