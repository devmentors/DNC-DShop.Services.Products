using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Products.Messages.Events
{
    public class ProductUpdated : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public ProductUpdated(Guid id, string name,
            string description, decimal price,
            int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
    }
}
