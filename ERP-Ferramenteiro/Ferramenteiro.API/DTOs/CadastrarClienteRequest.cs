namespace ERP_Ferramenteiro.Ferramenteiro.API.DTOs
{
    public class CadastrarClienteRequest
    {
        public required string TipoDocumento { get; init; }
        public required string Documento { get; init; }
        public required string NomeRazaoSocial { get; init; }
        public required string Telefone { get; init; }
        public required string EnderecoCompleto { get; init; }
        public string Cep { get; internal set; }
        public string Numero { get; internal set; }
        public string? NomeFantasia { get; internal set; }
    }
}
