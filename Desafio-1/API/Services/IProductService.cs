
using Domain;

namespace API.Services
{
    public interface IProductService
    {
        public Task<Product> CreateProductAsync(Product product);
    }
}