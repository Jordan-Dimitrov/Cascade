using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaming.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStreamingInfrastructure(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            return services;
        }
    }
}
