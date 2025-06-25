using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Bookify.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse :Result

{
    private readonly ILogger<LoggingBehavior<TRequest,TResponse>> _logger;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest,TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;
        try
        {
            _logger.LogInformation("Executing request {Request}", name);


            var result = await next();

            if (result.IsSuccess)
            {
                _logger.LogInformation("Request {Request} processed successfully",name);

            }
            else
            {
                using (LogContext.PushProperty("Error",result.Error,true))
                {
                    _logger.LogError("Request {Request} processed with Error", name);
                }
      
            } 

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "request {Request} processing failed", name);
            throw;
        }
        return default!;
    }
}
