using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductosApi.Domain.Abstractions;
using ProductosApi.Domain.Productos;
using ProductosApi.Infrastructure.Persistence;
using ProductosApi.Infrastructure.Repositories;

namespace ProductosApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Falta la cadena de conexión 'DefaultConnection'.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        services.AddMemoryCache();

        // Patrón Decorator quien pide IProductoRepository recibe el repositorio con caché,
        // que a su vez envuelve al repositorio real de EF Core.
        services.AddScoped<ProductoRepository>();
        services.AddScoped<IProductoRepository>(sp => new CachedProductoRepository(
            sp.GetRequiredService<ProductoRepository>(),
            sp.GetRequiredService<IMemoryCache>(),
            sp.GetRequiredService<ILogger<CachedProductoRepository>>()));

        return services;
    }
}
