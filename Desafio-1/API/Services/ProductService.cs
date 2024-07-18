using System.Text.Json;
using Domain;
using API.RabbitMQ;

namespace API.Services
{
    public class ProductService : IProductService 
    {
        public Task<Product> CreateProductAsync(Product product)
        {
            string productString = JsonSerializer.Serialize(product);
            var rabbit = new RabbitMQContext();
            rabbit.PublishMessage("products", productString);
            return Task.FromResult(product);
        }
    }
}