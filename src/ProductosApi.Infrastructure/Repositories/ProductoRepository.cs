using Microsoft.EntityFrameworkCore;
using ProductosApi.Domain.Productos;
using ProductosApi.Infrastructure.Persistence;

namespace ProductosApi.Infrastructure.Repositories;

internal sealed class ProductoRepository(AppDbContext context) : IProductoRepository
{
    public async Task<IReadOnlyList<Producto>> ObtenerTodosAsync(CancellationToken cancellationToken = default) =>
        await context.Productos
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync(cancellationToken);

    public Task<Producto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        context.Productos.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public Task<bool> ExisteNombreAsync(string nombre, int? excluirId = null, CancellationToken cancellationToken = default) =>
        context.Productos.AnyAsync(p => p.Nombre == nombre && p.Id != excluirId, cancellationToken);

    public void Agregar(Producto producto) => context.Productos.Add(producto);

    public void Actualizar(Producto producto) => context.Productos.Update(producto);

    public void Eliminar(Producto producto) => context.Productos.Remove(producto);
}
