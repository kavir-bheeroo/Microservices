using System;
using Microservices.BuildingBlocks.EventBus.Events;

namespace Microservices.Services.Resources.API.IntegrationEvents.Events
{
    public class BusAddedIntegrationEvent : IntegrationEvent
    {
        public Guid BusId { get; private set; }

        public BusAddedIntegrationEvent(Guid busId) => BusId = busId;
    }
}