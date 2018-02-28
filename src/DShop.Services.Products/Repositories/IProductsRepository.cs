using DShop.Common.Mongo;
using DShop.Services.Products.Dtos;
using DShop.Services.Products.Entities;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Products.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(Guid id);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}