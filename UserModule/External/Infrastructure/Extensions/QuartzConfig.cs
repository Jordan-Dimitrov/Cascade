using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Users.Infrastructure.BackgroundJobs;

namespace Users.Infrastructure.Extensions
{
    internal class QuartzConfig
    {
        private const int _Interval = 10;
        internal static void ConfigureQuartz(IServiceCollection services)
        {
            services.AddQuartz(configure =>
            {
                var processOutboxMessagesjobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
                var removeOldOutboxMessagejobKey = new JobKey(nameof(RemoveOldOutboxMessageJob));

                configure.AddJob<ProcessOutboxMessagesJob>(processOutboxMessagesjobKey)
                    .AddTrigger(trigger => trigger.ForJob(processOutboxMessagesjobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(_Interval)
                    .RepeatForever()));

                configure.AddJob<RemoveOldOutboxMessageJob>(removeOldOutboxMessagejobKey)
                    .AddTrigger(trigger => trigger.ForJob(removeOldOutboxMessagejobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(_Interval)
                    .RepeatForever()));

                configure.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService();
        }
    }
}
