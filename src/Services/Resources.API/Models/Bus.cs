using System;

namespace Microservices.Services.Resources.API.Models
{
    public class Bus
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public int SeatingCapacity { get; set; }
        public int StandingCapacity { get; set; }
        public string Description { get; set; }
    }
}