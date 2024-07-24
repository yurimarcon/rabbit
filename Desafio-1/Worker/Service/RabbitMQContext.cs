using System.Text;
using System.Text.Json;
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
            try
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

                    try
                    {
                        var prod = JsonSerializer.Deserialize<Product>(message);
                        if (prod != null)
                        {
                            await _productRepository.CreateProductAsync(prod);
                            Console.WriteLine(" [x] Product saved to database.");
                        }
                        else
                        {
                            Console.WriteLine(" [x] Received invalid product data.");
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.WriteLine($" [x] JSON deserialization error: {jsonEx.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" [x] Error saving product: {ex.Message}");
                    }
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("===> Can't connect with RabbitMQ!");
                Console.WriteLine($"===> Exception: {ex.Message}");
            }

        }

    }
}