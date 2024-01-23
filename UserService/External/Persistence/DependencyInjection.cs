using Microsoft.Extensions.DependencyInjection;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Persistence.Repositories;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Data;
using Dapper.FluentMap;
namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            services.AddScoped<IRefreshTokenCommandRepository, RefreshTokenCommandRepository>();
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();
            services.AddScoped<IRefreshTokenQueryRepository, RefreshTokenqQueryRepository>();

            return services;
        }
    }
}
