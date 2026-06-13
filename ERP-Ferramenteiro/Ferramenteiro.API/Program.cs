using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
=======
>>>>>>> dd52afd86e4746ec5e2604f30e14780786a4515b
using FluentValidation;
using FluentValidation.AspNetCore;
using Scalar.AspNetCore;

// Namespaces do projeto
using Ferramenteiro.API.Middleware;
<<<<<<< HEAD
//using Ferramenteiro.API.Validators;
=======
using Ferramenteiro.API.Validators;
>>>>>>> dd52afd86e4746ec5e2604f30e14780786a4515b
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Application.Services;
using Ferramenteiro.Infra.Data;
using Ferramenteiro.Infra.Persistence;
using Ferramenteiro.Infrastructure.Repositories;
<<<<<<< HEAD
using Ferramenteiro.Application.Services;


>>>>>>> Stashed changes
=======
using Ferramenteiro.Ferramenteiro.Application.Services;


>>>>>>> dd52afd86e4746ec5e2604f30e14780786a4515b

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
<<<<<<< HEAD
<<<<<<< Updated upstream
builder.Services.AddOpenApi();
=======
builder.Services.AddFluentValidationAutoValidation();
 //builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteValidator>();

>>>>>>> Stashed changes
=======
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteValidator>();

>>>>>>> dd52afd86e4746ec5e2604f30e14780786a4515b
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();
builder.Services.AddScoped<IFerramentaRepository, FerramentaRepository>();

builder.Services.AddHttpClient<IViaCepService, ViaCepService>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
<<<<<<< HEAD
<<<<<<< Updated upstream
var app = builder.Build();

=======

// Use Cases
//builder.Services.AddScoped<CriarClienteService>();

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
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("PermitirTudo");



app.UseMiddleware<GlobalExceptionMiddleware>();


>>>>>>> Stashed changes
=======

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


>>>>>>> dd52afd86e4746ec5e2604f30e14780786a4515b
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