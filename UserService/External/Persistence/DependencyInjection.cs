using Microsoft.Extensions.DependencyInjection;
using Domain.Wrappers;
namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services;
        }
    }
}
