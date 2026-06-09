using ERP_Ferramenteiro.Application.Services;
using ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Ferramenteiro.Application.Services;
using ERP_Ferramenteiro.Ferramenteiro.Infra.Data;
using ERP_Ferramenteiro.Infrastructure.Data;
using ERP_Ferramenteiro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// --- Configuração da política de CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// --- Repositórios ---
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();
builder.Services.AddScoped<IFerramentaRepository, FerramentaRepository>();

// --- Clientes Externos ---
builder.Services.AddHttpClient<IViaCepService, ViaCepService>();

// --- Serviços ---
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// --- Middleware Global de Tratamento de Exceções ---
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (exceptionHandlerPathFeature?.Error != null)
        {
            var exception = exceptionHandlerPathFeature.Error;

            var errorResponse = new
            {
                sucesso = false,
                mensagem = exception.Message,
                detalhes = (string?)null
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// --- Ativando a política de CORS na Pipeline ---
app.UseCors("FrontendCorsPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();