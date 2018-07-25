using System.Threading.Tasks;
using DShop.Common.Dispatchers;
using DShop.Services.Products.Dtos;
using DShop.Services.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Products.Controllers
{
    [Route("[controller]")]
    public class ProductsController : BaseController
    {
        public ProductsController(IDispatcher dispatcher)
            :base(dispatcher)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetAsync([FromRoute] GetProduct query)
            => Single(await DispatchAsync<GetProduct, ProductDto>(query));
    }
}
