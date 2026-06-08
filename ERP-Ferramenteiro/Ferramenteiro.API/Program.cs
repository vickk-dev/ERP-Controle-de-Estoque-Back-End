using ERP_Ferramenteiro.Application.Services;
using ERP_Ferramenteiro.Ferramenteiro.Application.Interfaces;
using ERP_Ferramenteiro.Ferramenteiro.Application.Services;
using ERP_Ferramenteiro.Ferramenteiro.Infra.Data;
using ERP_Ferramenteiro.Infrastructure.Data;
using ERP_Ferramenteiro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddHttpClient<IViaCepService, ViaCepService>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();
builder.Services.AddScoped<IFerramentaRepository, FerramentaRepository>()
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();