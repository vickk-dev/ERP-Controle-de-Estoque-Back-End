using Ferramenteiro.Domain.Entities;
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Ferramenteiro.Infra.Persistence;

namespace Ferramenteiro.Infra.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        // Alterado de ExisteDocumentoAsync para ExistePorDocumentoAsync
        public async Task<bool> ExistePorDocumentoAsync(string documento, CancellationToken cancellationToken) => await _context.Clientes
                .AsNoTracking()
                .AnyAsync(c => c.Documento == documento, cancellationToken);

        public async Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken)
        {
            await _context.Clientes.AddAsync(cliente, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}