using ERP_Ferramenteiro.Domain.Entities;

namespace ERP_Ferramenteiro.Application.Services
{

    public interface IJwtTokenService
    {
        string GerarToken(Funcionario funcionario);
    }
}