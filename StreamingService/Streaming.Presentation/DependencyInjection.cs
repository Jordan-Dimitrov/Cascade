using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaming.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStreamingPresentation(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            return services;
        }
    }
}
