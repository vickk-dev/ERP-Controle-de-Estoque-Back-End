using Ferramenteiro.API.DTOs;
using Ferramenteiro.Application.DTOs; // Os DTOs devem estar aqui
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Domain.Entities; // Apenas para a Exception, se ela estiver aqui
using Microsoft.AspNetCore.Mvc;

namespace Ferramenteiro.API.Controllers;

[ApiController]
[Route("api/v1/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CadastrarCliente(
        [FromBody] CadastrarClienteRequest dto,
        CancellationToken cancellationToken) 
    {
        try
        {
            var clienteId = await _clienteService.CadastrarAsync(dto, cancellationToken);

           
            return Created($"/api/v1/clientes/{clienteId}", new { id = clienteId });
        }
        catch (DocumentoDuplicadoException ex)
        {
            
            return Conflict(new { mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}