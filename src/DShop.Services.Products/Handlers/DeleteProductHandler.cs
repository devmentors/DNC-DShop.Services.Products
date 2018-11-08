using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Services.Products.Messages.Commands;
using DShop.Services.Products.Messages.Events;
using DShop.Services.Products.Repositories;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public sealed class DeleteProductHandler : ICommandHandler<DeleteProduct>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IBusPublisher _busPublisher;

        public DeleteProductHandler(
            IProductsRepository productsRepository,
            IBusPublisher busPublisher)
        {
            _productsRepository = productsRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(DeleteProduct command, ICorrelationContext context)
        {
            if (!await _productsRepository.ExistsAsync(command.Id))
            {
                throw new DShopException("product_not_found",
                    $"Product with id: '{command.Id}' was not found.");
            }

            await _productsRepository.DeleteAsync(command.Id);
            await _busPublisher.PublishAsync(new ProductDeleted(command.Id), context);
        }
    }
}
