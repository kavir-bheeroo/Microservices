using System;
using Microservices.BuildingBlocks.EventBus.Events;

namespace Microservices.Services.Roster.API.IntegrationEvents.Events
{
    public class BusAddedIntegrationEvent : IntegrationEvent
    {
        public Guid BusId { get; set; }

        public BusAddedIntegrationEvent(Guid busId) => BusId = busId;
    }
}