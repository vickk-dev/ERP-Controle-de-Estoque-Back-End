namespace Ferramenteiro.API.DTOs;

/// <summary>
/// Response do relatório de faturamento por período.
/// </summary>
public class FaturamentoResponseDto
{
    public DateOnly PeriodoInicio { get; set; }
    public DateOnly PeriodoFim { get; set; }
    public decimal TotalFaturado { get; set; }
    public List<ContratoFaturamentoDto> Contratos { get; set; } = [];
}

/// <summary>
/// Item individual do relatório — apenas contratos Finalizados.
/// </summary>
public class ContratoFaturamentoDto
{
    public int ContratoId { get; set; }
    public string ClienteNome { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
    public DateTime DataDevolucao { get; set; }
}