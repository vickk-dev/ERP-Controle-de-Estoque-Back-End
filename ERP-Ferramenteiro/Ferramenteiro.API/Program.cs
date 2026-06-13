using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Scalar.AspNetCore;

// Namespaces do projeto
using Ferramenteiro.API.Middleware;
using Ferramenteiro.API.Validators;
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Application.Services;
using Ferramenteiro.Infra.Data;
using Ferramenteiro.Infra.Persistence;
using Ferramenteiro.Infrastructure.Repositories;
using Ferramenteiro.Ferramenteiro.Application.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteValidator>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();
builder.Services.AddScoped<IFerramentaRepository, FerramentaRepository>();

builder.Services.AddHttpClient<IViaCepService, ViaCepService>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<IClienteService, ClienteService>();

// Use Cases
builder.Services.AddScoped<CriarClienteService>();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((doc, ctx, ct) =>
    {
        doc.Info.Title = "Ferramenteiro API";
        doc.Info.Version = "v1";
        doc.Info.Description = "ERP Ferramenteiro — módulo de Clientes, Estoque e Locações.";
        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();            
    app.MapScalarApiReference(); 
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Necessário para testes de integração
public partial class Program { }