using Ferramenteiro.Application.Services;
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Infra.Persistence;
using Ferramenteiro.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Ferramenteiro.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// --- Configuração do Banco ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// --- CORS Unificado ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// --- Injeções de Dependência ---
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();
builder.Services.AddScoped<IFerramentaRepository, FerramentaRepository>();
builder.Services.AddHttpClient<IViaCepService, ViaCepService>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// --- Middleware de Erro ---
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (error != null)
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { mensagem = error.Message }));
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// --- Aplicando CORS ---
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();