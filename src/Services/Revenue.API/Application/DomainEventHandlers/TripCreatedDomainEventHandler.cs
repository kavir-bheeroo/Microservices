using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microservices.Services.Revenue.Domain.AggregatesModel.DailyRevenueAggregate;
using Microservices.Services.Revenue.Domain.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microservices.Services.Revenue.API.Application.DomainEventHandlers
{
    public class TripCreatedDomainEventHandler : INotificationHandler<TripCreatedDomainEvent>
    {
        private readonly IDailyRevenueRepository _dailyRevenueRepository;
        private readonly ILogger<TripCreatedDomainEventHandler> _logger;

        public TripCreatedDomainEventHandler(IDailyRevenueRepository dailyRevenueRepository, ILogger<TripCreatedDomainEventHandler> logger)
        {
            _dailyRevenueRepository = dailyRevenueRepository ?? throw new ArgumentNullException(nameof(dailyRevenueRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public async Task Handle(TripCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {nameof(TripCreatedDomainEvent)}");

            // Check if revenue exists for the date the trip was created.
            var dailyRevenue = await _dailyRevenueRepository.FindByDateAsync(notification.Trip.TripDate);

            if (dailyRevenue == null)
            {
                dailyRevenue = new DailyRevenue(notification.Trip.TripDate, notification.Trip.TotalRevenue);
                await _dailyRevenueRepository.AddAsync(dailyRevenue);
            }
            else
            {
                dailyRevenue.AddIncome(notification.Trip.TotalRevenue);
                _dailyRevenueRepository.Update(dailyRevenue);
            }

            await _dailyRevenueRepository.UnitOfWork.SaveEntitiesAsync();

            _logger.LogInformation($"Handled {nameof(TripCreatedDomainEvent)}");
        }
    }
}