using Microsoft.EntityFrameworkCore;
using ERP_Ferramenteiro.Domain.Entities; 

namespace ERP_Ferramenteiro.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Ferramenta> Ferramentas { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<LocacaoItem> LocacaoItens { get; set; }
        public DbSet<Faturamento> Faturamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().HasIndex(c => c.Documento).IsUnique();
            modelBuilder.Entity<Funcionario>().HasIndex(f => f.Matricula).IsUnique();
            modelBuilder.Entity<Ferramenta>().HasIndex(f => f.PatrimonioId).IsUnique();

            modelBuilder.Entity<Cliente>().Property(c => c.Tipo).HasConversion<string>();
            modelBuilder.Entity<Ferramenta>().Property(f => f.Status).HasConversion<string>();
            modelBuilder.Entity<Locacao>().Property(l => l.Status).HasConversion<string>();
            modelBuilder.Entity<LocacaoItem>().Property(li => li.TipoCobranca).HasConversion<string>();
            modelBuilder.Entity<Faturamento>().Property(f => f.StatusPagamento).HasConversion<string>();

            modelBuilder.Entity<Ferramenta>().Property(f => f.ValorHora).HasPrecision(18, 2);
            modelBuilder.Entity<Ferramenta>().Property(f => f.ValorDia).HasPrecision(18, 2);
            modelBuilder.Entity<Ferramenta>().Property(f => f.ValorSemana).HasPrecision(18, 2);
            modelBuilder.Entity<Ferramenta>().Property(f => f.ValorMes).HasPrecision(18, 2);

            modelBuilder.Entity<Locacao>().Property(l => l.ValorTotal).HasPrecision(18, 2);
            modelBuilder.Entity<LocacaoItem>().Property(li => li.ValorUnitarioAplicado).HasPrecision(18, 2);
            modelBuilder.Entity<Faturamento>().Property(f => f.ValorFaturado).HasPrecision(18, 2);

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

            modelBuilder.Entity<LocacaoItem>()
                .HasOne(li => li.Ferramenta)
                .WithMany()
                .HasForeignKey(li => li.FerramentaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}