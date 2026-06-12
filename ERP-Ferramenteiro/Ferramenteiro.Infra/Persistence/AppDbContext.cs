using Ferramenteiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ferramenteiro.Infra.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Declarações de DbSet necessárias
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Contrato> Contratos => Set<Contrato>();
    public DbSet<Ferramenta> Ferramentas => Set<Ferramenta>();
    public DbSet<Locacao> Locacoes => Set<Locacao>();
    public DbSet<LocacaoItem> LocacaoItens => Set<LocacaoItem>();
    public DbSet<Faturamento> Faturamentos => Set<Faturamento>();

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

        modelBuilder.Entity<Locacao>()
            .HasMany(l => l.Itens)
            .WithOne(li => li.Locacao)
            .HasForeignKey(li => li.LocacaoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Locacao>()
            .HasOne(l => l.Faturamento)
            .WithOne(f => f.Locacao)
            .HasForeignKey<Faturamento>(f => f.LocacaoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Locacao>()
            .HasOne(l => l.Cliente)
            .WithMany()
            .HasForeignKey(l => l.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Locacao>()
            .Navigation(l => l.Itens)
            .HasField("_itens");

        modelBuilder.Entity<LocacaoItem>()
            .HasOne(li => li.Ferramenta)
            .WithMany()
            .HasForeignKey(li => li.FerramentaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}