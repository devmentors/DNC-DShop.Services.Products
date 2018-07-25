using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Products;
using DShop.Messages.Events.Products;
using DShop.Services.Products.Repositories;
using DShop.Services.Products.Services;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public class DeleteProductHandler : ICommandHandler<DeleteProduct>
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
                    await _productsRepository.DeleteAsync(command.Id);
                })
                .OnSuccess(async () =>
                {
                    await _busPublisher.PublishEventAsync(new ProductDeleted(command.Id), context);
                })
                .ExecuteAsync();
    }
}
