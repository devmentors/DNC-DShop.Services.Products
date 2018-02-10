using DShop.Messages.Entities;

namespace DShop.Services.Products.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Descirption { get; set; }
        public string Vendor { get; set; }
        public decimal Price { get; set; }
    }
}