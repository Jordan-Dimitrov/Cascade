using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Presentation.Shared.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMusicPresentation(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            ControllerConfig.ConfigureControllers(services, assembly);
            RateLimitingConfig.ConfigureRateLimiting(services);

            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            services.TryAddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.TryAddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.TryAddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.TryAddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            return services;
        }
    }
}
