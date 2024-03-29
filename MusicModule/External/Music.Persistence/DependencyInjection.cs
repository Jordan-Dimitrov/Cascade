﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Persistence.CachedRepositories;
using Music.Persistence.Repositories;
using Persistence.Extensions;
using Persistence.Shared.Interceptors;

namespace Music.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMusicPersistence(this IServiceCollection services, string connectionString)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.TryAddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            services.AddScoped<IAlbumCommandRepository, AlbumCommandRepository>();
            services.AddScoped<IAlbumQueryRepository, AlbumQueryRepository>();

            services.AddScoped<IArtistCommandRepository, ArtistCommandRepository>();
            services.AddScoped<IArtistQueryRepository, ArtistQueryRepository>();

            services.Decorate<IArtistCommandRepository, CachedArtistCommandRepository>();
            services.Decorate<IArtistQueryRepository, CachedArtistQueryRepository>();
            services.Decorate<IAlbumCommandRepository, CachedAlbumCommandRepository>();
            services.Decorate<IAlbumQueryRepository, CachedAlbumQueryRepository>();

            services.AddTransient<MusicSeed>();
            services.AddScoped<IMusicUnitOfWork, MusicUnitOfWork>();

            DbContextConfig.ConfigureDbContext(services, connectionString);

            return services;
        }
    }
}
