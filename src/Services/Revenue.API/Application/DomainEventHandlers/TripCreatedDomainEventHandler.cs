using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microservices.Services.Revenue.Domain.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microservices.Services.Revenue.API.Application.DomainEventHandlers
{
    public class TripCreatedDomainEventHandler : INotificationHandler<TripCreatedDomainEvent>
    {
        private readonly ILogger<TripCreatedDomainEventHandler> _logger;

        public TripCreatedDomainEventHandler(ILogger<TripCreatedDomainEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(TripCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"In {nameof(TripCreatedDomainEventHandler)}");
            var jsonData = JsonConvert.SerializeObject(notification);
            _logger.LogInformation(jsonData);

            return Task.CompletedTask;
        }
    }
}