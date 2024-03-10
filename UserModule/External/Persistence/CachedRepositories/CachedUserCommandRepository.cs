using Application.Shared.Abstractions;
using Users.Application.Constants;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;

namespace Users.Persistence.CachedRepositories
{
    internal sealed class CachedUserCommandRepository : IUserCommandRepository
    {
        private readonly IUserCommandRepository _Decorated;
        private readonly ICacheService _CacheService;
        public CachedUserCommandRepository(IUserCommandRepository userCommandRepository,
            ICacheService cacheService)
        {
            _Decorated = userCommandRepository;
            _CacheService = cacheService;
        }
        public async Task DeleteAsync(User value)
        {
            await _CacheService.RemoveAsync(CacheKeys.GetUserKey(value.Id));
            await _Decorated.DeleteAsync(value);
        }

        public async Task InsertAsync(User value)
        {
            await _Decorated.InsertAsync(value);
        }

        public async Task UpdateAsync(User value)
        {
            await _CacheService.RemoveAsync(CacheKeys.GetUserKey(value.Id));
            await _Decorated.UpdateAsync(value);
        }
    }
}
