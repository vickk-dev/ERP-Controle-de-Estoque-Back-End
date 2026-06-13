using Ferramenteiro.Domain.Entities;

namespace Ferramenteiro.Application.Interfaces
{
    public interface IFerramentaRepository
    {
        Task<Ferramenta?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);

        Task AdicionarAsync(Ferramenta ferramenta, CancellationToken cancellationToken);
    }
}
