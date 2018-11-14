using DShop.Common.Messages;
using Newtonsoft.Json;
using System;

namespace DShop.Services.Products.Messages.Commands
{
	public class CreateProduct : ICommand
	{
		public Guid Id { get; }
		public string Name { get; }
		public string Description { get; }
		public string Vendor { get; }
		public decimal Price { get; }
		public int Quantity { get; }

		[JsonConstructor]
		public CreateProduct(Guid id, string name,
			string description, string vendor,
			decimal price, int quantity)
		{
			Id = id;
			Name = name;
			Description = description;
			Vendor = vendor;
			Price = price;
			Quantity = quantity;
		}
	}
}