using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microservices.Services.Revenue.API.Application.Commands;
using Microservices.Services.Revenue.API.Application.Queries;
using Microservices.Services.Revenue.API.Application.ViewModels;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Revenue.API.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ITripQueries _tripQueries;

        public TripsController(IMediator mediator, ITripQueries tripQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _tripQueries = tripQueries ?? throw new ArgumentNullException(nameof(tripQueries));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TripViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTrip([FromBody]CreateTripCommand createTripCommand)
        {
            var result = await _mediator.Send(createTripCommand);
            return result != default(TripViewModel) ? (IActionResult)Ok(result) : (IActionResult)BadRequest();
        }

        [Route("{tripId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(TripViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTrip([FromRoute]int tripId)
        {
            try
            {
                var trip = await _tripQueries.GetTripAsync(tripId);
                return Ok(trip);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}