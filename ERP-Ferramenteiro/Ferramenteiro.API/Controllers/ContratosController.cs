
using global::Ferramenteiro.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ferramenteiro.API.Controllers;


[ApiController]
[Route("api/v1/contratos")]
public class ContratosController : ControllerBase
{
    private readonly ILocacaoService _locacaoService;

    public ContratosController(ILocacaoService locacaoService)
    {
        _locacaoService = locacaoService;
    }

    [HttpPatch("{id:guid}/devolucao")]
    public async Task<IActionResult> RegistrarDevolucao(
        [FromRoute] Guid id, // Capturando direto da URL
        CancellationToken cancellationToken)
    {
        await _locacaoService.RegistrarDevolucaoAsync(id, cancellationToken);

        return Ok(new { mensagem = "Devolução registrada e contrato encerrado com sucesso." });
    }
}