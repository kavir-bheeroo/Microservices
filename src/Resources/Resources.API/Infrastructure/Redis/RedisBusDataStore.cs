using System;
using Newtonsoft.Json;
using Resources.API.Models;
using StackExchange.Redis;

namespace Resources.API.Infrastructure.Redis
{
    public class RedisBusDataStore : IBusDataStore
    {
        private static IDatabase _redisDatabase;
        private static IDatabase RedisDatabase => _redisDatabase ?? RedisConnectorHelper.Connection.GetDatabase();

        public RedisBusDataStore()
        {
            _redisDatabase = RedisConnectorHelper.Connection.GetDatabase();
        }

        public void Add(Bus bus)
        {
            var serializedObject = JsonConvert.SerializeObject(bus);
            RedisDatabase.StringSet(bus.Id.ToString(), serializedObject);
        }

        public void Delete(Guid id)
        {
            RedisDatabase.KeyDelete(id.ToString());
        }

        public Bus GetById(Guid id)
        {
            var serializedData = RedisDatabase.StringGet(id.ToString()).ToString();

            if (serializedData == null)
                return null;

            return JsonConvert.DeserializeObject<Bus>(serializedData);
        }

        public void Update(Bus bus)
        {
            Delete(bus.Id);
            Add(bus);
        }
    }
}