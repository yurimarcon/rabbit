using API.Services;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace API.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("CreateProuct")]
        public async Task<IActionResult> Create(Product product)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.CreateProductAsync(product);

            return Ok(product);
        }

        [HttpGet("ListProducts")]
        public IActionResult Get()
        {
            return Ok();
        }

        // Chalege VVV

        [HttpGet("ValueTotalOrders")]
        public IActionResult ValueTotalOrders()
        {
            return Ok("R$1.000,00");
        }

        [HttpGet("QuantityOrderByClient")]
        public IActionResult QuantityOrderByClient()
        {
            return Ok("R$1.000,00");
        }

        [HttpGet("ListOrderByClient")]
        public IActionResult ListOrderByClient()
        {
            return Ok("1,2");
        }
    }
}