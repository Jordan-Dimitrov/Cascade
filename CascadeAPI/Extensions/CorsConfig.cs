namespace CascadeAPI.Extensions
{
    public static class CorsConfig
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
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
        }
    }
}
