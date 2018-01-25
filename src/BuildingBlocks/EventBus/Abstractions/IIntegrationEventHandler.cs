using System.Threading.Tasks;
using Microservices.BuildingBlocks.EventBus.Events;

namespace Microservices.BuildingBlocks.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
}