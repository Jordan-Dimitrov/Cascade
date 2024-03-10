using Application.Shared.Abstractions;
using Domain.Shared.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Music.Application.Constants;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.CachedRepositories
{
    internal sealed class CachedArtistQueryRepository : IArtistQueryRepository
    {
        private readonly IArtistQueryRepository _Decorated;
        private readonly ApplicationDbContext _Context;
        private readonly ICacheService _CacheService;
        public CachedArtistQueryRepository(IArtistQueryRepository decorated,
            ICacheService cacheService,
            ApplicationDbContext context)
        {
            _Decorated = decorated;
            _CacheService = cacheService;
            _Context = context;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Artist, bool>> condition)
        {
            return await _Decorated.ExistsAsync(condition);
        }

        public async Task<ICollection<Artist>> GetAllAsync(bool trackChanges)
        {
            return await _CacheService.GetAsync(CacheKeys.ArtistsKey,
                async () =>
                {
                    return await _Decorated
                    .GetAllAsync(trackChanges);
                });
        }

        public async Task<PagedList<Artist>> GetArtistsWithPaginationAsync(ArtistParameters artistParameters,
            bool trackChanges)
        {
            return await _Decorated
                    .GetArtistsWithPaginationAsync(artistParameters, trackChanges);
        }

        public async Task<Artist?> GetByIdAsync(Guid id, bool trackChanges)
        {
            Artist? artist = await _CacheService.GetAsync(CacheKeys.GetArtistKey(id),
                async () =>
                {
                    return await _Decorated
                    .GetByIdAsync(id, trackChanges);
                });

            if (artist is not null)
            {
                _Context.Set<Artist>().Attach(artist);
            }

            return artist;
        }

        public async Task<Artist?> GetByNameAsync(string username)
        {
            return await _Decorated.GetByNameAsync(username);
        }
    }
}
