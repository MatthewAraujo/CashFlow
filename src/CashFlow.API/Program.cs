using CashFlow.Api.Filters;
using CashFlow.API.Middleware;
using CashFlow.Application;
using CashFlow.Infrastructure;
using CashFlow.Infrastructure.Migrations;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    // Configuração para exibir DateOnly como string no formato yyyy-MM
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("")
    });
});
builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
var app = builder.Build(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await MigrateDatabase();

app.Run();

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();
    await DatabaseMigration.MigrateDatabase(scope.ServiceProvider);

}
