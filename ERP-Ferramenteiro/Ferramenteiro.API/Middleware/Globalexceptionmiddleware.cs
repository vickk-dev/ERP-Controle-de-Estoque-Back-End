using System.Net;
using System.Text.Json;
using Ferramenteiro.Domain.Entities;

namespace Ferramenteiro.API.Middleware;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DocumentoDuplicadoException ex)
        {
            // RN01 — 409 Conflict capturado globalmente como fallback
            _logger.LogWarning("Documento duplicado: {Message}", ex.Message);
            await EscreverRespostaAsync(context, HttpStatusCode.Conflict, ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Argumento inválido: {Message}", ex.Message);
            await EscreverRespostaAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
            await EscreverRespostaAsync(context, HttpStatusCode.InternalServerError,
                "Ocorreu um erro interno. Tente novamente mais tarde.");
        }
    }

    private static Task EscreverRespostaAsync(HttpContext context, HttpStatusCode status, string mensagem)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var body = JsonSerializer.Serialize(new
        {
            status = (int)status,
            mensagem
        });

        return context.Response.WriteAsync(body);
    }
}