using System;
using Ferramenteiro.Domain.Enums;

namespace Ferramenteiro.API.DTOs
{
    public record ItemLocacaoRequest(
        Guid FerramentaId,
        TipoCobranca TipoCobranca,
        int QuantidadePeriodo
    );
}