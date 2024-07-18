using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Task CreateProductAsync(Product product)
        {
            _databaseContext.Products.Add(product);
            return Task.CompletedTask;
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