using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ProductosApi.Infrastructure.Persistence;

internal sealed class UtcDateTimeConverter() : ValueConverter<DateTime, DateTime>(
    valor => valor,
    valor => DateTime.SpecifyKind(valor, DateTimeKind.Utc));
