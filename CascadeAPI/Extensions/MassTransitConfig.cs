using Domain.Shared.Wrappers;
using MassTransit;
using Music.Application.Consumers;
using Streaming.Application.Consumers;

namespace CascadeAPI.Extensions
{
    public static class MassTransitConfig
    {
        private const int _RetryLimit = 5;
        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.AddConsumer<UserHiddenEventConsumer>();
                busConfigurator.AddConsumer<UserCreatedEventConsumer>();
                busConfigurator.AddConsumer<SongCreatedEventConsumer>();
                busConfigurator.AddConsumer<SongHiddenEventConsumer>();
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

                    configurator.Host(new Uri(settings.Host), h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Username);
                    });

                    configurator.UseMessageRetry(x => x.Immediate(_RetryLimit));
                    configurator.ConfigureEndpoints(context);
                });
            });
        }
    }
}
