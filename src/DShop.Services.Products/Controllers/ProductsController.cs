using System;
using System.Threading.Tasks;
using DShop.Services.Products.Dtos;
using DShop.Services.Products.Services;
using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Products.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
            dynamic a = 2;
            a.GetType();
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> GetProductDetailsAsync(Guid id)
            => await _productsService.GetProductByIdAsync(id);
    }
}
