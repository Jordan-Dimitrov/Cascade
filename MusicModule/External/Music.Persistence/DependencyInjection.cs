using Microsoft.Extensions.DependencyInjection;
using Persistence.Extensions;
using Persistence.Shared.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMusicPersistence(this IServiceCollection services, string connectionString)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            DbContextConfig.ConfigureDbContext(services, connectionString);

            return services;
        }
    }
}
