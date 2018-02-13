using DShop.Common.Bus;
using DShop.Common.Handlers;
using DShop.Messages.Commands.Products;
using DShop.Services.Products.Services;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public sealed class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IProductsService _productsService;
        private readonly IPublishBus _publishBus;

        public CreateProductHandler(IProductsService productsService, IPublishBus publishBus)
        {
            _productsService = productsService;
            _publishBus = publishBus;
        }

        public async Task HandleAsync(CreateProduct command, ICorrelationContext context)
        {
            await _productsService.CreateAsync(command.Id, command.Name, command.Description, command.Vendor, command.Price);
        }
    }
}
