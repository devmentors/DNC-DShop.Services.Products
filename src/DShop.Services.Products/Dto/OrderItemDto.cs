using System;

namespace DShop.Services.Products.Dto
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}