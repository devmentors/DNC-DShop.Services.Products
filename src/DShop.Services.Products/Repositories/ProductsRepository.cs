using System;
using System.Threading.Tasks;
using DShop.Common.Mongo;
using DShop.Services.Products.Dtos;
using DShop.Services.Products.Entities;
using DShop.Services.Products.Extensions;
using MongoDB.Driver;

namespace DShop.Services.Products.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IMongoRepository<Product> _repository;

        public ProductsRepository(IMongoRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task CreateAsync(Product product)
            => await _repository.CreateAsync(product);

        public async Task UpdateAsync(Product product)
            => await _repository.UpdateAsync(product);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}