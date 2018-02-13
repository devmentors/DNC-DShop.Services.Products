using DShop.Messages.ReadModels;
using DShop.Services.Products.Entities;
using MongoDB.Driver;

namespace DShop.Services.Products.Extensions
{
    public static class ProductDetailsReadModelExtensions
    {
        public static IFindFluent<Product, ProductReadModel> AsProductReadModels(this IFindFluent<Product, Product> findFluent)
            => findFluent.Project(p => new ProductReadModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });

        public static IFindFluent<Product, ProductDetailsReadModel> AsProductDetailsReadModels(this IFindFluent<Product, Product> findFluent)
            => findFluent.Project(p => new ProductDetailsReadModel
            {
                Id = p.Id,
                Name = p.Name,
                Descirption = p.Descirption,
                Vendor = p.Vendor,
                Price = p.Price
            });
    }
}
