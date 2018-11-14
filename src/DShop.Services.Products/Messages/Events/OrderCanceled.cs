using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Products.Messages.Events
{
    [MessageNamespace("orders")]
    public class OrderCanceled : IEvent
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }

        [JsonConstructor]
        public OrderCanceled(Guid id, Guid customerId)
        {
            Id = id;
            CustomerId = customerId;
        }
    }
}
