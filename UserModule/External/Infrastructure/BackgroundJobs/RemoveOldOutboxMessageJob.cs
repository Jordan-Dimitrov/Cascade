using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Shared.Outbox;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class RemoveOldOutboxMessageJob : IJob
    {
        private readonly ApplicationDbContext _DbContext;
        public RemoveOldOutboxMessageJob(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _DbContext.Set<OutboxMessage>()
                .Where(x => x.ProcessedOnUtc != null && x.Error == null)
                .Take(3)
                .ToListAsync(context.CancellationToken);

            _DbContext.RemoveRange(messages);

            await _DbContext.SaveChangesAsync();
        }
    }
}
