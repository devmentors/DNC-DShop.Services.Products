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
        private readonly IHandler _handler;
        private readonly IBusPublisher _busPublisher;


        public UpdateProductHandler(
            IProductsRepository productsRepository,
            IHandler handler,
            IBusPublisher busPublisher)
        {
            _productsRepository = productsRepository;
            _handler = handler;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(UpdateProduct command, ICorrelationContext context)
            => await _handler
                .Handle(async () =>
                {
                    var product = await _productsRepository.GetAsync(command.Id);

                    if (product == null)
                    {
                        throw new DShopException("Product does not exist.");
                    }

                    product.SetName(command.Name);
                    product.SetDescription(command.Description);
                    product.SetPrice(command.Price);

                    await _productsRepository.UpdateAsync(product);
                })
                .OnSuccess(async () =>
                {
                    await _busPublisher.PublishAsync(new ProductUpdated(command.Id), context);
                })
                .ExecuteAsync();
    }
}
