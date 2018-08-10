using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Products.Messages.Events
{
    public class ProductUpdated : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ProductUpdated(Guid id)
        {
            Id = id;
        }
    }
}
