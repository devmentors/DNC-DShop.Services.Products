using System;
using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Products.Handlers;
using DShop.Services.Products.Messages.Commands;
using DShop.Services.Products.Messages.Events;
using DShop.Services.Products.Repositories;
using NSubstitute;
using Xunit;

namespace DShop.Services.Products.Tests.Handlers
{
    public class CreateProductHandlerTests
    {
        private Guid _id => Guid.Parse("3cde6f3d-aeef-404b-8c5f-a32277957a3c");
        private string _name => "product";
        private string _description => "description";
        private string _vendor => "vendor";
        private decimal _price => 10m;
        private int _quantity = 10;

        private CreateProduct _command => new CreateProduct(
            _id, _name, _description, _vendor, _price, _quantity);

        private async Task Act(CreateProduct command)
            => await _commandHandler.HandleAsync(command, _context);

        [Fact]

        public async Task handle_async_published_create_product_rejecte_if_product_with_given_name_already_exists()
        {
            _productsRepository
                .ExistsAsync(_command.Name)
                .Returns(true);

            await Act(_command);

            await _busPublisher
                .Received()
                .PublishAsync(Arg.Is<CreateProductRejected>(e =>
                    e.Id == _command.Id
                    && e.Code == "product_already_exists"
                    && e.Reason == $"Product: '{_command.Name}' already exists."), _context);
        }

        #region ARRANGE

        private readonly IProductsRepository _productsRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly ICorrelationContext _context;
        private readonly CreateProductHandler _commandHandler;

        public CreateProductHandlerTests()
        {
            _productsRepository = Substitute.For<IProductsRepository>();
            _busPublisher = Substitute.For<IBusPublisher>();
            _context = Substitute.For<ICorrelationContext>();

            _commandHandler = new CreateProductHandler(_productsRepository, _busPublisher);
            _commandHandler = new CreateProductHandler(_productsRepository, _busPublisher);
        }

        #endregion
    }
}
