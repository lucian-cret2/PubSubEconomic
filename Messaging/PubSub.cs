﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Messaging
{
    public class PubSub<T> : IPubSub<T> where T : class
    {
        private readonly IModel _channel;

        public PubSub()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public async Task SubscribeAsync(string exchangeName, string topic, Func<T, Task> handler)
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

            var queueName = _channel.QueueDeclare().QueueName;

            _channel.QueueBind(queue: queueName,
                   exchange: exchangeName,
                   routingKey: topic);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageAsString = Encoding.UTF8.GetString(body);
                T message = JsonSerializer.Deserialize<T>(messageAsString);

                handler(message);
            };
            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public async Task PublishAsync(string exchangeName, string topic, T message)
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            _channel.BasicPublish(exchange: exchangeName,
                                 routingKey: topic,
                                 basicProperties: null,
                                 body: body);
        }
    }
}