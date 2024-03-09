using Application.Shared.Abstractions;
using Domain.Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Abstractions;
using Users.Application.Constants;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.RequestFeatures;

namespace Users.Persistence.CachedRepositories
{
    internal sealed class CachedUserQueryRepository : IUserQueryRepository
    {
        private readonly IUserQueryRepository _Decorated;
        private readonly ICacheService _CacheService;
        public CachedUserQueryRepository(IUserQueryRepository decorated, ICacheService cacheService)
        {
            _Decorated = decorated;
            _CacheService = cacheService;
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
            return await _CacheService.GetAsync(CacheKeys.GetUserKey(id), 
                async() =>
                {
                    return await _Decorated
                    .GetByIdAsync(id, trackChanges);
                });
        }

        public async Task<User?> GetByNameAsync(string username)
        {
            return await _Decorated.GetByNameAsync(username);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _Decorated.GetUserByRefreshTokenAsync(refreshToken);
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
