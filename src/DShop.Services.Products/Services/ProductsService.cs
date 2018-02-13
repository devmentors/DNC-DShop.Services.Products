using DShop.Common.Types;
using DShop.Services.Products.Entities;
using DShop.Services.Products.Repositories;
using System;
using System.Threading.Tasks;

namespace DShop.Services.Products.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task CreateAsync(Guid id, string name, string description, string vendor, decimal price)
        {
            var product = new Product(id, name, description, vendor, price);
            await _productsRepository.CreateAsync(product);
        }

        public async Task UpdateAsync(Guid id, string name, string description, decimal price)
        {
            var product = await _productsRepository.GetByIdAsync(id);

            if(product == null)
            {
                throw new DShopException("Maybe we shall introduce some maybe pattern or derviced exceptions?");
            }

            product.SetName(name);
            product.SetDescription(description);
            product.SetPrice(price);

            await _productsRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
            => await _productsRepository.DeleteAsync(id);
    }
}