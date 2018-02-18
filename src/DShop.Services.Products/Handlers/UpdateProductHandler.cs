using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Products;
using DShop.Messages.Events.Products;
using DShop.Services.Products.Services;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public sealed class UpdateProductHandler : ICommandHandler<UpdateProduct>
    {
        private readonly IProductsService _productsService;
        private readonly IBusPublisher _busPublisher;


        public UpdateProductHandler(IProductsService productsService, IBusPublisher busPublisher)
        {
            _productsService = productsService;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(UpdateProduct command, ICorrelationContext context)
        {
            await _productsService.UpdateAsync(command.Id, command.Name, command.Description, command.Price);
            await _busPublisher.PublishEventAsync(new ProductUpdated(context.ResourceId, context.UserId));
        }
    }
}
