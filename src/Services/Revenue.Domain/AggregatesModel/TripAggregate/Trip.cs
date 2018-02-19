using System;
using System.Collections.Generic;
using System.Linq;
using Microservices.Services.Revenue.Domain.Seedwork;

namespace Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate
{
    public class Trip : Entity, IAggregateRoot
    {
        public Guid BusId { get; private set; }
        public Guid DriverId { get; private set; }
        public Guid ConductorId { get; private set; }

        public List<TripLeg> TripLegs { get; private set; }
        public int TotalTrips => TripLegs.Count;
        public decimal TotalRevenue => TripLegs.Sum(t => t.GetRevenue());

        public Trip(Guid busId, Guid driverId, Guid conductorId)
        {
            // todo: Add validations
            BusId = busId;
            DriverId = driverId;
            ConductorId = conductorId;
            TripLegs = new List<TripLeg>();
        }

        public void AddTripLeg(string route, decimal revenue)
        {
            var tripLeg = new TripLeg(route, revenue);
            TripLegs.Add(tripLeg);
        }
    }
}