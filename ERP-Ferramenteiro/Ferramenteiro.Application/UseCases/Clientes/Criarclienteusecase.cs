using Ferramenteiro.Application.DTOs;
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Domain.Entities;
using Ferramenteiro.Domain.Enums;

namespace Ferramenteiro.Application.UseCases.Clientes;

public class CriarClienteUseCase
{
    private readonly IClienteRepository _clienteRepository;

    public CriarClienteUseCase(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Cliente> ExecutarAsync(CriarClienteDto dto, CancellationToken cancellationToken = default)
    {
        bool documentoExiste = await _clienteRepository.DocumentoJaExisteAsync(dto.Documento, cancellationToken);
        if (documentoExiste)
            throw new DocumentoDuplicadoException($"O documento '{dto.Documento}' já está cadastrado.");

        if (!Enum.TryParse<TipoDocumento>(dto.TipoDocumento, ignoreCase: true, out var tipoDocumento))
            throw new ArgumentException($"TipoDocumento inválido: '{dto.TipoDocumento}'. Use 'CPF' ou 'CNPJ'.");

        var cliente = new Cliente(
    tipoDocumento,
    dto.Documento,
    dto.NomeRazaoSocial,
    dto.EnderecoCompleto,
    dto.Telefone
); ;

        return await _clienteRepository.AdicionarAsync(cliente, cancellationToken);
    }
}