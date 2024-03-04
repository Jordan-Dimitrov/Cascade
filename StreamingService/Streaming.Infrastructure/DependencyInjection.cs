using Microsoft.Extensions.DependencyInjection;
using Streaming.Application.Abstractions;
using Streaming.Application.Wrappers;
using Streaming.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streaming.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStreamingInfrastructure(this IServiceCollection services,
            FFMpegConfig fFMpegConfig)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddHostedService<QueueService>();
            services.AddSingleton<IBackgroundQueue, BackgroundQueue>();
            services.AddScoped<IFileProcessingService, FileProccesingService>();

            return services;
        }
    }
}
