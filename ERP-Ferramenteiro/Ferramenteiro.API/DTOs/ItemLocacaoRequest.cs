using System;
using ERP_Ferramenteiro.Domain.Enums;

namespace ERP_Ferramenteiro.Ferramenteiro.API.DTOs
{
    public record ItemLocacaoRequest(
        Guid FerramentaId,
        TipoCobranca TipoCobranca,
        int QuantidadePeriodo
    );
}