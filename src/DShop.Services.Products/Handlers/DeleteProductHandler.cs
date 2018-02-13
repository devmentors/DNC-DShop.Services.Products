using DShop.Common.Bus;
using DShop.Common.Handlers;
using DShop.Messages.Commands.Products;
using DShop.Services.Products.Services;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public class DeleteProductHandler : ICommandHandler<DeleteProduct>
    {
        private readonly IProductsService _productsService;
        private readonly IPublishBus _publishBus;

        public DeleteProductHandler(IProductsService productsService, IPublishBus publishBus)
        {
            _productsService = productsService;
            _publishBus = publishBus;
        }

        public async Task HandleAsync(DeleteProduct command, ICorrelationContext context)
        {
            await _productsService.DeleteAsync(command.Id);
        }
    }
}
