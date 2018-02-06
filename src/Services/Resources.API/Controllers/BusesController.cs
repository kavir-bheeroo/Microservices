using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microservices.Services.Resources.API.Infrastructure;
using Microservices.Services.Resources.API.Models;
using Microservices.BuildingBlocks.EventBus.Abstractions;
using Microservices.Services.Resources.API.IntegrationEvents.Events;

namespace Microservices.Services.Resources.API.Controllers
{
    [Route("api/[controller]")]
    public class BusesController : Controller
    {
        private readonly IBusDataStore _busDataStore;
        private readonly IEventBus _eventBus;

        public BusesController(IBusDataStore busDataStore, IEventBus eventBus)
        {
            _busDataStore = busDataStore ?? throw new ArgumentNullException(nameof(busDataStore));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var bus = _busDataStore.GetById(id);

            if (bus == null)
                return NotFound();

            return Ok(bus);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Bus bus)
        {
            if (bus == null)
                throw new ArgumentNullException(nameof(bus));

            bus.Id = Guid.NewGuid();
            _busDataStore.Add(bus);

            // Publish event
            var @event = new BusAddedIntegrationEvent(bus.Id);
            _eventBus.Publish(@event);

            return Created("test", bus);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Bus bus)
        {
            if (bus == null)
                throw new ArgumentNullException(nameof(bus));

            bus.Id = id;
            _busDataStore.Update(bus);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _busDataStore.Delete(id);
            return Ok();
        }
    }
}
