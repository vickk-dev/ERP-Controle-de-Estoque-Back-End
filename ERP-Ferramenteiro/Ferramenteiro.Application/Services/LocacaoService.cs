using Ferramenteiro.API.DTOs;
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Domain.Entities;
using Ferramenteiro.Infra.Persistence;
using Ferramenteiro.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ferramenteiro.Application.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IFerramentaRepository _ferramentaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LocacaoService(
            ILocacaoRepository locacaoRepository,
            IFerramentaRepository ferramentaRepository,
            IUnitOfWork unitOfWork)
        {
            _locacaoRepository = locacaoRepository;
            _ferramentaRepository = ferramentaRepository;
            _unitOfWork = unitOfWork;
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
        public async Task<Guid> AbrirLocacaoAsync(AbrirLocacaoRequest request, CancellationToken cancellationToken)
        {
            var locacao = new Locacao(
                clienteId: request.ClienteId,
                dataFimPrevista: request.DataFimPrevista
            );

            foreach (var itemDto in request.Itens)
            {
                var ferramenta = await _ferramentaRepository.ObterPorIdAsync(itemDto.FerramentaId, cancellationToken);

                if (ferramenta == null)
                {
                    throw new KeyNotFoundException($"A ferramenta com ID {itemDto.FerramentaId} não foi encontrada.");
                }

                locacao.AdicionarItem(ferramenta, itemDto.TipoCobranca, itemDto.QuantidadePeriodo);
            }

            await _locacaoRepository.AdicionarAsync(locacao, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return locacao.Id;
        }
    }
}
