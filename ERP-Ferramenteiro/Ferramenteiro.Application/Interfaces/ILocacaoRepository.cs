using ERP_Ferramenteiro.Domain.Entities;
using ERP_Ferramenteiro.Ferramenteiro.API.DTOs;

namespace ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<IEnumerable<Locacao>> ObterLocacoesAtivasAsync(CancellationToken cancellationToken);
        Task<Locacao?> ObterLocacaoPorIdComVinculosAsync(Guid id, CancellationToken cancellationToken);
    }
}