using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microservices.Services.Revenue.API.Application.Commands;
using Microservices.Services.Revenue.API.Application.ViewModels;
using Microservices.Services.Revenue.Domain.AggregatesModel.TripAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Revenue.API.Controllers
{
    [Route("trip")]
    public class TripController : Controller
    {
        private readonly IMediator _mediator;

        public TripController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(TripViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTrip([FromBody]CreateTripCommand createTripCommand)
        {
            var result = await _mediator.Send(createTripCommand);
            return result != default(TripViewModel) ? (IActionResult)Ok(result) : (IActionResult)BadRequest();
        }
    }
}