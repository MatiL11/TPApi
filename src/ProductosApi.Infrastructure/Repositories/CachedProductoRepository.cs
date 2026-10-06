using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Infrastructure.Repositories;

// Patrón Decorator: implementa la misma interfaz que ProductoRepository y lo envuelve,
// agregando caché en memoria sin modificar al repositorio original ni a quienes lo usan.
internal sealed class CachedProductoRepository(
    IProductoRepository inner,
    IMemoryCache cache,
    ILogger<CachedProductoRepository> logger) : IProductoRepository
{
    private const string TodosCacheKey = "productos:todos";
    private static readonly TimeSpan Expiracion = TimeSpan.FromMinutes(1);

    public async Task<IReadOnlyList<Producto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        if (cache.TryGetValue(TodosCacheKey, out IReadOnlyList<Producto>? productos) && productos is not null)
        {
            logger.LogInformation("Productos obtenidos desde la caché");
            return productos;
        }

        productos = await inner.ObtenerTodosAsync(cancellationToken);
        cache.Set(TodosCacheKey, productos, Expiracion);
        logger.LogInformation("Productos obtenidos desde la base de datos y guardados en la caché");

        return productos;
    }

    // No se cachea: devuelve la entidad rastreada por EF Core, que luego se modifica o elimina.
    public Task<Producto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        inner.ObtenerPorIdAsync(id, cancellationToken);

    public Task<bool> ExisteNombreAsync(string nombre, int? excluirId = null, CancellationToken cancellationToken = default) =>
        inner.ExisteNombreAsync(nombre, excluirId, cancellationToken);

    public void Agregar(Producto producto)
    {
        inner.Agregar(producto);
        InvalidarCache();
    }

    public void Actualizar(Producto producto)
    {
        inner.Actualizar(producto);
        InvalidarCache();
    }

    public void Eliminar(Producto producto)
    {
        inner.Eliminar(producto);
        InvalidarCache();
    }

    private void InvalidarCache() => cache.Remove(TodosCacheKey);
}
