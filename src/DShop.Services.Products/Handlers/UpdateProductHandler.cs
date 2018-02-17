using DShop.Common.Bus;
using DShop.Common.Handlers;
using DShop.Messages.Commands.Products;
using DShop.Messages.Events.Products;
using DShop.Services.Products.Services;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public sealed class UpdateProductHandler : ICommandHandler<UpdateProduct>
    {
        private readonly IProductsService _productsService;
        private readonly IPublishBus _publishBus;

        public UpdateProductHandler(IProductsService productsService, IPublishBus publishBus)
        {
            _productsService = productsService;
            _publishBus = publishBus;
        }

        public async Task HandleAsync(UpdateProduct command, ICorrelationContext context)
        {
            await _productsService.UpdateAsync(command.Id, command.Name, command.Description, command.Price);
            await _publishBus.PublishEventAsync(new ProductUpdated(context.ResourceId, context.UserId));
        }
    }
}
