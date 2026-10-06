using MediatR;
using ProductosApi.Domain.Abstractions;

namespace ProductosApi.Application.Productos.ObtenerTodos;

public sealed record ObtenerProductosQuery : IRequest<Result<IReadOnlyList<ProductoResponse>>>;
