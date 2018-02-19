using System.Collections.Generic;
using Microservices.Services.Revenue.Domain.Seedwork;

namespace Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate
{
    public class TripLeg : ValueObject
    {
        private string _route;
        private decimal _revenue;

        public TripLeg(string route = null, decimal revenue = 0)
        {
            // todo: Add validations.
            _route = route;
            _revenue = revenue;
        }

        public decimal GetRevenue()
        {
            return _revenue;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _route;
            yield return _revenue;
        }
    }
}