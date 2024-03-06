using AspNetCoreRateLimit;
using CascadeAPI.Extensions;
using CascadeAPI.Middlewares;
using Persistence;
using Serilog;
namespace CascadeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            BuildConfig.ConfigureBuilder(builder);

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
                    var service = scope.ServiceProvider.GetService<UserSeed>();
                    service.SeedApplicationContext();
                }
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.MapHealthChecks("/health");

            app.UseResponseCaching();

            app.UseIpRateLimiting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
