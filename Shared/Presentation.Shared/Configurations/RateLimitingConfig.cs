﻿using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Shared.Configurations
{
    public static class RateLimitingConfig
    {
        public static void ConfigureRateLimiting(IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
                {
                new RateLimitRule
                    {
                        Endpoint = RateLimits.Endpoint,
                        Limit = RateLimits.Limit,
                        Period = RateLimits.Period
                    }
                };

            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });
        }
    }
}