using System.Threading.Tasks;
using ERP_Ferramenteiro.Domain.Entities;
using ERP_Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace ERP_Ferramenteiro.Infra.Persistence.Repositories
{
    public sealed class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly AppDbContext _context;

        public FuncionarioRepository(AppDbContext context)
            => _context = context;

        /// <inheritdoc/>
        public Task<Funcionario?> ObterPorEmailAsync(string email)
            => _context.Funcionarios
                       .AsNoTracking()
                       .FirstOrDefaultAsync(f => f.Email == email.ToLowerInvariant() && f.Ativo);
    }
}