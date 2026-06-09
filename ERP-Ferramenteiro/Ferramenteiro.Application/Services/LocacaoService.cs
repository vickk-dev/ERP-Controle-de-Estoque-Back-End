using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERP_Ferramenteiro.Ferramenteiro.API.DTOs;
using ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces;

namespace ERP_Ferramenteiro.Ferramenteiro.Application.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;

        public LocacaoService(ILocacaoRepository locacaoRepository)
        {
            _locacaoRepository = locacaoRepository;
        }

        public async Task<IEnumerable<LocacaoAtivaResponse>> ObterPainelAtivosAsync(CancellationToken cancellationToken)
        {
            var locacoes = await _locacaoRepository.ObterLocacoesAtivasAsync(cancellationToken);

            return locacoes.Select(l =>
            {
                var primeiroItem = l.Itens.FirstOrDefault();

                return new LocacaoAtivaResponse(
                    LocacaoId: l.Id,
                    ClienteNome: l.Cliente.NomeRazaoSocial,
                    MaquinaModelo: primeiroItem?.Ferramenta.Nome ?? "N/A",
                    CodigoPatrimonio: primeiroItem?.Ferramenta.PatrimonioId ?? "N/A",
                    DataPrevistaDevolucao: l.DataFimPrevista,
                    ValorTotal: l.Faturamento?.ValorFaturado ?? 0m
                );
            });
        }

        public async Task RegistrarDevolucaoAsync(Guid locacaoId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}