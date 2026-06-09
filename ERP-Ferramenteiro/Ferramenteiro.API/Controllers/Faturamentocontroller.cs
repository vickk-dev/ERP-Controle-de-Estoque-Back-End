using Microsoft.AspNetCore.Mvc;
using Ferramenteiro.API.DTOs;
using Ferramenteiro.Application.Services;

namespace Ferramenteiro.API.Controllers;

[ApiController]
[Route("api/v1/relatorios")]
[Produces("application/json")]
public class FaturamentoController : ControllerBase
{
    private readonly IFaturamentoService _faturamentoService;

    public FaturamentoController(IFaturamentoService faturamentoService)
    {
        _faturamentoService = faturamentoService;
    }

    /// <summary>
    /// Retorna o faturamento realizado (contratos Finalizados) dentro do período informado.
    /// </summary>
    /// <param name="dataInicio">Data de início do período (yyyy-MM-dd). Obrigatório.</param>
    /// <param name="dataFim">Data de fim do período (yyyy-MM-dd). Obrigatório.</param>
    /// <returns>Relatório com total faturado e lista de contratos encerrados.</returns>
    /// <response code="200">Relatório gerado com sucesso.</response>
    /// <response code="400">Parâmetros inválidos ou dataInicio maior que dataFim.</response>
    [HttpGet("faturamento")]
    [ProducesResponseType(typeof(FaturamentoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFaturamento(
        [FromQuery] DateOnly? dataInicio,
        [FromQuery] DateOnly? dataFim)
    {
        // ── Validação: campos obrigatórios ────────────────────────────────────
        if (dataInicio is null || dataFim is null)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Parâmetros obrigatórios ausentes.",
                Detail = "Os query parameters 'dataInicio' e 'dataFim' são obrigatórios " +
                         "e devem estar no formato yyyy-MM-dd.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        // ── Validação: intervalo lógico ───────────────────────────────────────
        if (dataInicio > dataFim)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Intervalo de datas inválido.",
                Detail = $"'dataInicio' ({dataInicio}) não pode ser maior que 'dataFim' ({dataFim}).",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var resultado = await _faturamentoService.ObterFaturamentoAsync(
            dataInicio.Value,
            dataFim.Value);

        return Ok(resultado);
    }
}