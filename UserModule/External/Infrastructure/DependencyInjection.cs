using Infrastructure.BackgroundJobs;
using Infrastructure.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Domain.Shared.Abstractions;
using System.Text;
using Users.Application.Abstractions;
using Users.Application.Dtos;
using Users.Domain.Wrappers;
using Infrastructure.Shared.Services;
using Application.Shared.Abstractions;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserInfrastructure(this IServiceCollection services,
            JwtTokenSettings settings, RefreshTokenSettings refreshTokenSettings)
        {
            services.AddScoped<IDataShaper<UserDto>, DataShaper<UserDto>>();
            services.AddHttpContextAccessor();
            services.AddScoped<ILinkService, LinkService>();

            var assembly = typeof(DependencyInjection).Assembly;

            AuthenticationConfig.ConfigureAuthentication(services, settings);

            QuartzConfig.ConfigureQuartz(services);

            services.AddScoped<IFileConversionService, FileConversionService>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IEventBus, EventBus>();

            return services;
        }
    }
}
