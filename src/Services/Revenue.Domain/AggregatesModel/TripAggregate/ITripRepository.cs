using System.Threading.Tasks;
using Microservices.Services.Revenue.Domain.Seedwork;

namespace Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate
{
    public interface ITripRepository : IRepository<Trip>
    {
        Task<Trip> AddAsync(Trip trip);
        Task<Trip> UpdateAsync(Trip trip);
    }
}