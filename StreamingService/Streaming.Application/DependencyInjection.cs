using Microsoft.Extensions.DependencyInjection;

namespace Streaming.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStreamingApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
