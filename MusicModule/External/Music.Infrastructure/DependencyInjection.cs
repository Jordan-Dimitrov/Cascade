﻿using Application.Shared.Abstractions;
using Domain.Shared.Abstractions;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Music.Application.Dtos;

namespace Music.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMusicInfrastructure(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddScoped<IDataShaper<AlbumDto>, DataShaper<AlbumDto>>();
            services.AddHttpContextAccessor();

            services.TryAddScoped<ILinkService, LinkService>();
            services.TryAddTransient<IEventBus, EventBus>();

            services.TryAddScoped<IFileConversionService, FileConversionService>();
            services.TryAddScoped<IUserInfoService, UserInfoService>();
            services.TryAddSingleton<ICacheService, CacheService>();
            services.TryAddSingleton<IFtpClient, FtpClient>();

            return services;
        }
    }
}
