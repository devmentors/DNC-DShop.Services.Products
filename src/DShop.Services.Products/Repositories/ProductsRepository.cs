using System;
using System.Threading.Tasks;
using DShop.Common.Databases.Mongo;
using DShop.Services.Products.Dtos;
using DShop.Services.Products.Entities;
using DShop.Services.Products.Extensions;
using MongoDB.Driver;

namespace DShop.Services.Products.Repositories
{
    public class ProductsRepository : MongoRepository<Product>, IProductsRepository
	{
		public ProductsRepository(IMongoDatabase database) : base(database, "Products")
		{
		}

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
            => await Collection
                .Find(p => p.Id == id)
                .AsProductDtos()
                .SingleOrDefaultAsync();
    }
}