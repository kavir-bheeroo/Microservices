using System;
using System.Collections.Generic;
using Microservices.Services.Revenue.Domain.Seedwork;

namespace Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate
{
    public class TripLeg : ValueObject
    {
        public Guid Id { get; private set; }
        public string Route { get; private set; }
        public decimal Revenue { get; private set; }

        public TripLeg(string route = null, decimal revenue = 0)
        {
            // todo: Add validations.
            Id = Guid.NewGuid();
            Route = route;
            Revenue = revenue;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Route;
            yield return Revenue;
        }
    }
}