using ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces;
using System.Net.Http.Json;

namespace ERP_Ferramenteiro.Ferramenteiro.Application.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        
        public async Task<EnderecoViaDto> BuscarEnderecoPorCepAsync(string cep,CancellationToken cancellationToken)
        {
            
            var ceplimpo = cep.Replace("-", "").Trim();

            var endereco = await _httpClient.GetFromJsonAsync<EnderecoViaDto>($"https://viacep.com.br/ws/{cep}/json/", cancellationToken);
            
            if (endereco != null && endereco.Erro)
            {
                return null;
            }
            return endereco;
        }

       
    } 
}
