namespace Ferramenteiro.Domain.Entities;

/// <summary>
/// Entidade principal de contratos de aluguel/locação.
/// Adapte as propriedades conforme o seu modelo existente.
/// </summary>
public class Contrato
{
    public int Id { get; set; }

    public string ClienteNome { get; set; } = string.Empty;

    public decimal ValorTotal { get; set; }

    /// <summary>
    /// Status do contrato: "Ativo" | "Finalizado"
    /// Use enum se preferir — ver nota abaixo.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Data em que a máquina foi devolvida e o contrato encerrado.
    /// Null enquanto o contrato ainda está ativo.
    /// </summary>
    public DateTime? DataDevolucao { get; set; }
}

/* -------------------------------------------------------
 * DICA: substitua a string Status por um enum, ex:
 *
 *   public enum StatusContrato { Ativo, Finalizado }
 *   public StatusContrato Status { get; set; }
 *
 * e ajuste a query no Service de:
 *   c.Status == "Finalizado"
 * para:
 *   c.Status == StatusContrato.Finalizado
 * ------------------------------------------------------- */