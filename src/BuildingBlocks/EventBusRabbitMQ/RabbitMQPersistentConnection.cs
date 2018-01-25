using System;
using System.Net.Sockets;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace Microservices.BuildingBlocks.EventBusRabbitMQ
{
    public class RabbitMQPersistentConnection : IDisposable
    {
        private readonly IConnectionFactory _connectionFactory;
        internal IConnection _connection;
        private bool _disposed;

        object sync_lock = new object();

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new System.ArgumentNullException(nameof(connectionFactory));
        }

        public bool IsConnected 
        { 
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateModel()
        {
            return _connection.CreateModel();
        }

        public bool TryConnect()
        {
            lock (sync_lock)
            {
                var policy = Policy
                    .Handle<BrokerUnreachableException>()
                    .Or<SocketException>()
                    .WaitAndRetry(5, (retryCount) => TimeSpan.FromSeconds(2 << retryCount));

                policy.Execute(() =>
                { 
                    _connection = _connectionFactory.CreateConnection();
                });
                
                return _connection.IsOpen;
            }
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _connection.Dispose();
        }
    }
}