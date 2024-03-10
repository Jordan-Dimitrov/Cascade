using Microsoft.Extensions.DependencyInjection;

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
