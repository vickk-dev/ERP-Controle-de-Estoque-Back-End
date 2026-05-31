using System.Threading.Tasks;
using ERP_Ferramenteiro.Domain.Entities;

namespace ERP_Ferramenteiro.Application.Interfaces
{
	
	public interface IFuncionarioRepository
	{

		Task<Funcionario?> ObterPorEmailAsync(string email);
	}
}