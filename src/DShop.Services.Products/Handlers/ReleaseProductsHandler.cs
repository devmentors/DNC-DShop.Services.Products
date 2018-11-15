using System;
using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Products.Messages.Commands;
using DShop.Services.Products.Messages.Events;
using DShop.Services.Products.Repositories;

namespace DShop.Services.Products.Handlers
{
    public class ReleaseProductsHandler : ICommandHandler<ReleaseProducts>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IProductsRepository _productsRepository;

        public ReleaseProductsHandler(IBusPublisher busPublisher,
            IProductsRepository productsRepository)
        {
            _busPublisher = busPublisher;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(ReleaseProducts command, ICorrelationContext context)
        {
            foreach ((Guid productId, int quantity) in command.Products)
            {
                var product = await _productsRepository.GetAsync(productId);
                if (product == null)
                {
                    continue;
                }

                product.SetQuantity(product.Quantity + quantity);
                await _productsRepository.UpdateAsync(product);
            }

            await _busPublisher.PublishAsync(new ProductsReleased(command.OrderId,
                command.Products), context);
        }
    }
}