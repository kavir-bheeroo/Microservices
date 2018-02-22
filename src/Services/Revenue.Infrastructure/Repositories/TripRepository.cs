using System.Threading.Tasks;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microservices.Services.Revenue.Domain.Seedwork;

namespace Microservices.Services.Revenue.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly RevenueContext _revenueContext;

        public TripRepository(RevenueContext revenueContext)
        {
            _revenueContext = revenueContext ?? throw new System.ArgumentNullException(nameof(revenueContext));
        }

        public IUnitOfWork UnitOfWork => _revenueContext;

        public async Task<Trip> AddAsync(Trip trip)
        {
            var result = await _revenueContext.Trip.AddAsync(trip);
            return result.Entity;
        }

        public Task<Trip> UpdateAsync(Trip trip)
        {
            throw new System.NotImplementedException();
        }
    }
}