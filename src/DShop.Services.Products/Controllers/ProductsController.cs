using System.Threading.Tasks;
using DShop.Common.Dispatchers;
using DShop.Common.Types;
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

        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductDto>>> Get(BrowseProducts query)
            => Collection(await DispatchAsync<BrowseProducts, PagedResult<ProductDto>>(query));

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetAsync([FromRoute] GetProduct query)
            => Single(await DispatchAsync<GetProduct, ProductDto>(query));
    }
}
