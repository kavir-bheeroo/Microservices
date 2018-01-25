using System;
using StackExchange.Redis;

namespace Microservices.Services.Resources.API.Infrastructure.Redis
{
    public class RedisConnectorHelper  
    {                  
        static RedisConnectorHelper()  
        {  
            RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>  
            {
                System.Console.WriteLine(RedisEndpoint);
                return ConnectionMultiplexer.Connect(RedisEndpoint);  
            });  
        }  
          
        private static Lazy<ConnectionMultiplexer> lazyConnection;          
      
        public static ConnectionMultiplexer Connection  
        {  
            get  
            {  
                return lazyConnection.Value;  
            }  
        } 

        private static string _redisEndpoint;

        public static string RedisEndpoint
        { 
            get
            {
                return _redisEndpoint ?? throw new Exception($"{nameof(RedisEndpoint)} cannot be null");
            }

            set
            {
                _redisEndpoint = value;
            }
        }
    }  

}