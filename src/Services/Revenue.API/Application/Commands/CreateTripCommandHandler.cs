using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microservices.Services.Revenue.API.Application.ViewModels;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microsoft.Extensions.Logging;

namespace Microservices.Services.Revenue.API.Application.Commands
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripViewModel>
    {
        private readonly ITripRepository _tripRepository;

        public CreateTripCommandHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository ?? throw new ArgumentNullException(nameof(tripRepository));
        }

        public async Task<TripViewModel> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            var tripLegs = new List<(string route, decimal revenue)>();
            foreach (var tuple in request.TripLegs)
            {
                tripLegs.Add((tuple.Route, tuple.Revenue));
            }
            
            var trip = new Trip(request.TripDate, request.BusId, request.DriverId, request.ConductorId, tripLegs);
            
            var entity = await _tripRepository.AddAsync(trip);
            await _tripRepository.UnitOfWork.SaveEntitiesAsync();

            return MapTripToTripViewModel(entity);
        }

        private TripViewModel MapTripToTripViewModel(Trip trip)
        {
            return new TripViewModel
            {
                TripId = trip.Id,
                BusId = trip.BusId,
                DriverId = trip.DriverId,
                ConductorId = trip.ConductorId,
                TotalTrips = trip.TotalTrips,
                TotalRevenue = trip.TotalRevenue,
                TripDate = trip.TripDate,
                TripLegs = trip.TripLegs.Select(t => MapTripLegToTripLegViewModel(t)).ToList()
            };

            TripLegViewModel MapTripLegToTripLegViewModel(TripLeg tripLeg)
            {
                return new TripLegViewModel
                {
                    Route = tripLeg.Route,
                    Revenue = tripLeg.Revenue
                };
            }
        }
    }
}