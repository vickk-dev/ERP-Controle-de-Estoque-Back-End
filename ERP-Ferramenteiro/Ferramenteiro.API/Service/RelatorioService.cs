using ERP_Ferramenteiro.Application.DTOs;
using ERP_Ferramenteiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP_Ferramenteiro.Application.Services
{
    public class RelatorioService
    {
        private readonly AppDbContext _context;

        public RelatorioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RelatorioFaturamentoDto> ObterFaturamentoPorPeriodoAsync(
            DateOnly dataInicio,
            DateOnly dataFim,
            CancellationToken cancellationToken)
        {
            // Converte para DateTime UTC para comparar com DataDevolucaoReal
            var inicio = dataInicio.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
            var fim    = dataFim.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);

            var contratos = await _context.Locacoes
                .AsNoTracking()
                .Where(l =>
                    l.Status == StatusLocacao.Concluida &&        
                    l.DataDevolucaoReal.HasValue &&
                    l.DataDevolucaoReal.Value >= inicio &&
                    l.DataDevolucaoReal.Value <= fim)
                .Include(l => l.Cliente)
                .Select(l => new ContratoFaturadoDto
                {
                    ContratoId    = l.Id,
                    ClienteNome   = l.Cliente.NomeRazaoSocial,
                    ValorTotal    = l.ValorTotal,
                    DataDevolucao = l.DataDevolucaoReal!.Value
                })
                .ToListAsync(cancellationToken);

            var total = contratos.Sum(c => c.ValorTotal); 

            return new RelatorioFaturamentoDto
            {
                PeriodoInicio  = dataInicio,
                PeriodoFim     = dataFim,
                TotalFaturado  = total,
                Contratos      = contratos
            };
        }
    }
}