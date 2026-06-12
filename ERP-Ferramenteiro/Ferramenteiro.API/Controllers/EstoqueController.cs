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

            await _estoqueService.CadastrarCatalogoFisicoAsync(dto);
            return StatusCode(201, new { mensagem = "Catálogo e itens físicos inseridos com sucesso." });
        }
    }
}