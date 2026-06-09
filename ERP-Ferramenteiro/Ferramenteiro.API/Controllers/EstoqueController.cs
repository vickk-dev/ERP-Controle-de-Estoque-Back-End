using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ERP_Ferramenteiro.Application.DTOs;
using ERP_Ferramenteiro.Application.Services;

namespace ERP_Ferramenteiro.API.Controllers
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