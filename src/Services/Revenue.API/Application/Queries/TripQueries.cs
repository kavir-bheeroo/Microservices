using Dapper;
using Microservices.Services.Revenue.API.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Services.Revenue.API.Application.Queries
{
    public class TripQueries : ITripQueries
    {
        private readonly string _connectionString;

        public TripQueries(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        
        public async Task<TripViewModel> GetTripAsync(int id)
        {
            if (id == default(int)) return null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new { id };
                var query = @"SELECT t.Id, t.BusId, t.DriverId, t.ConductorId, t.TotalRevenue, t.TotalTrips, t.TripDate,
                        tl.Route, tl.Revenue
                    FROM Revenue.Trips t
                    LEFT JOIN Revenue.TripLegs tl on t.Id= tl.TripId
                    WHERE t.Id = @Id";

                var result =  await connection.QueryAsync<dynamic>(query, parameters);

                if (!result.AsList().Any())
                    throw new KeyNotFoundException();

                return MapResultToTripViewModel(result);
            }
        }

        private TripViewModel MapResultToTripViewModel(dynamic result)
        {
            var tripViewModel = new TripViewModel
            {
                TripId = result[0].Id,
                BusId = result[0].BusId,
                DriverId = result[0].DriverId,
                ConductorId = result[0].ConductorId,
                TotalRevenue = result[0].TotalRevenue,
                TotalTrips = result[0].TotalTrips,
                TripDate = result[0].TripDate,
                TripLegs = new List<TripLegViewModel>()
            };

            foreach (var resultRow in result)
            {
                var tripLeg = new TripLegViewModel
                {
                    Route = resultRow.Route,
                    Revenue = resultRow.Revenue
                };

                tripViewModel.TripLegs.Add(tripLeg);
            }

            return tripViewModel;
        }
    }
}