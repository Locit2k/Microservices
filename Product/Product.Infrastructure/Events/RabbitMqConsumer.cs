using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IModel = RabbitMQ.Client.IModel;

namespace Product.Infrastructure.Events
{
    public class RabbitMqConsumer : BackgroundService
    {
        private IConnection? _connection;
        private IModel? _channel;
        private readonly string _exchangeName = "SAGA.ECommerce";
        private readonly string _queueName = "Order.OrderCreated";
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
            _channel.QueueDeclare("Order.OrderCreated", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind("Order.OrderCreated", _exchangeName, routingKey: "OrderCreated");

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    Console.WriteLine($"[Consumer] Event: {ea.RoutingKey}, Data: {message}");

                    switch (ea.RoutingKey)
                    {
                        case "OrderCreated":
                            Console.WriteLine("Order created success!");
                            break;
                        default:
                            Console.WriteLine("Not found event matched!");
                            break;
                    }

                    // ⚡ Acknowledge message
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    _channel.BasicNack(ea.DeliveryTag, false, requeue: false);
                }

                await Task.Yield();
            };

            // ⚠️ autoAck = false để đảm bảo chỉ ack khi xử lý thành công
            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            Console.WriteLine("✅ RabbitMQ consumer started...");
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
