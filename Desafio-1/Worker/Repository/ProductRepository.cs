using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Worker.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ProductRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task CreateProductAsync(Product product)
        {
            Console.WriteLine("Entrou no Repository");
            if (string.IsNullOrEmpty(product.Name))
            {
                Console.WriteLine("Name is required!!!");
            }

            try
            {
                await _databaseContext.Products.AddAsync(product);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errooooo!!!");
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> ListProductsAsync()
        {
            return await _databaseContext.Products.ToListAsync();
        }
    }
}