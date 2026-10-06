using ProductosApi.Domain.Abstractions;

namespace ProductosApi.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblem(this Error error)
    {
        var (statusCode, title) = error.Type switch
        {
            ErrorType.Validation => (StatusCodes.Status400BadRequest, "Error de validación"),
            ErrorType.NotFound => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
            ErrorType.Conflict => (StatusCodes.Status409Conflict, "Conflicto"),
            _ => (StatusCodes.Status500InternalServerError, "Error interno")
        };

        return Results.Problem(
            statusCode: statusCode,
            title: title,
            detail: error.Description,
            extensions: new Dictionary<string, object?> { ["code"] = error.Code });
    }
}
