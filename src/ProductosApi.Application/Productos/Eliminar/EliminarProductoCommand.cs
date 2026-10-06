using MediatR;
using ProductosApi.Domain.Abstractions;

namespace ProductosApi.Application.Productos.Eliminar;

public sealed record EliminarProductoCommand(int Id) : IRequest<Result>;
