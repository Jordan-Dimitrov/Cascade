using Application.Shared.Abstractions;
using Domain.Shared.RequestFeatures;
using Music.Application.Constants;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.CachedRepositories
{
    internal sealed class CachedAlbumQueryRepository : IAlbumQueryRepository
    {
        private readonly IAlbumQueryRepository _Decorated;
        private readonly ICacheService _CacheService;
        public CachedAlbumQueryRepository(IAlbumQueryRepository decorated, ICacheService cacheService)
        {
            _Decorated = decorated;
            _CacheService = cacheService;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Album, bool>> condition)
        {
            return await _Decorated.ExistsAsync(condition);
        }

        public async Task<PagedList<Album>> GetAlbumsWithPaginationAsync(AlbumParameters albumParameters,
            bool trackChanges)
        {
            return await _Decorated
                    .GetAlbumsWithPaginationAsync(albumParameters, trackChanges);
        }

        public async Task<ICollection<Album>> GetAllAsync(bool trackChanges)
        {
            return await _CacheService.GetAsync(CacheKeys.AlbumsKey,
                async () =>
                {
                    return await _Decorated
                    .GetAllAsync(trackChanges);
                });
        }

        public async Task<Album?> GetByIdAsync(Guid id, bool trackChanges)
        {
            return await _CacheService.GetAsync(CacheKeys.GetAlbumKey(id),
                async () =>
                {
                    return await _Decorated
                    .GetByIdAsync(id, trackChanges);
                });
        }

        public async Task<Album?> GetByNameAsync(string username)
        {
            return await _Decorated.GetByNameAsync(username);
        }
    }
}
