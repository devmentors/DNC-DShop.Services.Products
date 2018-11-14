using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Products.Messages.Events;
using DShop.Services.Products.Repositories;
using DShop.Services.Products.Services;

namespace DShop.Services.Products.Handlers
{
    public class OrderCreatedHandler : IEventHandler<OrderCreated>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IOrdersService _ordersService;

        public OrderCreatedHandler(IProductsRepository productsRepository,
            IOrdersService ordersService)
        {
            _productsRepository = productsRepository;
            _ordersService = ordersService;
        }

        public async Task HandleAsync(OrderCreated @event, ICorrelationContext context)
        {
            var order = await _ordersService.GetAsync(@event.Id);
            if (order == null)
            {
                return;
            }

            foreach (var orderItem in order.Items)
            {
                var product = await _productsRepository.GetAsync(orderItem.Id);
                if (product == null)
                {
                    continue;
                }

                product.SetQuantity(product.Quantity - orderItem.Quantity);
                await _productsRepository.UpdateAsync(product);
            }
        }
    }
}