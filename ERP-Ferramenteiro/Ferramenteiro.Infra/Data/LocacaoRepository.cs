using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ferramenteiro.Domain.Entities;
using Ferramenteiro.Domain.Enums; 
using Ferramenteiro.Infra.Persistence; 
using Microsoft.EntityFrameworkCore;
using Ferramenteiro.Application.Interfaces;

namespace Ferramenteiro.Infra.Data 
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

        public async Task<Locacao?> ObterLocacaoPorIdComVinculosAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Locacoes
                .Include(l => l.Cliente)
                .Include(l => l.Itens)
                .ThenInclude(i => i.Ferramenta)
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }
    }
}