using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Products.Messages.Commands;
using DShop.Services.Products.Messages.Events;
using DShop.Services.Products.Domain;
using DShop.Services.Products.Repositories;
using System.Threading.Tasks;
using DShop.Common.Types;

namespace DShop.Services.Products.Handlers
{
    public sealed class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IBusPublisher _busPublisher;

        public CreateProductHandler(
            IProductsRepository productsRepository,
            IBusPublisher busPublisher)
        {
            _productsRepository = productsRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateProduct command, ICorrelationContext context)
        {
            if (command.Quantity < 0)
            {
                throw new DShopException("invalid_product_quantity",
                    "Product quantity cannot be negative.");
            }

            if (await _productsRepository.ExistsAsync(command.Name))
            {
                throw new DShopException("product_already_exists",
                    $"Product: '{command.Name}' already exists.");
            }

            var product = new Product(command.Id, command.Name,
                command.Description, command.Vendor, command.Price, command.Quantity);
            await _productsRepository.AddAsync(product);
            await _busPublisher.PublishAsync(new ProductCreated(command.Id, command.Name,
                command.Description, command.Vendor, command.Price, command.Quantity), context);
        }
    }
}
