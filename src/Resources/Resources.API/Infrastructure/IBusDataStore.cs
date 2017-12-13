using System;
using Resources.API.Models;

namespace Resources.API.Infrastructure
{
    public interface IBusDataStore
    {
        Bus GetById(Guid id);
        void Add(Bus bus);
        void Delete(Guid id);
        void Update(Bus bus);
    }
}