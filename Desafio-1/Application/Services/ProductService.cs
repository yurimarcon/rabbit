using System.Text.Json;
using API.Services;
using Application.DTOs;
using Application.RabbitMQ;

namespace Application.Services
{
    public class ProductService : IProductService 
    {
        private readonly IRabbitMQContext rabbitMQContext;
        // private readonly IProductRepository productRepository;

        public ProductService(IRabbitMQContext _rabbitMQContext)
        {
            rabbitMQContext = _rabbitMQContext;
        }
        public Task<ProductDTO> CreateProductAsync(ProductDTO product)
        {
            string productString = JsonSerializer.Serialize(product);
            rabbitMQContext.PublishMessage("products", productString);
            return Task.FromResult(product);
        }
    }
}