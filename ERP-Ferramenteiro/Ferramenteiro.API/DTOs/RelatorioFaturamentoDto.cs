namespace ERP_Ferramenteiro.Application.DTOs
{
    public class RelatorioFaturamentoDto
    {
        public DateOnly PeriodoInicio { get; init; }
        public DateOnly PeriodoFim { get; init; }
        public decimal TotalFaturado { get; init; }
        public List<ContratoFaturadoDto> Contratos { get; init; } = [];
    }

    public class ContratoFaturadoDto
    {
        public Guid ContratoId { get; init; }
        public string ClienteNome { get; init; } = string.Empty;
        public decimal ValorTotal { get; init; }
        public DateTime DataDevolucao { get; init; }
    }
}