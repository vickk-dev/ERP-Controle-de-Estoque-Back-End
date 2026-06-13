using Ferramenteiro.Domain.Entities;

namespace Ferramenteiro.Application.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<IEnumerable<Locacao>> ObterLocacoesAtivasAsync(CancellationToken cancellationToken);
        Task<Locacao?> ObterLocacaoPorIdComVinculosAsync(Guid id, CancellationToken cancellationToken);
        Task AdicionarAsync(Locacao locacao, CancellationToken cancellationToken);
    }
}