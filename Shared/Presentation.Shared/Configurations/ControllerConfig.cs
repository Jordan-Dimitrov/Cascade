using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Presentation.Shared.Constants;
using System.Reflection;

namespace Presentation.Shared.Configurations
{
    public static class ControllerConfig
    {
        public static void ConfigureControllers(IServiceCollection services, Assembly assembly)
        {
            NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
                new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
                .Services.BuildServiceProvider()
                .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>().First();

            services.AddControllers(options =>
            {
                options.CacheProfiles.TryAdd(CacheProfiles.Default,
                    new CacheProfile()
                    {
                        Duration = 10
                    });


                options.InputFormatters.Add(GetJsonPatchInputFormatter());

            }).AddNewtonsoftJson()
                .PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
        }
    }
}
