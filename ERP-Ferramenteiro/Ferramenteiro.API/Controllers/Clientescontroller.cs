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

    /// <summary>
    /// Cadastra um novo cliente garantindo unicidade de CPF/CNPJ.
    /// </summary>
    /// <response code="201">Cliente cadastrado com sucesso.</response>
    /// <response code="400">Falha de validação de campos obrigatórios.</response>
    /// <response code="409">Documento (CPF/CNPJ) já cadastrado.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CriarCliente(
        [FromBody] CriarClienteDto dto,
        CancellationToken cancellationToken)
    {
        // Validação de campos (400) é feita pelo FluentValidation no pipeline
        try
        {
            var cliente = await _criarClienteUseCase.ExecutarAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(CriarCliente), new { id = cliente.Id }, cliente);
        }
        catch (DocumentoDuplicadoException ex)
        {
            // RN01 - 409 Conflict para documento duplicado
            return Conflict(new { mensagem = ex.Message });
        }
    }
}