using ERP_Ferramenteiro.Domain.Entities;

namespace ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces
{
    public interface IClienteRepository
    {
        Task<bool> ExisteDocumentoAsync(string documentoLimpo, CancellationToken cancellationToken);
        Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken);


    }
}