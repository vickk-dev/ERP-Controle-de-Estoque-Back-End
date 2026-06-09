using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Domain.Entities;
using Ferramenteiro.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ferramenteiro.Infra.Persistence.Repository;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    // RN01 — verificação assíncrona de unicidade antes de persistir
    public async Task<bool> DocumentoJaExisteAsync(string documento, CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .AsNoTracking()
            .AnyAsync(c => c.Documento == documento, cancellationToken);
    }

    public async Task<Cliente> AdicionarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        await _context.Clientes.AddAsync(cliente, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cliente;
    }
}