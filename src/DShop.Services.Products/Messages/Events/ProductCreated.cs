using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Products.Messages.Events
{
    public class ProductCreated : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ProductCreated(Guid id)
        {
            Id = id;
        }
    }
}
