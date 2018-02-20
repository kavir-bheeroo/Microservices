using System;
using MediatR;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;

namespace Microservices.Services.Revenue.Domain.Events
{
    public class TripCreatedDomainEvent : INotification
    {
        public Trip Trip { get; private set; }

        public TripCreatedDomainEvent(Trip trip)
        {
            Trip = trip;
        }
    }
}