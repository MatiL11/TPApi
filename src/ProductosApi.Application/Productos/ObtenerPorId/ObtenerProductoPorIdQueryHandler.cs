using MediatR;
using ProductosApi.Domain.Abstractions;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Application.Productos.ObtenerPorId;

public sealed class ObtenerProductoPorIdQueryHandler(IProductoRepository productoRepository)
    : IRequestHandler<ObtenerProductoPorIdQuery, Result<ProductoResponse>>
{
    public async Task<Result<ProductoResponse>> Handle(ObtenerProductoPorIdQuery request, CancellationToken cancellationToken)
    {
        var producto = await productoRepository.ObtenerPorIdAsync(request.Id, cancellationToken);

        return producto is null
            ? ProductoErrors.NoEncontrado(request.Id)
            : producto.ToResponse();
    }
}
