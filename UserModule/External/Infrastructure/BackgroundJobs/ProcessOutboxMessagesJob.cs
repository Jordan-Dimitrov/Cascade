using Domain.Shared.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence;
using Persistence.Shared.Outbox;
using Polly;
using Polly.Retry;
using Quartz;

namespace Users.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private const int _Count = 20;
        private readonly ApplicationDbContext _DbContext;
        private readonly IPublisher _Publisher;
        public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
        {
            _DbContext = dbContext;
            _Publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _DbContext.Set<OutboxMessage>()
                .Where(x => x.ProcessedOnUtc == null)
                .Take(_Count)
                .ToListAsync(context.CancellationToken);

            foreach (var message in messages)
            {
                var domainEventType = Type.GetType(message.Type);
                var domainEvent = JsonConvert.DeserializeObject(message.Content, domainEventType) as IDomainEvent;

                if (domainEvent is null)
                {
                    continue;
                }

                AsyncRetryPolicy policy = Policy
                    .Handle<Exception>()
                    .RetryAsync(3);

                PolicyResult result = await policy.ExecuteAndCaptureAsync(() =>
                    _Publisher
                    .Publish(domainEvent,
                    context.CancellationToken));

                message.Error = result.FinalException?.ToString();
                message.ProcessedOnUtc = DateTime.UtcNow;
            }

            await _DbContext.SaveChangesAsync();
        }

    }
}
