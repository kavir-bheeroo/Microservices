using System;
using System.Net.Sockets;
using System.Text;
using Microservices.BuildingBlocks.EventBus.Abstractions;
using Microservices.BuildingBlocks.EventBus.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace Microservices.BuildingBlocks.EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus
    {
        const string BROKER_NAME = "microservices_event_bus";

        private readonly RabbitMQPersistentConnection _persistentConnection;
        private readonly string _queueName;

        public EventBusRabbitMQ(RabbitMQPersistentConnection persistentConnection, string queueName)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _queueName = queueName ?? throw new ArgumentNullException(nameof(queueName));
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

             var policy = Policy
                    .Handle<BrokerUnreachableException>()
                    .Or<SocketException>()
                    .WaitAndRetry(5, (retryCount) => TimeSpan.FromSeconds(2 << retryCount));

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.ExchangeDeclare(exchange: BROKER_NAME,
                                    type: "direct");

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                
                policy.Execute(() =>
                { 
                    channel.BasicPublish(exchange: BROKER_NAME,
                                        routingKey: eventName,
                                        basicProperties: null,
                                        body: body);
                });
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            if (!_persistentConnection.IsConnected)
                _persistentConnection.TryConnect();

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = typeof(T).Name;
                
                channel.QueueBind(queue:_queueName,
                                exchange: BROKER_NAME,
                                routingKey: eventName);
            }
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = typeof(T).Name;

                channel.QueueUnbind(queue: _queueName,
                    exchange: BROKER_NAME,
                    routingKey: eventName);
            }
        }
    }
}
