using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DShop.Common.Databases.Mongo;
using DShop.Messages.ReadModels;
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

        public async Task<ProductDetailsReadModel> GetProductDetailsAsync(Guid id)
            => await Collection
                .Find(p => p.Id == id)
                .AsProductDetailsReadModels()
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<ProductReadModel>> GetFilteredProductsAsync(Expression<Func<Product, bool>> predicate)
            => await Collection
                .Find(predicate)
                .AsProductReadModels()
                .ToListAsync();
    }
}