using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.IntegrationEvents;

namespace Music.Application.Consumers
{
    public sealed class UserHiddenEventConsumer : IConsumer<UserHiddenIntegrationEvent>
    {
        public Task Consume(ConsumeContext<UserHiddenIntegrationEvent> context)
        {
            Console.WriteLine("111111111111");

            return Task.CompletedTask;
        }
    }
}
