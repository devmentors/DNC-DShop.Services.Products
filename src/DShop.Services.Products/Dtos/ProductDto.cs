using System;

namespace DShop.Services.Products.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public decimal Price { get; set; }
    }
}
