
using Ferramenteiro.API.DTOs;
using Ferramenteiro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ferramenteiro.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] 
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(
            [FromBody] CadastrarClienteRequest request,
            CancellationToken cancellationToken)
        {
            var clienteId = await _clienteService.CadastrarAsync(request, cancellationToken);
            return Created($"/api/v1/clientes/{clienteId}", new { id = clienteId });
        }
    }
}