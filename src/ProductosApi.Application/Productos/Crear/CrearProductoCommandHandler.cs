using MediatR;
using ProductosApi.Domain.Abstractions;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Application.Productos.Crear;

public sealed class CrearProductoCommandHandler(IProductoRepository productoRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CrearProductoCommand, Result<ProductoResponse>>
{
    public async Task<Result<ProductoResponse>> Handle(CrearProductoCommand request, CancellationToken cancellationToken)
    {
        var productoResult = Producto.Crear(request.Nombre, request.Descripcion, request.Precio, request.Stock);
        if (productoResult.IsFailure)
        {
            return productoResult.Error;
        }

        var producto = productoResult.Value;

        if (await productoRepository.ExisteNombreAsync(producto.Nombre, cancellationToken: cancellationToken))
        {
            return ProductoErrors.NombreDuplicado(producto.Nombre);
        }

        productoRepository.Agregar(producto);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return producto.ToResponse();
    }
}
