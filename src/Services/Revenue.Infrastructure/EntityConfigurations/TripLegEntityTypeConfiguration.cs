using System;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Services.Revenue.Infrastructure.EntityConfigurations
{
    internal class TripLegEntityTypeConfiguration : IEntityTypeConfiguration<TripLeg>
    {
        public void Configure(EntityTypeBuilder<TripLeg> builder)
        {
            builder.ToTable("TripLegs", RevenueContext.DEFAULT_SCHEMA);
        }
    }
}