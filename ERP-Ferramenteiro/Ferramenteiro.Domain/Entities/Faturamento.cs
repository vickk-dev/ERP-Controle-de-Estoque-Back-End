using Ferramenteiro.Domain.Enums;

namespace Ferramenteiro.Domain.Entities
{
    public class Faturamento
    {
        public Guid Id { get; private set; }
        public Guid LocacaoId { get; private set; }
        public StatusPagamento StatusPagamento { get; private set; }
        public string? MetodoPagamento { get; private set; }
        public decimal ValorFaturado { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public DateTime? DataPagamento { get; private set; }

        public virtual Locacao Locacao { get; private set; }

        private Faturamento() { }

        public Faturamento(Guid locacaoId, decimal valorFaturado, DateTime dataVencimento)
        {
            Id = Guid.NewGuid();
            LocacaoId = locacaoId;
            ValorFaturado = valorFaturado;
            DataVencimento = dataVencimento;
            StatusPagamento = StatusPagamento.Pendente;
        }

        public void RegistrarPagamento(string metodoPagamento)
        {
            if (StatusPagamento == StatusPagamento.Pago)
                throw new InvalidOperationException("Fatura já está paga.");

            StatusPagamento = StatusPagamento.Pago;
            MetodoPagamento = metodoPagamento;
            DataPagamento = DateTime.UtcNow;
        }
    }
}
