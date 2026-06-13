using ERP_Ferramenteiro.Application.Services;
using ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Ferramenteiro.Application.Services;
using ERP_Ferramenteiro.Ferramenteiro.Infra.Data;
using ERP_Ferramenteiro.Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream
=======
using FluentValidation;
using FluentValidation.AspNetCore;
using Scalar.AspNetCore;

// Namespaces do projeto
using Ferramenteiro.API.Middleware;
//using Ferramenteiro.API.Validators;
using Ferramenteiro.Application.Interfaces;
using Ferramenteiro.Application.Services;
using Ferramenteiro.Infra.Data;
using Ferramenteiro.Infra.Persistence;
using Ferramenteiro.Infrastructure.Repositories;
using Ferramenteiro.Application.Services;


>>>>>>> Stashed changes

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
<<<<<<< Updated upstream
builder.Services.AddOpenApi();
=======
builder.Services.AddFluentValidationAutoValidation();
 //builder.Services.AddValidatorsFromAssemblyContaining<CriarClienteValidator>();

>>>>>>> Stashed changes
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddHttpClient<IViaCepService, ViaCepService>();

builder.Services.AddScoped<IClienteService, ClienteService>();
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
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();