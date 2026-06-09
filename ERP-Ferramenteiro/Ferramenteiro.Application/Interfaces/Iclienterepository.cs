using Ferramenteiro.Domain.Entities;

namespace Ferramenteiro.Application.Interfaces;

public interface IClienteRepository
{
    Task<bool> DocumentoJaExisteAsync(string documento, CancellationToken cancellationToken = default);
    Task<Cliente> AdicionarAsync(Cliente cliente, CancellationToken cancellationToken = default);
}