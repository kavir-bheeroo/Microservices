using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;

namespace Microservices.Services.Revenue.API.Application.Commands
{
    [DataContract]
    public class CreateTripCommand : IRequest<bool>
    {
        private readonly List<TripLegDto> _tripLegs;
        
        [DataMember]
        public DateTime TripDate { get; private set; }

        [DataMember]
        public Guid BusId { get; private set; }

        [DataMember]
        public Guid DriverId { get; private set; }

        [DataMember]
        public Guid ConductorId { get; private set; }

        [DataMember]
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

        public TripLegDto(string route, decimal revenue)
        {
            Route = route;
            Revenue = revenue;
        }
    }
}