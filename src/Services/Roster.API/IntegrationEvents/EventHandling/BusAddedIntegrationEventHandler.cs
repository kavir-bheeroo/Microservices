using System;
using System.Threading.Tasks;
using Microservices.BuildingBlocks.EventBus.Abstractions;
using Microservices.Services.Roster.API.IntegrationEvents.Events;

namespace Microservices.Services.Roster.API.IntegrationEvents.EventHandling
{
    public class BusAddedIntegrationEventHandler : IIntegrationEventHandler<BusAddedIntegrationEvent>
    {
        public async Task Handle(BusAddedIntegrationEvent @event)
        {
            Console.WriteLine($"Bus added with id: {@event.BusId}");
            Console.WriteLine($"Timestamp: {@event.CreationDate}");
            Console.WriteLine($"Event Id: {@event.Id}");

            await Task.FromResult(0);
        }
    }
}