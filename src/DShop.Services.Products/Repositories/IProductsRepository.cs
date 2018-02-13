using DShop.Common.Databases;
using DShop.Messages.ReadModels;
using DShop.Services.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DShop.Services.Products.Repositories
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<ProductDetailsReadModel> GetProductDetailsAsync(Guid id);
        Task<IEnumerable<ProductReadModel>> GetFilteredProductsAsync(Expression<Func<Product, bool>> predicate);
    }
}