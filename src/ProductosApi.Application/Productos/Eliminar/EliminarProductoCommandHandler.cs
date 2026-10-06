using MediatR;
using ProductosApi.Domain.Abstractions;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Application.Productos.Eliminar;

public sealed class EliminarProductoCommandHandler(IProductoRepository productoRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<EliminarProductoCommand, Result>
{
    public async Task<Result> Handle(EliminarProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = await productoRepository.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (producto is null)
        {
            return ProductoErrors.NoEncontrado(request.Id);
        }

        productoRepository.Eliminar(producto);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
