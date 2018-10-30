using DShop.Common.Handlers;
using DShop.Common.Mongo;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Services.Products.Domain;
using DShop.Services.Products.Handlers;
using DShop.Services.Products.Messages.Commands;
using DShop.Services.Products.Messages.Events;
using DShop.Services.Products.Repositories;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DShop.Services.Products.UnitTests.Handlers
{
    public class CreateProductHandlerTests
    {
        private Guid _id => Guid.Parse("3cde6f3d-aeef-404b-8c5f-a32277957a3c");
        private string _name => "product";
        private string _description => "description";
        private string _vendor => "vendor";
        private decimal _price => 10m;

        private CreateProduct _command => new CreateProduct(
            _id, _name, _description, _vendor, _price);

        public async Task Act(CreateProduct command)
            => await _commandHandler.HandleAsync(command, _context);

        [Fact]
        public async Task HandleAsync_Publishes_CreateProductRejected_If_Prodcut_With_Given_Name_Already_Exists()
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
        private readonly IHandler _handler;
        private readonly CreateProductHandler _commandHandler;

        public CreateProductHandlerTests()
        {
            _productsRepository = Substitute.For<IProductsRepository>();
            _busPublisher = Substitute.For<IBusPublisher>();
            _context = Substitute.For<ICorrelationContext>();
            _handler = new Handler();

            _commandHandler = new CreateProductHandler(_productsRepository, _handler, _busPublisher);
        }

#endregion
    }
}
