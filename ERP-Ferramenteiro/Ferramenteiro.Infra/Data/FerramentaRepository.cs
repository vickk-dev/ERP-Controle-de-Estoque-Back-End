
using ERP_Ferramenteiro.Domain.Entities;
using ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace ERP_Ferramenteiro.Infrastructure.Repositories;

public class FerramentaRepository : IFerramentaRepository
{
    private readonly AppDbContext _context;

    public FerramentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ferramenta?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // O EF Core precisa rastrear a entidade para capturar as mudanças de status (Disponível -> Alugada)
        return await _context.Ferramentas
            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task AdicionarAsync(Ferramenta ferramenta, CancellationToken cancellationToken)
    {
        await _context.Ferramentas.AddAsync(ferramenta, cancellationToken);
    }
}