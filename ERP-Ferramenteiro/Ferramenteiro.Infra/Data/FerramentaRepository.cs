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

using Microsoft.EntityFrameworkCore;

namespace Ferramenteiro.Infrastructure.Repositories;

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