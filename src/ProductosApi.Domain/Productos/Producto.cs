using ProductosApi.Domain.Abstractions;

namespace ProductosApi.Domain.Productos;

public sealed class Producto
{
    public const int NombreMaxLength = 100;
    public const int DescripcionMaxLength = 500;

    // Constructor requerido por EF Core para materializar la entidad.
    private Producto()
    {
    }

    public int Id { get; private set; }

    public string Nombre { get; private set; } = string.Empty;

    public string? Descripcion { get; private set; }

    public decimal Precio { get; private set; }

    public int Stock { get; private set; }

    public DateTime FechaCreacion { get; private set; }

    public DateTime? FechaModificacion { get; private set; }

    public static Result<Producto> Crear(string nombre, string? descripcion, decimal precio, int stock)
    {
        var validacion = Validar(nombre, descripcion, precio, stock);
        if (validacion.IsFailure)
        {
            return validacion.Error;
        }

        return new Producto
        {
            Nombre = nombre.Trim(),
            Descripcion = Normalizar(descripcion),
            Precio = precio,
            Stock = stock,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public Result Actualizar(string nombre, string? descripcion, decimal precio, int stock)
    {
        var validacion = Validar(nombre, descripcion, precio, stock);
        if (validacion.IsFailure)
        {
            return validacion.Error;
        }

        Nombre = nombre.Trim();
        Descripcion = Normalizar(descripcion);
        Precio = precio;
        Stock = stock;
        FechaModificacion = DateTime.UtcNow;

        return Result.Success();
    }

    private static Result Validar(string nombre, string? descripcion, decimal precio, int stock)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return ProductoErrors.NombreRequerido;
        }

        if (nombre.Trim().Length > NombreMaxLength)
        {
            return ProductoErrors.NombreMuyLargo;
        }

        if (descripcion?.Trim().Length > DescripcionMaxLength)
        {
            return ProductoErrors.DescripcionMuyLarga;
        }

        if (precio <= 0)
        {
            return ProductoErrors.PrecioInvalido;
        }

        if (stock < 0)
        {
            return ProductoErrors.StockInvalido;
        }

        return Result.Success();
    }

    private static string? Normalizar(string? texto) =>
        string.IsNullOrWhiteSpace(texto) ? null : texto.Trim();
}
