﻿namespace Streaming.Application.Abstractions
{
    public interface IBackgroundQueue
    {
        void QueueTask(Func<CancellationToken, Task> task);
        Task<Func<CancellationToken, Task>> PopQueue(CancellationToken cancellationToken);
        void AddStatus(Guid taskId, string status);
        string GetStatus(Guid taskId);
        Task ProcessQueueAsync(CancellationToken cancellationToken);
    }
}
