using Microsoft.Extensions.DependencyInjection;
using Music.Domain.Abstractions;
using Music.Persistence.Repositories;
using Persistence.Extensions;
using Persistence.Shared.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMusicPersistence(this IServiceCollection services, string connectionString)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            services.AddScoped<IAlbumCommandRepository, AlbumCommandRepository>();
            services.AddScoped<IAlbumQueryRepository, AlbumQueryRepository>();

            services.AddScoped<IArtistCommandRepository, ArtistCommandRepository>();
            services.AddScoped<IArtistQueryRepository,  ArtistQueryRepository>();

            services.AddScoped<IListenerCommandRepository, ListenerCommandRepository>();
            services.AddScoped<IListenerQueryRepository, ListenerQueryRepository>();

            services.AddScoped<IPlaylistCommandRepository, PlaylistCommandRepository>();
            services.AddScoped<IPlaylistQueryRepository, PlaylistQueryRepository>();

            services.AddScoped<ISongCommandRepository, SongCommandRepository>();
            services.AddScoped<ISongQueryRepository, SongQueryRepository>();

            DbContextConfig.ConfigureDbContext(services, connectionString);

            return services;
        }
    }
}
