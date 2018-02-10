using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }
    }
}
