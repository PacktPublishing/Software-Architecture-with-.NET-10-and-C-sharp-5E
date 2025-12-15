

using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
