using MediatR;
using ProductosApi.Domain.Abstractions;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Application.Productos.ObtenerTodos;

public sealed class ObtenerProductosQueryHandler(IProductoRepository productoRepository)
    : IRequestHandler<ObtenerProductosQuery, Result<IReadOnlyList<ProductoResponse>>>
{
    public async Task<Result<IReadOnlyList<ProductoResponse>>> Handle(ObtenerProductosQuery request, CancellationToken cancellationToken)
    {
        var productos = await productoRepository.ObtenerTodosAsync(cancellationToken);

        return productos.Select(p => p.ToResponse()).ToList();
    }
}
