using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddControllers()
                .PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
            return services;
        }
    }
}
