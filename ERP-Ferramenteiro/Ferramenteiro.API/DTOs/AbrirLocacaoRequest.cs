namespace Ferramenteiro.API.DTOs
{
    public record AbrirLocacaoRequest(
     Guid ClienteId,
     DateTime DataFimPrevista,
     List<ItemLocacaoRequest> Itens 
 );
}
