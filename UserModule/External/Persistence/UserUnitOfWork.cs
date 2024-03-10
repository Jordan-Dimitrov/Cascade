using Users.Application.Abstractions;

namespace Persistence
{
    internal sealed class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly ApplicationDbContext _DbContext;
        public UserUnitOfWork(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _DbContext.SaveChangesAsync();
        }
    }
}
