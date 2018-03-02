using System;
using System.Threading.Tasks;
using Microservices.Services.Revenue.Domain.Seedwork;

namespace Microservices.Services.Revenue.Domain.AggregatesModel.DailyRevenueAggregate
{
    public interface IDailyRevenueRepository : IRepository<DailyRevenue>
    {
        Task<DailyRevenue> AddAsync(DailyRevenue dailyRevenue);
        void Update(DailyRevenue dailyRevenue);
        Task<DailyRevenue> FindByDateAsync(DateTime date);
    }
}