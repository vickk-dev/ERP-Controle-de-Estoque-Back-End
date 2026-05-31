using System;
using System.Threading.Tasks;
using ERP_Ferramenteiro.API.DTOs;
using ERP_Ferramenteiro.Application.UseCases;
using ERP_Ferramenteiro.Domain.Entities;
using ERP_Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Application.Services;
using Moq;
using Xunit;

namespace ERP_Ferramenteiro.Tests.Application
{
	public class AuthenticateUseCaseTests
	{
		// ── helpers ──────────────────────────────────────────────────────────
		private static Funcionario CriarFuncionario(string email = "operador@oferramenteiro.com")
			=> new(matricula: "MAT001", nome: "João Silva", email: email, senhaHash: "hash_valido");

		private static (AuthenticateUseCase useCase,
						Mock<IFuncionarioRepository> repoMock,
						Mock<IPasswordHasher> hasherMock,
						Mock<IJwtTokenService> jwtMock)
			CriarSut()
		{
			var repo = new Mock<IFuncionarioRepository>();
			var hasher = new Mock<IPasswordHasher>();
			var jwt = new Mock<IJwtTokenService>();
			var sut = new AuthenticateUseCase(repo.Object, hasher.Object, jwt.Object);
			return (sut, repo, hasher, jwt);
		}

		// ── Cenário 1: Login com Credenciais Válidas ──────────────────────────
		[Fact(DisplayName = "Cenário 1 – Credenciais válidas retornam token e nome do usuário")]
		public async Task ExecutarAsync_CredenciaisValidas_RetornaTokenENome()
		{
			// Arrange
			var (sut, repo, hasher, jwt) = CriarSut();
			var funcionario = CriarFuncionario();
			const string tokenEsperado = "eyJhbGciOiJIUzI1NiIsIn...";

			repo.Setup(r => r.ObterPorEmailAsync(funcionario.Email))
				.ReturnsAsync(funcionario);
			hasher.Setup(h => h.Verify("senha_secreta", funcionario.SenhaHash))
				  .Returns(true);
			jwt.Setup(j => j.GerarToken(funcionario))
			   .Returns(tokenEsperado);

			var request = new LoginRequestDto
			{ Email = funcionario.Email, Senha = "senha_secreta" };

			// Act
			var response = await sut.ExecutarAsync(request);

			// Assert
			Assert.Equal(tokenEsperado, response.Token);
			Assert.Equal(funcionario.Nome, response.NomeUsuario);
		}

		// ── Cenário 2a: Usuário não existe ────────────────────────────────────
		[Fact(DisplayName = "Cenário 2a – Usuário inexistente lança UnauthorizedAccessException")]
		public async Task ExecutarAsync_UsuarioInexistente_LancaUnauthorized()
		{
			// Arrange
			var (sut, repo, _, _) = CriarSut();
			repo.Setup(r => r.ObterPorEmailAsync(It.IsAny<string>()))
				.ReturnsAsync((Funcionario?)null);

			var request = new LoginRequestDto
			{ Email = "naoexiste@test.com", Senha = "qualquer" };

			// Act & Assert
			await Assert.ThrowsAsync<UnauthorizedAccessException>(
				() => sut.ExecutarAsync(request));
		}

		// ── Cenário 2b: Senha incorreta ───────────────────────────────────────
		[Fact(DisplayName = "Cenário 2b – Senha incorreta lança UnauthorizedAccessException")]
		public async Task ExecutarAsync_SenhaIncorreta_LancaUnauthorized()
		{
			// Arrange
			var (sut, repo, hasher, _) = CriarSut();
			var funcionario = CriarFuncionario();

			repo.Setup(r => r.ObterPorEmailAsync(funcionario.Email))
				.ReturnsAsync(funcionario);
			hasher.Setup(h => h.Verify(It.IsAny<string>(), funcionario.SenhaHash))
				  .Returns(false);

			var request = new LoginRequestDto
			{ Email = funcionario.Email, Senha = "senha_errada" };

			// Act & Assert
			await Assert.ThrowsAsync<UnauthorizedAccessException>(
				() => sut.ExecutarAsync(request));
		}

		// ── Cenário 2c: Funcionário inativo ───────────────────────────────────
		[Fact(DisplayName = "Cenário 2c – Funcionário inativo lança UnauthorizedAccessException")]
		public async Task ExecutarAsync_FuncionarioInativo_LancaUnauthorized()
		{
			// Arrange
			var (sut, repo, _, _) = CriarSut();
			var funcionario = CriarFuncionario();
			funcionario.Inativar();

			repo.Setup(r => r.ObterPorEmailAsync(funcionario.Email))
				.ReturnsAsync(funcionario);

			var request = new LoginRequestDto
			{ Email = funcionario.Email, Senha = "senha_secreta" };

			// Act & Assert
			await Assert.ThrowsAsync<UnauthorizedAccessException>(
				() => sut.ExecutarAsync(request));
		}

		// ── Garante que o token nunca é gerado em falha de autenticação ───────
		[Fact(DisplayName = "Segurança – JWT não é gerado quando autenticação falha")]
		public async Task ExecutarAsync_FalhaAutenticacao_NaoGeraToken()
		{
			// Arrange
			var (sut, repo, hasher, jwt) = CriarSut();
			repo.Setup(r => r.ObterPorEmailAsync(It.IsAny<string>()))
				.ReturnsAsync((Funcionario?)null);

			var request = new LoginRequestDto
			{ Email = "x@x.com", Senha = "x" };

			// Act
			try { await sut.ExecutarAsync(request); } catch { /* esperado */ }

			// Assert – GerarToken nunca deve ter sido chamado
			jwt.Verify(j => j.GerarToken(It.IsAny<Funcionario>()), Times.Never);
		}
	}
}