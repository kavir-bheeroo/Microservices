using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Services.Revenue.Infrastructure.EntityConfigurations
{
    internal class TripEntityTypeConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.ToTable("Trip", RevenueContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Ignore(o => o.DomainEvents);
            builder.Property(o => o.Id)
                .ForSqlServerUseSequenceHiLo("TripSeq", RevenueContext.DEFAULT_SCHEMA);

            builder.OwnsOne(o => o.TripLegs);
        }
    }
}