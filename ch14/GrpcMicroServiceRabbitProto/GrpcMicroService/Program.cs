using DBDriver.Extensions;
using DDD.ApplicationLayer;
using GrpcMicroService.HostedServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddApplicationServices();
        services.AddDbDriver(hostContext.Configuration.GetConnectionString("DefaultConnection")??string.Empty);
        services.AddHostedService<ProcessPurchases>();
    })
    .Build();

await host.RunAsync();
