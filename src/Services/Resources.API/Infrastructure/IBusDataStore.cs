using System;
using Microservices.Services.Resources.API.Models;

namespace Microservices.Services.Resources.API.Infrastructure
{
    public interface IBusDataStore
    {
        Bus GetById(Guid id);
        void Add(Bus bus);
        void Delete(Guid id);
        void Update(Bus bus);
    }
}