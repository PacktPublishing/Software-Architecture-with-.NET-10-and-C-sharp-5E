

using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PackagesManagementDB.Models;
using System;

namespace DBDriver.Extensions
{
    public static class DBExtensions
    {
        public static IServiceCollection AddDbDriver(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<IUnitOfWork, MainDbContext>(options =>
                options.UseSqlServer(connectionString, 
                    b => b.MigrationsAssembly(typeof(DBExtensions).Assembly.GetName().Name)));
            services.AddAllRepositories(typeof(DBExtensions).Assembly);
            return services;
        }
        private static async Task Seed(MainDbContext context)
        {
            if (!await context.Destinations.AnyAsync())
            {
                var firstDestination = new Destination
                {
                    Name = "Florence",
                    Country = "Italy",
                    Packages = new List<Package>()
                        {
                            new Package
                            {
                                Name = "Summer in Florence",
                                StartValidityDate = new DateTime(2019, 6, 1),
                                EndValidityDate = new DateTime(2019, 10, 1),
                                DurationInDays=7,
                                Price=1000,
                                EntityVersion=1
                            },
                            new Package
                            {
                                Name = "Winter in Florence",
                                StartValidityDate = new DateTime(2019, 12, 1),
                                EndValidityDate = new DateTime(2020, 2, 1),
                                DurationInDays=7,
                                Price=500,
                                EntityVersion=1
                            }
                        }
                };
                context.Destinations.Add(firstDestination);
                await context.SaveChangesAsync();
            }
        } 
        public static async Task<IServiceProvider> UseDatabase(this
            IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider
                .GetRequiredService<IUnitOfWork>() as MainDbContext;
            if (context != null)
            {
                await context.Database.MigrateAsync();
                await Seed(context);
            }  
            return serviceProvider;
        }

    }
}
