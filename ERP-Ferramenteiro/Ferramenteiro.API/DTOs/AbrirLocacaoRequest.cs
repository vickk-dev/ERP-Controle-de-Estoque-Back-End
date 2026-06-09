namespace Ferramenteiro.API.DTOs
{
    public record AbrirLocacaoRequest(
     Guid ClienteId,
     Guid FuncionarioId,
     DateTime DataFimPrevista,
     List<ItemLocacaoRequest> Itens 
 );
}
