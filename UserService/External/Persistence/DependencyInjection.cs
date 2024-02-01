using Microsoft.Extensions.DependencyInjection;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Persistence.Repositories;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Data;
using Dapper.FluentMap;
using Persistence.Interceptors;
namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            services.AddDbContext<ApplicationDbContext>(
                (sp, options) =>
                {
                    var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

                    options.UseSqlServer(connectionString)
                    .AddInterceptors(interceptor);
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();

            return services;
        }
    }
}
