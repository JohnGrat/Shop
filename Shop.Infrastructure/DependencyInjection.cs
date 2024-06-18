using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;
using Shop.Infrastructure.Dispatchers;
using Shop.Infrastructure.Persistence.SqlDb;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Settings;
using Shop.Shared.Constants;


namespace Shop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddTransient<SqlDbContext>();
            services.AddTransient<ISqlDbRepository, SqlDbRepository>();

            var eventStoreConnectionString = configuration.GetRequiredSection(AppSettingConstants.DatabaseSettings)[AppSettingConstants.EventStoreConnectionString];
            services.AddEventStoreClient(eventStoreConnectionString);

            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddTransient<IEventStoreRepository, EventStoreRepository>();

            return services;
        }
    }
}
