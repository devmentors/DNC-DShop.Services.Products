using Microsoft.AspNetCore.Hosting;
using DShop.Common.Builders;
using Autofac;
using DShop.Services.Products.Repositories;
using DShop.Services.Products.Services;
using DShop.Messages.Commands.Products;
using DShop.Services.Products.Handlers;
using DShop.Common.Handlers;

namespace DShop.Services.Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>           
            ServiceBuilder
                .Create<Startup>(args)
                .WithPort(5001)
                .WithAutofac(containerBuilder => 
                {
                    containerBuilder.RegisterType<ProductsRepository>().As<IProductsRepository>();
                    containerBuilder.RegisterType<ProductsService>().As<IProductsService>();

                    containerBuilder.RegisterType<CreateProductHandler>().As<ICommandHandler<CreateProduct>>();
                    containerBuilder.RegisterType<UpdateProductHandler>().As<ICommandHandler<UpdateProduct>>();
                    containerBuilder.RegisterType<DeleteProductHandler>().As<ICommandHandler<DeleteProduct>>();
                })
                .WithMongoDb("mongo")
                .WithServiceBus("service-bus", subscribeBus => 
                {
                    subscribeBus.SubscribeToCommand<CreateProduct>();
                    subscribeBus.SubscribeToCommand<UpdateProduct>();
                    subscribeBus.SubscribeToCommand<DeleteProduct>();
                })
                .Build();
    }
}
