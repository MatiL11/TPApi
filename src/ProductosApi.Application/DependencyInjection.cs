using Microsoft.Extensions.DependencyInjection;
using ProductosApi.Application.Behaviors;

namespace ProductosApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            // Registra automáticamente todos los handlers
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            // Comportamiento transversal que se ejecuta antes y después de cada handler
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        return services;
    }
}
