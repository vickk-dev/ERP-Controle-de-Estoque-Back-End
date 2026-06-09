using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Ferramenteiro.Application.DTOs;
using Ferramenteiro.Application.Services;
using Ferramenteiro.Domain.Entities;
using Ferramenteiro.Infrastructure.Data;

namespace Ferramenteiro.Tests.Services
{
    public class EstoqueServiceTests
    {
        private AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task Cenário1_CadastroTransacional_DeveInserirTodasAsFerramentas()
        {
            // Arrange
            var context = GetContext();
            var service = new EstoqueService(context);
            var dto = new CadastroCatalogoDto
            {
                Categoria = "Pesadas",
                NomeModelo = "Martelete Rompedor",
                PrecoHora = 0.00m,
                PrecoDia = 150.00m,
                Patrimonios = new List<string> { "MART-01", "MART-02" }
            };

            // Act
            await service.CadastrarCatalogoFisicoAsync(dto);

            // Assert
            var totalFerramentas = await context.Ferramentas.CountAsync();
            Assert.Equal(2, totalFerramentas);
            Assert.Contains(context.Ferramentas, f => f.PatrimonioId == "MART-01");
            Assert.Contains(context.Ferramentas, f => f.PatrimonioId == "MART-02");
        }

        [Fact]
        public async Task Cenário2_FalhaPorViolacaoDeIntegridade_DeveGerarRollback()
        {
            var context = GetContext();
            var ferramentaExistente = new Ferramenta("MART-01", "Broca", "Leves", 50m, 100m, 300m);
            context.Ferramentas.Add(ferramentaExistente);
            await context.SaveChangesAsync();

            var service = new EstoqueService(context);
            var dto = new CadastroCatalogoDto
            {
                Categoria = "Pesadas",
                NomeModelo = "Martelete Rompedor",
                PrecoHora = 0.00m,
                PrecoDia = 150.00m,
                Patrimonios = new List<string> { "MART-01", "MART-02" } 
            };

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => service.CadastrarCatalogoFisicoAsync(dto)
            );

            Assert.Contains("MART-01 já existe", ex.Message);

            var totalFerramentas = await context.Ferramentas.CountAsync();
            Assert.Equal(1, totalFerramentas); 
            Assert.DoesNotContain(context.Ferramentas, f => f.PatrimonioId == "MART-02");
        }
    }
}