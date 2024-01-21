using Microsoft.Extensions.DependencyInjection;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Persistence.Repositories;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Data;
namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IDbConnection>(sp =>
            {
                var connection = new System.Data.SqlClient.SqlConnection(connectionString);

                connection.Open();

                return connection;
            });

            return services;
        }
    }
}
