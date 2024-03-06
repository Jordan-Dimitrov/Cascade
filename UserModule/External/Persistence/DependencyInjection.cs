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
using Users.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserPersistence(this IServiceCollection services, string connectionString)
        {
            services.TryAddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            DbContextConfig.ConfigureDbContext(services, connectionString);

            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
            services.AddTransient<UserSeed>();

            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();

            return services;
        }
    }
}
