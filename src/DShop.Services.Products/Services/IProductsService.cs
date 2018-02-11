using System;
using System.Threading.Tasks;

namespace DShop.Services.Products.Services
{
    public interface IProductsService
    {
        Task CreateAsync(Guid id, string name, string description, string vendor, decimal price);
        Task UpdateAsync(Guid id, string name, string description, decimal price);
        Task DeleteAsync(Guid id);
    }
}