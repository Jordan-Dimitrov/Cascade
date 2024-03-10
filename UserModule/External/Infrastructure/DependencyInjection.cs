using Application.Shared.Abstractions;
using Domain.Shared.Abstractions;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Users.Application.Abstractions;
using Users.Application.Dtos;
using Users.Domain.Wrappers;
using Users.Infrastructure.Extensions;
using Users.Infrastructure.Services;

namespace Users.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserInfrastructure(this IServiceCollection services,
            JwtTokenSettings settings, RefreshTokenSettings refreshTokenSettings)
        {
            services.AddScoped<IDataShaper<UserDto>, DataShaper<UserDto>>();
            services.AddHttpContextAccessor();
            services.TryAddScoped<ILinkService, LinkService>();

            var assembly = typeof(DependencyInjection).Assembly;

            AuthenticationConfig.ConfigureAuthentication(services, settings);

            QuartzConfig.ConfigureQuartz(services);

            services.TryAddScoped<IFileConversionService, FileConversionService>();
            services.TryAddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IAuthService, AuthService>();
            services.TryAddTransient<IEventBus, EventBus>();
            services.TryAddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
