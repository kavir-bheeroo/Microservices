using System;
using System.Collections.Generic;
using MediatR;

namespace Microservices.Services.Revenue.API.Application.Commands
{
    public class CreateTripCommand : IRequest<bool>
    {
        private readonly List<TripLegDto> _tripLegs;
        
        public DateTime TripDate { get; private set; }
        public Guid BusId { get; private set; }
        public Guid DriverId { get; private set; }
        public Guid ConductorId { get; private set; }

        public IEnumerable<TripLegDto> TripLegs => _tripLegs;

        public CreateTripCommand()
        {
            _tripLegs = new List<TripLegDto>();
        }

        public CreateTripCommand(DateTime tripDate, Guid busId, Guid driverId, Guid conductorId, List<TripLegDto> tripLegs) : this()
        {
            TripDate = tripDate;
            BusId = busId;
            DriverId = driverId;
            ConductorId = conductorId;
            _tripLegs = tripLegs;
        }
    }

    public class TripLegDto
    {
        public string Route { get; private set; }
        public decimal Revenue { get; private set; }
    }
}