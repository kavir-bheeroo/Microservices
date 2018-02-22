using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Microservices.Services.Revenue.API.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default(TResponse);

            try
            {
                _logger.LogInformation($"Handling {typeof(TRequest).Name}");
                response = await next();
                _logger.LogInformation($"Handled {typeof(TRequest).Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return response;
        }
    }
}