using System.Text.Json.Serialization;

namespace ERP_Ferramenteiro.Ferramenteiro.Application
{
    public class EnderecoViaDto
    {
        

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; init; } = string.Empty;

        [JsonPropertyName("bairro")]
        public string Bairro { get; init; } = string.Empty;
        
        [JsonPropertyName("localidade")]
        public string Cidade { get; init; } = string.Empty;

        [JsonPropertyName("cep")]
        public string Cep { get; init; } = string.Empty;

        [JsonPropertyName("uf")]
        public string Estado { get; init; } = string.Empty;

        [JsonPropertyName("erro")]
        public bool Erro { get; init; }
    }
}
