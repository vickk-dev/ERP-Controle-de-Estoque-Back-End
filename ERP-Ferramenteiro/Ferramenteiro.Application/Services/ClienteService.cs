
using ERP_Ferramenteiro.Domain.Entities;
using ERP_Ferramenteiro.Ferramenteiro.API.DTOs;
using ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces;


namespace ERP_Ferramenteiro.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IViaCepService _viaCepService;

        public ClienteService(IClienteRepository clienteRepository, IViaCepService viaCepService)
        {
            _clienteRepository = clienteRepository;
            _viaCepService = viaCepService;
        }

        public async Task<Guid> CadastrarAsync(CadastrarClienteRequest request, CancellationToken cancellationToken)
        {
            var documentoLimpo = request.Documento.Replace(".", "").Replace("-", "").Replace("/", "");

            var existe = await _clienteRepository.ExisteDocumentoAsync(documentoLimpo, cancellationToken);
            if (existe)
            {
                throw new InvalidOperationException("Já existe um cliente com esse documento.");
            }

            var endereco = await _viaCepService.BuscarEnderecoPorCepAsync(request.Cep, cancellationToken);
            if (endereco == null)
            {
                throw new ArgumentException("CEP inválido ou inexistente na base dos Correios.");
            }

         var cliente = new Cliente(
                tipo: request.TipoDocumento,
                documento: documentoLimpo,
                nomeRazaoSocial: request.NomeRazaoSocial, 
                nomeFantasia: request.NomeFantasia,
                telefone: request.Telefone,
                logradouro: endereco.Logradouro,
                numero: request.Numero,
                bairro: endereco.Bairro,
                cidade: endereco.Cidade,
                estado: endereco.Estado,
                cep: request.Cep
            );

          
            await _clienteRepository.AdicionarAsync(cliente, cancellationToken);

            return cliente.Id;
        }
    }

   
}