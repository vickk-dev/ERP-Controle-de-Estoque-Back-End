using System;
using Ferramenteiro.Domain.Enums;

namespace Ferramenteiro.Domain.Entities
{
    public class LocacaoItem
    {
        public Guid Id { get; private set; }
        public Guid LocacaoId { get; private set; }
        public Guid FerramentaId { get; private set; }
        public TipoCobranca TipoCobranca { get; private set; }
        public int QuantidadePeriodo { get; private set; }
        public decimal ValorUnitarioAplicado { get; private set; } // Snapshot de preço

        public virtual Locacao Locacao { get; private set; }
        public virtual Ferramenta Ferramenta { get; private set; }

        private LocacaoItem() { }

        internal LocacaoItem(Guid locacaoId, Guid ferramentaId, TipoCobranca tipoCobranca, int quantidadePeriodo, decimal valorUnitarioAplicado)
        {
            Id = Guid.NewGuid();
            LocacaoId = locacaoId;
            FerramentaId = ferramentaId;
            TipoCobranca = tipoCobranca;
            QuantidadePeriodo = quantidadePeriodo;
            ValorUnitarioAplicado = valorUnitarioAplicado;
        }
    }
}

