using Application.Shared.Abstractions;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Streaming.Application.Abstractions;
using Streaming.Application.Wrappers;
using Streaming.Infrastructure.Services;

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
            services.TryAddSingleton<IFtpClient, FtpClient>();

            return services;
        }
    }
}
