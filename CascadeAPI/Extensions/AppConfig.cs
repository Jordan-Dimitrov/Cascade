using AspNetCoreRateLimit;
using CascadeAPI.Middlewares;
using Music.Persistence;
using Persistence;
using Serilog;

namespace CascadeAPI.Extensions
{
    public static class AppConfig
    {
        public static void ConfigureAppPipeline(WebApplication app, string[] args)
        {
            SeedData(app);

            void SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory.CreateScope())
                {
                    var usersSeedService = scope.ServiceProvider.GetService<UserSeed>();
                    usersSeedService.SeedApplicationContext();

                    var musicSeedService = scope.ServiceProvider.GetService<MusicSeed>();
                    musicSeedService.SeedApplicationContext();
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

            app.UseRouting();

            app.UseCors("AllowOrigin");

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
