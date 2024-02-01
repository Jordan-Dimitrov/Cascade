using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Duration = 10
                    });
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = true;

            }).PartManager.ApplicationParts.Add(new AssemblyPart(assembly));

            services.AddMemoryCache();

            var rateLimitRules = new List<RateLimitRule>
                {
                new RateLimitRule
                    {
                        Endpoint = "*",
                        Limit = 3,
                        Period = "5m"
                    }
                };

            services.Configure<IpRateLimitOptions>(opt => {
                opt.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IRateLimitCounterStore,
            MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
