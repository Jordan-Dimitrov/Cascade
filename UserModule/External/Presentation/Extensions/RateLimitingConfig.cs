using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Extensions
{
    internal static class RateLimitingConfig
    {
        internal static void ConfigureRateLimiting(IServiceCollection services)
        {
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
        }
    }
}
