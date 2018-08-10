using DShop.Common.Handlers;
using DShop.Services.Products.Dto;
using DShop.Services.Products.Queries;
using DShop.Services.Products.Repositories;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public sealed class GetProductHandler : IQueryHandler<GetProduct, ProductDto>
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductHandler(IProductsRepository productsRepository)
            => _productsRepository = productsRepository;

        public async Task<ProductDto> HandleAsync(GetProduct query)
        {
            var product = await _productsRepository.GetAsync(query.Id);

            return product == null ? null : new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Vendor = product.Vendor,
                Price = product.Price
            };
        }
    }
}
