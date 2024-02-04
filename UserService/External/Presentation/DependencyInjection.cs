using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() => 
                new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
                .Services.BuildServiceProvider()
                .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>().First();

            services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Duration = 10
                    });


                options.InputFormatters.Add(GetJsonPatchInputFormatter());

            }).AddNewtonsoftJson()
                .PartManager.ApplicationParts.Add(new AssemblyPart(assembly));

            services.AddMemoryCache();

            var rateLimitRules = new List<RateLimitRule>
                {
                new RateLimitRule
                    {
                        Endpoint = "*",
                        Limit = 30,
                        Period = "1m"
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
