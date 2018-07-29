using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Products;
using DShop.Messages.Events.Products;
using DShop.Services.Products.Entities;
using DShop.Services.Products.Repositories;
using System.Threading.Tasks;

namespace DShop.Services.Products.Handlers
{
    public sealed class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IHandler _handler;
        private readonly IBusPublisher _busPublisher;

        public CreateProductHandler(
            IProductsRepository productsRepository,
            IHandler handler,
            IBusPublisher busPublisher)
        {
            _productsRepository = productsRepository;
            _handler = handler;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateProduct command, ICorrelationContext context)
            => await _handler
                .Handle(async () =>
                {
                    var product = new Product(command.Id, command.Name, command.Description, command.Vendor, command.Price);
                    await _productsRepository.CreateAsync(product);
                })
                .OnSuccess(async () =>
                {
                    await _busPublisher.PublishEventAsync(new ProductCreated(command.Id), context);
                })
                .OnDShopError(async Exception => 
                {
                    await _busPublisher.PublishEventAsync(new UpdateProductRejected(command.Id, Exception.Message, Exception.Code), context);
                })
                .ExecuteAsync();        
    }
}
