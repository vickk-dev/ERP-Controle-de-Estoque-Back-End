namespace Ferramenteiro.API.DTOs;

public class FaturamentoResponseDto
{
    public DateOnly PeriodoInicio { get; set; }
    public DateOnly PeriodoFim { get; set; }
    public decimal TotalFaturado { get; set; }
    public List<ContratoFaturamentoDto> Contratos { get; set; } = [];
}

public class ContratoFaturamentoDto
{
    public int ContratoId { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
    public DateTime DataDevolucao { get; set; }
}