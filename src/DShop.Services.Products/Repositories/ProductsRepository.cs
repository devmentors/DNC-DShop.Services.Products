using DShop.Common.Databases.Mongo;
using DShop.Services.Products.Entities;
using MongoDB.Driver;

namespace DShop.Services.Products.Repositories
{
	public class ProductsRepository : MongoRepository<Product>, IProductsRepository
	{
		protected ProductsRepository(IMongoDatabase database) : base(database, "Products")
		{
		}
	}
}