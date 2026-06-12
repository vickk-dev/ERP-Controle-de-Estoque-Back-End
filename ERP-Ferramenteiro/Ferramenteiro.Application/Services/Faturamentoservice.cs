using Microsoft.EntityFrameworkCore;
using Ferramenteiro.API.DTOs;
using Ferramenteiro.Domain.Entities;
using Ferramenteiro.Infra.Persistence;

namespace Ferramenteiro.Application.Services;

// ── Interface ────────────────────────────────────────────────────────────────

public interface IFaturamentoService
{
    Task<FaturamentoResponseDto> ObterFaturamentoAsync(DateOnly dataInicio, DateOnly dataFim);
}

// ── Implementação ─────────────────────────────────────────────────────────────

public class FaturamentoService : IFaturamentoService
{
    private readonly AppDbContext _context;

    public FaturamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FaturamentoResponseDto> ObterFaturamentoAsync(
        DateOnly dataInicio,
        DateOnly dataFim)
    {
        var inicio = DateTime.SpecifyKind(dataInicio.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        var fim = DateTime.SpecifyKind(dataFim.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Utc);

        // Busca do banco: apenas contratos Finalizados com devolução no período
        var contratos = await _context.Contratos
            .Where(c =>
                c.Status == "Finalizado" &&
                c.DataDevolucao.HasValue &&
                c.DataDevolucao.Value >= inicio &&
                c.DataDevolucao.Value <= fim)
            .Select(c => new ContratoFaturamentoDto
            {
                ContratoId = c.Id,
                ClienteNome = c.ClienteNome,
                ValorTotal = c.ValorTotal,
                DataDevolucao = c.DataDevolucao!.Value
            })
            .AsNoTracking()
            .ToListAsync();

        // Soma feita em memória com LINQ (critério de aceite #4)
        var totalFaturado = contratos.Sum(c => c.ValorTotal);

        return new FaturamentoResponseDto
        {
            PeriodoInicio = dataInicio,
            PeriodoFim = dataFim,
            TotalFaturado = totalFaturado,
            Contratos = contratos
        };
    }
}