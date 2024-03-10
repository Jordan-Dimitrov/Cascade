using Application.Shared.Abstractions;
using Domain.Shared.RequestFeatures;
using Persistence;
using System.Linq.Expressions;
using Users.Application.Constants;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.RequestFeatures;

namespace Users.Persistence.CachedRepositories
{
    internal sealed class CachedUserQueryRepository : IUserQueryRepository
    {
        private readonly ApplicationDbContext _Context;
        private readonly IUserQueryRepository _Decorated;
        private readonly ICacheService _CacheService;
        public CachedUserQueryRepository(IUserQueryRepository decorated,
            ICacheService cacheService, ApplicationDbContext context)
        {
            _Decorated = decorated;
            _CacheService = cacheService;
            _Context = context;
        }

        public async Task<bool> ExistsAsync(Expression<Func<User, bool>> condition)
        {
            return await _Decorated.ExistsAsync(condition);
        }

        public async Task<ICollection<User>> GetAllAsync(bool trackChanges)
        {
            return await _CacheService.GetAsync(CacheKeys.UsersKey,
                async () =>
                {
                    return await _Decorated
                    .GetAllAsync(trackChanges);
                });
        }

        public async Task<User?> GetByIdAsync(Guid id, bool trackChanges)
        {
            User? user = await _CacheService.GetAsync(CacheKeys.GetUserKey(id),
                async () =>
                {
                    return await _Decorated
                    .GetByIdAsync(id, trackChanges);
                });

            if (user is not null)
            {
                _Context.Set<User>().Attach(user);
            }

            return user;
        }

        public async Task<User?> GetByNameAsync(string username)
        {
            return await _Decorated.GetByNameAsync(username);
        }

        public async Task<PagedList<User>> GetUsersWithPaginationAsync(UserParameters userParameters, bool trackChanges)
        {
            //Find a better way to cache this

            /*string key = $"user-{userParameters.OrderBy}" +
                $"-{userParameters.MinRole}-{userParameters.MaxRole}" +
                $"-{userParameters.OrderBy}-{userParameters.SearchTerm}" +
                $"-{userParameters.PageSize}-{userParameters.PageNumber}" +
                $"-{userParameters.Fields}";

            return await _CacheService.GetAsync(key,
                async () =>
                {
                    return await _Decorated
                    .GetUsersWithPaginationAsync(userParameters, trackChanges);
                });*/

            return await _Decorated
                    .GetUsersWithPaginationAsync(userParameters, trackChanges);
        }
    }
}
