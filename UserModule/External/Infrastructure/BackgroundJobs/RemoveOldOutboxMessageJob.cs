using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Shared.Outbox;
using Quartz;

namespace Users.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class RemoveOldOutboxMessageJob : IJob
    {
        private const int _Count = 10;
        private readonly ApplicationDbContext _DbContext;
        public RemoveOldOutboxMessageJob(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _DbContext.Set<OutboxMessage>()
                .Where(x => x.ProcessedOnUtc != null && x.Error == null)
                .Take(_Count)
                .ToListAsync(context.CancellationToken);

            _DbContext.RemoveRange(messages);

            await _DbContext.SaveChangesAsync();
        }
    }
}
