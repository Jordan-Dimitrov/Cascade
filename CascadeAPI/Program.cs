using AspNetCoreRateLimit;
using CascadeAPI.Middlewares;
using Domain.Shared.Wrappers;
using HealthChecks.UI.Client;
using Infrastructure;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Options;
using Music.Application;
using Music.Application.Consumers;
using Music.Infrastructure;
using Music.Persistence;
using Music.Presentation;
using Persistence;
using Presentation;
using Serilog;
using System.Reflection;
using Users.Application;
using Users.Domain.Wrappers;
namespace CascadeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<JwtTokenSettings>(builder.Configuration.GetSection("JwtTokenSettings"));
            builder.Services.Configure<RefreshTokenSettings>(builder.Configuration
                .GetSection("RefreshTokenSettings"));

            builder.Services.Configure<MessageBrokerSettings>(
               builder.Configuration.GetSection("MessageBroker"));

            builder.Services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.AddConsumer<UserHiddenEventConsumer>();
                busConfigurator.AddConsumer<UserCreatedEventConsumer>();
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

                    configurator.Host(new Uri(settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Username);
                    });

                    configurator.ConfigureEndpoints(context);
                });
            });

            builder.Services
                .AddSingleton(x => x
                .GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

            JwtTokenSettings? jwtTokenSettings = builder.Configuration
                .GetSection("JwtTokenSettings")
                .Get<JwtTokenSettings>();

            RefreshTokenSettings? refreshTokenSettings = builder.Configuration
                .GetSection("RefreshTokenSettings")
                .Get<RefreshTokenSettings>();

            builder.Services
                .AddUserApplication()
                .AddUserInfrastructure(jwtTokenSettings, refreshTokenSettings)
                .AddUserPersistence(builder.Configuration.GetConnectionString("SDR"))
                .AddUserPresentation()
                .AddMusicApplication()
                .AddMusicInfrastructure()
                .AddMusicPersistence(builder.Configuration.GetConnectionString("SDR"))
                .AddMusicPresentation();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:5173")
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .AllowAnyMethod()
                           .WithExposedHeaders("X-Pagination");
                });
            });

            builder.Services.AddHealthChecks();

            builder.Services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true;
            });

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console());

            var app = builder.Build();

            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                SeedData(app);
            }

            void SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<Seed>();
                    service.SeedApplicationContext();
                }
            }

            app.MapHealthChecks("/health");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseResponseCaching();

            app.UseIpRateLimiting();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            //app.MapGet("/", () => "Hello World!").WithName("GetUser");

            app.Run();
        }
    }
}
