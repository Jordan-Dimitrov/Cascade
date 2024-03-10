using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.Extensions;
using Persistence.Repositories;
using Persistence.Shared.Interceptors;
using Users.Application.Abstractions;
using Users.Domain.Abstractions;
using Users.Persistence.CachedRepositories;
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
            services.Decorate<IUserQueryRepository, CachedUserQueryRepository>();
            services.Decorate<IUserCommandRepository, CachedUserCommandRepository>();

            return services;
        }
    }
}
