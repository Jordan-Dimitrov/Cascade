﻿using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

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