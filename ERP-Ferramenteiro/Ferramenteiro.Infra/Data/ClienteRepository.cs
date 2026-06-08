using ERP_Ferramenteiro.Domain.Entities;
using ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP_Ferramenteiro.Ferramenteiro.Infra.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        // Alterado de ExisteDocumentoAsync para ExistePorDocumentoAsync
        public async Task<bool> ExistePorDocumentoAsync(string documento, CancellationToken cancellationToken)
        {
            return await _context.Clientes
                .AsNoTracking()
                .AnyAsync(c => c.Documento == documento, cancellationToken);
        }

        public async Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken)
        {
            await _context.Clientes.AddAsync(cliente, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}