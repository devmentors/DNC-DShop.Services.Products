using DShop.Common.Mongo;
using DShop.Services.Products.Dtos;
using DShop.Services.Products.Entities;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Products.Repositories
{
    public interface IProductsRepository : IMongoRepository<Product>
    {
        Task<ProductDto> GetProductByIdAsync(Guid id);
    }
}