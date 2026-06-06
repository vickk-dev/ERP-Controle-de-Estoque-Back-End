using Ferramenteiro.API.Middleware;
using Ferramenteiro.API.Validators;
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Application.UseCases.Clientes;
using Ferramenteiro.Infra.Persistence.Repository;
using Ferramenteiro.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers ───────────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── FluentValidation ──────────────────────────────────────────────────────────
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteValidator>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Infra — Repositório em memória (desenvolvimento, sem banco de dados) ──────
// Quando quiser usar banco de dados real, veja o bloco comentado abaixo.
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();


// ── Application — Use Cases ───────────────────────────────────────────────────
builder.Services.AddScoped<CriarClienteUseCase>();

// ── OpenAPI / Scalar ──────────────────────────────────────────────────────────
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((doc, ctx, ct) =>
    {
        doc.Info.Title = "Ferramenteiro API";
        doc.Info.Version = "v1";
        doc.Info.Description = "ERP Ferramenteiro — módulo de Clientes (CPF/CNPJ único, RN01/RN02).";
        return Task.CompletedTask;
    });
});

// ── Build ─────────────────────────────────────────────────────────────────────
var app = builder.Build();

// Middleware global de exceções — deve ser o primeiro da cadeia
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();            // /openapi/v1.json
    app.MapScalarApiReference(); // /scalar/v1
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program { }