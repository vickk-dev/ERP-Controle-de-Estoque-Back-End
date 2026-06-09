using Ferramenteiro.Application.DTOs;
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Application.UseCases.Clientes;
using Ferramenteiro.Domain.Entities;
using Moq;
using Xunit;

namespace Ferramenteiro.Tests.Application;

public class CriarClienteUseCaseTests
{
    private readonly Mock<IClienteRepository> _repoMock = new();
    private readonly CriarClienteUseCase _useCase;

    public CriarClienteUseCaseTests()
    {
        _useCase = new CriarClienteUseCase(_repoMock.Object);
    }

    // Cenário 1 — Cadastro inédito com sucesso → 201
    [Fact]
    public async Task ExecutarAsync_DocumentoNovo_RetornaClientePersistido()
    {
        var dto = new CriarClienteDto
        {
            TipoDocumento = "CPF",
            Documento = "12345678901",
            NomeRazaoSocial = "Ricardo Almeida",
            Telefone = "44999999999",
            EnderecoCompleto = "Rua Exemplo, 123, Maringá-PR"
        };

        _repoMock.Setup(r => r.DocumentoJaExisteAsync(dto.Documento, default))
                 .ReturnsAsync(false);

        _repoMock.Setup(r => r.AdicionarAsync(It.IsAny<Cliente>(), default))
                 .ReturnsAsync((Cliente c, CancellationToken _) => c);

        var resultado = await _useCase.ExecutarAsync(dto);

        Assert.NotNull(resultado);
        Assert.Equal(dto.Documento, resultado.Documento);
        Assert.Equal(dto.EnderecoCompleto, resultado.EnderecoCompleto);
    }

    // Cenário 2 — Documento duplicado → DocumentoDuplicadoException → 409
    [Fact]
    public async Task ExecutarAsync_DocumentoDuplicado_LancaDocumentoDuplicadoException()
    {
        var dto = new CriarClienteDto
        {
            TipoDocumento = "CPF",
            Documento = "12345678901",
            NomeRazaoSocial = "Ricardo Almeida",
            EnderecoCompleto = "Rua Exemplo, 123"
        };

        _repoMock.Setup(r => r.DocumentoJaExisteAsync(dto.Documento, default))
                 .ReturnsAsync(true);

        await Assert.ThrowsAsync<DocumentoDuplicadoException>(
            () => _useCase.ExecutarAsync(dto));

        // Garante que não tentou persistir
        _repoMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>(), default), Times.Never);
    }
}