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
        public Task Handle(TripCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // todo: re-calculate revenue.
            return Task.CompletedTask;
        }
    }
}