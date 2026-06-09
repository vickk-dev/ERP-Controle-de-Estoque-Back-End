using Ferramenteiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ferramenteiro.Infra.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => c.Documento)
                  .IsUnique()
                  .HasDatabaseName("IX_Clientes_Documento");

            entity.Property(c => c.Documento)
                  .IsRequired()
                  .HasMaxLength(14);

            entity.Property(c => c.NomeRazaoSocial)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(c => c.EnderecoCompleto)
                  .IsRequired()
                  .HasMaxLength(500);

            entity.Property(c => c.Telefone)
                  .HasMaxLength(11);
        });
    }
}
