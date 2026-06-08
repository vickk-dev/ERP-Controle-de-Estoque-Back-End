using ERP_Ferramenteiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "ERP Ferramenteiro API",
        Version = "v1",
        Description = "API para gerenciamento de locação de ferramentas"
    });
});

// Banco de dados (ajuste a connection string no appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependência (adicione seus services e repositories aqui)
// builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
// builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// Swagger só em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP Ferramenteiro v1");
        c.RoutePrefix = string.Empty; // Abre o Swagger na raiz "/"
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();