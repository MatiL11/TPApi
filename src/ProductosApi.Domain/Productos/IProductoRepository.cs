namespace ProductosApi.Domain.Productos;

public interface IProductoRepository
{
    Task<IReadOnlyList<Producto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

    Task<Producto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> ExisteNombreAsync(string nombre, int? excluirId = null, CancellationToken cancellationToken = default);

    void Agregar(Producto producto);

    void Actualizar(Producto producto);

    void Eliminar(Producto producto);
}
