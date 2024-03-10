using Domain.Shared.Wrappers;
using Microsoft.Extensions.Options;
using Streaming.Application.Wrappers;
using Users.Domain.Wrappers;
using Music.Application;
using Music.Infrastructure;
using Music.Persistence;
using Music.Presentation;
using Persistence;
using Presentation;
using Streaming.Application;
using Streaming.Infrastructure;
using Streaming.Presentation;
using Users.Application;
using Users.Infrastructure;
namespace CascadeAPI.Extensions
{
    public static class BuildConfig
    {
        public static void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<JwtTokenSettings>(builder.Configuration.GetSection("JwtTokenSettings"));
            builder.Services.Configure<RefreshTokenSettings>(builder.Configuration
                .GetSection("RefreshTokenSettings"));

            builder.Services.Configure<CacheSettings>(builder.Configuration
                .GetSection("CacheSettings"));

            builder.Services.Configure<FtpServerSettings>(builder.Configuration.GetSection("FtpServerSettings"));

            builder.Services.Configure<MessageBrokerSettings>(
               builder.Configuration.GetSection("MessageBroker"));

            MassTransitConfig.ConfigureMassTransit(builder.Services);

            builder.Services
                .AddSingleton(x => x
                .GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

            builder.Services.Configure<FFMpegConfig>(builder.Configuration.GetSection("FFMpegConfig"));

            JwtTokenSettings? jwtTokenSettings = builder.Configuration
                .GetSection("JwtTokenSettings")
                .Get<JwtTokenSettings>();

            RefreshTokenSettings? refreshTokenSettings = builder.Configuration
                .GetSection("RefreshTokenSettings")
                .Get<RefreshTokenSettings>();

            builder.Services.AddSingleton<FFMpegConfig>();

            FFMpegConfig? ffmpegConfig = builder.Configuration
                .GetSection("FFMpegConfig")
                .Get<FFMpegConfig>();

            builder.Services
                .AddUserApplication()
                .AddUserInfrastructure(jwtTokenSettings, refreshTokenSettings)
                .AddUserPersistence(builder.Configuration.GetConnectionString("SDR"))
                .AddUserPresentation()
                .AddMusicApplication()
                .AddMusicInfrastructure()
                .AddMusicPersistence(builder.Configuration.GetConnectionString("SDR"))
                .AddMusicPresentation()
                .AddStreamingApplication()
                .AddStreamingInfrastructure(ffmpegConfig)
                .AddStreamingPresentation();

            CorsConfig.ConfigureCors(builder.Services);

            builder.Services.AddHealthChecks();

            builder.Services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });

            SerilogConfig.ConfigureSerilog(builder);

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddStackExchangeRedisCache(redisOptions =>
            {
                string? connection = builder.Configuration
                    .GetConnectionString("Redis");

                redisOptions.Configuration = connection;
            });
        }
    }
}
