using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Services.Revenue.API.Application.Queries
{
    public interface ITripQueries
    {
        Task<dynamic> GetTripAsync(int id); 
    }
}