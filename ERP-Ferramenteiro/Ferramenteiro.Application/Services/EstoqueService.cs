using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ferramenteiro.Domain.Entities;
using Ferramenteiro.Infra.Data;
using Ferramenteiro.Infra.Persistence;
using Ferramenteiro.Application.DTOs;

namespace Ferramenteiro.Application.Services
{
    public class EstoqueService
    {
        private readonly AppDbContext _context;

        public EstoqueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CadastrarCatalogoFisicoAsync(CadastroCatalogoDto dto)
        {
            bool isPesada = dto.Categoria.Equals("Pesadas", StringComparison.OrdinalIgnoreCase);
            string tipoTamanhoEntidade = isPesada ? "Maquinario Pesado" : dto.Categoria;


            decimal? valorHoraFinal = dto.PrecoHora;
            if (isPesada && (dto.PrecoHora == 0 || dto.PrecoHora == null))
            {
                valorHoraFinal = null;
            }

            var usaTransacao = _context.Database.IsRelational();
            var transaction = usaTransacao ? await _context.Database.BeginTransactionAsync() : null;

            try
            {
                foreach (var patrimonio in dto.Patrimonios)
                {
                    if (_context.Ferramentas.Any(f => f.PatrimonioId == patrimonio))
                    {
                        throw new InvalidOperationException($"Violação de integridade: O patrimônio {patrimonio} já existe.");
                    }

                    var ferramenta = new Ferramenta(
                        patrimonioId: patrimonio,
                        nome: dto.NomeModelo,
                        tipoTamanho: tipoTamanhoEntidade,
                        valorDia: dto.PrecoDia,
                        valorSemana: 0m, 
                        valorMes: 0m, 
                        valorHora: valorHoraFinal
                    );

                    await _context.Ferramentas.AddAsync(ferramenta);
                }

                await _context.SaveChangesAsync();

                if (usaTransacao)
                    await transaction.CommitAsync();
            }
            catch (Exception)
            {
                if (usaTransacao)
                    await transaction.RollbackAsync();

                throw; 
            }
        }
    }
}