using ERP_Ferramenteiro.Domain.Entities;
using ERP_Ferramenteiro.Ferramenteiro.API.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces
{
    public interface IClienteService
    {
        Task<Guid> CadastrarAsync(CadastrarClienteRequest request, CancellationToken cancellationToken);
    
    }       
}