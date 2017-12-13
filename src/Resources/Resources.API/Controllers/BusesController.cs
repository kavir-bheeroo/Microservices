using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resources.API.Infrastructure;
using Resources.API.Models;

namespace Resources.API.Controllers
{
    [Route("api/[controller]")]
    public class BusesController : Controller
    {
        private readonly IBusDataStore _busDataStore;

        public BusesController(IBusDataStore busDataStore)
        {
            _busDataStore = busDataStore ?? throw new ArgumentNullException(nameof(busDataStore));
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
