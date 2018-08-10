using DShop.Common.Types;
using DShop.Services.Products.Dto;

namespace DShop.Services.Products.Queries
{
    public class BrowseProducts : PagedQueryBase, IQuery<PagedResult<ProductDto>>
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; } = decimal.MaxValue;
    }
}
