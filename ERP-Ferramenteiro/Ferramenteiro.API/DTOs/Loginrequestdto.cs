using System.ComponentModel.DataAnnotations;

namespace ERP_Ferramenteiro.API.DTOs
{
    public sealed class LoginRequestDto
    {
        [Required(ErrorMessage = "O campo 'email' é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo 'email' deve ter um formato válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo 'senha' é obrigatório.")]
        public string Senha { get; set; } = string.Empty;
    }
}