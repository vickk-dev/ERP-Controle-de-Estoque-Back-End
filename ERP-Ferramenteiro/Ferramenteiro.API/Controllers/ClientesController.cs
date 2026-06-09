using Ferramenteiro.Application.DTOs;
using Ferramenteiro.Application.UseCases.Clientes;
using Ferramenteiro.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ferramenteiro.API.Controllers;

[ApiController]
[Route("api/v1/clientes")]
public class ClientesController : ControllerBase
{
    private readonly CriarClienteUseCase _criarClienteUseCase;

    public ClientesController(CriarClienteUseCase criarClienteUseCase)
    {
        _criarClienteUseCase = criarClienteUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CriarCliente(
        [FromBody] CriarClienteDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var cliente = await _criarClienteUseCase.ExecutarAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(CriarCliente), new { id = cliente.Id }, cliente);
        }
        catch (DocumentoDuplicadoException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
    }
}
