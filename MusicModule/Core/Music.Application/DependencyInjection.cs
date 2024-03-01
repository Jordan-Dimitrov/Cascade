using Microsoft.Extensions.DependencyInjection;
using Music.Domain.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMusicApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(assembly));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ArtistService>();
            services.AddScoped<AlbumService>();
            
            return services;
        }
    }
}
