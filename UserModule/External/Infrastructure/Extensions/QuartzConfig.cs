using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Infrastructure.BackgroundJobs;

namespace Users.Infrastructure.Extensions
{
    internal class QuartzConfig
    {
        internal static void ConfigureQuartz(IServiceCollection services)
        {
            services.AddQuartz(configure =>
            {
                var processOutboxMessagesjobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
                var removeOldOutboxMessagejobKey = new JobKey(nameof(RemoveOldOutboxMessageJob));

                configure.AddJob<ProcessOutboxMessagesJob>(processOutboxMessagesjobKey)
                    .AddTrigger(trigger => trigger.ForJob(processOutboxMessagesjobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10)
                    .RepeatForever()));

                configure.AddJob<RemoveOldOutboxMessageJob>(removeOldOutboxMessagejobKey)
                    .AddTrigger(trigger => trigger.ForJob(removeOldOutboxMessagejobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10)
                    .RepeatForever()));

                configure.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService();
        }
    }
}
