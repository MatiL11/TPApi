using MediatR;
using ProductosApi.Api.Contracts;
using ProductosApi.Api.Extensions;
using ProductosApi.Application.Productos;
using ProductosApi.Application.Productos.Actualizar;
using ProductosApi.Application.Productos.Crear;
using ProductosApi.Application.Productos.Eliminar;
using ProductosApi.Application.Productos.ObtenerPorId;
using ProductosApi.Application.Productos.ObtenerTodos;

namespace ProductosApi.Api.Endpoints;

public static class ProductoEndpoints
{
    private const string ObtenerPorIdRoute = "ObtenerProductoPorId";

    public static IEndpointRouteBuilder MapProductoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productos").WithTags("Productos");

        group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new ObtenerProductosQuery(), cancellationToken);

                return result.Match(Results.Ok, error => error.ToProblem());
            })
            .WithSummary("Lista todos los productos")
            .Produces<IReadOnlyList<ProductoResponse>>();

        group.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new ObtenerProductoPorIdQuery(id), cancellationToken);

                return result.Match(Results.Ok, error => error.ToProblem());
            })
            .WithName(ObtenerPorIdRoute)
            .WithSummary("Obtiene un producto por su Id")
            .Produces<ProductoResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", async (ProductoRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CrearProductoCommand(request.Nombre, request.Descripcion, request.Precio, request.Stock);
                var result = await sender.Send(command, cancellationToken);

                return result.Match(
                    producto => Results.CreatedAtRoute(ObtenerPorIdRoute, new { id = producto.Id }, producto),
                    error => error.ToProblem());
            })
            .WithSummary("Crea un producto")
            .Produces<ProductoResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapPut("/{id:int}", async (int id, ProductoRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new ActualizarProductoCommand(id, request.Nombre, request.Descripcion, request.Precio, request.Stock);
                var result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, error => error.ToProblem());
            })
            .WithSummary("Actualiza un producto existente")
            .Produces<ProductoResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapDelete("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new EliminarProductoCommand(id), cancellationToken);

                return result.Match(Results.NoContent, error => error.ToProblem());
            })
            .WithSummary("Elimina un producto")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }
}
