using Microsoft.EntityFrameworkCore;
using ProductosApi.Infrastructure.Persistence;

namespace ProductosApi.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}
