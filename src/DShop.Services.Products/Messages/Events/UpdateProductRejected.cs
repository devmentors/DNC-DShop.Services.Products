using DShop.Common.Messages;
using Newtonsoft.Json;
using System;

namespace DShop.Services.Products.Messages.Events
{
    public class UpdateProductRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public UpdateProductRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}
