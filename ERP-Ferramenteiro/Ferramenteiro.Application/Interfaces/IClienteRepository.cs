using System.Threading;
using System.Threading.Tasks;
using Ferramenteiro.Domain.Entities;

namespace Ferramenteiro.Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<bool> ExistePorDocumentoAsync(string documentoLimpo, CancellationToken cancellationToken);
        Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken);
    }
}