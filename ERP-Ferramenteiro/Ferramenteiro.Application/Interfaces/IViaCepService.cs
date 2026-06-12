namespace Ferramenteiro.Application.Interfaces
{
    public interface IViaCepService
    {
        Task<EnderecoViaDto> BuscarEnderecoPorCepAsync(string cep, CancellationToken cancellationToken);
      
    }
}
