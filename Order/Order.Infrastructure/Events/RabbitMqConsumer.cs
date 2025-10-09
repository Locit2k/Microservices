using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Infrastructure.Events
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly string _exchangeName = "demo.ExchangeName";
        private readonly string _queueName = "demo.QueueName";
        private IConnection? _connection;
        private IModel? _channel;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: true);
            _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    Console.WriteLine($"[Consumer] Event: {ea.RoutingKey}, Data: {message}");
                    if (ea.RoutingKey == "order.created")
                    {
                        Console.WriteLine("order.created");
                    }
                    _channel.BasicAck(ea.DeliveryTag, false);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    _channel.BasicNack(ea.DeliveryTag, false, requeue: false);
                }
            };
            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
