namespace ProductosApi.Api.Contracts;

public sealed record ProductoRequest(string Nombre, string? Descripcion, decimal Precio, int Stock);
