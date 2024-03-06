using CascadeAPI.Extensions;
namespace CascadeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            BuildConfig.ConfigureBuilder(builder);

            var app = builder.Build();

            AppConfig.ConfigureAppPipeline(app, args);
        }
    }
}
