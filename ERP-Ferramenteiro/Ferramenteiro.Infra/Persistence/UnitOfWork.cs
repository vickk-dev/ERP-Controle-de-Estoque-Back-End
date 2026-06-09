using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Infra.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Ferramenteiro.Infra.Persistence;

namespace Ferramenteiro.Infra.Persistence
{
    public class UnitOfWork : IUnitOfWork

    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            // Trava o banco iniciando a transação física
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            if (_transaction == null) throw new InvalidOperationException("Transação não iniciada.");
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken)
        {
            if (_transaction != null) await _transaction.RollbackAsync(cancellationToken);
        }
    }
}
