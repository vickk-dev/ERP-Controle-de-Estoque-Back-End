using Ferramenteiro.API.DTOs; 


namespace Ferramenteiro.Application.Interfaces
{
    public interface ILocacaoService
    {
        Task<IEnumerable<LocacaoAtivaResponse>> ObterPainelAtivosAsync(CancellationToken cancellationToken);
        Task RegistrarDevolucaoAsync(Guid locacaoId, CancellationToken cancellationToken);
        Task<Guid> AbrirLocacaoAsync(AbrirLocacaoRequest request, CancellationToken cancellationToken);

    }
}