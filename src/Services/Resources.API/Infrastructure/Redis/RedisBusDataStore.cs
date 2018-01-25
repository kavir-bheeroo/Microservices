using System;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microservices.Services.Resources.API.Configuration;
using Microservices.Services.Resources.API.Models;
using Microservices.Services.Resources.API.Infrastructure.Redis;
using StackExchange.Redis;

namespace Microservices.Services.Resources.API.Infrastructure.Redis
{
    public class RedisBusDataStore : IBusDataStore
    {
        private static IDatabase _redisDatabase;
        private readonly EndpointsConfig _endpointsConfig;

        private static IDatabase RedisDatabase => _redisDatabase ?? RedisConnectorHelper.Connection.GetDatabase();

        public RedisBusDataStore(IOptions<EndpointsConfig> endpointsConfig)
        {
            if (endpointsConfig == null)throw new ArgumentNullException(nameof(endpointsConfig));

            _endpointsConfig = endpointsConfig.Value;
            RedisConnectorHelper.RedisEndpoint = _endpointsConfig.Redis;
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