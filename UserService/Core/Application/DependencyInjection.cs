using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => 
                configuration.RegisterServicesFromAssembly(assembly));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
