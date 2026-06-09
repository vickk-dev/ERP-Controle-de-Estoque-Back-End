using ERP_Ferramenteiro.Application.DTOs;
using ERP_Ferramenteiro.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Ferramenteiro.API.Controllers
{
    [ApiController]
    [Route("api/v1/relatorios")]
    public class RelatoriosController : ControllerBase
    {
        private readonly RelatorioService _relatorioService;

        public RelatoriosController(RelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        /// <summary>
        /// Retorna o faturamento realizado (locações encerradas) dentro do período informado.
        /// </summary>
        [HttpGet("faturamento")]
        public async Task<IActionResult> ObterFaturamento(
            [FromQuery] DateOnly dataInicio,
            [FromQuery] DateOnly dataFim,
            CancellationToken cancellationToken)
        {
            if (dataInicio > dataFim)
                return BadRequest(new { erro = "dataInicio não pode ser posterior a dataFim." });

            var resultado = await _relatorioService
                .ObterFaturamentoPorPeriodoAsync(dataInicio, dataFim, cancellationToken);

            return Ok(resultado);
        }
    }
}