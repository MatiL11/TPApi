using Microsoft.OpenApi;
using ProductosApi.Api.Endpoints;
using ProductosApi.Api.Extensions;
using ProductosApi.Application;
using ProductosApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Registro de las capas
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Productos API",
        Version = "v1",
        Description = "API de ejemplo con Minimal API, EF Core (SQLite), MediatR y patrón Result."
    });
});

// Errores no controlados respuesta ProblemDetails
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
app.MapProductoEndpoints();

app.Run();
