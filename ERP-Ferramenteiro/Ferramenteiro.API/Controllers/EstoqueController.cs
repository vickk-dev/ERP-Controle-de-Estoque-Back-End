using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Ferramenteiro.Application.DTOs;
using Ferramenteiro.Application.Services;

namespace Ferramenteiro.API.Controllers
{
    [ApiController]
    [Route("api/v1/estoque")]
    public class EstoqueController : ControllerBase
    {
        private readonly EstoqueService _estoqueService;

        public EstoqueController(EstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        [HttpPost("catalogo")]
        public async Task<IActionResult> CadastrarCatalogo([FromBody] CadastroCatalogoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Captura os IDs que o serviço devolveu
            var ferramentasIds = await _estoqueService.CadastrarCatalogoFisicoAsync(dto);

            // Anexa a lista de IDs no payload da resposta HTTP 201
            return StatusCode(201, new
            {
                mensagem = "Catálogo e itens físicos inseridos com sucesso.",
                idsGerados = ferramentasIds
            });
        }

        [HttpGet] // A rota será GET /api/v1/estoque
        public async Task<IActionResult> ListarEstoque(CancellationToken cancellationToken)
        {
            var estoque = await _estoqueService.ObterEstoqueAgrupadoAsync(cancellationToken);

            return Ok(estoque); // Retorna HTTP 200 com a lista JSON perfeita para o Front
        }

    }
}