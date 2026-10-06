using ProductosApi.Domain.Abstractions;

namespace ProductosApi.Domain.Productos;

public static class ProductoErrors
{
    public static Error NoEncontrado(int id) =>
        Error.NotFound("Producto.NoEncontrado", $"No existe un producto con Id {id}.");

    public static Error NombreDuplicado(string nombre) =>
        Error.Conflict("Producto.NombreDuplicado", $"Ya existe un producto con el nombre '{nombre}'.");

    public static readonly Error NombreRequerido =
        Error.Validation("Producto.NombreRequerido", "El nombre es obligatorio.");

    public static readonly Error NombreMuyLargo =
        Error.Validation("Producto.NombreMuyLargo", $"El nombre no puede superar los {Producto.NombreMaxLength} caracteres.");

    public static readonly Error DescripcionMuyLarga =
        Error.Validation("Producto.DescripcionMuyLarga", $"La descripción no puede superar los {Producto.DescripcionMaxLength} caracteres.");

    public static readonly Error PrecioInvalido =
        Error.Validation("Producto.PrecioInvalido", "El precio debe ser mayor a cero.");

    public static readonly Error StockInvalido =
        Error.Validation("Producto.StockInvalido", "El stock no puede ser negativo.");
}
