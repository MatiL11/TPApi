using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductosApi.Domain.Productos;

namespace ProductosApi.Infrastructure.Persistence.Configurations;

internal sealed class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("Productos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nombre)
            .HasMaxLength(Producto.NombreMaxLength)
            .UseCollation("NOCASE") 
            .IsRequired();

        builder.HasIndex(p => p.Nombre).IsUnique();

        builder.Property(p => p.Descripcion)
            .HasMaxLength(Producto.DescripcionMaxLength);

        builder.Property(p => p.Precio)
            .HasPrecision(18, 2);

        builder.Property(p => p.Stock);

        builder.Property(p => p.FechaCreacion);

        builder.Property(p => p.FechaModificacion);

        // Datos iniciales para poder probar la API apenas se levanta
        var fechaSeed = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        builder.HasData(
            new { Id = 1, Nombre = "Teclado mecánico", Descripcion = (string?)"Teclado mecánico switch red, layout español", Precio = 85000m, Stock = 15, FechaCreacion = fechaSeed },
            new { Id = 2, Nombre = "Mouse inalámbrico", Descripcion = (string?)"Mouse óptico 2.4 GHz, 1600 DPI", Precio = 25000m, Stock = 40, FechaCreacion = fechaSeed },
            new { Id = 3, Nombre = "Monitor 24\"", Descripcion = (string?)null, Precio = 320000m, Stock = 8, FechaCreacion = fechaSeed });
    }
}
