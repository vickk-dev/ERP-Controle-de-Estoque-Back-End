using ERP_Ferramenteiro.Ferramenteiro.API.DTOs;

namespace ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces
{
    public interface ILocacaoService
    {
        Task<IEnumerable<LocacaoAtivaResponse>> ObterPainelAtivosAsync(CancellationToken cancellationToken);
        Task RegistrarDevolucaoAsync(Guid locacaoId, CancellationToken cancellationToken);

    }
}
