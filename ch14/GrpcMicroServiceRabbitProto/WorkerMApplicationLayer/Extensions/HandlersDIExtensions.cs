using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DDD.DomainLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
#nullable disable
namespace DDD.ApplicationLayer
{
    public static class HandlersDIExtensions
    {
        public static IServiceCollection AddApplicationServices
            (this IServiceCollection services, Assembly assembly)

        {
            AddAllQueries(services, assembly);
            AddAllCommandHandlers(services, assembly);
            AddAllEventHandlers(services, assembly);
            services.AddScoped<EventMediator>();
            return services;
        }
        public static IServiceCollection AddApplicationServices
            (this IServiceCollection services)
        {
            return AddApplicationServices(services,
                typeof(HandlersDIExtensions).Assembly);
        }
        #region private members
        private static IServiceCollection AddEventHandler<T, H>
            (this IServiceCollection services)
            where T : IEventNotification
            where H : class, IEventHandler<T>
        {
            services.AddScoped<H>();
            services.TryAddScoped(typeof(EventTrigger<>));

            return services;
        }
        
        private static IServiceCollection AddAllEventHandlers
            (this IServiceCollection services, Assembly assembly)
        {
            var method=typeof(HandlersDIExtensions)
                .GetMethod(nameof(AddEventHandler),
                BindingFlags.Static | BindingFlags.Public);

            var handlers=assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass 
                && typeof(IEventHandler).IsAssignableFrom(x));
            foreach(var handler in handlers)
            {
                var handlerInterface = handler.GetInterfaces()
                    .Where(i => i.IsGenericType && typeof(IEventHandler).IsAssignableFrom(i))
                    .SingleOrDefault();
                if(handlerInterface != null)
                {
                    var eventType = handlerInterface.GetGenericArguments()[0];
                    method.MakeGenericMethod(new Type[] { eventType, handler })
                        .Invoke(null, new object[] { services });
                }
            }
            return services;
        }
        private static IServiceCollection AddAllCommandHandlers
            (this IServiceCollection services, Assembly assembly)
        {
            var handlers = assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass
                && typeof(ICommandHandler).IsAssignableFrom(x));
            foreach (var handler in handlers)
            {
                var handlerInterface = handler.GetInterfaces()
                    .Where(i => i.IsGenericType && typeof(ICommandHandler).IsAssignableFrom(i))
                    .SingleOrDefault();
                if (handlerInterface != null)
                {
                    services.AddScoped(handlerInterface, handler);
                }
            }
            return services;
        }
        private static IServiceCollection AddAllQueries
            (this IServiceCollection services, Assembly assembly)
        {
            var queries = assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass
                && typeof(IQuery).IsAssignableFrom(x));
            foreach (var query in queries)
            {
                var queryInterface = query.GetInterfaces()
                    .Where(i => !i.IsGenericType && typeof(IQuery) != i &&
                    typeof(IQuery).IsAssignableFrom(i))
                    .SingleOrDefault();
                if (queryInterface != null)
                {
                    services.AddTransient(queryInterface, query);
                }
            }
            return services;
        }
        #endregion
    }
}
