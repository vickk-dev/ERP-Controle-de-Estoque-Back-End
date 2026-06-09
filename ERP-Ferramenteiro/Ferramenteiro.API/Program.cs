using Ferramenteiro.API.Middleware;
using Ferramenteiro.API.Validators;
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Application.UseCases.Clientes;
using Ferramenteiro.Infra.Persistence.Repository;
using Ferramenteiro.Infra.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteValidator>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<CriarClienteUseCase>();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((doc, ctx, ct) =>
    {
        doc.Info.Title = "Ferramenteiro API";
        doc.Info.Version = "v1";
        doc.Info.Description = "ERP Ferramenteiro - módulo de Clientes (CPF/CNPJ único, RN01/RN02).";
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

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program { }
