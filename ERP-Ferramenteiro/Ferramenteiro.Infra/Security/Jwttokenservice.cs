using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ERP_Ferramenteiro.Domain.Entities;
using ERP_Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Application.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ERP_Ferramenteiro.Infra.Security
{
    public sealed class JwtSettings
    {
        public string SecretKey { get; init; } = string.Empty;
        public string Issuer { get; init; } = string.Empty;
        public string Audience { get; init; } = string.Empty;
        public int ExpiracaoMinutos { get; init; } = 60;
    }

    public sealed class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _settings;

        public JwtTokenService(IOptions<JwtSettings> options)
            => _settings = options.Value;

        public string GerarToken(Funcionario funcionario)
        {
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub,   funcionario.Id.ToString()),
                new(JwtRegisteredClaimNames.Name,  funcionario.Nome),
                new(JwtRegisteredClaimNames.Email, funcionario.Email),
                new("matricula",                   funcionario.Matricula),
                new(ClaimTypes.Role,               "OperadorBalcao"),
                new(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiracaoMinutos),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}