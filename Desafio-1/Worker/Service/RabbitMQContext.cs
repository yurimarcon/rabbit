using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Worker.Repository;

namespace Worker.Service
{
    public class RabbitMQContext
    {
        private readonly IProductRepository _productRepository;

        public RabbitMQContext(IProductRepository productRepository)
        {
            _productRepository = productRepository;            
        }
        public void SearchInQueue(string queueName)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
            var prod  = JsonSerializer.Deserialize<Product>(message);
            await _productRepository.CreateProductAsync(prod);
        };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}