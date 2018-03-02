using Microservices.Services.Revenue.Domain.AggregatesModel.DailyRevenueAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Services.Revenue.Infrastructure.EntityConfigurations
{
    internal class DailyRevenueEntityTypeConfiguration : IEntityTypeConfiguration<DailyRevenue>
    {
        public void Configure(EntityTypeBuilder<DailyRevenue> builder)
        {
            builder.ToTable("DailyRevenue", RevenueContext.DEFAULT_SCHEMA);
        }
    }
}