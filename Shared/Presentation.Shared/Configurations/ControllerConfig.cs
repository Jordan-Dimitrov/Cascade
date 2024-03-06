using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Presentation.Shared.Constants;

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
