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
        public Task<bool> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
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