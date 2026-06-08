using ERP_Ferramenteiro.Domain.Enums;

namespace ERP_Ferramenteiro.Domain.Entities
{
	public class Ferramenta
	{
		public Guid Id { get; private set; }
		public string PatrimonioId { get; private set; }
		public string Nome { get; private set; }
		public string TipoTamanho { get; private set; }
		public StatusFerramenta Status { get; private set; }
		public decimal? ValorHora { get; private set; }
		public decimal ValorDia { get; private set; }
		public decimal ValorSemana { get; private set; }
		public decimal ValorMes { get; private set; }

		private Ferramenta() { }

		public Ferramenta(string patrimonioId, string nome, string tipoTamanho,
						  decimal valorDia, decimal valorSemana, decimal valorMes, decimal? valorHora = null)
		{
			if (string.IsNullOrWhiteSpace(patrimonioId)) throw new ArgumentException("Patrimônio é obrigatório.");

			
			if (tipoTamanho.Equals("Maquinario Pesado", StringComparison.OrdinalIgnoreCase) && valorHora.HasValue)
				throw new InvalidOperationException("Maquinário pesado não pode ter cobrança por hora.");

			Id = Guid.NewGuid();
			PatrimonioId = patrimonioId;
			Nome = nome;
			TipoTamanho = tipoTamanho;
			Status = StatusFerramenta.Disponivel; 
			ValorHora = valorHora;
			ValorDia = valorDia;
			ValorSemana = valorSemana;
			ValorMes = valorMes;
		}

		public void AlterarStatus(StatusFerramenta novoStatus) => Status = novoStatus;
	}
}