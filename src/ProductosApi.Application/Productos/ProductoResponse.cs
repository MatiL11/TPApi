using ProductosApi.Domain.Productos;

namespace ProductosApi.Application.Productos;

public sealed record ProductoResponse(
    int Id,
    string Nombre,
    string? Descripcion,
    decimal Precio,
    int Stock,
    DateTime FechaCreacion,
    DateTime? FechaModificacion);

internal static class ProductoMappings
{
    public static ProductoResponse ToResponse(this Producto producto) => new(
        producto.Id,
        producto.Nombre,
        producto.Descripcion,
        producto.Precio,
        producto.Stock,
        producto.FechaCreacion,
        producto.FechaModificacion);
}
