using System;
using System.Collections.Generic;
using System.Linq;
using Microservices.Services.Revenue.Domain.Events;
using Microservices.Services.Revenue.Domain.Seedwork;

namespace Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate
{
    public class Trip : Entity, IAggregateRoot
    {
        public DateTime TripDate { get; private set; }

        public Guid BusId { get; private set; }
        public Guid DriverId { get; private set; }
        public Guid ConductorId { get; private set; }

        private readonly List<TripLeg> _tripLegs;
        public IReadOnlyCollection<TripLeg> TripLegs => _tripLegs;

        public int TotalTrips { get { return TripLegs.Count; } private set { } }
        public decimal TotalRevenue { get { return TripLegs.Sum(t => t.Revenue); } private set { } }

        public Trip(DateTime tripDate, Guid busId, Guid driverId, Guid conductorId, List<(string route, decimal revenue)> tripLegs)
        {
            // todo: Add validations
            TripDate = tripDate;
            
            BusId = busId;
            DriverId = driverId;
            ConductorId = conductorId;
            
            _tripLegs = new List<TripLeg>();
            tripLegs.ForEach((tuple) => AddTripLeg(tuple.route, tuple.revenue));

            AddDomainEvent(new TripCreatedDomainEvent(this));
        }

        private void AddTripLeg(string route, decimal revenue)
        {
            var tripLeg = new TripLeg(route, revenue);
            _tripLegs.Add(tripLeg);
        }
    }
}