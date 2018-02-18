using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Products;
using DShop.Messages.Events.Products;
using DShop.Services.Products.Services;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public sealed class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IProductsService _productsService;
        private readonly IBusPublisher _busPublisher;

        public CreateProductHandler(IProductsService productsService, IBusPublisher busPublisher)
        {
            _productsService = productsService;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateProduct command, ICorrelationContext context)
        {
            await _productsService.CreateAsync(command.Id, command.Name, command.Description, command.Vendor, command.Price);
            await _busPublisher.PublishEventAsync(new ProductCreated(context.ResourceId, context.UserId));
        }
    }
}
