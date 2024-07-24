using Application.DTOs;

namespace API.Services
{
    public interface IProductService
    {
        public Task<ProductDTO> CreateProductAsync(ProductDTO product);
    }
}