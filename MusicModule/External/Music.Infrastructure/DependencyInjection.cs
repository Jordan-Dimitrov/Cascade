using Domain.Shared.Abstractions;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Music.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMusicInfrastructure(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddScoped<IDataShaper<AlbumDto>, DataShaper<AlbumDto>>();

            return services;
        }
    }
}
