using DShop.Common.Databases;
using DShop.Services.Products.Dtos;
using DShop.Services.Products.Entities;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Products.Repositories
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<ProductDto> GetProductByIdAsync(Guid id);
    }
}