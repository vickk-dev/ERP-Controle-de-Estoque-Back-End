namespace ERP_Ferramenteiro.Ferramenteiro.API.DTOs;
public record LocacaoAtivaResponse(
    Guid LocacaoId,
    string ClienteNome,
    string MaquinaModelo,
    string CodigoPatrimonio,
    DateTime DataPrevistaDevolucao,
    decimal ValorTotal
);