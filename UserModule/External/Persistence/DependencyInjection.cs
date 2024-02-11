using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Persistence.Repositories;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Data;
using Dapper.FluentMap;
using Persistence.Extensions;
using Domain.Shared.Abstractions;
using Users.Domain.Abstractions;
using Persistence.Shared.Interceptors;
using Persistence.Shared;
namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            DbContextConfig.ConfigureDbContext(services, connectionString);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<Seed>();
            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();

            return services;
        }
    }
}
