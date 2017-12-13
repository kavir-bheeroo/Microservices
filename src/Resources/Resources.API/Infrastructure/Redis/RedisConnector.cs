using System;
using StackExchange.Redis;

namespace Resources.API.Infrastructure.Redis
{
    public class RedisConnectorHelper  
    {                  
        static RedisConnectorHelper()  
        {  
            RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>  
            {  
                return ConnectionMultiplexer.Connect("localhost:32768");  
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
    }  

}