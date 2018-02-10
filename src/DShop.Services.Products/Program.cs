﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DShop.Common.Builders;

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
                .WithRegistration(containerBuilder => 
                {
                })
                .WithMongoDb("mongo")
                .WithServiceBus("service-bus", subscribeBus => 
                {
                })
                .Build();
    }
}
