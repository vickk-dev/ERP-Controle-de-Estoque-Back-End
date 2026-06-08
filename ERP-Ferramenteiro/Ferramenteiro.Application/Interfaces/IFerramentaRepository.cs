using ERP_Ferramenteiro.Domain.Entities;

namespace ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces
{
    public interface IFerramentaRepository
    {
        Task<Ferramenta?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);

        Task AdicionarAsync(Ferramenta ferramenta, CancellationToken cancellationToken);
    }
}
