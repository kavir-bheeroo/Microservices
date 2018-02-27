using System;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Services.Revenue.Infrastructure.EntityConfigurations
{
    internal class TripEntityTypeConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.ToTable("Trips", RevenueContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Ignore(o => o.DomainEvents);
            builder.Property(o => o.Id)
                .ForSqlServerUseSequenceHiLo("TripSeq", RevenueContext.DEFAULT_SCHEMA);

            builder.Property<DateTime>("TripDate").IsRequired();
            builder.Property<Guid>("BusId").IsRequired();
            builder.Property<Guid>("DriverId").IsRequired();
            builder.Property<Guid>("ConductorId").IsRequired();
            builder.Property<int>("TotalTrips").IsRequired();
            builder.Property<decimal>("TotalRevenue").IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Trip.TripLegs));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder.OwnsOne(t => t.TripLegs);
            //builder.HasMany<TripLeg>();
        }
    }
}