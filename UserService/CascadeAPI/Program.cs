using Application;
using AspNetCoreRateLimit;
using CascadeAPI.Middlewares;
using Domain.Wrappers;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Persistence;
using Presentation;
using Serilog;
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

            JwtTokenSettings? jwtTokenSettings = builder.Configuration
                .GetSection("JwtTokenSettings")
                .Get<JwtTokenSettings>();

            RefreshTokenSettings? refreshTokenSettings = builder.Configuration
                .GetSection("RefreshTokenSettings")
                .Get<RefreshTokenSettings>();

            builder.Services
                .AddApplication()
                .AddInfrastructure(jwtTokenSettings, refreshTokenSettings)
                .AddPersistence(builder.Configuration.GetConnectionString("SDR"))
                .AddPresentation();

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

            //app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            //app.MapGet("/", () => "Hello World!").WithName("GetUser");

            app.Run();
        }
    }
}
