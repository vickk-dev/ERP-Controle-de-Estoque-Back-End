namespace Ferramenteiro.Application.DTOs;

public class CriarClienteDto
{
    public string TipoDocumento { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string NomeRazaoSocial { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string EnderecoCompleto { get; set; } = string.Empty;
}