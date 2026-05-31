using System;
using System.Threading.Tasks;
using ERP_Ferramenteiro.API.DTOs;
using ERP_Ferramenteiro.Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ERP_Ferramenteiro.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public sealed class AuthController : ControllerBase
    {
        private readonly AuthenticateUseCase _useCase;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthenticateUseCase useCase, ILogger<AuthController> logger)
        {
            _useCase = useCase;
            _logger  = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = await _useCase.ExecutarAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Tentativa de login mal-sucedida para o e-mail {Email}: {Mensagem}",
                    request.Email, ex.Message);

                return Unauthorized(new { mensagem = "Credenciais inválidas." });
            }
        }
    }
}