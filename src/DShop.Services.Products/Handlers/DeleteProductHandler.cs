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
        private readonly IHandler _handler;
        private readonly IBusPublisher _busPublisher;

        public DeleteProductHandler(
            IProductsRepository productsRepository,
            IHandler handler,
            IBusPublisher busPublisher)
        {
            _productsRepository = productsRepository;
            _handler = handler;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(DeleteProduct command, ICorrelationContext context)
            => await _handler
                .Handle(async () =>
                {
                    if (!await _productsRepository.ExistsAsync(command.Id))
                    {   
                        throw new DShopException("product_not_found",
                            $"Product with id: '{command.Id}' was not found.");
                    }
                    await _productsRepository.DeleteAsync(command.Id);
                })
                .OnSuccess(async () => await _busPublisher.PublishAsync(
                    new ProductDeleted(command.Id), context)
                )
                .OnCustomError(async ex => await _busPublisher.PublishAsync(
                    new DeleteProductRejected(command.Id, ex.Message, ex.Code), context)
                )
                .OnError(async ex => await _busPublisher.PublishAsync(
                    new DeleteProductRejected(command.Id, ex.Message, "delete_product_failed"), context)
                )
                .ExecuteAsync();
    }
}
