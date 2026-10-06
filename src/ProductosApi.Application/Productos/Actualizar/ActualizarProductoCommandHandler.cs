using MediatR;
using ProductosApi.Domain.Abstractions;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Application.Productos.Actualizar;

public sealed class ActualizarProductoCommandHandler(IProductoRepository productoRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<ActualizarProductoCommand, Result<ProductoResponse>>
{
    public async Task<Result<ProductoResponse>> Handle(ActualizarProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = await productoRepository.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (producto is null)
        {
            return ProductoErrors.NoEncontrado(request.Id);
        }

        var actualizacion = producto.Actualizar(request.Nombre, request.Descripcion, request.Precio, request.Stock);
        if (actualizacion.IsFailure)
        {
            return actualizacion.Error;
        }

        if (await productoRepository.ExisteNombreAsync(producto.Nombre, excluirId: producto.Id, cancellationToken))
        {
            return ProductoErrors.NombreDuplicado(producto.Nombre);
        }

        productoRepository.Actualizar(producto);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return producto.ToResponse();
    }
}
