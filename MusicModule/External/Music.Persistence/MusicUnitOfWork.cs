using Music.Application.Abstractions;

namespace Music.Persistence
{
    internal sealed class MusicUnitOfWork : IMusicUnitOfWork
    {
        private readonly ApplicationDbContext _DbContext;
        public MusicUnitOfWork(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _DbContext.SaveChangesAsync() > 0;
        }
    }
}
