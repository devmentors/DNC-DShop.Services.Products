using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DShop.Common;
using DShop.Common.Dispatchers;
using DShop.Common.Mongo;
using DShop.Common.Mvc;
using DShop.Common.RabbitMq;
using DShop.Common.Redis;
using DShop.Common.Swagger;
using DShop.Services.Products.Messages.Commands;
using DShop.Services.Products.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DShop.Common.Consul;
using Consul;
using DShop.Common.Jaeger;
using DShop.Services.Products.Messages.Events;

namespace DShop.Services.Products
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddRedis();
            services.AddInitializers(typeof(IMongoDbInitializer));

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq();
            builder.AddMongo();
            builder.AddMongoRepository<Product>("Products");
            builder.AddDispatchers();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, IConsulClient client,
            IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();
            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateProduct>(onError: (c, e) =>
                    new CreateProductRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<UpdateProduct>(onError: (c, e) =>
                    new UpdateProductRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<DeleteProduct>(onError: (c, e) =>
                    new DeleteProductRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<ReserveProducts>(onError: (c, e) =>
                    new ReserveProductsRejected(c.OrderId, e.Message, e.Code))
                .SubscribeCommand<ReleaseProducts>(onError: (c, e) =>
                    new ReleaseProductsRejected(c.OrderId, e.Message, e.Code));

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                Container.Dispose();
            });

            startupInitializer.InitializeAsync();
        }
    }
}
