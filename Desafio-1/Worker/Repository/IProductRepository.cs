using Domain;

namespace Worker.Repository
{
    public interface IProductRepository
    {
        public Task CreateProductAsync(Product product);
        public Task<List<Product>> ListProductsAsync();
        public Task<Product> GetProductByIdAsync(int id);
    }
}