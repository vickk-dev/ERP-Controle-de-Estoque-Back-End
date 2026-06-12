using Ferramenteiro.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ferramenteiro.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/contratos")]
    public class LocacoesController : ControllerBase
    {
          private readonly ILocacaoService _locacaoService;

            public LocacoesController(ILocacaoService locacaoService)
            {
                _locacaoService = locacaoService;
            }

            [HttpGet("ativos")]
            public async Task<IActionResult> ObterAtivos(CancellationToken cancellationToken)
            {
                var painel = await _locacaoService.ObterPainelAtivosAsync(cancellationToken);
                return Ok(painel);
            }
        }
}
