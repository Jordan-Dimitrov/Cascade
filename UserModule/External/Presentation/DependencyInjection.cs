using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Presentation.Shared.Configurations;
namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserPresentation(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            ControllerConfig.ConfigureControllers(services, assembly);
            RateLimitingConfig.ConfigureRateLimiting(services);

            services.AddMemoryCache();

            services.TryAddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.TryAddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.TryAddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.TryAddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
