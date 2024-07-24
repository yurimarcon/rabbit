using System.Text;
using RabbitMQ.Client;

namespace Application.RabbitMQ
{
    public class RabbitMQContext : IRabbitMQContext
    {
        public Task PublishMessage(string queueName, string message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                routingKey: queueName,
                                basicProperties: null,
                                body: body);
                                
            return Task.CompletedTask;
        }
    }
}