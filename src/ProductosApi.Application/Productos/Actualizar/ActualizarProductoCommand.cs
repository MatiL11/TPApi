using MediatR;
using ProductosApi.Domain.Abstractions;

namespace ProductosApi.Application.Productos.Actualizar;

public sealed record ActualizarProductoCommand(int Id, string Nombre, string? Descripcion, decimal Precio, int Stock)
    : IRequest<Result<ProductoResponse>>;
