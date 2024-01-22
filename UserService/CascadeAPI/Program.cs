using Application;
using Domain.Wrappers;
using Infrastructure;
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

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
                .WriteTo.Console());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
