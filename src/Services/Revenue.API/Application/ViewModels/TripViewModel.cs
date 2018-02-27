using System;
using System.Collections.Generic;

namespace Microservices.Services.Revenue.API.Application.ViewModels
{
    public class TripViewModel
    {
        public int TripId { get; set; }
        public Guid BusId { get; set; }
        public Guid DriverId { get; set; }
        public Guid ConductorId { get; set; }
        public int TotalTrips { get; set; }
        public decimal TotalRevenue { get; set; }
        public DateTime TripDate { get; set; }
        public List<TripLegViewModel> TripLegs { get; set; }
    }

    public class TripLegViewModel
    {
        public string Route { get; set; }
        public decimal Revenue { get; set; }
    }
}