using System.Collections.Generic;
using System.Threading.Tasks;
using Microservices.Services.Revenue.API.Application.ViewModels;

namespace Microservices.Services.Revenue.API.Application.Queries
{
    public interface ITripQueries
    {
        Task<TripViewModel> GetTripAsync(int id);   
    }
}