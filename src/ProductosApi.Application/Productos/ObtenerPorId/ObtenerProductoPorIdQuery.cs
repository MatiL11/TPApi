using MediatR;
using ProductosApi.Domain.Abstractions;

namespace ProductosApi.Application.Productos.ObtenerPorId;

public sealed record ObtenerProductoPorIdQuery(int Id) : IRequest<Result<ProductoResponse>>;
