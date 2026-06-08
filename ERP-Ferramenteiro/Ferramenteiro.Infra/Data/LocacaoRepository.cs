using ERP_Ferramenteiro.Domain.Entities;
using ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP_Ferramenteiro.Ferramenteiro.Infra.Data
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly AppDbContext _context;

        public LocacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locacao>> ObterLocacoesAtivasAsync(CancellationToken cancellationToken)
        {
            return await _context.Locacoes
                .AsNoTracking()         
                .Include(l => l.Cliente)
                .Include(l => l.Itens)
                .ThenInclude(i => i.Ferramenta)
                .Where(l => l.Status == StatusLocacao.Ativa)
                .ToListAsync(cancellationToken);
        }

    }
}
