using Ferramenteiro.Domain.Entities;
using Ferramenteiro.API.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ferramenteiro.Application.Interfaces
{
    public interface IClienteService
    {
        Task<Guid> CadastrarAsync(CadastrarClienteRequest request, CancellationToken cancellationToken);
    
    }       
}