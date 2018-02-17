using DShop.Services.Products.Dtos;
using DShop.Services.Products.Entities;
using MongoDB.Driver;

namespace DShop.Services.Products.Extensions
{
    public static class ProductDtoExtensions
    {
        public static IFindFluent<Product, ProductDto> AsProductDtos(this IFindFluent<Product, Product> findFluent)
            => findFluent.Project(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Descirption = p.Descirption,
                Vendor = p.Vendor,
                Price = p.Price
            });
    }
}
