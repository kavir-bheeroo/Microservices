using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microservices.Services.Revenue.Domain.Seedwork;
using Microservices.Services.Revenue.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Services.Revenue.Infrastructure
{
    public class RevenueContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "Revenue";

        public DbSet<Trip> Trip { get; set; }

        private readonly IMediator _mediator;

        public RevenueContext(DbContextOptions<RevenueContext> options) : base(options) { }

        public RevenueContext(DbContextOptions<RevenueContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TripEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TripLegEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch domain events.
            await _mediator.DispatchDomainEventsAsync(this);

            // Save all database changes.
            await base.SaveChangesAsync();

            return true;
        }
    }
}