using Application.Abstractions;
using Application.Dtos;
using Domain.Abstractions;
using Domain.Wrappers;
using Infrastructure.BackgroundJobs;
using Infrastructure.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            JwtTokenSettings settings, RefreshTokenSettings refreshTokenSettings)
        {
            services.AddScoped<IDataShaper<UserDto>, DataShaper<UserDto>>();
            services.AddHttpContextAccessor();
            services.AddScoped<ILinkService, LinkService>();

            var assembly = typeof(DependencyInjection).Assembly;

            AuthenticationConfig.ConfigureAuthentication(services, settings);

            QuartzConfig.ConfigureQuartz(services);

            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
