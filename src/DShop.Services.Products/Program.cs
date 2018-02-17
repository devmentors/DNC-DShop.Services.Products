using Microsoft.AspNetCore.Hosting;
using DShop.Common.Builders;
using Autofac;
using DShop.Services.Products.Repositories;
using DShop.Services.Products.Services;
using DShop.Messages.Commands.Products;
using DShop.Services.Products.Handlers;
using DShop.Common.Handlers;
using Microsoft.AspNetCore;

namespace DShop.Services.Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
