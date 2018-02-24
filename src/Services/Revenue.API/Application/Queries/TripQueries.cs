using Dapper;
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
        
        public async Task<dynamic> GetTripAsync(int id)
        {
            if (id == default(int)) return null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM Revenue.Trips";

                var result =  await connection.QueryAsync<dynamic>(query);

                if (!result.AsList().Any())
                    throw new KeyNotFoundException();

                return result;
            }
        }
    }
}