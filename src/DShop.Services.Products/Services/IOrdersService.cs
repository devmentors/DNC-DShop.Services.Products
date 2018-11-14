using System;
using System.Threading.Tasks;
using DShop.Services.Products.Dto;
using RestEase;

namespace DShop.Services.Products.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IOrdersService
    {
        [AllowAnyStatusCode]
        [Get("orders/{id}")]
        Task<OrderDto> GetAsync([Path] Guid id);
    }
}