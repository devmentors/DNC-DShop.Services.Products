using DShop.Messages.ReadModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DShop.Services.Products.Services
{
    public interface IProductsService
    {
        Task<ProductDetailsReadModel> GetProductDetailsAsync(Guid id);
        Task<IEnumerable<ProductReadModel>> GetAllProductsAsync();
        Task<IEnumerable<ProductReadModel>> GetProductsByVendorAsync(string vendor);

        Task CreateAsync(Guid id, string name, string description, string vendor, decimal price);
        Task UpdateAsync(Guid id, string name, string description, decimal price);
        Task DeleteAsync(Guid id);
    }
}