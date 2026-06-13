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

        public async Task<List<Guid>> CadastrarCatalogoFisicoAsync(CadastroCatalogoDto dto)
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

            // 1. Cria a lista para guardar os IDs
            var idsCadastrados = new List<Guid>();

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

                    // 2. O Guid já foi gerado no construtor da Entidade. Guardamos ele agora.
                    idsCadastrados.Add(ferramenta.Id);
                }

                await _context.SaveChangesAsync();

                if (usaTransacao)
                    await transaction.CommitAsync();

                // 3. Devolve a lista para a Controller
                return idsCadastrados;
            }
            catch (Exception)
            {
                if (usaTransacao)
                    await transaction.RollbackAsync();

                throw;
            }
        }

        // Adicione os usings necessários no topo se faltar algum:
        // using System.Collections.Generic;
        // using Ferramenteiro.Application.DTOs;

        public async Task<IEnumerable<EstoqueItemResponse>> ObterEstoqueAgrupadoAsync(CancellationToken cancellationToken = default)
        {
            // 1. Buscamos todas as ferramentas que não estão alugadas ou inativas
            // (Assumindo que o StatusFerramenta.Disponivel é o seu status padrão para estoque livre)
            var ferramentas = await _context.Ferramentas
                .AsNoTracking()
                // .Where(f => f.Status == StatusFerramenta.Disponivel) // Descomente e ajuste conforme o seu Enum
                .ToListAsync(cancellationToken);

            // 2. Agrupamos na memória do servidor para montar o layout do React
            var estoqueAgrupado = ferramentas
                .GroupBy(f => new { f.Nome, Marca = f.TipoTamanho, f.ValorDia }) // Agrupa por modelo e preço
                .Select(grupo => new EstoqueItemResponse(
                    Id: grupo.First().Id.ToString(), // Pega o Guid do primeiro item como Key do React
                    Nome: grupo.Key.Nome,
                    Marca: grupo.Key.Marca,
                    Estoque: grupo.Count(), // Conta quantas furadeiras existem neste grupo
                    PrecoDia: grupo.Key.ValorDia
                ))
                .ToList();

            return estoqueAgrupado;
        }

    }
}