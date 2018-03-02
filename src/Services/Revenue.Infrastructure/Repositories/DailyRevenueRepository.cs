using System;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Services.Revenue.Domain.AggregatesModel.DailyRevenueAggregate;
using Microservices.Services.Revenue.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Services.Revenue.Infrastructure.Repositories
{
    public class DailyRevenueRepository : IDailyRevenueRepository
    {
        private readonly RevenueContext _revenueContext;

        public IUnitOfWork UnitOfWork => _revenueContext;

        public DailyRevenueRepository(RevenueContext revenueContext)
        {
            _revenueContext = revenueContext ?? throw new ArgumentNullException(nameof(revenueContext));
        }

        public async Task<DailyRevenue> AddAsync(DailyRevenue dailyRevenue)
        {
            var result = await _revenueContext.DailyRevenue.AddAsync(dailyRevenue);
            return result.Entity;
        }

        public void Update(DailyRevenue dailyRevenue)
        {
            _revenueContext.DailyRevenue.Update(dailyRevenue);
        }

        public async Task<DailyRevenue> FindByDateAsync(DateTime date)
        {
            return await _revenueContext.DailyRevenue.SingleOrDefaultAsync(r => r.Date.Date == date.Date);
        }
    }
}