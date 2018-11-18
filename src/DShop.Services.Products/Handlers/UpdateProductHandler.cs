using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Services.Products.Messages.Commands;
using DShop.Services.Products.Messages.Events;
using DShop.Services.Products.Repositories;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public sealed class UpdateProductHandler : ICommandHandler<UpdateProduct>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IBusPublisher _busPublisher;

        public UpdateProductHandler(
            IProductsRepository productsRepository,
            IBusPublisher busPublisher)
        {
            _productsRepository = productsRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(UpdateProduct command, ICorrelationContext context)
        {
            var product = await _productsRepository.GetAsync(command.Id);
            if (product == null)
            {
                throw new DShopException("product_not_found",
                    $"Product with id: '{command.Id}' was not found.");
            }

            product.SetName(command.Name);
            product.SetDescription(command.Description);
            product.SetVendor(command.Vendor);
            product.SetPrice(command.Price);
            product.SetQuantity(command.Quantity);
            await _productsRepository.UpdateAsync(product);
            await _busPublisher.PublishAsync(new ProductUpdated(command.Id, command.Name,
                command.Description, command.Vendor, command.Price, command.Quantity), context);
        }
    }
}
