using DShop.Common.Types;
using DShop.Services.Products.Domain;
using DShop.Services.Products.Queries;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Products.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(Guid id);
        Task<PagedResult<Product>> BrowseAsync(BrowseProducts query);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}