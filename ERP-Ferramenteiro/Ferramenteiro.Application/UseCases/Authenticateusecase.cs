using System;
using System.Threading.Tasks;
using ERP_Ferramenteiro.API.DTOs;
using ERP_Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Application.Services;
using ERP_Ferramenteiro.Domain.Entities;

namespace ERP_Ferramenteiro.Application.UseCases
{
    public sealed class AuthenticateUseCase
    {
        private readonly IFuncionarioRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtService;

        public AuthenticateUseCase(
            IFuncionarioRepository repository,
            IPasswordHasher passwordHasher,
            IJwtTokenService jwtService)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> ExecutarAsync(LoginRequestDto request)
        {
            var funcionario = await _repository.ObterPorEmailAsync(request.Email);

            
            if (funcionario != null)
            {
                var verificou = _passwordHasher.Verify(request.Senha, funcionario.SenhaHash);
                Console.WriteLine($">>> BCRYPT VERIFY: {verificou}");
            }

            if (funcionario is null || !funcionario.Ativo
                || !_passwordHasher.Verify(request.Senha, funcionario.SenhaHash))
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            var token = _jwtService.GerarToken(funcionario);
            return new LoginResponseDto { Token = token, NomeUsuario = funcionario.Nome };
        }
    }
}