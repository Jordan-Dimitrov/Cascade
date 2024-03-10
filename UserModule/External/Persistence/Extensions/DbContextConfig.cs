using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Shared.Interceptors;

namespace Persistence.Extensions
{
    internal static class DbContextConfig
    {
        internal static void ConfigureDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(
                (sp, options) =>
                {
                    var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

                    options.UseSqlServer(connectionString)
                    .AddInterceptors(interceptor);
                });
        }
    }
}
