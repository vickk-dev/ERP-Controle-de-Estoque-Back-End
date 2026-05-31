namespace ERP_Ferramenteiro.API.DTOs
{
    /// <summary>
    /// Payload de saída após autenticação bem-sucedida.
    /// Nenhuma senha é exposta neste objeto.
    /// </summary>
    public sealed class LoginResponseDto
    {
        public string Token { get; init; } = string.Empty;
        public string NomeUsuario { get; init; } = string.Empty;
    }
}