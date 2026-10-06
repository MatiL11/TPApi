using MediatR;
using Microsoft.Extensions.Logging;
using ProductosApi.Domain.Abstractions;

namespace ProductosApi.Application.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Procesando {RequestName}", requestName);

        var result = await next(cancellationToken);

        if (result.IsSuccess)
        {
            logger.LogInformation("{RequestName} finalizó correctamente", requestName);
        }
        else
        {
            logger.LogWarning("{RequestName} finalizó con error {ErrorCode}: {ErrorDescription}",
                requestName, result.Error.Code, result.Error.Description);
        }

        return result;
    }
}
