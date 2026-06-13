//using Ferramenteiro.Application.DTOs;
//using Ferramenteiro.Application.Interfaces;
//using Ferramenteiro.Domain.Entities;
//using Ferramenteiro.Domain.Enums;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Ferramenteiro.Ferramenteiro.Application.Services;

//public class CriarClienteService
//{
//    private readonly IClienteRepository _clienteRepository;

//    public CriarClienteService(IClienteRepository clienteRepository)
//    {
//        _clienteRepository = clienteRepository;
//    }

//    public async Task<Cliente> ExecutarAsync(CriarClienteDto dto, CancellationToken cancellationToken = default)
//    {
//        bool documentoExiste = await _clienteRepository.ExistePorDocumentoAsync(dto.Documento, cancellationToken);
//        if (documentoExiste)
//            throw new DocumentoDuplicadoException($"O documento '{dto.Documento}' já está cadastrado.");

//        if (!Enum.TryParse<TipoDocumento>(dto.TipoDocumento, ignoreCase: true, out var tipoDocumento))
//            throw new ArgumentException($"TipoDocumento inválido: '{dto.TipoDocumento}'. Use 'CPF' ou 'CNPJ'.");

//        var cliente = new Cliente(
//            tipoDocumento,
//            dto.Documento,
//            dto.NomeRazaoSocial,
//            dto.EnderecoCompleto,
//            dto.Telefone
//        );

//        await _clienteRepository.AdicionarAsync(cliente, cancellationToken);

//        return cliente;
//    }
//}