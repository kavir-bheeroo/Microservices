using System;
using System.Collections.Generic;
using System.Linq;
using Microservices.Services.Revenue.Domain.Events;
using Microservices.Services.Revenue.Domain.Seedwork;

namespace Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate
{
    public class Trip : Entity, IAggregateRoot
    {
        private DateTime _tripDate;

        public Guid BusId { get; private set; }
        public Guid DriverId { get; private set; }
        public Guid ConductorId { get; private set; }

        public List<TripLeg> TripLegs { get; private set; }
        public int TotalTrips => TripLegs.Count;
        public decimal TotalRevenue => TripLegs.Sum(t => t.GetRevenue());

        public Trip(DateTime tripDate, Guid busId, Guid driverId, Guid conductorId, List<(string route, decimal revenue)> tripLegs)
        {
            // todo: Add validations
            _tripDate = tripDate;
            
            BusId = busId;
            DriverId = driverId;
            ConductorId = conductorId;
            
            TripLegs = new List<TripLeg>();
            tripLegs.ForEach((tuple) => AddTripLeg(tuple.route, tuple.revenue));

            AddDomainEvent(new TripCreatedDomainEvent(this));
        }

        private void AddTripLeg(string route, decimal revenue)
        {
            var tripLeg = new TripLeg(route, revenue);
            TripLegs.Add(tripLeg);
        }
    }
}