using MediatR;
using ProductosApi.Domain.Abstractions;

namespace ProductosApi.Application.Productos.Crear;

public sealed record CrearProductoCommand(string Nombre, string? Descripcion, decimal Precio, int Stock)
    : IRequest<Result<ProductoResponse>>;
