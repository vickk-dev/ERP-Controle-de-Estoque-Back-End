using System;
using System.Collections.Generic;
using ERP_Ferramenteiro.Domain.Enums;

namespace ERP_Ferramenteiro.Domain.Entities
{
    public class Locacao
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid FuncionarioId { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFimPrevista { get; private set; }
        public DateTime? DataDevolucaoReal { get; private set; }
        public StatusLocacao Status { get; private set; }
        public decimal ValorTotal { get; private set; }

        public virtual Cliente Cliente { get; private set; }
        public virtual Funcionario Funcionario { get; private set; }
        public virtual Faturamento Faturamento { get; private set; }

        private readonly List<LocacaoItem> _itens = new();
        public virtual IReadOnlyCollection<LocacaoItem> Itens => _itens.AsReadOnly();

        private Locacao() { }

        public Locacao(Guid clienteId, Guid funcionarioId, DateTime dataFimPrevista)
        {
            if (dataFimPrevista <= DateTime.UtcNow)
                throw new ArgumentException("A data de devolução prevista deve ser no futuro.");

            Id = Guid.NewGuid();
            ClienteId = clienteId;
            FuncionarioId = funcionarioId;
            DataInicio = DateTime.UtcNow;
            DataFimPrevista = dataFimPrevista;
            Status = StatusLocacao.Aberta;
            ValorTotal = 0;
        }

        public void AdicionarItem(Ferramenta ferramenta, TipoCobranca tipoCobranca, int quantidadePeriodo)
        {
            if (ferramenta.Status != StatusFerramenta.Disponivel)
                throw new InvalidOperationException($"A ferramenta {ferramenta.Nome} não está disponível.");

            decimal valorAplicado = tipoCobranca switch
            {
                TipoCobranca.Hora => ferramenta.ValorHora ?? throw new InvalidOperationException("Ferramenta não tem valor por hora."),
                TipoCobranca.Dia => ferramenta.ValorDia,
                TipoCobranca.Semana => ferramenta.ValorSemana,
                TipoCobranca.Mes => ferramenta.ValorMes,
                _ => throw new ArgumentOutOfRangeException()
            };

            var item = new LocacaoItem(this.Id, ferramenta.Id, tipoCobranca, quantidadePeriodo, valorAplicado);
            _itens.Add(item);

            ValorTotal += (valorAplicado * quantidadePeriodo);
            ferramenta.AlterarStatus(StatusFerramenta.Alugada);
        }

        public void EncerrarLocacao()
        {
            Status = StatusLocacao.Concluida;
            DataDevolucaoReal = DateTime.UtcNow;
        }
    }
}