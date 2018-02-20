using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microsoft.Extensions.Logging;

namespace Microservices.Services.Revenue.API.Application.Commands
{
    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, bool>
    {
        private readonly ILogger<CreateTripCommandHandler> _logger;

        public CreateTripCommandHandler(ILogger<CreateTripCommandHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public Task<bool> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"In {nameof(CreateTripCommandHandler)}");

            var tripLegs = new List<(string route, decimal revenue)>();
            foreach (var tuple in request.TripLegs)
            {
                tripLegs.Add((tuple.Route, tuple.Revenue));
            }
            
            var trip = new Trip(request.TripDate, request.BusId, request.DriverId, request.ConductorId, tripLegs);
            
            return Task.FromResult(true);
        }
    }
}