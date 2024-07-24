
namespace Application.RabbitMQ
{
    public interface IRabbitMQContext
    {
        public Task PublishMessage(string queueName, string message);
    }
}