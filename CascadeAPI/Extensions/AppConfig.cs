using AspNetCoreRateLimit;
using CascadeAPI.Middlewares;
using Persistence;
using Serilog;

namespace CascadeAPI.Extensions
{
    public static class AppConfig
    {
        public static void ConfigureAppPipeline(WebApplication app, string[] args)
        {
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
