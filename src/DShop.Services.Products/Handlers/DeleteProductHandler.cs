using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Products;
using DShop.Messages.Events.Products;
using DShop.Services.Products.Services;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public class DeleteProductHandler : ICommandHandler<DeleteProduct>
    {
        private readonly IProductsService _productsService;
        private readonly IBusPublisher _busPublisher;

        public DeleteProductHandler(IProductsService productsService, IBusPublisher busPublisher)
        {
            _productsService = productsService;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(DeleteProduct command, ICorrelationContext context)
        {
            await _productsService.DeleteAsync(command.Id);
            await _busPublisher.PublishEventAsync(new ProductDeleted(context.ResourceId, context.UserId));
        }
    }
}
